;Created 20/04/2021 By Roger Williams
;
;Clear ships background (3 lines)
;
;UNSUED here for reference only
;
*=$c242   ;49730


start
    lda #0
    ldx #0
    ldy #0
    ;set 55296..55335
    ;    55336..55375
    ;    55376..55415
    ;set to blue
    lda #6
clearloop
    sta 55296,x
    sta 55336,x
    sta 55376,x
    inx
    cpx #40
    bne clearloop

exit
    lda #14    ;reset cursor colour to light blue
    sta 646 
    rts
    brk