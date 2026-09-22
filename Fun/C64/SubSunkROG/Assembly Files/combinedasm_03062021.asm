;
;
;Levels (including screen memory pos):
;
;       1 - 1704
;       2 - 1504
;       3 - 1184
;       4 - 1904 (intro only)
;
;mem loc
;
;53000  - boat y pos
;53001  - level
;53002  - wave forward char  77 = /
;53003  - wave reversed char 78 = \
;53004  - bomb y pos
;53005  - bomb x pos
;53006  - sub y pos
;53007  - sub direction 0 =right 1 =left
;53008  - boat sound hi
;53009  - boat sound lo
;53010  - sub sound hi
;53011  - sub sound lo
;53012  - amount of times delay routine ran
;53013  - text colour for intro (routine inc value)
;53014  - depth charge being animate
;53015  - raster wait delay 120 about 1 second
;53016  - release depth charge (set to 1 after drop)
;
;
;sub sound iterates between hi:8, lo:255 and hi:5, lo:71
;boat sound iterates between hi:4, lo:180 and hi:4, lo:12
;
;NOTE: using variables when referenced in code use #<var name> e.g. lda #level
;      setting variables use directly e.g. sta level

*=$c000 ;49152

;********************initialise***************************
         ldx #0 
         ldy #0 
         lda #0
         ;initialise zero locations 
         sta 53000     ;set default boat y pos
         sta 53004     ;bomb y pos         
         sta 53006     ;set default sub y pos
         sta 53007     ;set default sub direction
         sta 53012     ;reset time delay count
         sta 53013     ;set default colour
         sta 53012     ;reset numbers of delay counter
         sta 53013     ;set text colour to black
         sta 53014     ;set num of depth chrages dropped to zero 
         sta 53016     ;set depth charge released to no!
         ;initialise sound 
         lda #4
         sta 53008     ;boat sound hi
         lda #180
         sta 53008     ;boat sound lo      
         lda #8
         sta 53010     ;sub sound hi
         lda #255
         sta 53011     ;sub sound lo     

         ;initialise non zero locations      
         lda #78        
         sta 53002     ;wave forward symbol / 
         lda #77        
         sta 53003     ;wave reversed symbol \ 
         lda #4
         sta 53001     ;get level to 4 (intro)
         jsr intro     ;show intro   
         lda #190
         sta 53015     ;set wait delay
         jsr $e544     ;clear screen     
         lda #1        ;set level to 1  
         sta 53001      
         lda #15
         sta 53005     ;bomb x pos level 1
         lda #0        ;reset accumulator
         sta 198       ;clear keyboard buffer 
         jsr drawship  ;draw ship for the first time (next time drawn is
                       ;when ship is moved)

;***********************main game loop**********************************
main    
        ;order:
        ;      ;draw forward wave
        ;      check for key has BEEN pressed
        ;      draw reverse wave
        ;      raster wait (stops sea going too fast)
        ;      check for collision 
        ;      draw depth charge
        ;      move sub
        ;
        ;
         jsr keypress
         jsr movesub
         jsr drawdepth  
         jsr fwdwave
         jmp main
;************************end of main loop*******************************

keypress
;handles all keypreses
         jsr $FF9F       ;scan keyboard
         jsr $ffe4       ;check if key in buffer 
         ;key presses are stored as ASCII
         cmp #88         ;x pressed?
         beq leave
         cmp #65         ;a pressed?
         beq movel  
         cmp #68         ;d pressed? 
         beq mover
         cmp #32         ;space pressed?
         beq reldepth
         rts

leave
         brk

;using short jumps to here then jsr to avoid page boundary restrictions
;that stop BEQ/BNE etc from working as technically some code is only
;available via far jumps i.e. jsr/jmp
movel
         jsr lmoveship
         rts

mover
         jsr rmoveship
         rts


;"releases| depth charge
;sets 53016 to 1

reldepth
        lda #1
        sta 53016
        rts

