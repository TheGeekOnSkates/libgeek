#ifndef _GEEK_STRINGS_H
#define _GEEK_STRINGS_H

/**
 * Checks if two strings contain the same text
 * @param[in] The first string
 * @param[in] The second string
 * True if the strings are the same, false if not
 */
#define StringEquals(a, b) (strcmp(a, b) == 0)

/**
 * Checks is a string starts with a substring
 * @param[in] The string to be tested
 * @param[in] The string we're looking for
 * @returns True if string a starts with string b,
 * or false if not
 */
#define StringStartsWith(a, b) (strstr(a, b) == a) 

#endif

