;Created 10/05/2021 by Roger Williams
;
;RogSunk Intro!
;
;Uses mem loc 53243 for colour var 
;
;BASIC controls the colour
;
*=$c8c8  ;51400

start
    lda #0
    ldx #0
    lda 53243
      
col1
     sta 55296,x
     inx
     cpx #240
     beq snext120
     jmp col1

snext120
     ldx #0   ;reset x pos
     
next120
     sta 55536,x
     inx
     cpx #120
     beq loop1
     jmp next120
   
loop1   ;delay loop
     iny   
     cpy #255
     beq loop2
     jmp loop1
     ldy #0       ;reset delay counter

loop2    ;delay loop
     iny   
     cpy #255
     beq loop3
     jmp loop2
     ldy #0       ;reset delay counter

loop3    ;delay loop
     iny   
     cpy #255
     beq colsunk
     jmp loop3

colsunk
     ldy #0       ;reset delay counter
     ldx #0
     inc 53243
     lda 53243

;now colour word SUNK
col2
     sta 55696,x
     inx
     cpx #240
     beq snxt120
     jmp col2

snxt120
     ldx #0   ;reset x pos
     
nxt120
     sta 55896,x
     inx
     cpx #240
     beq exit
     jmp nxt120
   
exit
    rts
    brk

