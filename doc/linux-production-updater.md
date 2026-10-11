# Linux Production Updater

`syncro-updater` is a native Qt 6 application (C++17) that checks, updates and rolls back the production checkout. It replaces the old YAD scripts.

Install the build dependencies (Arch Linux) and the updater from the repository checkout:

```sh
sudo pacman -S --needed base-devel cmake qt6-base qt6-svg
sh tools/install-update-launcher.sh
```

The installer builds `tools/syncro-updater`, installs the binary to `~/.local/bin/syncro-updater`, and creates:

- menu entries **Aktualizator Syncro**, **Aktualizuj Syncro** (`--update`) and **Przywróć poprzednią wersję Syncro** (`--rollback`),
- a desktop launcher (when a Desktop directory exists),
- a KDE autostart entry that starts the updater in the system tray (`--tray`).

Rerun the installer after pulling changes to the updater itself.

## What it does

The window shows the current version, the upstream branch, the list of new commits waiting on the server, the version available for rollback, and whether the checkout has local changes. Every step and all command output appear in a live log. Update and rollback logs are also saved to `~/.local/share/Syncro/syncro-updater/logs` (the last 30 are kept).

- **Sprawdź aktualizacje** runs `git fetch --prune` and compares `HEAD` with its upstream. The tray instance repeats this every 60 minutes (`--check-interval <minutes>`, `0` disables) and shows a notification when new commits appear.
- **Aktualizuj** checks the environment, requires a clean checkout, fetches, runs `git pull --ff-only`, records the previous commit in `refs/syncro-production/previous`, and runs `make update-production`.
- **Przywróć poprzednią wersję** restores the recorded commit with `git reset --hard`, saves the commit it left in `refs/syncro-production/rollback-from`, and runs `make update-production`. Rollback does not reverse PostgreSQL migrations or restore database data.
- **Uruchom ponownie** (`--restart`) runs `make restart-production` (`docker compose restart`) without fetching or rebuilding.

Fetching and checking can be cancelled; the build and deployment step cannot, and the updater refuses to quit while it runs. Git never prompts for credentials, so a missing key fails with a clear error instead of hanging. Only one instance runs per user; launching it again (or from another menu entry) forwards the action to the running instance.

The repository path is taken from `--repo`, then `SYNCRO_REPO`, the last saved path, and finally the checkout the installer was run from.

Requirements:

- The checkout must be on a branch with a Git upstream configured.
- The checkout must be clean; the updater stops rather than overwriting local changes.
- `.env.production` must be present and configured.
- The desktop user must be allowed to run Docker Compose without an interactive sudo prompt.

`tools/update-production.sh` and `tools/rollback-production.sh` remain available for terminal or SSH use and follow the same rules.

The application container does not run Git or Docker updates. The updater runs on the Linux host where the checkout and Docker Compose deployment live.

Production uses host networking so the application can reach PostgreSQL on the host's VM network. The default app port is `3000` because rootless Docker cannot bind a non-root process to port `80`; open `http://<host-address>:3000`. Set `APP_PORT` in `.env.production` only if you choose another unprivileged port. Use a host reverse proxy if the public URL must stay on port `80` or `443`.
