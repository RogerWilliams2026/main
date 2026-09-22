;created 01/05/20201 by roger williams
;
;animates the sub when hit and animates BOOM! text
;(flashes sub different colours)
;
*=$c3b4  ;50100

start
    lda #0
    ldx #0
    ldy #0

drawtext   ;write BOOM! on the screen
    lda #2
    sta 1440
    lda #15
    sta 1441
    lda #15
    sta 1442
    lda #13
    sta 1443
    lda #33
    sta 1444
    
init
    lda #0
    sta 53238  ;use as colour index

colourship
    ;flash sub by iterating through the colours
    sta 56176,y
    sta 56216,y
    sta 56256,y
    ;flash BOOM! text
    sta 55696,y
    iny
    cpy #40
    beq nextcolour
    jmp colourship

nextcolour
    inc 53238  ;get next colour
    ldy #0
    ldx #0

    ;wait
wait
    inx
    cpx #255
    bne wait

    ;put value (last colour) back into x register from acculuator
    lda 53238
    cmp #255
    beq exit    
    jmp colourship
    
exit
    ldx #0
    sta 53238  ;reset mem loc to 0
    lda #14    ;reset cursor colour to light blue
    sta 646 
    rts
    brk