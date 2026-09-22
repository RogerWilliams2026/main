;Created 14/04/2020 By Roger Williams
;
;draws waves for subsunkrog game
;
;can draw in to different ways to create illusion of movement
;
;basic program controls animation and even the characters to draw the sea!
;
;mem locations:
;
;53230 used to determine if drawing /\ or \/ (movement) either 0 or 1 
;53231 = 78  /
;53232 = 77  \
;
;basic program handles 50000 value by switching between 0 and 1 in a GET loop
;and using a small for..next loop can create animation!
;
;
;Modified 11/05/2020 By Roger Williams
;
;Added mem loction: 53244 to determine game level
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
*=$c000  ;49152

start
    ldx #0     ;used to draw across the screen
    ldy 53244  ;get game level to determine wave x pos
    cpy #0     ;no level?
    bne lv1norm
    rts
    brk

    ;if not level 2-4 can only be level 1   
lv1norm
    cpy #2
    beq lv2norm
    cpy #3
    beq lv3norm
    cpy #4
    beq lv4norm
;level 1
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53231
    sta 1704,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53232
    sta 1704,x
    inx
    cpx #40
    bne lv1norm
    jmp exit
lv2norm    
;level 2
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53231
    sta 1504,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53232
    sta 1504,x
    inx
    cpx #40
    bne lv2norm
    jmp exit    
;level 3
lv3norm
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53231
    sta 1184,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53232
    sta 1184,x
    inx
    cpx #40
    bne lv3norm
    jmp exit    
;level 4 (intro)
lv4norm
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53231
    sta 1984,x
    inx
    cpx #40
    beq exit
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53232
    sta 1984,x
    inx
    cpx #40
    bne lv4norm

exit
    lda #14    ;reset cursor colour to light blue
    sta 646 
    rts
    brk
