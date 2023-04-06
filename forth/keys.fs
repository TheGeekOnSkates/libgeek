\ These move the cursor based on what key we're checking.
\ Note that some keys actually produce 3 characters; i.e.
\ The arrows are things like "\x1b[A", so that translates
\ like this: 0x1B = 27, [ = 91, A = ...66?  lol I forget
\ which numbers correspond to which keys.  But the bottom
\ line here is that to use this you may have to do stuff
\ like "key key key isUp isDown isLeft isRight".  Or for
\ a kinda crazy real-world example, try this:
\ : test   0 100 DO key key key isUp isDown isLeft isRight LOOP ;
\
\ TO-DO'S:
\       1. Finish getKey so it puts one of the following constants on the stack
\       2. Finish adding constants :)
\

\ ------------------------------------------------------------------------
\ These constants do NOT correspond to standard ANSI key codes.  They're a
\ list of physical keys on most keyboards in my country; I know there are
\ keyboards out there with more keys, and also foreign keyboards, but this
\ is my personal library so I don't really care about those (for now). :)
\ ------------------------------------------------------------------------

1 constant KEY_ESCAPE
2 constant KEY_F1
3 constant KEY_F2
4 constant KEY_F3
5 constant KEY_F4
6 constant KEY_F5
7 constant KEY_F6
8 constant KEY_F7
9 constant KEY_F8
10 constant KEY_F9
11 constant KEY_F10
12 constant KEY_F11
13 constant KEY_F12
14 constant KEY_PRINTSCREEN
15 constant KEY_SCROLL_LOCK
16 constant KEY_PAUSE
17 constant KEY_ACCENT
18 constant KEY_1
19 constant KEY_2
20 constant KEY_3
21 constant KEY_4
22 constant KEY_5
23 constant KEY_6
24 constant KEY_7
25 constant KEY_8
26 constant KEY_9
27 constant KEY_0
28 constant KEY_DASH
29 constant KEY_EQUALS
30 constant KEY_BACKSPACE
31 constant KEY_TAB
32 constant KEY_Q
33 constant KEY_W
34 constant KEY_E
35 constant KEY_R
36 constant KEY_T
37 constant KEY_Y
38 constant KEY_U
39 constant KEY_I
40 constant KEY_O
41 constant KEY_P
42 constant KEY_LEFT_BRAKCET
43 constant KEY_RIGHT_BRACKET
44 constant KEY_BACKSLASH
45 constant KEY_CAPS_LOCK
46 constant KEY_A
47 constant KEY_S
48 constant KEY_D
49 constant KEY_F
50 constant KEY_G
51 constant KEY_H
52 constant KEY_J
53 constant KEY_K
54 constant KEY_L
55 constant KEY_SEMICOLON
56 constant KEY_APOSTROPHE
57 constant KEY_ENTER
58 constant KEY_SHIFT           \ NOTE: In most terminals, Shift is not
                                \ treated like other keys; that is, instead
                                \ of being logged as a separate key, it
                                \ subtracts 32 to other keycodes.  But since
                                \ my goal here is to get the actual key(s)
                                \ the user pressed, my plan is to put
                                \ KEY_SHIFT on the stack when this happens.
                                \ For example, if I press Shift-A, the stack
                                \ will contain first KEY_A, then KEY_SHIFT.
59 constant KEY_Z
60 constant KEY_X
61 constant KEY_C
62 constant KEY_V
63 constant KEY_B
64 constant KEY_N
65 constant KEY_M
66 constant KEY_COMMA
67 constant KEY_PERIOD
68 constant KEY_SLASH
\ Then would come another Shift - see above
69 constant KEY_CTRL           \ NOTE: CTRL is another weirdo.  Instead of
                               \ subtracting 32, it subtracts 96 (and if I
                               \ press Shift and CTRL together, it's still
                               \ just 96, so CTRL overrides Shift).  But as
                               \ I was planning to do with Shift, if I find
                               \ the keycode falls within the right range,
                               \ my key-reading word will put the key, and
                               \ then KEY_CTRL, on the stack.
70 constant KEY_SUPER          \ NOTE: This won't be supported for (maybe)
                               \ a very long time; this is because I'm in a
                               \ graphical environment, and pressing this
                               \ key causes the Linux start menu to appear
                               \ (or Windows if someone uses this on that).
