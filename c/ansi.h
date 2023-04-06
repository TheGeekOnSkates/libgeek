#ifndef ANSI_H
#define _ANSI_H

// Colors
#define ANSI_BLACK		0
#define ANSI_RED		1
#define ANSI_GREEN		2
#define ANSI_YELLOW		3
#define ANSI_BLUE		4
#define ANSI_CYAN		5
#define ANSI_MAGENTA	6
#define ANSI_WHITE		7

// Clears the screen
#define ANSI_Clear() printf("\x1b[2J\x1b[H")

// Sets the text color (use color constants above)
#define ANSI_Text(color) printf("\x1b[3%dm", color)

// Sets the background color
#define ANSI_Background(color) printf("\x1b[%4dm", color)

// Resets the terminal settings
#define ANSI_Reset() printf("\x1b[0m")

// Kind of a catch-all for things like blink, hidden,
// bold/italic/underline on some terminals, etc.
// Eventually I'll add constants for those too
#define ANSI_Setting(x) printf("\x1b[%dm", x)

// Moves the cursor to row Y, column X
#define ANSI_CursorTo(x, y) printf("\x1b[%d;%dH", y, x)

// Moves the cursor up/down/left/right n characters
#define ANSI_Up(n) printf("\x1b[%dA", n)
#define ANSI_Down(n) printf("\x1b[%dB", n)
#define ANSI_Right(n) printf("\x1b[%dC", n)
#define ANSI_Left(n) printf("\x1b[%dD", n)
#define ANSI_HiddenTextOn() printf("\x1b[8m");
#define ANSI_HiddenTextOff() printf("\x1b[28m");

#endif

