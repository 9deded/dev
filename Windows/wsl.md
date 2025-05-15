# [WSL Windows Subsystem for Linux]()

### Basic commands:
- `wsl` : launches the default Linux distribution.
- `wsl -l` or `wsl --list` : lists all installed Linux distributions.
- `wsl -d <distribution_name>` or `wsl --distribution <distribution_name>` : launches a specifie Linux distribution.
- `wsl --shutdown` : shuts down all running WSL sessions.
- `wsl --list --online` : list available distributions from the Microsoft Store.
- `wsl --set-version <distro_name> <version>` : changes the WSL version (1 or 2) for a specific distribution.

### Other userful commands:
- `wsl --import` : imports a Linux distribution from a TAR file or VHD file.
- `wsl --export` : exports a Linux distribution to a VHD file.
- `wsl --install` : installs a Linux distribution.
- `wsl --uninstall` : uninstalls a Linux distribution.
- `wsl --exec <command>` | runs a Linux command within a WSL shell.

### Examples:
- to launch Ubuntu: `wsl` or `wsl -d Ubuntu`
- to list installed distributions: `wsl -l`
- to shutdown all WSL sessions: `wsl --shutdown`
- to set Ubuntu to WSL 2 : `wsl --set-version Ubuntu 2`

