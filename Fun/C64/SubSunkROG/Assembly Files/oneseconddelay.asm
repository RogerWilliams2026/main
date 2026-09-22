;Right ALT+3 = #
;


*=$c000 ;49152
;creates a 1 second delay

start
        ldx #0 
        ldy #0 
        lda #0 
        ;mem loc 53200 temp used for number of loops done
        sta 53200

loop
        inx
        cpx #255
        bne loop
        iny
        cpy #255
        bne loop
        ;reset x and y for repeat
        ldy #0 
        ldx #0 
        inc 53200
        lda 53200
        cmp #2
        bne loop

exit
        rts        
        brk
