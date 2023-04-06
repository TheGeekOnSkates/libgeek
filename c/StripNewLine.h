#ifndef _GEEK_STRIPNEWLINE_H

#include <string.h>

static void StripNewLine(char* string) {
	static char* temp;
	if (string == NULL) return;
	temp = strchr(string, '\n');
	if (temp != NULL) temp[0] = '\0';
	temp = strchr(string, '\r');
	if (temp != NULL) temp[0] = '\0';
}

#endif
