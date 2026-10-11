#!/bin/sh
set -eu

if [ "$(id -u)" -ne 0 ]; then
    printf '%s\n' 'Uruchom przez sudo: sudo sh tools/configure-nft-syncro.sh [LAN_CIDR]' >&2
    exit 1
fi

lan_cidr=${1:-192.168.0.0/24}
case "$lan_cidr" in
    *[!0-9./]*|'')
        printf 'Nieprawidłowa podsieć IPv4: %s\n' "$lan_cidr" >&2
        exit 1
        ;;
esac

if ! command -v nft >/dev/null 2>&1; then
    printf '%s\n' 'Nie znaleziono nft; zainstaluj pakiet nftables.' >&2
    exit 1
fi
if [ ! -f /etc/nftables.conf ]; then
    printf '%s\n' 'Brak /etc/nftables.conf; nie zmieniam konfiguracji firewalla.' >&2
    exit 1
fi

rules_dir=/etc/nftables.d
rules_file="$rules_dir/syncro-port-forward.nft"
main_config=/etc/nftables.conf
include_line='include "/etc/nftables.d/syncro-port-forward.nft"'
mkdir -p "$rules_dir"

backup_suffix=$(date +%Y%m%d%H%M%S)
cp -a "$main_config" "$main_config.backup.$backup_suffix"
if [ -f "$rules_file" ]; then
    cp -a "$rules_file" "$rules_file.backup.$backup_suffix"
    had_rules_file=true
else
    had_rules_file=false
fi

rules_tmp=$(mktemp "$rules_dir/.syncro-port-forward.XXXXXX")
cat > "$rules_tmp" <<EOF
destroy table ip syncro_nat
table ip syncro_nat {
    chain prerouting {
        type nat hook prerouting priority dstnat; policy accept;
        ip saddr $lan_cidr tcp dport 80 redirect to :3000 comment "Syncro web from LAN"
    }
    chain output {
        type nat hook output priority dstnat; policy accept;
        tcp dport 80 redirect to :3000 comment "Syncro web on host"
    }
}
insert rule inet filter input ip saddr $lan_cidr tcp dport 3000 ct state new accept comment "Syncro web from LAN"
EOF
chmod 600 "$rules_tmp"
mv "$rules_tmp" "$rules_file"

if ! grep -Fqx "$include_line" "$main_config" && ! grep -Eq '^include[[:space:]]+"/etc/nftables\.d/\*\.nft"' "$main_config"; then
    printf '\n%s\n' "$include_line" >> "$main_config"
fi

if ! nft -c -f "$main_config"; then
    cp -a "$main_config.backup.$backup_suffix" "$main_config"
    if [ "$had_rules_file" = true ]; then
        cp -a "$rules_file.backup.$backup_suffix" "$rules_file"
    else
        rm -f "$rules_file"
    fi
    printf '%s\n' 'Konfiguracja nft jest niepoprawna; przywróciłem kopię.' >&2
    exit 1
fi

systemctl enable --now nftables
nft -f "$main_config"
printf 'Gotowe: TCP/80 przekierowuje na 3000, dostęp z %s.\n' "$lan_cidr"
printf 'Kopia nftables.conf: %s.backup.%s\n' "$main_config" "$backup_suffix"
