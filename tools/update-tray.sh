#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
if ! command -v yad >/dev/null 2>&1; then
    printf '%s\n' 'YAD is required for the KDE tray icon. Install it with your Linux package manager.' >&2
    exit 1
fi

exec yad \
    --notification \
    --image="$repo_root/tools/syncro-update.svg" \
    --text="Aktualizuj Syncro: kliknij, aby pobrać zmiany i wdrożyć produkcję" \
    --command="/usr/bin/env sh \"$repo_root/tools/run-update-terminal.sh\""