;drops depth charge from ship NOTE: not a loop
;
;Depth charge x pos based on level:
;
;Level 1        - 19
;Level 2        - 14
;Level 3        - 6
;
;
;53004 - y pos
;53005 - x pos
;53016 - animate bomb 
;
;
drawdepth
         lda 53016      ;depth charge released?
         cmp #0
         beq depthexit  ;mo? then exit    

         ;set y pos to boat y pos +4
         lda 53000      ;get boat y pos
         sta 53004      ;set as depth y pos
         lda #4
         adc 53004      ;add 4 for offset

         jsr waitawhile
         lda 53014      ;depth charge first time draw?
         cmp #0
         bne cleardepth
            
         lda #1         ;sets 53014 to 1 = depth charge being drawn
         sta 53014

         lda 53001      ;check level
         cmp #4
         beq depthexit  ;intro? then exit    
         cmp #3
         beq depth3
         cmp #2
         beq depth2 
         cmp #1
         beq depth1
      
depth3                  ;set depth drop height based on level Y pos always const
         lda #6
         sta 53005
;         lda #4
;         sta 53004 
         jmp ddepth

depth2
         lda #14
         sta 53005
;         lda #4
;         sta 53004 
         jmp ddepth

depth1
         lda #18
         sta 53005
;         lda #4
;         sta 53004 
         jmp ddepth

depthexit
         rts
  
cleardepth
         ;clear previous bomb by changing background colour to 6 (blue)
         lda #0
;         ldx 53005
;         dex
;         ldy 53004
;         clc 
;         jsr 65520     ;move cursor
;         lda #6
;         sta 646       ;set colour to blue
;         lda #113      ;bomb char
;         jsr $ffd2     ;print depth charge
                
 
;draw depth
ddepth
         lda #0
         ldx 53005     ;depth x
         ldy 53004     ;depth y
         clc 
         jsr 65520     ;move cursor
         lda #0
         sta 646       ;set colour to black
         lda #113      ;bomb char
         jsr $ffd2     ;print bomb
         inc 53005     ;move to next row

         ;check if bottom of screen
         ldx 53005
         cpx #25
         bne depthexit

         ;if so reset depth dropped mem loc
         lda #0
         sta 53016
         sta 53014

         ;checks if depth charge has hit sub
         lda #1
         ldx 53004     ;get depth charge y pos
         lda 1944,x    ;sub travel line
         cmp #32       ;hit sub?
         bne subhit          
        
         rts
              
subhit
         lda #25
         sta 1024
         jsr collision
         rts



;handles sub movement i.e. screen edge boundaries and standanrd movement
movesub
         lda 53007
         cmp #1         ;sub move left?
         beq subml    

submr
         inc 53006      ;inc sub y pos
         lda 53006         
         cmp #34
         bne anisub
         lda #1
         sta 53007      ;if rh edge of screen reverse movement direction
         jsr subdraw
         rts

subml
         dec 53006
         lda 53006
         cmp #0
         bne anisub
         lda #0
         sta 53007      ;change sub direction if lh side of screen         

anisub
         jsr subdraw
         jsr waitawhile       
         rts

collision
         rts


subdraw
    lda #0
    ldx #0
    ldy #0
    ;set to empty space
    lda #32   
clearloop
    sta 1904,x
    sta 1944,x
    sta 1984,x
    inx
    cpx #40
    bne clearloop
    
    ldy 53006    ;y pos for sub
    lda 53007    ;sub direction 
    cmp #0       ;which direction is sub moving in?
    beq rtconn   ;draw conn tower accordingly is 
                 ;+3 when going left  +2 when going right
;conn tower on left (going right)
    lda #108
    sta 1907,y
    lda #123
    sta 1908,y
    jmp subbody

;conn tower on right (going left)
rtconn
    lda #108
    sta 1905,y
    lda #123
    sta 1906,y
   
subbody
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


coloursub
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
    rts
          


