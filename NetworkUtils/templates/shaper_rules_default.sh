#!/bin/bash

/sbin/tc qdisc del dev eth1 handle 1: root
/sbin/tc qdisc del dev ifb0 handle 1: root

/sbin/tc filter add dev eth1 parent ffff: protocol ip u32 match u32 0 0 action mirred egress redirect dev ifb0

/sbin/tc qdisc add dev eth1 handle 1: root htb default 1
/sbin/tc class add dev eth1 parent 1: classid 1:1 htb rate 11000Kbit burst 1024Kbit cburst 1024Kbit quantum 200000
/sbin/tc filter add dev eth1 parent 1:0 protocol all u32 match u32 0 0 classid 1:1
/sbin/tc class add dev eth1 parent 1:1 classid 1:11 htb rate 10000Kbit burst 512Kbit cburst 512Kbit quantum 150000
/sbin/tc filter add dev eth1 parent 1:1 protocol all prio 2 u32 match u32 0 0 classid 1:11



/sbin/tc qdisc add dev ifb0 handle 1: root htb default 1
/sbin/tc class add dev ifb0 parent 1: classid 1:1 htb rate 11000Kbit burst 1024Kbit cburst 1024Kbit quantum 200000
/sbin/tc filter add dev ifb0 parent 1:0 protocol all u32 match u32 0 0 classid 1:1
/sbin/tc class add dev ifb0 parent 1:1 classid 1:11 htb rate 10000Kbit burst 512Kbit cburst 512Kbit quantum 150000
/sbin/tc filter add dev ifb0 parent 1:1 protocol all prio 2 u32 match u32 0 0 classid 1:11