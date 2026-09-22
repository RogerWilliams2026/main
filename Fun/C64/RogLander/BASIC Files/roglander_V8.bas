!--------------------------------------------------
!- Created   : 15 november 2020 14:42:08
!- By        : Roger Williams
!- import of : roglander.prg
!- from disk : e:\roglander_disk.d64
!- Platform  : commodore 64
!- Version   : 8
!- Modified  : 16/08/2024 By Roger Williams
!--------------------------------------------------
0 rem roglander v8!
1 rem changes this version
2 rem ====================
3 rem clears bonus character when collected
4 rem flashes sprite on collision
5 rem addition of : press space to exit text
6 rem using char 160 to remove words: bonus
13 rem ====================
14 rem machine code for background memory location- 49152
15 sa = 49152
16 for n = 0 to 138
17 read a : poke sa+n,a: next n
18 data 32,68,229,162,0,160,0,169
19 data 224,157,0,4,152,157,0,216
20 data 24,232,224,200,208,241,162,0
21 data 160,0,169,224,157,80,4,152
22 data 157,80,216,24,232,224,240,208
23 data 241,162,0,160,0,169,224,157
24 data 64,5,152,157,64,217,24,232
25 data 224,80,208,241,162,0,160,5
26 data 169,224,157,104,5,152,157,144
27 data 217,24,232,224,240,208,241,162
28 data 0,160,5,169,224,157,224,5
29 data 152,157,8,218,24,232,224,240
30 data 208,241,162,0,160,5,169,224
31 data 157,208,6,152,157,248,218,24
32 data 232,224,240,208,241,162,0,160
33 data 5,169,224,157,192,7,152,157
34 data 218,218,24,232,224,40,208,241
35 data 24,96,0
40 gosub 3200:rem show intro
45 lv=1:rem current game map level - incremented if player wins
47 d$="press space to exit":w=1063:p=len(d$):h=asc(mid$(d$,p,1))
49 rem clear screen draw level
55 print chr$(147):poke 53280,0
59 rem x1/y1 bits for movement cp=craft screen pos sc=score sp=score pos
60 rem bn = bonus score for hitting chr 81 set to 150 cl used for flashing col
65 x=100:y=50:sc=0:sp=1031:bn=150:v=53248:cp=1114:x1=0:y1=0:cl=0:r=0:g=1
69 rem draw player sprite
70 if lv=1then gosub 620:rem if level >1 dont redraw the sprite
75 poke v,x  :poke v+1,y :poke v+39,1:poke v+21,1:rem set sprite x/y and colour
79 rem draw level
80 gosub 920
84 rem update score
85 gosub 600
89 rem main loop
90 rem show press space to exit
91 poke 781,.:poke 782,21:poke 783,.:sys65520:printd$;
97 gosub 3700
99 geta$
100 if a$="w" then y=y-8
101 if a$="w" then y1=-1
110 if a$="s" then y=y+8
111 if a$="s" then y1=1
120 if a$="a" then x=x-8
121 if a$="a" then x1=-1
130 if a$="d" then x=x+8
131 if a$="d" then x1=1
140 if a$=" " then end
145 if a$="w" then gosub 3500
146 if a$="s" then gosub 3400
147 if a$="a" then gosub 3400
148 if a$="d" then gosub 3400
150 if x<20then x=20
160 if x>320 thenx=320
170 if y<40 theny=40
180 if y>230 then y=230
185 rem process movement
186 if x1<>0 then cp=cp+x1
187 if y1=-1 then cp=cp-40
188 if y1=1 then cp=cp+40
190 pokev+1,y
191 if x<255 then poke v+16,0
192 if x>255then poke v+16,1
193 if x<255 then pokev,x
194 if x>255 then pokev,x-255
205 rem collision detection
210 y1=(y-50)/8 :rem get screen y pos
220 x1=(x-50)/8 :rem get screen x pos
230 z=1024+(y1*40)+x1:z=z+80
240 gosub 3100
297 x1=0:y1=0
300 goto 90
305 rem winner!
310 sc=sc+100:lv=lv+1:if bn<>0 then sc=sc+bn:rem inc level number update score
325 rem land craft
335 rem clear previous position and play sound
340 poke 54272+co,6
350 poke cp-40,94
360 x=1:cl=0:bc=55589:sc=sc+100:lv=lv+1
365 rem show win text
370 poke 1318,23:poke1319,9 :poke1320,14:poke1321,14:poke1322,5:poke1323,18
371 for a=1 to 40
372 poke bc+x,cl:cl=cl+1:x=x+1
373 if cl>16 then cl=0
374 if x>6 then x=1
375 if x>6 then bc=5589
376 next a
377 for a=1to5:poke 55589+a,6:next a
378 gosub 600:rem update score
379 goto 55:rem start next level
389 rem play again subroutine called by crash
390 input "play again? (y/n>";a$
391 if a$ = "y" then goto 40
392 printchr$(147):print"bye!":poke53280,14:rem reset border colour
393 rem turn off sprite end game
394 poke v+21,0:pokev+39,0:end
395 rem loser!
400 poke781,12:poke782,12:poke783,.:sys65520
405 printchr$(5);"you";chr$(5);" crashed!"
410 cl=0
420 for a=0 to 10:poke53280,cl
430 cl=cl+1:if cl>16 then cl=0
435 rem flash text
440 poke 55788,2:poke55789,2:poke55790,2:poke55791,2:poke55792,2:poke55793,2
450 poke 55794,2:poke55795,2:poke55796,2:poke55797,2:poke55798,2:poke55799,2
451 poke 55788,3:poke55789,3:poke55790,3:poke55791,3:poke55792,3:poke55793,3
452 poke 55794,3:poke55795,3:poke55796,3:poke55797,3:poke55798,3:poke55799,3
453 poke 55788,8:poke55789,8:poke55790,8:poke55791,8:poke55792,8:poke55793,8
454 poke 55794,8:poke55795,8:poke55796,8:poke55797,8:poke55798,8:poke55799,8
455 poke55788,14:poke55789,14:poke55790,14:poke55791,14:poke55792,14
456 poke55793,14:poke 55794,14:poke55795,14:poke55796,14
457 poke55797,14:poke55798,14:poke55799,14
460 poke v+21,a:rem flash sprite colours
470 next a
480 goto 389
495 rem show bonus
500 z=1:cl=0:bc=55589:sc=sc+bn
505 rem poke words: bonus onto screen
510 poke 1318,2:poke1319,15:poke1320,14:poke1321,21:poke1322,19
520 for a=1 to 40
525 rem flash colours
530 poke bc+z,cl:cl=cl+1:z=z+1
540 if cl>16 then cl=0
550 if z>5 then z=1
560 if z>5 then bc=55589
570 next a
572 rem clear text and set background colour where text was to black
573 poke 55590,0:poke 55591,0:poke 55592,0: poke 55593,0:poke 55594,0
574 poke 1318,160:poke1319,160:poke1320,160:poke1321,160:poke1322,160
580 poke cp,224:poke cp+54272,5:rem remove bonus character reset background col
583 rem adjust player y pos to stop endless bonus loop
584 y=y-8:y1=y1-1:poke v+1,y:cp=cp-40
585 gosub 600
590 return
595 rem update score
600 poke 781,.:poke 782,.:poke 783,.:sys65520:printchr$(158);"score:";chr$(5);sc
610 return
615 rem draw player sprite
620 for m=896to958:read d:pokem,d:nextm
630 poke 2040,14
640 rem poke v,x  :poke v+1,y :poke v+39,1:poke v+21,1
690 return
695 rem sprite data
700 data0,255,0
710 data0,255,0
720 data0,195,0
730 data0,195,0
740 data0,255,0
750 data0,255,0
760 data3,255,192
770 data3,255,192
780 data31,255,248
790 data25,187,152
800 data25,131,152
810 data25,175,152
820 data31,183,248
830 data29,183,184
840 data13,255,240
850 data 3,255, 192
860 data 0,195, 0
870 data 0,195, 0
880 data 0,195, 0
890 data 0,195, 0
910 data 0,195, 0
915 rem clear screen - draw horizon
919 rem for a=1024 to 1503:poke 54272+a,0:pokea,32+128:next a:printchr$(149);
920 sys 49152
935 rem draw screen
940 for a=1to40
950 read m,c:poke m,c+128
955 poke 53281,2:rem set background to brown
960 rem whatever the background colour is (53280) the map lines are brown
980 next a
990 return
995 rem level1 map
1000 data 1984,100,1985,100,1986,100,1987,78,2005,77,2006,78,2007,77,2009,78
1005 data 2008,100
1010 data 2014,102,2015,78,2012,77,2013,102,2018,77,2019,78,2022,77,2023,78
1015 rem next line above
1020 data 1948,78,1964,77,1970,78,1971,77,1976,78,1977,77,1980,78,1981,77
1025 rem next line above
1030 data 1909,78,1923,77
1035 rem next line above
1040 data 1870,78,1871,77,1872,100,1873,100,1874,78,1882,77
1045 rem next line above
1050 data 1835,78,1841,77
1055 rem next line above
1060 data 1796,78,1800,77
1065 rem next line above
1070 data 1757,78,1759,77
1075 rem next line above
1080 data 1718,81
1085 rem level 2
1090 data 1624,77,1665,77,1680,78,1681,77
1095 rem next line down
1100 data 1706,77,1707,100,1708,100,1719,78,1722,77
1115 rem next line down
1120 data 1749,77,1755,100,1756,100,1757,100,1758,78,1763,77
1125 rem next line down
1130 data 1790,77,1794,78,1804,77,1810,78,1811,77
1135 rem next line down
1140 data 1831,77,1833,78,1845,77,1849,78,1852,77                      
1145 rem next line down
1150 data 1872,81,1886,77,1887,100,1888,78,1893,77,1900,100,1901,100,1902,100
1153 data 1903,100                   
1155 rem next line down
1160 data 1934,77,1939,78
1165 rem next line down
1170 data 1975,77,1978,78
1175 rem next line down
1180 data 2016,102,2017,102
3095 rem collision detection
3100 if peek(v+31) =0 then if peek(v+30)=0 then return:rem no collision
3115 rem landed!
3120 if peek(cp) = 102+128 then goto 310
3125 rem show bonus award
3130 if peek(cp) = 81+128 then gosub 500
3135 rem player crashed into the scenary
3140 if peek(cp) = 77+128or peek(cp)=781+128then goto 400
3150 if peek(cp) =100+128 then goto 400
3190 return
3195 rem intro screen
3200 print"{cyan}{clear}"
3205 poke 53280,6:poke 53281,6:rem set background/border to blue
3210 print"{space*2}{reverse on}{space*4}{reverse off} {reverse on}{space*4}{reverse off} {reverse on}{space*4}{reverse off}"
3220 print"{space*2}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}"
3230 print"{space*2}{reverse on}{space*4}{reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}"
3240 print"{space*2}{reverse on}{space*2}{reverse off}{space*3}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off} {reverse on}{space*2}{reverse off}"
3250 print"{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off}"
3260 print"{space*2}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on}{space*4}{reverse off} {reverse on}{space*4}{reverse off}"
3265 rem print lander in block spaces
3268 print"{yellow}"
3270 print"{space*8}{reverse on} {reverse off}{space*4}{reverse on}{space*4}{reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on}{space*3}{reverse off}{space*2}{reverse on}{space*4}{reverse off} {reverse on}{space*4}{reverse off}"
3280 print"{space*8}{reverse on} {reverse off}{space*4}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*4}{reverse on} {reverse off}{space*2}{reverse on} {reverse off}"
3290 print"{space*8}{reverse on} {reverse off}{space*4}{reverse on}{space*4}{reverse off} {reverse on}{space*2}{reverse off} {reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on}{space*4}{reverse off} {reverse on}{space*4}{reverse off}"
3300 print"{space*8}{reverse on} {reverse off}{space*4}{reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off} {reverse on}{space*2}{reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*4}{reverse on}{space*2}{reverse off}"
3310 print"{space*8}{reverse on}{space*4}{reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on} {reverse off}{space*2}{reverse on} {reverse off} {reverse on}{space*3}{reverse off} {reverse off} {reverse on}{space*4}{reverse off} {reverse on} {reverse off} {reverse on} {reverse off}"
3320 print"{space*33}{reverse on} {reverse off}{space*2}{reverse on} {reverse off}{light blue}"
3330 print:print:print:print:print"press a key"
3340 geta$:if a$=""then 3340
3350 return
3395 rem sound for moving
3400 s=54272:forl=stos+24:pokel,0:next
3420 poke s+24,15
3430 pokes+5,190
3440 pokes+6,248
3445 pokes+7,248:rem sus/rel
3450 pokes+1,16:pokes,195
3460 pokes+4,129
3470 fort=1to250:next:rem quarter note
3480 poke s+4,16:rem turn off note
3490 return
3495 rem sound for upwards
3500 s=54272:rem forl=stos+24:pokel,0:next
3520 pokes+24,15
3530 pokes+5,120
3540 pokes+6,248
3545 pokes+7,255:rem sus/rel
3550 pokes+1,16:pokes,195
3560 pokes+4,129
3570 fort=1to63 :next:rem eighth note
3580 poke s+4,16:rem turn off note
3590 pokes+5,120
3600 pokes+6,248
3610 pokes+7,255:rem sus/rel
3620 pokes+1,15:pokes,210
3630 pokes+4,129
3640 fort=1to250:next:rem quarter note
3650 poke s+4,16:rem turn off note
3660 return
3699 rem flash: press space to exit max left pos 1044 right col 1063
3700 for z=1 to len(d$)
3710 poke 55316+z,g
3720 next z
3730 r=r+1
3740 if r> len(d$) then r=0
3750 for q=1 to len(d$)
3760 poke 1044+q,32
3770 next q
3780 p=len(d$)
3790 return