rmoveship
;see if boat at right edge of screen
        inc 53000
        lda 53000
        cmp #36
        bne pdrawship  
        lda #35      ;set to 35 if >35
        sta 53000
        rts

lmoveship
;see if boat at left edge of screen
        clc               ;clear carry flag
        dec 53000
        lda 53000
        cmp #255
        bne pdrawship      ;carry flag set to true if <0
        lda #0            ;set ypos to 
        sta 53000       
        rts

pdrawship
        jsr drawship
        rts

drawship
        lda #0
        ldx #0
        ldy #0
        jsr initloop
        rts

initloop
        lda #32         ;use space to clear 
    
clearship
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
        bne clearship
        
        
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
        jsr lv1boat
        jmp boatexit
lv2b
        jsr lv2boat   
        rts
lv3b
        jsr lv3boat
boatexit   
        rts

;level 1
lv1boat
    ldy 53000    ;y pos for boat
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
    ldy 53000    ;y pos for boat
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
    ldy 53000    ;y pos for boat
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


;Created 26/05/2021 By Roger Williams
;
;Waits till raster line 251 then shows letter on screen
;Also has fancy delay loop to slow things down
;
waitawhile
;rasterwait
        ldx #0 
        ldy #0 
        lda #0        
        sta 53012       ;used for num times delay loop run 
;rloop1
;        lda #$fb        ;wait for raster line 251
;        cmp 53266
;        bne rloop1

        ldy #0 
delay1       
        iny
        cpy #255
        bne delay1

        ldy #0 
        inc 53012               ;inc num times run
        lda 53012
        cmp 53015                ;120 is about 1.5 seconds
        bne delay1
        clc
        rts        

        
;Created 10/05/2021 by Roger Williams
;
;RogSunk Intro!
;
;Draws text: ROG SUNK
;
;Second program Intro2 handles colour
;

intro
        jsr $e544   ;clear screen     
        lda #0
        ldx #0
        lda #6
        sta 53280  ;set border to blue 
        lda #150
        sta 53015  ;set raster wait delay to about 1 second
        lda #160   ;char to solid block
