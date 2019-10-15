#/bin/sh!

/sbin/iptables -t filter -F
/sbin/iptables -t filter -X
/sbin/iptables -t nat -F
/sbin/iptables -t nat -X
/sbin/iptables -t mangle -F
/sbin/iptables -t mangle -X

/sbin/iptables -t filter -A INPUT -i lo -j ACCEPT
/sbin/iptables -t filter -A OUTPUT -o lo -j ACCEPT

####firewall

#allow all established
/sbin/iptables -A INPUT -m state --state RELATED,ESTABLISHED -j ACCEPT

#accept ssh
/sbin/iptables -A INPUT -s 0.0.0.0/0 -p tcp -m tcp --dport 1022 -j ACCEPT

#close apache
/sbin/iptables -A INPUT -p tcp -m tcp --dport 80 -j DROP
#close ajenri
/sbin/iptables -A INPUT -p tcp -m tcp --dport 8000 -j DROP

/sbin/iptables -t mangle -A FORWARD -p tcp --tcp-flags SYN,RST SYN -j TCPMSS --set-mss 1360

#subnet to block
/sbin/iptables -N users_traff
/sbin/iptables -A FORWARD -s 10.255.0.0/16 -j users_traff

#accept all
/sbin/iptables -A users_traff -s 10.255.206.226/32 -j ACCEPT

#block all
#/sbin/iptables -A users_traff -s 10.255.0.0/16 -j DROP

#users blocker
/usr/local/yurox/utm_008/blocker.pl

#netflow
/sbin/iptables -t filter -A FORWARD -j NETFLOW -o eth1

# 2-way mapping example
/sbin/iptables -t nat -A PREROUTING -d white_addr -j DNAT --to-destination grey_addr
/sbin/iptables -t nat -I POSTROUTING 1 -s grey_addr -o eth0.52 -j SNAT --to-source white_addr