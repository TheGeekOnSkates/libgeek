/**
 * Formats a date in the format required for <input type="datetime-local" />
 * @param {string|Date} [d] An optional date string or JS Date object
 * @returns {string} The string in the format yyyy-mm-ddThh:mm
 */
function FormatDateTimeLocal(d = new Date()) {
	if (typeof(d) === "string") d = new Date(d);
	return d.getFullYear()
		+ "-" + ("0" + (d.getMonth() + 1).toString()).slice(-2)
		+ "-" + ("0" + d.getDate().toString()).slice(-2)
		+ "T" + ("0" + d.getHours().toString()).slice(-2)
		+ ":" + ("0" + (d.getMinutes() + 1).toString()).slice(-2);
}
