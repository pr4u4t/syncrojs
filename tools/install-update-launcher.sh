#!/bin/sh
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
if ! command -v yad >/dev/null 2>&1; then
    printf '%s\n' 'YAD is required for the KDE tray icon. Install it with your Linux package manager, then rerun this installer.' >&2
    exit 1
fi

launcher_dir=${XDG_DATA_HOME:-"$HOME/.local/share"}/applications
mkdir -p "$launcher_dir"
launcher="$launcher_dir/syncro-production-update.desktop"

cat > "$launcher" <<EOF
[Desktop Entry]
Type=Application
Name=Aktualizuj Syncro
Comment=Pobierz najnowszy kod i wdroż produkcję
Exec=/usr/bin/env sh "$repo_root/tools/update-production.sh"
Terminal=true
Categories=System;
EOF
chmod 644 "$launcher"
printf 'Launcher menu created: %s\n' "$launcher"

desktop_dir=
if command -v xdg-user-dir >/dev/null 2>&1; then
    desktop_dir=$(xdg-user-dir DESKTOP 2>/dev/null || true)
fi
if [ -z "$desktop_dir" ]; then
    desktop_dir="$HOME/Desktop"
fi
if [ -d "$desktop_dir" ]; then
    desktop_launcher="$desktop_dir/Aktualizuj Syncro.desktop"
    cp "$launcher" "$desktop_launcher"
    chmod +x "$desktop_launcher"
    printf 'Desktop button created: %s\n' "$desktop_launcher"
fi

autostart_dir=${XDG_CONFIG_HOME:-"$HOME/.config"}/autostart
mkdir -p "$autostart_dir"
cat > "$autostart_dir/syncro-production-update-tray.desktop" <<EOF
[Desktop Entry]
Type=Application
Name=Syncro Production Updater Tray
Comment=Updater Syncro w zasobniku KDE
Exec=/usr/bin/env sh "$repo_root/tools/update-tray.sh"
Terminal=false
X-KDE-autostart-after=panel
X-KDE-autostart-phase=2
EOF
chmod 644 "$autostart_dir/syncro-production-update-tray.desktop"
printf 'KDE tray autostart created: %s\n' "$autostart_dir/syncro-production-update-tray.desktop"
