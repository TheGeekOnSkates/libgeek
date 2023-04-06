\ Custom boot message
\ --------------------------------------------------------------------------

\ Not open-sourcing these - they're reminders to myself, to-do's etc.
include ~/notes.fs

: _boot_message
	clear-history		\ Clear the Forth history file
	cls					\ Clear the terminal screen
	bold yellow text	\ Self-explanatory :)
	.\" Gforth 0.7.3\n"
	blue text
	." Go FORTH and have fun! " smile .\" \n"
	reset			\ Reset terminal to default colors
	notes 10 emit		\ Show my notes
;

\ And this is the "hook" - tells Gforth that the word I just defined
\ IS their pre-defined word, "bootmessage" (3-4 lines by default)
' _boot_message is bootmessage
