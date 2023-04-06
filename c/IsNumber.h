#ifndef _GEEK_IS_NUMBER_H
#define _GEEK_IS_NUMBER_H

#include <stdint.h>
#include <stdbool.h>

// Allow a leading + or - sign when checking if a string is a number
const uint8_t ALLOW_SIGNS = 1;

// Allow decimal numbers when checking if a string is a number
const uint8_t ALLOW_DECIMAL_POINTS = 2;

/**
 * Checks if a string contains a number
 * @param[in] The string to be tested
 * @param[in] Zero or more of the ALLOW_* constants above (bitmask)
 * @returns True if it is, false if it isn't
 */
bool IsNumber(char* string, uint8_t flags) {
	static size_t i, length;
	static bool pastDot;
	if (string == NULL) return false;
	i = 0;
	if (
		flags && ALLOW_SIGNS
		&& (string[0] == '-' || string[0] == '+')
	) i++;
	length = strlen(string);
	if (length == 1 && (string[0] == '-' || string[0] == '+'))
		return false;
	pastDot = false;
	for (; i<length; i++) {
		if (string[i] >= '0' && string[i] <= '9') continue;
		if (string[i] == '.') {
			if (pastDot) return false;
			if (flags & ALLOW_DECIMAL_POINTS) {
				pastDot = true;
				continue;
			}
		}
		return false;
	}
	return true;
}

#endif

