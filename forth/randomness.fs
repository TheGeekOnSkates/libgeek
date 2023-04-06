require random.fs
utime drop seed !

: coin ( -- n )   2 random 1 - ;

: dice ( -- n )   12 random 1 + ;
