\ --------------------------------------------------------------------------------------------------------------------------------------------
\ Misc. stuff that doesn't belong anywhere else (yet) :D
\ --------------------------------------------------------------------------------------------------------------------------------------------

: pwd           s" pwd" system ;
: up1           s" ../" set-dir ;
: vim           s" vim" system ;
: telnet        s" telnet" system ;
: files ( -- )
	s" clear" system
	bold yellow text pwd
	reset 10 emit
	s" ls" system
;
: cd            set-dir files ; \ example: s" ../" cd

: ../ ( -- )    s" ../" cd ;

\ These are prefixed with "sys-" because they don't do
\ the same as their ANSI escape code counterparts.  Like
\ "reset" doesn't just reset terminal attributes; it also
\ clears the screen.  Similarly, the ANSI "clear" doesn't
\ actually clear the screen - you can scroll up to see what was "cleared".
: cls           s" clear" system ;
: sys-reset     s" reset" system ;
: clear-history s" rm ~/.gforth-history" system ; \ Clears the up-arrow history
: edit-my-stuff s" micro ~/my-stuff.fs" system ;

\ This uses Voxin, a really cool text-to-speech program providing the classic "Eloquence" voice on Linux.
\ I'm still learning the conventional way of documenting stack effects, so I hope str* and strlen make sense (think C).
\ Basically what you want to do is s" text to be spoken" say
: say  ( str* strlen -- )   s\" voxin-say \"" pad place pad +place s\" \" | aplay -q" pad +place pad count system ;

\ This uses Sox, a great Linux command-line tool for playing sounds.
\ Again, not sure if I got the funky shorthand for stack effect down, so here's an example:
\ s" my-sound.ogg" play
: play  ( str* strlen --)   s\" play -q \"" pad place pad +place s\" \" 1> /dev/null 2> /dev/null &" pad +place pad count system ;
: play-sync  ( str* strlen --)   s\" play -q \"" pad place pad +place s\" \" 1> /dev/null 2> /dev/null" pad +place pad count system ;

: pic  ( str* strlen -- )   s\" ascii-image-converter \"" pad place pad +place s\" \" -C" pad +place pad count system ;

\ Useful for commands like sleep, usleep, ANSI escape codes etc. - anything that requires typing string versions of whole numbers greater than zero (i.e. 12345).
: type-uint 0 <<# #s #> type #>> ;

\ Clears/empties the stack
\ Clears/empties the stack
\ NOTE: There was a bug; when depth = 0, it was freezing.
\ idk why "0 0 do ... loop" would cause a freeze (you'd think it would
\ just return immediately) but it must somehow be an infinite loop.
\ Anyway, now having the dup and the if-statement fixes that.
: clear-stack ( -- )
	depth dup 0 > if
		0 do drop loop
	then
;

: su s" sudo su" system ; 

: confirm ( string* stringLength -- true|false )
	yellow text type reset
	10 emit
	." Press " blue text ." ENTER" reset ."  for yes, or any other key for no: "
	key 13 = if true else false then
;

\ Calls the "sleep" system command
\ Note that seconds has to be < 10
: sleep ( seconds -- )
	48 +			\ Convert the number to the char code
	s" sleep 1s"	\ Stack: char string* stringLength
	drop			\ Stack: char string*
	dup 6 +			\ Stack: char string* string*+6
	2 roll swap		\ Stack: string* char string*+6
	c!				\ Stack: string* (string*+6 = char now)
	8 system		\ Stack: string* 8, then system clears it
;

\ Catch: Drawing characters with values > 255 break this
: center ( str* strlen -- )
	dup 2 /
	cols 2 /
	swap -
	spaces type cr
;

: vim s" vim " pad place pad +place pad count system ;
