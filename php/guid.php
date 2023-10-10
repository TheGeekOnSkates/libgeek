<?php

/**
 * Uses the Linux "uuidgen" command to create a new Globally Unique ID (GUID)
 * @returns {string} The GUID in what I've called "unpacked" (36-character) format
 * @remarks I use the acronym "GUID" cuz it's easier to say and more commonly known by Windows sysadmins, but
 * it's the same thing as a UUID (see https://en.wikipedia.org/wiki/Universally_unique_identifier)
 */
function guid_create() {
	return str_replace("\n", "", shell_exec("uuidgen"));
}

/**
 * "Packs" a GUID so it only occupies 16 bytes (for storage in i.e. a database with a BINARY(16) type field)
 * @param {string} The 36-character representation of the GUID (i.e. "61fb3a1b-1a9c-4315-8c4a-767c9a1a8b1d")
 * @returns {string} The 16-byte equivalent, or an empty string if it fails
 * @remarks If you want to see a "packed" GUID in the 36-char format, use guid_unpack.
 */
function guid_pack($str) {
	if (!is_string($str)) {
		error_log("uuid_pack: invalid parameter type: " . var_export($str, true));
		return "";
	}
	if (!preg_match("~^[0-9a-zA-Z]{8}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{12}$~", $str)) {
		error_log("uuid_pack: invalid GUID: " . var_export($str, true));
		return "";
	}
	$guid = hex2bin(str_replace("-", "", $str));
	if (!is_string($guid)) {
		error_log("uuid_pack: hex2bin failed: " . var_export($str, true));
		return "";
	}
	return $guid;
}

/**
 * Converts a "packed" GUID so it appears in its more human-readable 36-character format.
 * @param {string} A 16-byte GUID like you would get from guid_pack or an Active Directory search
 * @returns {string} The GUID's 36-character version, or an empty string if it fails
 */
function guid_unpack($blob) {
	$hex = bin2hex($blob);
	$guid = substr($hex, 0, 8) . '-' . substr($hex, 8, 4)
		. '-' . substr($hex, 12, 4) . '-' . substr($hex, 16, 4)
		. '-' . substr($hex, 20);
	if (strlen($guid) != 36) {
		error_log("uuid_unpack: invalid parameter: " . var_export($blob, true));
		return "";
	}
	return $guid;
}

/*
// Examples
$test1 = "61fb3a1b-1a9c-4315-8c4a-767c9a1a8b1d";
$test2 = "3bd09999-9646-475d-8d56-c414a618691f";

$test1_packed =  guid_pack($test1);
$test2_packed =  guid_pack($test2);

$test1_unpacked = guid_unpack($test1_packed);
$test2_unpacked = guid_unpack($test2_packed);

if ($test1_unpacked == $test1)
	echo "BINGO on 1!\n";
if ($test2_unpacked == $test2)
	echo "BINGO on 2!\n";
*/