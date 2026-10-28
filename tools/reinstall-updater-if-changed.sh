#!/bin/sh
# Rebuilds and reinstalls syncro-updater when its sources changed since the given commit.
# Prints SYNCRO_UPDATER_REINSTALLED after a reinstall so a running GUI knows to restart itself.
set -eu

repo_root=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
cd "$repo_root"

before=${1:?Usage: reinstall-updater-if-changed.sh <previous-commit>}

if git diff --quiet "$before" HEAD -- tools/syncro-updater tools/syncro-update.svg \
    tools/install-update-launcher.sh tools/reinstall-updater-if-changed.sh; then
    printf '%s\n' 'Updater unchanged.'
    exit 0
fi

printf '%s\n' 'Updater sources changed, reinstalling...'
sh tools/install-update-launcher.sh
printf '%s\n' 'SYNCRO_UPDATER_REINSTALLED'
