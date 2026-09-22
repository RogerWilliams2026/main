;Created 10/05/2021 by Roger Williams
;
;RogSunk Intro!
;
;Draws text: ROG SUNK
;
;Second program Intro2 handles colour
;

*=$c670  ;50800

start
     lda #0
     ldx #0
     lda #6
     sta 53280  ;set border to blue 
     lda #160

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

exit
    rts
    brk







