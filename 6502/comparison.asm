; 16-bit number comparison I found on a forum:
; http://forum.6502.org/viewtopic.php?t=6136
;
; A few notes here:
;		* The "x" and "y" here are NOT registers.
;		  They are the two 16-bit numbers being compared.
;		* Obviously I haven't defined ishigher, islower
;		  or issame here - they're just placeholders
; ------------------------------------------------------------------

	LDA #>x			; MSB of 1st number
	CMP #>y			; MSB of 2nd number
	BCC islower		; X < Y
	BNE ishigher	; X > Y
	LDA #<x			; LSB of 1st number
	CMP #<y			; LSB of 2nd number
	BCC islower		; X < Y
	BEQ issame		; X = Y
	BNE ishigher	; X > Y
