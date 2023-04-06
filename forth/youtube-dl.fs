: get-mp4
	s" youtube-dl 'bestvideo[ext=mp4]+bestaudio[ext=m4a]/mp4' " pad place
	pad +place pad count system
;

: get-mp3
	s" youtube-dl -x --audio-format mp3 " pad place
	pad +place pad count system
;

: get-ogg
	s" youtube-dl -x --audio-format ogg " pad place
	pad +place pad count system
;
