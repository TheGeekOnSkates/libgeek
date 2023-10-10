#ifndef _GEEK_STRINGENDSWITH_H
#define _GEEK_STRINGENDSWITH_H

#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

bool StringEndsWith(char* string, const char *end) {
	if (string == NULL || end == NULL) return false;
	return strcmp(string + strlen(string) - strlen(end),
		end) == 0;
}

#endif