drawtext
        sta 1029  ;R
        sta 1030
        sta 1031
        sta 1032
        sta 1033
        sta 1034  ;6

        sta 1036  ;O
        sta 1037
        sta 1038
        sta 1039
        sta 1040
        sta 1041

        sta 1043  ;G
        sta 1044
        sta 1045
        sta 1046
        sta 1047
        sta 1048

        sta 1069  ;R
        sta 1074

        sta 1076  ;O
        sta 1081

        sta 1083  ;G

        sta 1109  ;R
        sta 1114

        sta 1116  ;O
        sta 1121

        sta 1123  ;G

        sta 1149  ;R
        sta 1154  

        sta 1156  ;O
        sta 1161

        sta 1163  ;G

        sta 1189  ;R
        sta 1190
        sta 1191
        sta 1192
        sta 1193
        sta 1194

        sta 1196  ;O
        sta 1201

        sta 1203  ;G

        sta 1229  ;R
        sta 1230

        sta 1236  ;O
        sta 1241

        sta 1243  ;G

        sta 1269  ;R
        sta 1271

        sta 1276  ;O
        sta 1281

        sta 1283  ;G
        sta 1285
        sta 1286
        sta 1287
        sta 1288

        sta 1309  ;R
        sta 1312

        sta 1316  ;O
        sta 1321

        sta 1323  ;G
        sta 1326

        sta 1349  ;R
        sta 1353

        sta 1356  ;O
        sta 1357
        sta 1358
        sta 1359
        sta 1360
        sta 1361

        sta 1363  ;G
        sta 1364
        sta 1365
        sta 1366

        sta 1432  ;S
        sta 1433
        sta 1434
        sta 1435
        sta 1436
        sta 1437

        sta 1439  ;U
        sta 1444  

        sta 1446  ;N
        sta 1451

        sta 1453  ;K

        sta 1472  ;S

        sta 1479  ;U
        sta 1484

        sta 1486  ;N
        sta 1487
        sta 1491

        sta 1493  ;K
        sta 1498

        sta 1512  ;S

        sta 1519  ;U
        sta 1524

        sta 1526  ;N
        sta 1528
        sta 1531
        sta 1526

        sta 1533  ;K
        sta 1537

        sta 1552  ;S

        sta 1559  ;U
        sta 1564

        sta 1566  ;N
        sta 1569
        sta 1571

        sta 1573  ;K
        sta 1576

        sta 1592  ;S

        sta 1599  ;U
        sta 1604

        sta 1606  ;N
        sta 1610
        sta 1611

        sta 1613  ;K
        sta 1615

        sta 1632  ;S
        sta 1633
        sta 1634
        sta 1635
        sta 1636
        sta 1637

        sta 1639  ;U
        sta 1644

        sta 1646  ;N
        sta 1651

        sta 1653  ;K
        sta 1654

        sta 1677  ;S

        sta 1679  ;U
        sta 1684

        sta 1686  ;N
        sta 1691

        sta 1693  ;K
        sta 1695

        sta 1717  ;S

        sta 1719  ;U
        sta 1724

        sta 1726  ;N
        sta 1731

        sta 1733  ;K
        sta 1736

        sta 1757  ;S

        sta 1759  ;U
        sta 1764

        sta 1766  ;N
        sta 1771

        sta 1773  ;K
        sta 1777

        sta 1797  ;S

        sta 1799  ;U
        sta 1804

        sta 1806  ;N
        sta 1811

        sta 1813  ;K
        sta 1818

        sta 1832  ;S
        sta 1833
        sta 1834
        sta 1835
        sta 1836
        sta 1837

        sta 1839  ;U
        sta 1840
        sta 1841
        sta 1842
        sta 1843
        sta 1844

        sta 1846  ;N
        sta 1851

        sta 1853  ;K
;print "press a key"     
        lda #16            ;p    
        sta 1960
        lda #18            ;r
        sta 1961
        lda #5             ;e
        sta 1962
        lda #19            ;s
        sta 1963
        lda #19            ;s
        sta 1964
        lda #32
        sta 1965
        lda #1             ;a
        sta 1966
        lda #32
        sta 1967
        lda #11            ;k
        sta 1968
        lda #5             ;e
        sta 1969
        lda #25            ;y
        sta 1970
        lda #0
        sta 198   ;clear keyboard buffer 

intloop
        jsr colintro  
        jsr fwdwave
;        jsr waitawhile
;        jsr revwave


 
        inc 53013        ;change text colour 
        lda 53013        
        cmp #16          ;end of colours? 
        beq rescol      

          
       ;wait for keypress
        jsr $FF9F        ;scan keyboard
        jsr $ffe4
        beq intloop

        lda #0
        sta 198
        sta 631          ;clear keyboard buffer
        rts
          
rescol   
        lda #0           ;reset colour to 0
        sta 53013 
        jmp intloop


;Created 10/05/2021 by Roger Williams
;
;RogSunk Intro!
;
;Uses mem loc 53013 for colour var 
;
;

colintro      
        lda #0
        ldx #0
        lda 53013
     
col1
        sta 55296,x
        inx
        cpx #240
        bne col1

snext120
        ldx #0   ;reset x pos
     
next120
        sta 55536,x
        inx
        cpx #120
        bne next120
        ldx #0      ;reset delay counters
        ldy #0

loop1   ;delay loop
        inx
        cpx #255
        bne loop1
        iny
        cpy #255
        bne loop1

colsunk
        ldy #0       ;reset delay counter
        ldx #0
        inc 53013
        lda 53013

;now colour word SUNK
col2
        sta 55696,x
        inx
        cpx #240
        bne col2
        ldx #0   ;reset x pos
  
nxt240
        sta 55896,x
        inx
        cpx #240
        bne nxt240
;now colour "press a key" 
        ldx #0   ;reset x pos

