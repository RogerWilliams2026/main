*=$c5a8  ;50600

;Created 02/05/2021 by Roger Williams
;
;use Voice 2 noise to create engine sound (BASIC calls the sys in get loop)
;
;
;uses mem loc:
;
; 53241 - note hi       
; 53242 - note lo
;

start
    lda #0
    ldx #0
    lda #0
    lda 53241
    cmp #0
    beq exit    ;check if no note to play if so exit!
init
    sta 54279
    sta 54280
    sta 54284
    sta 54285
    lda #128
    sta 54283
    lda #0
startsnd
    lda #15     ;set volume to max
    sta 54296
    lda #5      ;attack
    sta 54284
    lda #0      ;decay
    sta 54289
    lda #15
    sta 54285   ;sustain
    lda #15    
    sta 54291   ;release
    lda 53241
    sta 54280   ;note hi
    lda 53242
    sta 54279   ;note lo
    lda #129
    sta 54283   ;voice 2 pulse
    
exit
    rts
    brk
