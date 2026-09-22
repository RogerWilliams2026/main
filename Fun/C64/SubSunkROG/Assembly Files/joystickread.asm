;Created 108/05/20201 By Roger Williams
;
;Joystick 1/2 input test
;
;Right ALT+3 = #
;


*=$c000 ;49152
jstick2   = $dc00   ;joy1 $dc01 = joy2
jstick1   = $dc01
offset    = $c100                
fire      = $c102


start
        ldx #0 
        ldy #0 
        lda #0 
        ;keep
        lda $ff 
        sta offset
        lda #64
        sta fire

main
        lda 50000       ;contains 49 for joy 1 50 for joy 2
        cmp #50         ;petscii for 2
        beq joy2 
        sta 1024        ;print joy number being tested for
        lda jstick1     
        jmp cont
joy2
        sta 1024        ;print joy number being tested for
        lda jstick2     
cont
        eor offset
        clc
        ;accumulator holds joystick value

        ;joy2
        cmp #223         ;neutral
        beq white
        cmp #222         ;up 
        beq black
        cmp #221         ;down
        beq blue
        cmp #219         ;left
        beq cyan
        cmp #215         ;right    
        beq yellow      
        cmp #213         ;u/r
         ;beq 
        cmp #217         ;u/l
        ;beq 
        cmp #212         ;d/r
        ;beq 
        cmp #216         ;d/r
        ;beq 
        cmp #207         ;fire
        beq ltblue

        ;joy1
        cmp #95         ;neutral
        beq white
        cmp #71         ;up 
        beq black
        cmp #94         ;down
        beq blue
        cmp #91         ;left
        beq cyan
        cmp #87         ;right    
        beq yellow      
        cmp #86         ;u/r
         ;beq 
        cmp #90         ;u/l
        ;beq 
        cmp #85         ;d/r
        ;beq 
        cmp #72         ;d/r
        ;beq 
        cmp #79         ;fire
        beq ltblue

        lda #0  
        sta 198
        lda 197     
        cmp #0
        bne exit

        jmp main

   

white
        ldx #1
        stx $d020
        jmp main
black
        ldx #0
        stx $d020
        jmp main
blue
        ldx #6
        stx $d020
        jmp main
cyan
        ldx #3
        stx $d020
        jmp main
yellow
        ldx #7
        stx $d020
        jmp main
ltblue
        ldx #14
        stx $d020
        jmp main
          

exit
        rts        
        brk
        