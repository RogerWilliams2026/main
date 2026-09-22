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
;53004  - bomb current y pos
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
;53015  - wait delay 120 about 1 second
;53016  - release depth charge (set to 1 after drop)
;53017  - collision detected?
;53018  - attempts - level 1=5, 2=5, 3=3
;53019  - sub hit colour counter
;53020  - user won reset var
;53021  - game over! text x pos
;53022  - game over! text y pos
;53023  - game over! text colour
;53024  - game over text printed counter
;53025  - tells intro to not wait for keypress or flash colours if =1
;
;
;
;
;sub sound iterates between hi:8, lo:255 and hi:5, lo:71
;boat sound iterates between hi:4, lo:180 and hi:4, lo:12
;
;NOTE: using variables when referenced in code use #<var name> e.g. lda #level
;      setting variables use directly e.g. sta level

*=$c000 ;49152

;********************initialise***************************
start                  ;if players loses this is called again
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
         sta 53017     ;set collision detected to no!
         sta 53019     ;set sub hit colour counter to 0
         sta 53025     ;set intro to show everything
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
         sta 53002     ;wave forward symbol  / 
         lda #77        
         sta 53003     ;wave reversed symbol \ 
         lda #4
         sta 53001     ;get level to 4 (intro)
         jsr intro     ;show intro   
         jsr drawinstructions
         lda #6 
         sta 53280     ;set background colour to blue 
         sta 53281
         ;configure for level 1
         lda #5
         sta 53018     ;set attempts to level 1 =5

         lda #1        ;set level to 1  
         sta 53001      
         lda #15
         sta 53005     ;bomb x pos level 1
         lda #0        ;reset accumulator

         sta 198       ;clear keyboard buffer 
 
maininit               ;called again if player wins/new level reached
         lda #190
         sta 53015     ;set wait delay

         jsr $e544     ;clear screen     
         lda #0
         sta 53020         ;reset player won start again var to 0
         jsr drawship      ;draw ship for the first time (next time drawn is
                           ;when ship is moved)

;***********************main game loop**********************************
main    
        ;order:
        ;      draw forward wave
        ;      check for key has BEEN pressed
        ;      draw reverse wave
        ;      raster wait (stops sea going too fast)
        ;      check for collision 
        ;      draw depth charge
        ;      move sub
        ;
        ;
         jsr drawattempts       ;writes attempts left at 1034-1044
;         jsr fwdwave
         jsr movesub
         jsr keypress
         jsr drawdepth  
         jsr fwdwave

         lda 53018              ;check player attempts
         cmp #0                 ;player no more attempts?
         beq playerlost

         lda 53020              ;player completed a level?
         cmp #1
         bne main               ;no carry on
         jmp maininit           ;yes reset    

playerlost
         jmp drawyoulose       
;         jmp main
;************************end of main loop*******************************

keypress
;handles all keypreses
         jsr $ff9f       ;scan keyboard
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
         cmp #89         ;y pressed? for testing routines ONLY       
         beq testkey
         rts

testkey
         jsr drawyouwonthegame

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


;"releases" depth charge
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
         lda 53016          ;depth charge released?
         cmp #0
         bne drawdepthcont  ;no? then exit    
         rts
 
drawdepthcont
         ;checks if depth charge has hit sub
         jsr collision        
         lda 53017         
         cmp #1
         beq depthreset
         jmp drawdepthplot

depthreset
         ;if so reset depth dropped mem loc as sub hit
         lda #0
         sta 53016
         sta 53014
         rts

drawdepthplot
         jsr waitawhile
         lda 53014       ;depth charge first time draw?
         cmp #0
         bne ddepth
            
         lda 53001       ;check level
         cmp #4
         beq ddepthexit  ;intro? then exit    
         cmp #3
         beq depth3
         cmp #2
         beq depth2 
         cmp #1
         beq depth1
      
depth3                 ;set depth drop height based on level Y pos always const
         lda #6
         sta 53005
         jmp ddepth

depth2
         lda #14
         sta 53005
         jmp ddepth

depth1
         lda #18
         sta 53005
         jmp ddepth

ddepthexit
         rts
 
;draw depth
ddepth
         lda 53014      ;depth charge first time draw?
         cmp #0
         beq drawnewbomb
         jmp drawexisting

drawnewbomb
         ;set y pos to boat y pos +4
         lda 53000      ;get boat y pos
         sta 53004      ;set as depth y pos
         lda #6
         adc 53004      ;add 6 for offset
         clc
         lda #1         ;sets 53014 to 1 = depth charge being drawn
         sta 53014

