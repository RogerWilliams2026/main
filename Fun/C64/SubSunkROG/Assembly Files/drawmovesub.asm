;Created 29/04/2021 By Roger Williams
;
;Clear subs background (3 lines) 
;Reads mem loc:
;
;53236 for sub y pos
;53237 for sub direction 0 =right 1 =left
;
;Draws sub
;
;
*=$c2ec   ;49900


start
    lda #0
    ldx #0
    ldy #0

;    ldx 53004     ;get depth charge y pos
;    lda 1944,x    ;sub travel line

    ;set to empty space
;    lda #32   
;    ldy 53237
;    cmp #0
;    beq clearr

;clearl
;    ldx 53236     ;sub y pos
;    dex
     
;clear if moving right
    ldx 53236     ;sub y pos
    ;set to empty space
    lda #32   

clearloopl
    sta 1905,x
    sta 1944,x
    sta 1984,x
    iny
    cpy #4
    bne clearloopl
;    jmp shipdraw
    
;clearr
;    ldx 53236     ;sub y pos
;    inx
     
;clear if moving right
;clearloopl
;    sta 1907,x
;    sta 1944,x
;    sta 1984,x
;    dex
;    cpx #3
;    bne clearloopr

    inc 53236

shipdraw
    ldy 53236    ;y pos for sub
    sty 1024
    lda 53237
    cmp #0       ;which direction is sub moving in?
    beq rtconn   ;draw conn tower accordingly is 
                 ;+3 when going left  +2 when going right
;conn tower on left (going right)
    lda #108
    sta 1907,y
    lda #123
    sta 1908,y
    jmp bodydraw
;conn tower on right (going left)

rtconn
    lda #108
    sta 1905,y
    lda #123
    sta 1906,y
   
bodydraw
;body top
    lda #108
    sta 1944,y
    lda #160
    sta 1945,y
    lda #160
    sta 1946,y
    lda #160
    sta 1947,y
    lda #160
    sta 1948,y
    lda #123
    sta 1949,y
;body bottom
    lda #124
    sta 1984,y
    lda #160
    sta 1985,y
    lda #160
    sta 1986,y
    lda #160
    sta 1987,y
    lda #160
    sta 1988,y
    lda #126
    sta 1989,y


colourship
    ;set to green
    lda #5
;conn tower
    sta 56177,y
    sta 56178,y
    sta 56179,y
    sta 56180,y
;middle
    sta 56216,y
    sta 56217,y
    sta 56218,y
    sta 56219,y
    sta 56220,y
    sta 56221,y
;bottom
    sta 56256,y
    sta 56257,y
    sta 56258,y
    sta 56259,y
    sta 56260,y
    sta 56261,y

exit
 
   rts
    brk