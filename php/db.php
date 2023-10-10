<?php

///////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////

$_db_creds = NULL;		// Database credentials (see db_get_creds)
$_db_pdo = NULL;		// A PDO object used between calls
$_db_last_id = -1;		// Last ID inserted in a SELECT query
$_db_database = "";		// Set in db_set
$_db_connected = false;	// Set to true when db_connect() succeeds



///////////////////////////////////////////////////////////////////////////
// FUNCTIONS
///////////////////////////////////////////////////////////////////////////

/**
 * Sets the credentials before trying to connect
 * @param {string} $file The location of the JSON file
 * @returns {bool} True if it works, false if it doesn't
 * @remarks Logs errors if it returns false
 */
function db_get_creds($file) {
	global $_db_creds;
	$_db_creds = NULL;
	if (!file_exists($file)) {
		error_log("db_get_creds: File not found: $file");
		return false;
	}
	$json = file_get_contents($file);
	if ($json === false) {
		// Usually, PHP will log other stuff if this fails, but still...
		error_log("db_get_creds: file_get_contents failed");
		return false;
	}
	$_db_creds = json_decode($json);
	$error = "db_get_creds: json_decode failed: ";
	switch(json_last_error()) {
		case JSON_ERROR_DEPTH:
			$error .= "The maximum stack depth has been exceeded";
			error_log($error);
			return false;
		case JSON_ERROR_STATE_MISMATCH:
			$error .= "Invalid or malformed JSON";
			error_log($error);
			return false;
		case JSON_ERROR_CTRL_CHAR:
			$error .= "Control character error, possibly incorrectly encoded";
			error_log($error);
			return false;
		case JSON_ERROR_SYNTAX:
			$error .= "Syntax error";
			error_log($error);
			return false;
		case JSON_ERROR_UTF8:
			$error .= "Malformed UTF-8 characters, possibly incorrectly encoded";
			error_log($error);
			return false;
		case JSON_ERROR_RECURSION:
			$error .= "One or more recursive references in the value to be encoded";
			error_log($error);
			return false;
		case JSON_ERROR_INF_OR_NAN:
			$error .= "One or more NAN or INF values in the value to be encoded ";
			error_log($error);
			return false;
		case JSON_ERROR_UNSUPPORTED_TYPE:
			$error .= "A value of a type that cannot be encoded was given";
			error_log($error);
			return false;
		case JSON_ERROR_INVALID_PROPERTY_NAME:
			$error .= "A property name that cannot be encoded was given";
			error_log($error);
			return false;
		case JSON_ERROR_UTF16:
			$error .= "Malformed UTF-16 characters, possibly incorrectly encoded";
			error_log($error);
			return false;
	}
	$required_fields = array("host", "user", "password");
	foreach($required_fields as $field) {
		if (!property_exists($_db_creds, $field)) {
			error_log("db_get_creds: missing $field");
			return false;
		}
	}
	return true;
}

/**
 * Sets the name of the database to use
 * @param {string} $name The name of the database
 * @returns {bool} True if it works, false if it doesn't
 * @remarks Like db_get_creds, this just sets a global variable
 */
function db_set($name) {
	global $_db_database, $_db_connected;
	$_db_connected = false;
	if (!is_string($name)) {
		error_log("db_set: invalid database name: "
			. var_export($name, true));
		return false;
	}
	if (empty($name)) {
		error_log("db_set: invalid database name: <empty>");
		return false;
	}
	$_db_database = $name;
	return true;
}

/**
 * Reads data from the database
 * @param {string} $query An SQL query
 * @param {mixed} $params Data to be used in the query
 * @param {boolean} $assoc Set to TRUE to fetch an associative array
 * @returns An array containing the data, or NULL if something goes wrong
 */
function db_read($query, $params = NULL, $assoc = FALSE) {
	if (!_db_connect()) return NULL;
	try {
		$stmt = _db_run($query, $params);
		$stmt->setFetchMode($assoc ? PDO::FETCH_ASSOC : PDO::FETCH_NUM);
		$data = Array();
		while($rows = $stmt->fetch())
			$data[] = $rows;
		return $data;
	}
	catch(PDOException $e) {
		error_log("db_read: " . $e->getMessage());
		return NULL;
	}
}

