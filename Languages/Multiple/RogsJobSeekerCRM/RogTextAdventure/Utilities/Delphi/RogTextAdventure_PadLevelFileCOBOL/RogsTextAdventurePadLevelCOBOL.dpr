program RogsTextAdventurePadLevelCOBOL;

{$APPTYPE CONSOLE}

{$R *.res}
{
 Created 01/08/2024 By Roger Williams

 adjusts the level1.txt file in:

 C:\projects\COBOL\Projects\RogTextAdventureCOBOL

 so the file is padded to 80 characters per line as per COBOL console
 restrictions


}
uses
  System.SysUtils;

procedure AdjustLevelFile;
{
 Created 01/08/2024 By Roger Williams

 adjusts the level1.txt file

 pads each numeric line with spaces till line is 80 chars long
 for strings cuts the string if it exceeds 80 chars at the closets SPACE
 to the column width

}
var
  filLevelIn  : TextFile;
  filLevelOut : TextFile;
  strData     : string;
  strTemp     : string;
  intNum      : integer;

begin
  AssignFile(filLevelIn,'C:\projects\COBOL\Projects\RogTextAdventureCOBOL\level1.txt');
  AssignFile(filLevelOut,'C:\projects\COBOL\Projects\RogTextAdventureCOBOL\level1_COBOL.txt');

  Reset(filLevelIn);
  ReWrite(filLevelOut);

  while not eof(filLevelIn) do
  begin
   readln(filLevelIn,strData);

   if length(strData) <= 80 then
   begin
     //write line to new file
     strData:=strData+StringOfChar(' ',80-length(strData));
     writeln(filLevelOut,strData);
   end
  else
   begin
     //split line so not more than 80 chars

     for intNum := 1 to 120 do
     begin
       if strData[intNum] = ' ' then
         if intNum >70 then
         begin
           //split string at point
           strTemp:=strData.Substring(0,intNum-1);
           writeln(filLevelOut,strTemp);
           strTemp:=strData.Substring(intNum,length(strData)-intNum);
           writeln(filLevelOut,strTemp);
           break;
         end;
     end;
   end;
  end;

  CloseFile(filLevelIn);
  CloseFile(filLevelOut);
end;

begin
  try
   AdjustLevelFile;
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;
end.