drawexisting
         ;clear previous bomb
         lda #0
         ldx 53005
         ldy 53004
         clc 
         jsr 65520      ;move cursor
         lda #6
         sta 646        ;set colour to blue
         lda #32        ;space
         jsr $ffd2      ;print bomb
         inc 53005      ;move to next row
       
drawbomb
         ;check if bottom of screen
         lda 53005
         cmp #25
         beq depthreset2
         jmp depthplot

depthreset2
         ;if so reset depth dropped mem loc
         lda #0
         sta 53016
         sta 53014
         ;if player missed decrement attempts
         dec 53018
         rts

depthplot
         lda #0
         ldx 53005 
         ldy 53004
         clc 
         jsr 65520     ;move cursor
         lda #0
         sta 646       ;set colour to black
         lda #113      ;bomb char
         jsr $ffd2     ;print bomb

depthexit
         rts



;handles sub movement i.e. screen edge boundaries and standard movement
movesub
         lda 53017      ;check if collision recorded
         cmp #1
         bne movesubcont  ;if not move/draw sub
         rts

movesubcont
         ldy #0
         ldx 53006     ;sub y pos
         cpx #0

         ;set to empty space
         lda #32   
         
clearsubloop1
         sta 1904,x     ;use x as y pos could be 10 etc.
         sta 1944,x    
         sta 1984,x
         inx
         iny
         cpy #6         ;cleared 6 chars?
         bne clearsubloop1

submovecheck
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


;draws sub
subdraw
    lda #0
    ldx #0
    ldy #0
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

;shows how many attempts the user has left to sink the sub
drawattempts
        lda #1          ;draws: attempts: 
        sta 1034
        lda #20
        sta 1035
        sta 1036
        lda #5
        sta 1037
        lda #13
        sta 1038
        lda #16
        sta 1039
        lda #20
        sta 1040
        lda #19
        sta 1041
        lda #58
        sta 1042
        lda 53018       ;get attempts number
        adc #48         ;add 48 to accumulator to convert to PETSCII number
        sta 1044        ;show
        ;colour text
        lda #1
        ldx #0

attemptsloop 
        sta 55306,x     ;colour text
        inx
        cpx #9
        bne attemptsloop
        rts


;**************************other routines***********************************
;intro
;instructions
;collision
;subhit
;drawyoulose
;inclevel
;drawyouwonthegame
;drawyouwon
;waitawhile
;

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

        lda 53025       ;check not being used for game over
        cmp #1
        bne introcont
        rts

introcont
        lda #0 
        sta 198   ;clear keyboard buffer 

intloop
        jsr colintro  
        jsr fwdwave
 
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

;handles sub being hit with depth charge
collision
    ;check depth charge is on the 1944 x pos before continuing
         lda 53005
         cmp #24
         bne collexit
   ;if depth charge at 1994 x pos check for collision
         ldx 53004         ;get depth charge y pos
         lda 1944,x
         cmp #32
         beq collexit
         cmp #160
         beq subhit
         cmp #108
         beq subhit
         cmp #123
         beq subhit

collexit
        rts
        

subhit
        lda #1
        sta 53017     ;set collision detected
        
;if 53017 if 1 then sub has been hit now animate the sub and remove it
        ;set to black
        ldx #0
        sta 53019
        ldy 53006      ;sub y pos
        
flashloop
;conn tower
        lda 53019      ;get colour
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
        sta 53019
        jsr waitawhile     ;delay a while
        ldy 53006         
        lda 53019
        inc 53019          ;inc hit colour counter
        cmp #10
        bne flashloop
        ;hide sub
        lda #32
;conn tower on left (going right)
        sta 1907,y
        sta 1908,y

;conn tower on right (going left)
        sta 1905,y
        sta 1906,y
   
;body top
        sta 1944,y
        sta 1945,y
        sta 1946,y
        sta 1947,y
        sta 1948,y
        sta 1949,y
;body bottom
        sta 1984,y
        sta 1985,y
        sta 1986,y
        sta 1987,y
        sta 1988,y
        sta 1989,y
        jsr inclevel
        rts

