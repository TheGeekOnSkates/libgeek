/**
 * Converts a string into an 8-bit hex dump
 * @param {string} The string
 * @returns {string} The string as a sequence of hex digits
 * @remarks This was a joke, literally just for the lolz, but since I like
 * to work on emulators and stuff I figured it was worth saving.  Obviously,
 * it would make more sense to do a Uint8Array like this, but whatever :)
 */
function str2hexdump(str) {
    var r = "";
    for (var i=0; i<str.length; i++)
        r += str.charCodeAt(i).toString(16) + " ";
    return r;
}

