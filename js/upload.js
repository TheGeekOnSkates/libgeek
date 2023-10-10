/**
 * Prompts the user to upload a file
 * @returns {Promise<ArrayBuffer>} The file's contents on success,
 * or null if there's an error reading the file's contents
 */
function upload() {
	return new Promise(function(resolve) {
		var i = document.createElement('input');
		i.type = "file";
		i.style.display = "none";
		i.onchange = function(e) {
			let f = e.target.files[0];
			e.target.remove();
			let r = new FileReader();
			r.onerror = function(event) {
				resolve(null);
			};
			r.onload = function() {
				resolve(r.result);
			};
			r.readAsArrayBuffer(f);
		};
		i.click();
	});
}