;shows user instructions screen
drawinstructions
        jsr $e544       ;clear screen     
        lda #0
        sta 53280
        sta 53281

        ldx #00         ;Select row 
        ldy #15         ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst1  ;load x with pointer of string start
        ldy #>strinst1  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #4          ;Select row 
        ldy #0          ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst2  ;load x with pointer of string start
        ldy #>strinst2  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #6          ;Select row 
        ldy #12         ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst3  ;load x with pointer of string start
        ldy #>strinst3  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #8          ;Select row 
        ldy #5          ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst4  ;load x with pointer of string start
        ldy #>strinst4  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #9          ;Select row 
        ldy #5          ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst5  ;load x with pointer of string start
        ldy #>strinst5  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #10         ;Select row 
        ldy #13         ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst6  ;load x with pointer of string start
        ldy #>strinst6  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #12         ;Select row 
        ldy #0          ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst7  ;load x with pointer of string start
        ldy #>strinst7  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #13         ;Select row 
        ldy #12         ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst8  ;load x with pointer of string start
        ldy #>strinst8  ;load y with pointer of string end
        jsr $ab1e       ;print 
 
        ldx #17         ;Select row 
        ldy #5          ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst9  ;load x with pointer of string start
        ldy #>strinst9  ;load y with pointer of string end
        jsr $ab1e       ;print 

        ldx #21         ;Select row 
        ldy #10         ;Select column 
        jsr $e50c       ;Set cursor 58636
    
        lda #<strinst10 ;load x with pointer of string start
        ldy #>strinst10 ;load y with pointer of string end
        jsr $ab1e       ;print 

keywaitinst
         jsr $ff9f      ;scan key 
         jsr $ffe4      ;get keypress
         cmp #0
         beq keywaitinst  
         rts


;tells user they lost
drawyoulose

;writes: you lose in a square box!
;flashes centre text
;

;youlose!youlose!
;o              y 
;u              o
;l              u
;o   you lose!  l
;s              o
;e              s
;!              e
;youlose!youlose!
         lda #120
         sta 53015     ;set wait delay
 
;top
         lda #25        ;y
         sta 1316
         lda #15        ;o
         sta 1317
         lda #21        ;u
         sta 1318
         lda #12        ;l
         sta 1319
         lda #15        ;o
         sta 1320
         lda #19        ;s
         sta 1321
         lda #5         ;e
         sta 1322
         lda #33        ;!
         sta 1323

         lda #25        ;y
         sta 1324
         lda #15        ;o
         sta 1325
         lda #21        ;u
         sta 1326
         lda #12        ;l
         sta 1327
         lda #15        ;o
         sta 1328
         lda #19        ;s
         sta 1329
         lda #5         ;e
         sta 1330
         lda #33        ;!
         sta 1331


;bottom
        ;youlose!youlose!
         lda #33        ;!
         sta 1596
         lda #25        ;y
         sta 1597
         lda #15        ;o
         sta 1598
         lda #21        ;u
         sta 1599
         lda #12        ;l
         sta 1600
         lda #15        ;o
         sta 1601
         lda #19        ;s
         sta 1602
         lda #5         ;e
         sta 1603
         lda #33        ;!
         sta 1604

         lda #25        ;y
         sta 1605
         lda #15        ;o
         sta 1606
         lda #21        ;u
         sta 1607
         lda #12        ;l
         sta 1608
         lda #15        ;o
         sta 1609
         lda #19        ;s
         sta 1610
         lda #5         ;e
         sta 1611
   
;sides
;        left side       
         lda #15        ;o
         sta 1356
         lda #21        ;u
         sta 1396
         lda #12        ;l
         sta 1436
         lda #15        ;o
         sta 1476
         ;write you lose! - across the screen
         lda #25
         sta 1479
         lda #15        ;o
         sta 1480
         lda #21        ;u
         sta 1481
         lda #12        ;l
         sta 1483
         lda #15        ;o
         sta 1484
         lda #19        ;s
         sta 1485
         lda #5         ;e
         sta 1486
         lda #33        ;!
         sta 1487

;       cont with rest of left side
         lda #19        ;s
         sta 1516
         lda #5         ;e
         sta 1556

;        right side 
         lda #25        ;y
         sta 1371      
         lda #15        ;o
         sta 1411
         lda #21        ;u
         sta 1451
         lda #12        ;l
         sta 1491
         lda #15        ;o
         sta 1531
         lda #19        ;s
         sta 1571
         lda #5         ;e
         sta 1611

         ldx #0
