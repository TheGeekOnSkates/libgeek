<?php

/**
 * Checks if the user is running PHP from a terminal
 * @returns {bool} True if s/he is, false if s/he isn't
 */
function is_terminal() { return defined('STDIN'); }

/**
 * These replace the usual extra hoops I gotta jump thru :)
 * @returns {bool} True if s/he is, false if s/he isn't
 */
function get($name) { return isset($_GET[$name]) ? $_GET[$name] : ""; }
function post($name) { return isset($_POST[$name]) ? $_POST[$name] : ""; }
