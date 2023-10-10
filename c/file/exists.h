#ifndef _FILE_EXISTS_H
#define _FILE_EXISTS_H

#include <stdio.h>
#include <stdbool.h>

#ifdef __WIN32

#include <io.h>

/* For more info: https://stackoverflow.com/questions/230062/whats-the-best-way-to-check-if-a-file-exists-in-c */
bool FileExists(const char* fileName) {
	return _access(fileName, 0) == 0;
}

#else

#include <unistd.h>

/* For more info: https://stackoverflow.com/questions/230062/whats-the-best-way-to-check-if-a-file-exists-in-c */
bool FileExists(const char* fileName) {
	return access(fileName, F_OK) == 0;
}

#endif

#endif