colourloop
         ;write you lose! - across the screen
         stx 55751
         stx 55752
         stx 55753
         stx 55755
         stx 55756
         stx 55757
         stx 55758
         stx 55759

         jsr waitawhile
         inx
         cpx #20
         bne colourloop
 
        ;reset wait delay to default
         lda #190
         sta 53015     ;set wait delay
         ;ask user if wishes to play again 
         ;play again? y/n"     
        lda #16            ;p    
        sta 1958
        lda #12            ;l
        sta 1959
        lda #1             ;a
        sta 1960
        lda #25            ;y
        sta 1961
        lda #1             ;a
        sta 1963
        lda #7             ;g     
        sta 1964
        lda #1             ;a
        sta 1965
        lda #9             ;i             
        sta 1966
        lda #14            ;n             
        sta 1967
        lda #25            ;y
        sta 1969
        lda #47            ;/
        sta 1970
        lda #14            ;n
        sta 1971

;        lda #0
;        sta 198   ;clear keyboard buffer 
      
keywait
        lda #0
        sta 198         ;clear keyboard buffer 
        sta 631          ;clear keyboard buffer
        
        jsr $ff9f      ;scan key 
        jsr $ffe4      ;get keypress
        cmp #0
        beq keywait  

        cmp #78        ;n?
        beq leavegame
        jmp start      ;restart game from level 1        
        rts

leavegame
        lda #14
        sta 646
        rts
        brk


;increments the level number (unless already at max) 
;resets attempts (53018)
;clears the screen
;

inclevel
        lda 53001       ;get current game level
        cmp #1
        beq setuplevel2
        cmp #2
        beq setuplevel3
        cmp #3          ;user completed game?
        beq drawyouwonthegame
        rts

setuplevel2

        lda #4
        sta 53018
        jmp inclevelcont

setuplevel3
        
        lda #3
        sta 53018

inclevelcont 

        lda #0
        sta 53017       ;reset sub hit 
        sta 53006       ;y pos for sub
        sta 53007       ;sub direction 
        ;reset depth dropped mem loc as sub hit
        sta 53016
        sta 53014

        inc 53001       ;move to next level
        lda #1
        sta 53020       ;tell main loop to reset to show new game level      
        jsr drawyouwon        
        rts


;shows game over screen if user completed all 3 levels
drawyouwonthegame
        lda #8 
        sta 53021
        lda #12
        sta 53022
        lda #0 
        sta 53023
        sta 53024 
        lda #1
        sta 53025       ;set intro to NOT clear the screen flash words
                        ;or wait for key press
        jsr intro
        lda #120
        sta 53015       ;set wait delay to 1 second

overloop
        clc
        ldx #0
        ldy #0
        lda #0
        sta 198
        sta 631          ;clear keyboard buffer

        ldx 53021
        ldy 53022
        jsr $e50c       ;Set cursor 58636

        lda 53023       ;set etxt colour
        sta 646
        lda #<instrgameover  ;load x with pointer of string start
        ldy #>instrgameover  ;load y with pointer of string end
        jsr $ab1e       ;print 

        inc 53023       ;colour
        inc 53024       ;count
        inc 53021       ;x
        inc 53022       ;y

        jsr waitawhile
 
       ;wait for keypress
        jsr $ff9f        ;scan keyboard
        jsr $ffe4
        cmp #0         
        beq gooverloop

gameoverexit
        lda #0
        sta 198
        sta 631          ;clear keyboard buffer
        jsr $e544        ;clear screen     

        lda #14          ;set text colour
        sta 646
        ldx #1
        ldy #1
        jsr $e50c       ;Set cursor 58636

        lda #<instrgameoverexit  ;load x with pointer of string start
        ldy #>instrgameoverexit  ;load y with pointer of string end
        jsr $ab1e       ;print 

        lda #240
        sta 53015       ;set wait delay to 2 seconds
        jsr waitawhile
        jsr waitawhile
;        rts     
        brk             ;exit game


gooverloop
        lda 53022       ;y
        cmp #20        
        bne overloop

        lda #8          ;reset x
        sta 53021
        lda #12         ;reset y
        sta 53022

        lda 53024
        cmp #40
        bne overloop

        lda #0          ;reset loop as key press exits
        sta 53024
        jmp overloop


drawyouwon
;writes: you won in a square box!
;flashes centre text
;

;youwon!youwon!
;o            y 
;u            o
;w  you won!  u
;o  you won!  w
;n            o 
;!            n
;youwon!youwon!

         lda #120
         sta 53015     ;set wait delay
 
;top
         lda #25        ;y
         sta 1316
         lda #15        ;o
         sta 1317
         lda #21        ;u
         sta 1318
         lda #23        ;w
         sta 1319
         lda #15        ;o
         sta 1320
         lda #14        ;n
         sta 1321
         lda #33        ;!
         sta 1322
         lda #25        ;y
         sta 1323
         lda #15        ;o
         sta 1324
         lda #21        ;u
         sta 1325
         lda #23        ;w
         sta 1326
         lda #15        ;o
         sta 1327
         lda #14        ;n
         sta 1328
         lda #33        ;!
         sta 1329

