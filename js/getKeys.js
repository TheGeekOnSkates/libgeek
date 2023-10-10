/**
 * Gets all children of an HTML tag with a "data-key" attribute
 * @param {HTMLElement} E The parent element
 * @returns {object} An object with each field/property/whatever
 * named after the data-key attribute (see examples below)
 */
function getKeys(E) {
	var k = {};
	E.querySelectorAll('[data-key]').forEach(function(e) {
		k[e.getAttribute("data-key")] = e;
	});
	return k;
}