/**
 * Writes data to the database
 * @param {string} $query An SQL query
 * @param {mixed} $params Data to be used in the query
 * @returns The number of rows updated, or -1 if there was an error
 */
function db_write($query, $params = NULL) {
	if (!_db_connect()) return -1;
	try {
		$stmt = _db_run($query, $params);
		return $stmt->rowCount();
	}
	catch(PDOException $e) {
		error_log("db_write: " . $e->getMessage());
		return -1;
	}
}
/**
 * Gets the last inserted ID, or last value in a query
 * @returns {number} The last inserted ID, or -1 if something went wrong
 */
function db_last_id() {
	global $_db_last_id;
	return $_db_last_id;
}

/**
 * Creates a bunch of question marks for use in database queries
 * @param {number} $records How many records you need question marks for
 * @param {number} $fields How many question marks (fields) per record
 * (default is 1); this is for INSERT'ing multiple rows in a single query
 * @returns {string} If $fields is 1, you get $records question marks
 * joined by commas (so if $records is 3, you get "?, ?, ?").  If $fields
 * is greater than 1, you get that many question marks for every item in
 * $fields (so if $records is 3 and $fields is 2, you get "(?, ?), (?, ?),
 * (?, ?)").  This is especially useful for WHERE ... IN or for inserting
 * multiple rows.
 */
function db_question_marks($records, $fields = 1) {
	$result = array();
	for ($i=0; $i<$records; $i++) {
		if ($fields == 1) {
			array_push($result, "?");
			continue;
		}
		$group = array();
		for($j=0; $j<$fields; $j++) $group[$j] = "?";
		array_push($result, "(" . implode(", ", $group) . ")");
	}
	return implode(", ", $result);
}

///////////////////////////////////////////////////////////////////////////
// "INTERNAL" FUNCTIONS - that is, not meant to be used directly by the
// end-developer, functions that are "under the hood" of this library
///////////////////////////////////////////////////////////////////////////

/**
 * Tries to connect to the database
 * @returns {bool} True if it worked, or false if not
 */
function _db_connect() {
	global $_db_creds, $_db_pdo, $_db_connected, $_db_database;
	if (empty($_db_database)) {
		error_log("db_connect: Database has not been set (call db_set)");
		return false;
	}
	if (is_null($_db_creds)) return false;
	if ($_db_connected) return true;
	$_db_pdo = new PDO("mysql:host=" . $_db_creds->host
		 . ";dbname=" . $_db_database,
		$_db_creds->user, $_db_creds->password);
	$errors = $_db_pdo->errorInfo()[2];
	if (!is_null($errors)) {
		error_log("db_connect: $errors");
		return false;
	}
	$_db_pdo->setAttribute( PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION );
	$_db_connected = true;
	return true;
}

/**
 * Runs an SQL query using PDO
 * @param {string} $query An SQL query
 * @param {mixed} $params The query parameters
 * @returns A PDO "statement" object (used by read and write)
 * or NULL if something goes wrong
 */
function _db_run($query, $params) {
	global $_db_creds, $_db_pdo,
		$_db_connected, $_db_database, $_db_last_id;
	$stmt = $_db_pdo->prepare($query);
	$errors = $stmt->errorInfo()[2];
	if (!is_null($errors)) {
		error_log("_db_run: $errors");
		return NULL;
	}
	if (is_array($params)) {
		$total = count($params);
		$stmt->execute($params);
	}
	else if (!is_null($params)) {
		$stmt->bindParam(1, $params);
		$stmt->execute();
	}
	else $stmt->execute();
	$errors = $stmt->errorInfo()[2];
	if (!is_null($errors)) {
		error_log("_db_run: $errors");
		return NULL;
	}
	$_db_last_id = $_db_pdo->lastInsertId();
	if ($_db_last_id === false) $_db_last_id = -1;
	$_db_last_id = intval($_db_last_id);
	return $stmt;
}

// End of database micro-library
