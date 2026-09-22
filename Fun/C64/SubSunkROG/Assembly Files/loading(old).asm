;Created 12/05/2021 By Roger Williams
;
;Converted from the original BASIC routine to show a "loading"
;progress bar while loading asm routines into memory
;
;
;
;mem locs
;
;53245 - y pos for progress bar blocks
;53246 - org y pos
;53247 - which text message to show (1-11)
;

*=$c92c   ;51500


start
            ldx #0 
            ldy #0 
            lda #0 
            ldx 53245    ;start y pos
            ldy 53246    ;def start y pos

            ;show LOADING text
            lda #12
            sta 1024
            lda #15
            sta 1025
            lda #1
            sta 1026
            lda #4
            sta 1027
            lda #9
            sta 1028
            lda #14
            sta 1029
            lda #7
            sta 1030
           ;colour text white
            lda #1
            sta 55296
            lda #1
            sta 55297
            lda #1
            sta 55298
            lda #1
            sta 55299
            lda #1
            sta 55300
            lda #1
            sta 55301
            lda #1
            sta 55302
;draw border round progress bar
            lda #79
            sta 1472
            lda #116
            sta 1512
            lda #106
            sta 1535
            ldy #8
bloop1
            lda #119
            sta 1465,y
            sta 1544,y
            iny
            cpy #30
            bne bloop1       
        
            lda #80
            sta 1465,y      
            lda #119
            sta 1544,y
            sta 1545,y              

;show message 
            lda 53247
            cmp #1
            beq pr1
            cmp #2
            beq pr2
            cmp #3
            beq pr3 
            cmp #4
            beq pr4 
            cmp #5
            beq pr5 
            cmp #6
            beq pr6 
            cmp #7
            beq pr7 
            cmp #8
            beq pr8 
            cmp #9
            beq pr9 
            cmp #10
            beq pr10 
            cmp #11
            beq pr11

pr1
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg1  ;load x with pointer of string start
            ldy #>msg1  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block
pr2
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg2  ;load x with pointer of string start
            ldy #>msg2  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block

pr3            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg3  ;load x with pointer of string start
            ldy #>msg3  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block
pr4            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg4  ;load x with pointer of string start
            ldy #>msg4  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block
pr8
            jmp pr8a

pr5            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg5  ;load x with pointer of string start
            ldy #>msg5  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block
pr7
            jmp pr7a
pr9
            jmp pr9a
pr10
            jmp pr10a
pr11
            jmp pr11a 

pr6            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg6  ;load x with pointer of string start
            ldy #>msg6  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block

pr7a            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg7  ;load x with pointer of string start
            ldy #>msg7  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block

pr8a            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg8  ;load x with pointer of string start
            ldy #>msg8  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block

pr9a            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg9  ;load x with pointer of string start
            ldy #>msg9  ;load y with pointer of string end
            jsr $ab1e   ;print 
            jmp block

pr10a            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg10  ;load x with pointer of string start
            ldy #>msg10  ;load y with pointer of string end
            jsr $ab1e    ;print 
            jmp block

pr11a            
            ldx #$0a    ; Select row 
            ldy #$07    ; Select column 
            jsr $e50c   ; Set cursor 58636

            lda #<msg11  ;load x with pointer of string start
            ldy #>msg11  ;load y with pointer of string end
            jsr $ab1e    ;print 

block            
            ldx 53245
            lda #160
            sta 1504,x  ;print block
            lda #1
            sta 55776,x ;set colour to white
            inx
            stx 53245 
            lda 53245
            cmp #30
            bne exit
            ldx 53246
loop1
            lda #6
            sta 55776,x ;set colour to blue (background)
            inx
            cpx #30
            bne loop1 
          
            lda 53246
            sta 53245   ;reset y pos

exit
            rts
            brk


msg1  text "wave forward assembler code     ",0
msg2  text "depth charge assembler code     ",0
msg3  text "boat draw assembler code        ",0
msg4  text "sub draw assembler code         ",0
msg5  text "sub hit assembler code          ",0
msg6  text "boat engine assembler code      ",0
msg7  text "clear sub assembler code        ",0
msg8  text "sub engine assembler code       ",0
msg9  text "intro text assembler code       ",0
msg10 text "intro text colour assembler code",0
msg11 text "wave reversed assembler code    ",0