71 constant KEY_ALT            \ Alt is another weirdo.  It puts keycode 27
                               \ (escape) on the stack, then the key you
                               \ pressed.  So unlike CTRL, it doesn't undo
                               \ or override Shift; so for example, if I
                               \ press Alt-Shift-F, the stack will read
                               \ 27 (for Alt) then 70 (for the shifted F).
                               \ Again, I think I can have my key-reading
                               \ word check for this and put the correct
                               \ KEY_* constants on the stack instead.
72 constant KEY_SPACE
73 constant KEY_APPS           \ NOTE: Same issue as KEY_SUPER above
\ Then come another ALT, CTRL, and Shift - skipping those
74 constant KEY_HOME
75 constant KEY_END
76 constant KEY_PAGE_UP
77 constant KEY_PAGE_DOWN
78 constant KEY_INSERT
79 constant KEY_DELETE
80 constant KEY_UP
81 constant KEY_DOWN
82 constant KEY_LEFT
83 constant KEY_RIGHT
\ And this leaves just the number pad, which I don't need to bother with
\ because (1) terminals don't care about NumLock, and (2) the numpad sends
\ the same keystrokes as other keys already listed above.



\ ------------------------------------------------------------------------
\ These words are actions
\ ------------------------------------------------------------------------

\ Clears standard input (stdin)
: drop-keys ( -- )
	begin
		key?
		true = IF
			key drop
		THEN
	key? false = until
;

: getkey ( -- key )
	key
	dup 27 = if
		\ At this point, stack = 27
		\ So it's either
		\ (a) The Escape key,
		\ (b) A printable char + Alt, or
		\ (c) A multi-char key like UP, DOWN, HOME etc.
		key? 0= if
			\ It's the Escape key
			drop              \ Lose the 27
			KEY_ESCAPE        \ And we're done
		else
			." TO-DO: Handle Alt-keys and multi-char keys" cr
		then
	then
;

\ Gets as many keys are as in stdin
: keys
	key  \ Get at least 1
	key? true = if
		begin key key? 0 = until
	then
;

\ These words check for specific keys; ideally I'd like to make them more
\ reusable / less redundant, but that's a project for another nite. :)

: key.isF1 ( key1 key2 key3 -- bool )
	depth 3 >= if
		2 pick 2 pick 2 pick
		2 roll 27 =
		2 roll 79 =
		2 roll 80 =
		and and
	else false then
;
: key.isF2 ( key1 key2 key3 -- bool )
	depth 3 >= if
		2 pick 2 pick 2 pick
		2 roll 27 =
		2 roll 79 =
		2 roll 81 =
		and and
	else false then
;
: key.isF3 ( key1 key2 key3 -- bool )
	depth 3 >= if
		2 pick 2 pick 2 pick
		2 roll 27 =
		2 roll 79 =
		2 roll 82 =
		and and
	else false then
;
: key.isF4 ( key1 key2 key3 -- bool )
	depth 3 >= if
		2 pick 2 pick 2 pick
		2 roll 27 =
		2 roll 79 =
		2 roll 83 =
		and and
	else false then
;
: key.isF5 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 49 =
		4 roll 53 =
		4 roll 126 =
		and and and and
	else false then
;
: key.isF6 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 49 =
		4 roll 55 =
		4 roll 126 =
		and and and and
	else false then
;
: key.isF7 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 49 =
		4 roll 56 =
		4 roll 126 =
		and and and and
	else false then
;
: key.isF8 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 49 =
		4 roll 57 =
		4 roll 126 =
		and and and and
	else false then
;
: key.isF9 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 50 =
		4 roll 48 =
		4 roll 126 =
		and and and and
	else false then
;

\ F10 and F11 are GUI commands on xterm; skipping for now...

: key.isF12 ( key1 key2 key3 key4 key5 -- bool )
	depth 5 >= if
		4 pick 4 pick 4 pick 4 pick 4 pick
		4 roll 27 =
		4 roll 91 =
		4 roll 50 =
		4 roll 52 =
		4 roll 126 =
		and and and and
	else false then
;
