;Created 15/05/2020 By Roger Williams
;
;draws waves for subsunkrog game
;
;basic program controls the characters used to draw the sea!
;
;mem locations:
;
;53231 = 78  /
;53232 = 77  \
;
;
;mem location: 53244 to determine game level
;This sets the height of the wave (x pos) on the screen easy near bottom,
;hard near top
;
;Levels (including screen memory pos):
;
;       1 - 1704
;       2 - 1504
;       3 - 1184
;       4 - 1904 (intro only)
;
;
;
;
;
;
;
;
;
;
*=$c0a8  ;49320

start
    ldy 53244  ;get game level to determine wave x pos
    cpy #0     ;no level?
    bne cont
    rts
    brk
cont

;opposite direction
lv1rev
    cpy #2
    beq lv2rev
    cpy #3
    beq lv3rev
    cpy #4
    beq lv4rev
;level 1
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53232
    sta 1704,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53231
    sta 1704,x
    inx
    cpx #40
    bne lv1rev
    jmp exit

;level 2
lv2rev
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53232
    sta 1504,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53231
    sta 1504,x
    inx
    cpx #40
    bne lv2rev
    jmp exit

;level 3
lv3rev
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53232
    sta 1184,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53231
    sta 1184,x
    inx
    cpx #40
    bne lv3rev
    jmp exit

;level 4 (intro)
lv4rev
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53232
    sta 1984,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53231
    sta 1984,x
    inx
    cpx #40
    bne lv4rev

exit
    lda #14    ;reset cursor colour to light blue
    sta 646 
    rts
    brk
