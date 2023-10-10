/**
 * Sets up a datalist so it displays the selected option's inner text, but saves
 * its value to a hidden <input> element
 * @param {HTMLInputElement} display The element users will see
 * @param {HTMLInputElement} hidden The element storing the underlying value
 * @remarks Each option in the list should use "data-value" too (not "value")
 */
function datalistFix(display, hidden) {
	display.addEventListener('change', function(e) {
		var i = 0, o = e.target.list.options;
		for (; i<o.length; i++) {
			var v = o[i].getAttribute("data-value");
			if (o[i].innerText == e.target.value) {
				hidden.value = v;
				return;
			}
		}
	});
}