txtpress
        sta 56216,x
        inx
        cpx #40
        bne txtpress

coldelay             ;purposeful delay before returning to slow colour flash
        lda #0       ;reset delay counter
        sta 53012    ;reset number of delay counter
        ldy #0       ;reset delay counter
        ldx #0       ;reset delay counter

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
        inc 53012
        lda 53012
        cmp #1   ;run twice?
        bne loop
        rts

;Created 14/04/2020 By Roger Williams
;
;draws waves for subsunkrog game
;
;can draw in to different ways to create illusion of movement
;
;basic program controls animation and even the characters to draw the sea!
;
;mem locations:
;
;53230 used to determine if drawing /\ or \/ (movement) either 0 or 1 
;53231 = 78  /
;53232 = 77  \
;
;basic program handles 50000 value by switching between 0 and 1 in a GET loop
;and using a small for..next loop can create animation!
;
;
;Modified 11/05/2020 By Roger Williams
;
;Added mem loction: 53244 to determine game level
;This sets the height of the wave (x pos) on the screen easy near bottom,
;hard near top
;
;Levels (including screen memory pos):
;
;       1 - 1704
;       2 - 1504
;       3 - 1184
;       4 - 1904 (intro only)
;
;

fwdwave
    ldx #0     ;used to draw across the screen
    ldy 53001  ;get game level to determine wave x pos
    cpy #0     ;no level?
    bne lv1norm
    rts

    ;if not level 2-4 can only be level 1   
lv1norm
    cpy #2
    beq lv2norm
    cpy #3
    beq lv3norm
    cpy #4
    beq lv4norm
;level 1
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53002
    sta 1704,x
    inx
    cpx #40
    beq exwf
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53003
    sta 1704,x
    inx
    cpx #40
    bne lv1norm
    rts
lv2norm    
;level 2
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53002
    sta 1504,x
    inx
    cpx #40
    beq exwf
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53003
    sta 1504,x
    inx
    cpx #40
    bne lv2norm
    rts    
;level 3
lv3norm
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53002
    sta 1184,x
    inx
    cpx #40
    beq exwf
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53003
    sta 1184,x
    inx
    cpx #40
    bne lv3norm
    rts   
;level 4 (intro)
lv4norm
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53002
    sta 1984,x
    inx
    cpx #40
    beq exwf
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53003
    sta 1984,x
    inx
    cpx #40
    bne lv4norm
exwf
    jsr waitawhile
     
;    rts      


revwave          
    ldx #0     ;used to draw across the screen
    ldy 53001  ;get game level to determine wave x pos
    cpy #0     ;no level?
    bne revcont
    rts
revcont
    ;if not level 2-4 can only be level 1   
    cpy #2
    beq revlv2
    cpy #3
    beq revlv3
    cpy #4
    beq revlv4
;level 1
revlv1
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53003
    sta 1704,x
    inx
    cpx #40
    beq exwr
    lda #14    ;set wave colour
    sta 55976,x 
    lda 53002
    sta 1704,x
    inx
    cpx #40
    bne revlv1
    rts
revlv2    
;level 2
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53003
    sta 1504,x
    inx
    cpx #40
    beq exwr
    lda #14    ;set wave colour
    sta 55776,x 
    lda 53002
    sta 1504,x
    inx
    cpx #40
    bne revlv2
    rts    
;level 3
revlv3
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53003
    sta 1184,x
    inx
    cpx #40
    beq exwr
    lda #14    ;set wave colour
    sta 55456,x 
    lda 53002
    sta 1184,x
    inx
    cpx #40
    bne revlv3
    rts   
;level 4 (intro)
revlv4
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53003
    sta 1984,x
    inx
    cpx #40
    beq exwr
    lda #14    ;set wave colour
    sta 56256,x 
    lda 53002
    sta 1984,x
    inx
    cpx #40
    bne revlv4
exwr
    rts       

exitgame
    rts
    brk