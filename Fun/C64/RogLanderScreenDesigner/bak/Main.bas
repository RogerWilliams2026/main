!--------------------------------------------------
!- 07 october 2021 By Roger Williams
!- 
!- generic screen designer  
!-
!--------------------------------------------------
0 printchr$(147);
10 rv$=chr$(18):wh$=chr$(5):bl$=chr$(154)
19 rem st: used by level designer navigation sc: to create data statements code
20 st%=1624:sc%=1624
29 rem save to data statements arrays
30 dim s1(400):dim s2(400)
40 c=32:rd%=0:b=.:l$="":ln=1:lv=1:a1=0
99 rem start of code
100 printwh$;"key list:";bl$:print
110 printwh$;"1";bl$;" - ";chr$(78+32):printwh$;"2";bl$;" - ";chr$(77+32)
120 printwh$;"3";bl$;" - ";chr$(102+64):printwh$;"4";bl$;" - ";chr$(111+64)
130 printwh$;"5";bl$;" - ";chr$(81+32):printwh$;"6";bl$;" - (erase)"
140 printwh$;"s";bl$;" - save":printwh$;"x";bl$;" - exit":print
150 print "use cursor keys for movement"
170 rem draw horizon
180 for a=1 to 40:poke 1583+a,121:next a
190 poke 781,14:poke782,16:poke783,.:sys65520:print "horizon"
199 rem main loop
200 poke st%,c:c=.
210 sys 65439:sys 65508:rem geta$:c=.
220 if peek(780)=17 then st%=st%+40:rem down
230 if peek(780)=145 then st%=st%-40:rem up
240 if peek(780)=29 then st%=st%+1:rem right
250 if peek(780)=157 then st%=st%-1:rem left
260 if peek(780)=49 then c=78
270 if peek(780)=50 then c=77
280 if peek(780)=51 then c=102
290 if peek(780)=52 then c=121
300 if peek(780)=53 then c=81
310 if peek(780)=54 then c=32
330 if peek(780)=83 goto 500:rem save
340 if peek(780)=88 then end
350 if st%<1624 then st%=1624
360 if st%>2023 then st%=2023
370 if c >. then poke st%,c
380 c=peek(st%) 
390 poke st%,160
400 for a=1to150:next a
410 goto 200
499 rem now creates an array from the screen character memory
500 poke 781,8:poke782,12:poke783,.:sys65520:print wht$;"saving screen data";bl$
510 for a= sc% to 2023
520 rd%= peek(a)
530 if rd%<> 32 then s1(a1)=a:s2(a1)=rd%:a1=a1+1
540 next a
999 rem create data statements
1000 print chr$(147):a=0:b=1
1010 input "enter start line number: ";ln
1020 input "enter level number: ";lv
1030 l$=mid$(str$(ln),2,len(str$(ln)))+" rem level number: "+str$(lv)
1040 print chr$(147);l$::ln=ln+10
1050 a=a+1:rem loop until = array size (400)
1060 if b=1 then l$=str$(ln)+" data ":l$=mid$(l$,2,len(l$))
1080 if b<7 then l$=l$+mid$(str$(s1(a)),2,len(str$(s1(a))))+",":b=b+1
1090 if b<7 then l$=l$+mid$(str$(s2(a)),2,len(str$(s2(a))))+",":b=b+1
1100 if b=7 then ln=ln+10:print mid$(l$,1,len(l$)-1):l$="":b=1
1140 if a<> a1 then goto 1050
1159 rem move cursor to each line and create them via enter key
1160 print:print"press enter on each line to create them!"
1170 rem poke631,19:poke632,13:poke633,13:rem home and two returns
1180 rem poke631,13:poke632,13:poke633,13:rem three returns
1190 rem poke631,13:poke632,13:poke633,13:rem three returns
1200 rem poke 198,9:rem process keystrokes