;Created 14/04/2020 By Roger Williams
;
;draws the bomb dropped from the ship
;
;NOTE: is NOT looped simply draws one "frame" then exits
;
;mem locations:
;
;50003 - bomb start pos (column) bomb always drops from 1184-1273 range
;                                                       55416-55455 
;bomb colour = 0 (black)
;53233 - bomb x pos
;53234 - bomb y pos
;
;draws bomb using kernel function PLOT
;
*=$c200  ;49664

start
    lda 53233
    cmp #4        ;is this first bomb draw?
    beq drawbomb
    ;clear previous bomb by changing background colour to 6 (blue)
    lda #0
    ldx 53233
    dex
    ldy 53234
    clc 
    jsr 65520     ;move cursor
    lda #6
    sta 646       ;set colour to blue
    lda #113      ;bomb char
    jsr $ffd2     ;print bomb
    inx           ;move to next row

drawbomb
    lda #0
    ldx 53233
    ldy 53234
    clc 
    jsr 65520     ;move cursor
    lda #0
    sta 646       ;set colour to black
    lda #113      ;bomb char
    jsr $ffd2     ;print bomb

exit
    rts
    brk