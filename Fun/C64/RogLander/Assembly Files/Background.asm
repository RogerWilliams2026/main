;colours 1/3 screen black rest in green
*=$c000

start
        jsr $e544  ;clear screen
        ldx #0
        ldy #0
hloop1    ;draw black horizon
        lda #224
        sta 1024,x
        tya
        sta 55296,x
        clc
        inx
        cpx #200
        bne hloop1    
;draw next 200 spaces
        ldx #0
        ldy #0
hloop2
        lda #224
        sta 1104,x
        tya
        sta 55376,x
        clc
        inx
        cpx #240
        bne hloop2
;draw 80 spaces
        ldx #0
        ldy #0
hloop3
        lda #224
        sta 1344,x
        tya
        sta 55616,x
        clc
        inx
        cpx #80
        bne hloop3

;draw green land 240 spaces
        ldx #0
        ldy #5
lloop1
        lda #224
        sta 1384,x
        tya
        sta 55696,x
        clc
        inx
        cpx #240
        bne lloop1
;draw another 240 spaces
        ldx #0
        ldy #5
lloop2
        lda #224
        sta 1504,x
        tya
        sta 55816,x
        clc
        inx
        cpx #240
        bne lloop2
;draw another 240 spaces
        ldx #0
        ldy #5
lloop3
        lda #224
        sta 1744,x
        tya
        sta 56056,x  
        clc
        inx
        cpx #240
        bne lloop3
;draw last 40 spaces
        ldx #0
        ldy #5
lloop4
        lda #224
        sta 1984,x  
        tya
        sta 56026,x   
        clc
        inx
        cpx #40
        bne lloop4
exit
          clc
          rts
          brk