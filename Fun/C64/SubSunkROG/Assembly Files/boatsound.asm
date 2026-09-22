*=$c472  ;50290

;Created 02/05/2021 by Roger Williams
;
;use Voice 1 noise to create engine sound (BASIC calls the sys in get loop)
;
;
;uses mem loc:
;
; 53239 - note hi       
; 53240 - note lo
;


start
    lda #0
    ldx #0
    lda #0
    lda 53239
    cmp #0
    beq exit    ;check if no note to play if so exit!
    lda #15     ;set volume to max
    sta 54296
init
    sta 54272
    sta 54273
    sta 54274
    sta 54276
    lda #128
    sta 54276
    lda #0
startsnd
    lda #15     ;set volume to max
    sta 54296
    lda #5      ;attack
    sta 54277
    lda #0      ;decay
    sta 54282
    lda #15
    sta 54278   ;sustain
    lda #15    
    sta 54284   ;release
    lda 53241
    sta 54273   ;note hi
    lda 53242
    sta 54272   ;note lo
    lda #129
    sta 54276   ;voice 2 pulse

exit
    rts
    brk