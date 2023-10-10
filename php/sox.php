<?php

/**
 * Plays a sound using the "sox" command-line tool
 * @param {string} $file The file to play (sox supports ogg and I think wav)
 * @returns {array} The process resource and pipes
 */
function sox_play($file) {
	$descriptorspec = array(
		0 => array("pipe", "r"),	// stdin
		1 => array("pipe", "w"),	// stdout
		2 => array("pipe", "w")		// stderr
	);
	$process = proc_open("play \"$file\"", $descriptorspec, $pipes);
	return array("proc" => $process, "pipes" => $pipes);
}

/**
 * Checks if a sound is still playing
 * @param {resource} $file A process returned by sox_play
 * @returns {bool} True if the sound is still playing, false if not
 */
function sox_is_playing($file) {
	if (!is_array($file) || !array_key_exists("proc", $file)) return false;
	if (!is_resource($file["proc"])) return false;
	$status = proc_get_status($file["proc"]);
	if ($status["signaled"]) return true;
	return $status["running"];
}

/**
 * Stops a sound
 * @param {resource} $file A process returned by sox_play
 * @todo Get it to work :-D
 */
function sox_stop($file) {
	if (!is_array($file) || !array_key_exists("proc", $file)) return;
	if (!is_resource($file["proc"])) return;
	$status = proc_get_status($file["proc"]);
	$pid = $status["pid"];
	// Tried proc_close and proc_terminate, no dice.
	system("kill -9 $pid");
}

