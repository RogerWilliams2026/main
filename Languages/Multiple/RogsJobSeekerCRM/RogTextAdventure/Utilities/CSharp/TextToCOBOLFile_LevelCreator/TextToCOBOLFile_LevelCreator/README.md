# <u>Text To COBOL Level Creator</u>     
     
Creates level file data for the COBOL version of Rog's Text Adventure
Each level is comprised of a number of rooms

**NOTE:** this is a CREATOR not an editor, this is Phase1 so no need to be reinventing the wheel
    (just yet). This is for internal use not public domain. Hence no data validation, as I
    am not going to create a level for my game with incorrect data in it!
    Phase2 would be a full featured EDITOR.
    

**COBOL format is fixed length so:**

number  pic 99
message pic x(20)

10
hello world!

would be stored as:

10hello world!        <- plus spaces at end to make a text string of 20 chars

**Encoding Rules**

if number is less than 10 would need 0 added  
if string is less than 20 chars would need padding to be 20 chars long

The COBOL room data for each level is stored thus (taken from the COBOL source file):

01 REC_ROOM_INTERNAL.  
        03 REC_ROOM OCCURS 40 TIMES.  
            05 INT_ROOMID PIC 99 VALUE ZEROES.  
    *    'next 4 propoerties determine which room this one leads to 0=no room!  
            05 INT_NEXTROOMNORTH PIC 99 VALUE ZEROES.  
            05 INT_NEXTROOMSOUTH PIC 99 VALUE ZEROES.  
            05 INT_NEXTROOMEAST PIC 99 VALUE ZEROES.  
            05 INT_NEXTROOMWEST PIC 99 VALUE ZEROES.  
    *'used for text to describe room to player  
            05 STR_DESC_INT1 PIC X(80) VALUE SPACES. 
            05 STR_DESC_INT2 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT3 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT4 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT5 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT6 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT7 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT8 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT9 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT10 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT11 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT12 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT13 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT14 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT15 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT16 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT17 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT18 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT19 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT20 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT21 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT22 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT23 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT24 PIC X(80) VALUE SPACES.  
            05 STR_DESC_INT25 PIC X(80) VALUE SPACES.  


So each data field needs to be filled with 0's/spaces to meet the fixed record length requirements