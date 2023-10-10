#ifndef _FILE_INFO_H
#define _FILE_INFO_H

#include <stdio.h>
#include <stdbool.h>

#ifdef __WIN32


#else

#include <stdbool.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>

bool FileIsFolder(const char *path) {
    struct stat path_stat;
    stat(path, &path_stat);
    return S_ISDIR(path_stat.st_mode);
}

bool FileIsExecutable(const char *path) {
    return access(path, X_OK) == 0;
}


#endif

#endif
