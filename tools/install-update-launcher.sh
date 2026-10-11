#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
source_dir="$repo_root/tools/syncro-updater"
build_dir="$source_dir/build"
bin_dir=${XDG_BIN_HOME:-"$HOME/.local/bin"}
binary="$bin_dir/syncro-updater"

for tool in cmake c++; do
    if ! command -v "$tool" >/dev/null 2>&1; then
        printf '%s is required. On Arch Linux: sudo pacman -S --needed base-devel cmake qt6-base qt6-svg\n' "$tool" >&2
        exit 1
    fi
done

cmake -S "$source_dir" -B "$build_dir" -DCMAKE_BUILD_TYPE=Release -DSYNCRO_DEFAULT_REPO="$repo_root"
cmake --build "$build_dir" --parallel
mkdir -p "$bin_dir"
install -m 755 "$build_dir/syncro-updater" "$binary"
printf 'Updater installed: %s\n' "$binary"

icon_dir=${XDG_DATA_HOME:-"$HOME/.local/share"}/icons/hicolor/scalable/apps
mkdir -p "$icon_dir"
install -m 644 "$repo_root/tools/syncro-update.svg" "$icon_dir/syncro-updater.svg"

launcher_dir=${XDG_DATA_HOME:-"$HOME/.local/share"}/applications
mkdir -p "$launcher_dir"

write_entry() {
    # $1 file, $2 name, $3 comment, $4 extra arguments
    cat > "$1" <<EOF
[Desktop Entry]
Type=Application
Name=$2
Comment=$3
Exec="$binary" --repo "$repo_root" $4
Icon=syncro-updater
Terminal=false
Categories=System;
StartupNotify=true
EOF
    chmod 644 "$1"
}

launcher="$launcher_dir/syncro-updater.desktop"
write_entry "$launcher" 'Aktualizator Syncro' 'Sprawdź, zaktualizuj lub przywróć produkcję Syncro' ''
write_entry "$launcher_dir/syncro-production-rollback.desktop" 'Przywróć poprzednią wersję Syncro' 'Przywróć poprzedni commit i przebuduj produkcję' '--rollback'
write_entry "$launcher_dir/syncro-production-restart.desktop" 'Uruchom ponownie Syncro' 'Zrestartuj kontenery produkcyjne' '--restart'
# Same file name as the old YAD launcher, so existing menu entries switch to the new binary.
write_entry "$launcher_dir/syncro-production-update.desktop" 'Aktualizuj Syncro' 'Pobierz najnowszy kod i wdroż produkcję' '--update'
printf 'Menu launchers created in %s\n' "$launcher_dir"

desktop_dir=
if command -v xdg-user-dir >/dev/null 2>&1; then
    desktop_dir=$(xdg-user-dir DESKTOP 2>/dev/null || true)
fi
if [ -z "$desktop_dir" ]; then
    desktop_dir="$HOME/Desktop"
fi
if [ -d "$desktop_dir" ]; then
    rm -f "$desktop_dir/Aktualizuj Syncro.desktop" "$desktop_dir/Przywróć poprzednią wersję Syncro.desktop"
    cp "$launcher" "$desktop_dir/Aktualizator Syncro.desktop"
    chmod +x "$desktop_dir/Aktualizator Syncro.desktop"
    printf 'Desktop launcher created: %s\n' "$desktop_dir/Aktualizator Syncro.desktop"
fi

autostart_dir=${XDG_CONFIG_HOME:-"$HOME/.config"}/autostart
mkdir -p "$autostart_dir"
cat > "$autostart_dir/syncro-production-update-tray.desktop" <<EOF
[Desktop Entry]
Type=Application
Name=Aktualizator Syncro (zasobnik)
Comment=Sprawdza aktualizacje Syncro w tle
Exec="$binary" --repo "$repo_root" --tray
Icon=syncro-updater
Terminal=false
X-KDE-autostart-after=panel
X-KDE-autostart-phase=2
EOF
chmod 644 "$autostart_dir/syncro-production-update-tray.desktop"
printf 'Tray autostart created: %s\n' "$autostart_dir/syncro-production-update-tray.desktop"

if pkill -u "$(id -u)" -f 'yad --notification.*syncro-update' 2>/dev/null; then
    printf '%s\n' 'Stopped the old YAD tray icon.'
fi
