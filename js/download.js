/**
 * Prompts the user to download some text as a file
 * @param {string} data The data to put in the file
 * @param {string} name The file name (including extension) 
 * @param {truthy} [isText] If true (or 1, etc.) download it as a
 * plain-text file (not as a binary file, like Excel)
 * @param {string} [type] The header to use with the downloaded file (this is
 * important for Excel and probably other Office type downloads, and any
 * other binary data (pictures, audio, etc.)
 */
function download(data, name, isText=0, type='application/octet-stream') {
	var a = document.createElement('a');
	data = isText ? new TextEncoder().encode(data) : data;
	a.href = URL.createObjectURL(new Blob([data], { type: type }));
	a.download = name;
	document.body.appendChild(a);
	a.click();
	document.body.removeChild(a);
	URL.revokeObjectURL(a.href);
}
