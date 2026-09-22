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

