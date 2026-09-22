;Created 03/05/2021 by Roger Williams
;
;clears bottom 3 lines
;

*=$c544  ;50500


;now clear the sub from the screen!
    ;set to empty space
    lda #32
    ldx #0

clearloop
    sta 1904,x
    sta 1944,x
    sta 1984,x
    inx
    cpx #40
    bne clearloop

exit
    ldx #0
    sta 53238  ;reset mem loc to 0
    lda #14    ;reset cursor colour to light blue
    sta 646 
    rts
    brk