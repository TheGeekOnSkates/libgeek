/**
 * Draws a character on the screen using the character grid strategy Commodore
 * had on their 8-bit computers
 * @param {number} x The horizontal ("X") coordinate of the character cell
 * @param {number} y The vertical ("Y") coordinate of the character cell
 * @param {number} n The location in js6502.ram where the character data is stored
 * @param {Uint8Array} ram The emulated RAM to get the character data from
 * @param {number} charRamStart The address where character data starts in RAM
 * @param {HTMLCanvasElement} canvas The canvas to draw on :)
 * (the first of 8, since each character takes 8 bytes to set each "pixel")
 * @remarks This was something I created for a VM / fantasy emulator / whatever
 * you like to call it; notice that it's monochrome, no color defined or passed
 * to this.  Next time I decide I want to play with something like this, I
 * should probably add those things as function parameters. :)
 */
function drawCommodoreStyleChar(x, y, n, ram, charRamStart, canvas) {
	for (let i=0; i<8; i++) {
		for (let j=0; j<8; j++) {
			if (ram[charRamStart + (n * 8) + i] & (1 << j)) {
				canvas.fillRect(((x * 32) - (j * 4)) + 28, (y * 32) + (i * 4), 4, 4);
			}
		}
	}
}
