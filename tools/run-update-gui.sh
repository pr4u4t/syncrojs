#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
update_script="$repo_root/tools/update-production.sh"
rollback_script="$repo_root/tools/rollback-production.sh"
action=${1:-}

if ! command -v yad >/dev/null 2>&1; then
    printf '%s\n' 'YAD is not installed; opening the updater in a terminal.' >&2
    if [ "$action" = rollback ]; then
        exec sh "$repo_root/tools/run-update-terminal.sh" "$rollback_script"
    fi
    exec sh "$repo_root/tools/run-update-terminal.sh" "$update_script"
fi

if [ -z "$action" ]; then
    if yad --question \
        --title='Aktualizator Syncro' \
        --text='Wybierz operację: aktualizacja albo przywrócenie poprzedniej wersji.' \
        --button='Aktualizuj:0' \
        --button='Rollback:2' \
        --button='Anuluj:1' \
        --width=460 --center; then
        action=update
    else
        choice=$?
        case "$choice" in
            2) action=rollback ;;
            *) exit 0 ;;
        esac
    fi
fi

case "$action" in
    update)
        operation_script=$update_script
        operation_title='Aktualizacja Syncro'
        operation_text='Pobieranie zmian i wdrażanie produkcji…'
        ;;
    rollback)
        if ! yad --question \
            --title='Potwierdź rollback' \
            --text='Przywrócić poprzedni commit? Ta operacja nie cofa migracji ani danych PostgreSQL.' \
            --button='Przywróć:0' --button='Anuluj:1' --width=520 --center; then
            exit 0
        fi
        operation_script=$rollback_script
        operation_arg=--confirmed
        operation_title='Rollback Syncro'
        operation_text='Przywracanie poprzedniej wersji i wdrażanie produkcji…'
        ;;
    *)
        printf 'Nieznana operacja: %s\n' "$action" >&2
        exit 2
        ;;
esac

if [ "$action" = update ]; then
    operation_arg=
fi

work_dir=$(mktemp -d "${TMPDIR:-/tmp}/syncro-update.XXXXXX")
status_file="$work_dir/status"
log_file="$work_dir/update.log"
: > "$log_file"
trap 'rm -rf "$work_dir"' EXIT HUP INT TERM

(
    if [ -n "$operation_arg" ]; then
        if sh "$operation_script" "$operation_arg" 2>&1; then
            result=0
        else
            result=$?
        fi
    else
        if sh "$operation_script" 2>&1; then
            result=0
        else
            result=$?
        fi
    fi
    printf '%s\n' "$result" > "$status_file"
    printf '__SYNCRO_UPDATE_STATUS__:%s\n' "$result"
) | while IFS= read -r line; do
    case "$line" in
        __SYNCRO_UPDATE_STATUS__:0)
            printf '#Operacja zakończona.\n100\n'
            ;;
        __SYNCRO_UPDATE_STATUS__:*)
            printf '#Aktualizacja zakończyła się błędem.\n100\n'
            ;;
        *)
            printf '%s\n' "$line" >> "$log_file"
            printf '#%s\n' "$line"
            ;;
    esac
done | yad \
    --progress \
    --pulsate \
    --enable-log \
    --log-expanded \
    --log-height=360 \
    --auto-close \
    --no-cancel \
    --title="$operation_title" \
    --text="$operation_text" \
    --width=760 \
    --center

if [ ! -f "$status_file" ]; then
    yad --text-info --title='Operacja przerwana' --filename="$log_file" --width=760 --height=420 --button=gtk-close
    exit 1
fi

result=$(cat "$status_file")
if [ "$result" -ne 0 ]; then
    yad --text-info --title='Operacja nieudana' --filename="$log_file" --width=760 --height=420 --button=gtk-close
    exit "$result"
fi

yad --info --title="$operation_title" --text='Operacja zakończyła się powodzeniem.' --button=gtk-close
