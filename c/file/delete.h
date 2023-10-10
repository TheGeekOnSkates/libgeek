#ifndef _FILE_DELETE_H
#define _FILE_DELETE_H

#include <stdbool.h>

#ifdef __WIN32

#include <Windows.h>

bool FileDelete(const char* fileName) {
	return DeleteFileA(fileName) != 0;
}

#else

#include <unistd.h>

bool FileDelete(const char* fileName) {
	return unlink(fileName) == 0;
}

#endif

#endif

