# Linux Production Updater

Install the launcher and KDE tray entry from the repository checkout:

```sh
sh tools/install-update-launcher.sh
```

On Arch Linux, install YAD and Konsole if needed:

```sh
sudo pacman -S yad konsole
```

The installer adds **Aktualizuj Syncro** to the desktop application menu and, when a Desktop directory exists, places a launcher there. It also starts a tray icon automatically after KDE Plasma login. Clicking the tray icon opens Konsole (or another available terminal), fast-forwards the current Git branch, then runs `make update-production` to build and restart the production Compose services.

Requirements:

- The checkout must have a Git upstream configured.
- The checkout must be clean; the updater stops rather than overwriting local changes.
- `.env.production` must be present and configured.
- The desktop user must be allowed to run Docker Compose without an interactive sudo prompt.

The application container does not run Git or Docker updates. The launcher runs on the Linux host where the checkout and Docker Compose deployment live.
