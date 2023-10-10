#ifndef _IS_NUMBER_H_
#define _IS_NUMBER_H_

#include <stdbool.h>

/**
 * Checks if a string is a decimal number (decimal as in not hex/binary)
 * @param[in] The string to be tested
 * @param[in] If true, allow a dot (so stuff like "3.141592" is okay)
 * @param[in] If true, allow a leading sign (so "+10" or "-123" are okay)
 * @returns true if it is, false if it isn't
 * @remarks Other number bases will need separate functions.  For example,
 * letters A-F (or a-f) would cause this to return false, even though
 * they're perfectly valid in hexadecimal.  On the other hand, binary
 */
bool IsNumber(const char* string, bool allowDot, bool allowSign) {
	size_t i, length;
	bool pastDot;
	
	/* If the string is a NULL pointer, obviously, we have our answer */
	if (string == NULL) return false;

	/*
	Get our start position (0, or 1 if there's a
	leading + or - sign and that's ok
	*/
	i = 0;
	if (allowSign && (string[0] == '-' || string[0] == '+')) i++;
	
	/* Initialize our pastDot and length variables */
	pastDot = false;
	length = strlen(string);
	
	for (; i < length; i++) {
		/* If it's a dot, and we're past th dot, then no. */
		if (string[i] == '.' && allowDot) {
			if (pastDot) return false;
			pastDot = true;
			continue;
		}
		
		/* Otherwise, if it's a non-number, then no. */
		if (string[i] < '0' || string[i] > '9') return false;
	}
	
	/* If it gets here, then yes - it's a number. */
	return true;
}

#endif