;bottom
        ;youwon!youwon!
         lda #25        ;y
         sta 1596
         lda #15        ;o
         sta 1597
         lda #21        ;u
         sta 1598
         lda #23        ;w
         sta 1599
         lda #15        ;o
         sta 1600
         lda #14        ;n
         sta 1601
         lda #33        ;!
         sta 1602
         lda #25        ;y
         sta 1603
         lda #15        ;o
         sta 1604
         lda #21        ;u
         sta 1605
         lda #23        ;w
         sta 1606
         lda #15        ;o
         sta 1607
         lda #14        ;n
         sta 1608
         lda #33        ;!
         sta 1609
   
;sides
;        left side       
         lda #15        ;o
         sta 1356
         lda #21        ;u
         sta 1396
         lda #23        ;w
         sta 1436
         lda #15        ;o
         sta 1476

         ;write you won! - across the screen
         lda #25        ;y
         sta 1439
         lda #15        ;o
         sta 1440
         lda #21        ;u
         sta 1441
         lda #23        ;w
         sta 1443
         lda #15        ;o
         sta 1444
         lda #14        ;n
         sta 1445
         lda #33        ;!
         sta 1446

         lda #25        ;y
         sta 1479
         lda #15        ;o
         sta 1480
         lda #21        ;u
         sta 1481
         lda #23        ;w
         sta 1483
         lda #15        ;o
         sta 1484
         lda #14        ;n
         sta 1485
         lda #33        ;!
         sta 1486

;       cont with rest of left side
         lda #14        ;n
         sta 1516
         lda #33        ;!
         sta 1556

;        right side 
         lda #25        ;y
         sta 1369      
         lda #15        ;o
         sta 1409
         lda #21        ;u
         sta 1449
         lda #23        ;w
         sta 1489
         lda #15        ;o
         sta 1529
         lda #14        ;n
         sta 1569
 ;        lda #33        ;!
 ;        sta 1609

        ldx #0
colourloop2
        stx 55751
        stx 55752
        stx 55753
        stx 55755
        stx 55756
        stx 55757
        stx 55758
        stx 55759

        jsr waitawhile
        inx
        cpx #20
        bne colourloop2
        
        ;reset wait delay to default
        lda #190
        sta 53015     ;set wait delay
        ;ask user if wishes to play again 
        ;play again? y/n"     
        lda #14            ;n    
        sta 1958
        lda #5             ;e
        sta 1959
        lda #24            ;x
        sta 1960
        lda #20            ;t
        sta 1961
        lda #12            ;l
        sta 1963
        lda #5             ;e     
        sta 1964
        lda #22            ;v
        sta 1965
        lda #5             ;e             
        sta 1966
        lda #12            ;l             
        sta 1967
        lda #25            ;y
        sta 1969
        lda #47            ;/
        sta 1970
        lda #14            ;n
        sta 1971

        lda #0
        sta 198   ;clear keyboard buffer 
      
keywait2
         jsr $ff9f      ;scan key 
         jsr $ffe4      ;get keypress
         cmp #0
         beq keywait2  

         cmp #78        ;n?
         beq leavegame2
         jmp maininit      ;start game from next level         
         rts

leavegame2
         rts
         brk


;Created 26/05/2021 By Roger Williams
;
;Waits till raster line 251 then shows letter on screen
;Also has fancy delay loop to slow things down
;
waitawhile
;rasterwait
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
        cmp 53015               ;120 is about 1.5 seconds
        bne delay1
        clc
        rts        


;**************************end of other routines***************************


exitgame
    rts
    brk


;
;vars - text for instructions screen
;NOTE: need to add 0 at end of any string
;
strinst1 text "instructions",0                         ;<-40 char screen limit
strinst2 text "navigate the high seas hunting for subs",0
strinst3 text "by using keys:",0
strinst4 text "a - move left  d - move right",0
strinst5 text "space bar - drop depth charge",0
strinst6 text "x - exit game",0
strinst7 text "you only have a fixed amount of attempts",0
strinst8 text "to sink the sub",0
strinst9 text "can you complete all 3 levels?",0
strinst10 text "press a key to start",0

;strings for the gameover - you completed the game screen
instrgameover text "game over!",0
instrgameoverexit text "thank you for playing :)",0
