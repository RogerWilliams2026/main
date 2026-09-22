;Created 20/04/2021 By Roger Williams
;
;Clear ships background (3 lines) 
;Reads mem loc 53235 for boat y pos
;Draws boat
;
;
;Modified 26/05/2021 By Roger Williams
;
;added code to show boat at different heights due to level
;
;level number got from mem loc: 53001
;
;
*=$c242   ;49730


;start
;    lda #0
;    ldx #0
;    ldy #0
;    lda #32
;clearloop
;    sta 1064,x
;    sta 1104,x
;    sta 1144,x
;    inx
;    cpx #40
;    bne clearloop
;draw ship x pos depends on game level    
;    ldx #0     ;used to draw across the screen
;    ldy 53001  ;get game level to determine wave x pos
;    cpy #0     ;no level?
;    bne boatlv
;    cpy #4     ;intro?
;    bne boatlv
;    rts
;    brk

    ;if not level 2-4 can only be level 1   
;boatlv
;    cpy #2
;    beq lv2b
;    cpy #3
;    beq lv3b
;    jmp lv1boat
;lv2b
;    jsr lv2boat   
;    jmp exit
;lv3b
;    jsr lv3boat   
;    jmp exit

;level 1
;lv1boat
;    ldy 53235    ;y pos for boat
;    lda #118
;    sta 1586,y
;    lda #160
;    sta 1626,y
;    lda #95
;    sta 1664,y
;    lda #160
;    sta 1665,y
;    lda #160
;    sta 1666,y
;    lda #160
;    sta 1667,y
;    lda #105
;    sta 1668,y

;colourship
    ;set to white
;    lda #1
;    sta 55858,y
;    sta 55898,y
;    sta 55936,y
;    sta 55937,y
;    sta 55938,y
;    sta 55939,y
;    sta 55940,y
;    jmp exit
;level 2
;lv2boat
;    ldy 53235    ;y pos for boat
;    lda #118
;    sta 1386,y    
;    lda #160
;    sta 1426,y
;    lda #95
;    sta 1464,y
;    lda #160
;    sta 1465,y
;    lda #160
;    sta 1466,y
;    lda #160
;    sta 1467,y
;    lda #105
;    sta 1468,y

;colourship
    ;set to white
;    lda #1
;    sta 55658,y
;    sta 55698,y
;    sta 55736,y
;    sta 55737,y
;    sta 55738,y
;    sta 55739,y
;    sta 55740,y
;    rts

;level 3
;lv3boat
;    ldy 53235    ;y pos for boat
;    lda #118
;    sta 1066,y
;    lda #160
;    sta 1106,y
;    lda #95
;    sta 1144,y
;    lda #160
;    sta 1145,y
;    lda #160
;    sta 1146,y
;    lda #160
;    sta 1147,y
;    lda #105
;    sta 1148,y

;colourship
    ;set to white
;    lda #1
;    sta 55338,y
;    sta 55378,y
;    sta 55416,y
;    sta 55417,y
;    sta 55418,y
;    sta 55419,y
;    sta 55420,y
;    rts

;exit
;    rts
;    brk

start
        jsr drawship
        rts
        brk  
       
drawship
        lda #0
        ldx #0
        ldy #0
        jsr initloop
        rts

initloop
        lda #32         ;use space to clear 
    
clearloop
        ;level 1
        sta 1586,x
        sta 1626,x
        sta 1664,x
        ;level 2
        sta 1386,x    
        sta 1426,x
        sta 1464,x
        ;level 3
        sta 1064,x
        sta 1104,x
        sta 1144,x
        
        inx
        cpx #40          ;boat length
        bne clearloop
        
boatdrw
;draw ship x pos depends on game level    
        ldx #0     ;used to draw across the screen
        ldy 53001  ;get game level to determine wave x pos
        cpy #0     ;no level?
        bne boatlv
        cpy #4     ;intro?
        bne boatlv
        rts
        brk

    ;if not level 2-4 can only be level 1   
boatlv
        cpy #2
        beq lv2b
        cpy #3
        beq lv3b
        jmp lv1boat
lv2b
        jsr lv2boat   
        rts
lv3b
        jsr lv3boat   
        rts

;level 1
lv1boat
    ldy 53235    ;y pos for boat
    lda #118
    sta 1586,y
    lda #160
    sta 1626,y
    lda #95
    sta 1664,y
    lda #160
    sta 1665,y
    sta 1666,y
    sta 1667,y
    lda #105
    sta 1668,y

;colourship
    ;set to white
    lda #1
    sta 55858,y
    sta 55898,y
    sta 55936,y
    sta 55937,y
    sta 55938,y
    sta 55939,y
    sta 55940,y
    rts
;level 2
lv2boat
    ldy 53235    ;y pos for boat
    lda #118
    sta 1386,y    
    lda #160
    sta 1426,y
    lda #95
    sta 1464,y
    lda #160
    sta 1465,y
    sta 1466,y
    sta 1467,y
    lda #105
    sta 1468,y

;colourship
    ;set to white
    lda #1
    sta 55658,y
    sta 55698,y
    sta 55736,y
    sta 55737,y
    sta 55738,y
    sta 55739,y
    sta 55740,y
    rts

;level 3
lv3boat
    ldy 53235    ;y pos for boat
    lda #118
    sta 1066,y
    lda #160
    sta 1106,y
    lda #95
    sta 1144,y
    lda #160
    sta 1145,y
    sta 1146,y
    sta 1147,y
    lda #105
    sta 1148,y

;colourship
    ;set to white
    lda #1
    sta 55338,y
    sta 55378,y
    sta 55416,y
    sta 55417,y
    sta 55418,y
    sta 55419,y
    sta 55420,y
    rts
