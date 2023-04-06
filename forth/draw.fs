\ --------------------------------------------------------------------------------------------------------------------------------------------
\ Misc. text-interface stuff
\ --------------------------------------------------------------------------------------------------------------------------------------------

: title ( n1 n2 n3 -- )    clear reverse-on spaces home type reverse-off 1 2 cursor ;
\ Where n1 is a string pointer and n2 is the length of that string (use s" for these numbers),
\ and n3 is the number of spaces to print (with reverse on) for the title - ideally the width of your terminal)
\ example: s" Title Here" 80 title

\ Shows right-aligned text
: right-align ( str* length -- )
	dup	\ Stack is now str* length length
	cols	\ Put the number of columns (i.e. 80) on the stack
	swap -	\ Now stack = str* length cols-length
	
	\ Now print that many spaces
	0 DO
		32 emit 
	LOOP
	
	\ And type the string, and we're done
	type
;



\ TO-DO: Add words for box-drawing characters, getting input from the user, etc.

: fill-low-1 ." ▁" ;
: fill-low-2 ." ▂" ;
: fill-low-3 ." ▃" ;
: fill-low-4 ." ▄" ;
: fill-low-5 ." ▅" ;
: fill-low-6 ." ▆" ;
: fill-low-7 ." ▇" ;
: fill-all ." █" ;
: fill-hi-1 ." ▔" ;
: fill-hi-4 ." ▀" ;
: fill-right-1 ." ▕" ;
: fill-left-7 ." ▉" ;
: fill-left-6 ." ▊" ;
: fill-left-5 ." ▋" ;
: fill-left-4 ." ▌" ;
: fill-left-3 ." ▍" ;
: fill-left-2 ." ▎" ;
: fill-left-1 ." ▏" ;
: fill-right-half ." ▐" ;
: shade-25 ." ░" ;
: shade-50 ." ▒" ;
: shade-75 ." ▓" ;
: pixel-bottom-left ." ▖" ;
: pixel-bottom-right ." ▗" ;
: pixel-top-left ." ▘" ;
: pixel-top-right ." ▝" ;
: pixel-all-but-top-left ." ▟" ;
: pixel-all-but-top-right ." ▙" ;
: pixel-all-but-bottom-left ." ▜" ;
: pixel-all-but-bottom-right ." ▛" ;
: pixel-diagonal-up ." ▞" ;
: pixel-diagonal-down ." ▚" ;
: diagonal-line-up ." ╱" ;
: diagonal-line-down ." ╲" ;
: diagonal-line-x ." ╳" ;
: box-double-h ." ═" ;
: box-double-v ." ║" ;
: box-double-top-left ." ╔" ;
: box-double-top-right ." ╗" ;
: box-double-bottom-left ." ╚" ;
: box-double-bottom-right ." ╝" ;
: box-double-T-left ." ╠" ;
: box-double-T-right ." ╣" ;
: box-double-T-down ." ╦" ;
: box-double-T-up ." ╩" ;
: box-double-cross ." ╬" ;
: circle-top-left ." ╭" ;
: circle-top-right ." ╮" ;
: circle-bottom-left ." ╰" ;
: circle-bottom-right ." ╯" ;
: triangle-up-1 ." ◢" ;
: triangle-down-1 ." ◣" ;
: triangle-up-2 ." ◤" ;
: triangle-down-2 ." ◥" ;

\ Special characters for Spanish
: .`a ." á" ;
: s`a s" á" ;
: .`e ." é" ;
: s`e s" é" ;
: .`i ." í" ;
: s`i s" í" ;
: .`o ." ó" ;
: s`o s" ó" ;
: .`u ." ú" ;
: s`u s" ú" ;
\ To-do: Add capitals, also the N with a ~ and U with a :, and punctuation
