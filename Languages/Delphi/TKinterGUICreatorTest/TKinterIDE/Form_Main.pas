unit Form_Main;

{
  Created 06/05/2026 By Roger Williams

  test idea for an IDE to create basic Tkinter Python GUI files

  98% is the RogerBovver level editor code!

}

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms,
  Vcl.Menus, Vcl.ExtCtrls, Vcl.ComCtrls, Vcl.StdCtrls
  ,clsTkinterFormControls, Form_Properties, Math, Vcl.Dialogs, System.DateUtils,
  System.StrUtils;

type
  //for showing image during drag/drop - copied from StackOverFlow and fettled
  TclsDragObject = class(TDragControlObject)
    private
      CilstImageList:TImageList;
      CctlDragSource:TControl;
    protected
      function GetDragImages: TDragImageList; override;
    public
      Procedure StartDrag(graGraphic:TGraphic;pntPoint:TPoint;ctlDragSource:TControl);

      Constructor Create(ctlControl: TControl); override;
      Destructor Destroy;override;
      Property DragSource:TControl read CctlDragSource;
    end;


  TfrmMain = class(TForm)
    MNUMain: TMainMenu;
    MNUFile: TMenuItem;
    MMNUOpen: TMenuItem;
    MNUSave: TMenuItem;
    MNUNew: TMenuItem;
    N1: TMenuItem;
    MNUExit: TMenuItem;
    TMRFlashSelected: TTimer;
    STBStatus: TStatusBar;
    PANElements: TPanel;
    IMGButtonE: TImage;
    IMGLabelE: TImage;
    IMGEditBoxE: TImage;
    IMGCheckBoxE: TImage;
    IMGRadioButtonE: TImage;
    IMGListBoxE: TImage;
    IMGComboBoxE: TImage;
    IMGScrollBarE: TImage;
    IMGPanelE: TImage;
    IMGGroupBoxE: TImage;
    IMGTreeViewE: TImage;
    IMGListViewE: TImage;
    IMGFormE: TImage;
    PANProperties: TPanel;
    IMGSpinEditE: TImage;
    Edit1: TEdit;
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure MNUExitClick(Sender: TObject);
    procedure TMRFlashSelectedTimer(Sender: TObject);
    procedure PANElementsDragDrop(Sender, Source: TObject; X, Y: Integer);
    procedure FormMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure FormMouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
    procedure FormMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure FormDragOver(Sender, Source: TObject; X, Y: Integer;
      State: TDragState; var Accept: Boolean);
    procedure FormDragDrop(Sender, Source: TObject; X, Y: Integer);
    procedure FormDestroy(Sender: TObject);
    procedure MNUNewClick(Sender: TObject);
    procedure FormKeyDown(Sender: TObject; var Key: Word; Shift: TShiftState);
    procedure MNUSaveClick(Sender: TObject);

  private
    { Private declarations }
  type
    TaryProperties = array of array of string; //populated and passed when properties requested

  const
    CNST_INT_IDEWIDTH = 1600;
    CNST_INT_IDEHEIGHT = 780;

  var
    frmProps             : TfrmProperties;
    PANForm              : TPanel;
    strSelected          : string;
    rctSelRect           : TRect;
    clrFirstPixel        : TColor;
    
    blnImageMoving       : boolean;
    blnHasForm           : boolean;
    blnSelecting         : boolean;
//    blnResizing          : boolean;
//    blnMouseAtImageEdge  : boolean;
    blnFormSelected      : boolean;
    IMGSelected          : TImage;
    IMGTemp              : TImage;
    IMGAdded             : TImage;
    intImageMovingX      : integer;
    intImageMovingY      : integer;
    intCurX              : integer;
    intCurY              : integer;
    intSelImagesPos      : integer;
    intSelImageType      : integer;
    intSelX              : Integer;
    intSelY              : Integer;
    intLastX             : integer;
    intLastY             : integer;
    intFormWidth         : integer;
    intSelectedIndex     : integer;
    //used for user added controls
    intButtonNbr         : integer;
    intLabelNbr          : integer;
    intEditNbr           : integer;
    intCheckBoxNbr       : integer;
    intRadioButtonNbr    : integer;
    intListBoxNbr        : integer;
    intComboBoxNbr       : integer;
    intScrollBarNbr      : integer;
    intPanelNbr          : integer;
    intGroupBoxNbr       : integer;
    intTreeViewNbr       : integer;
    intListViewNbr       : integer;
    intSpinEditNbr       : integer;

    //stores multi selected images
    arySelImagesImage    : array of TImage;
     //for showing image during drag/drop
    clsDragObject        : TclsDragObject;
    //below used for accessing properties for controls
    clsFormControl       : TclsFormControl;
    aryProperties        : TaryProperties;  //array of array of string;
    //for manual mouse move of PANForm
    blnDragging          : boolean;
    intPANFormX          : integer;
    intPANFormY          : integer;

    //click event for dragged images
    procedure ImageClick(Sender: TObject);
    //mouse events for dragging images around form
    procedure ImageMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure ImageMouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
    procedure ImageMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    //for showing image during drag/drop
    procedure ImageStartDrag(Sender: TObject; var DragObject: TDragObject);
    //for selected images
    procedure ImageDblClick(Sender: TObject);
    procedure DrawImageSelectionBox(IMGWhat : TImage);
    procedure RemoveImageSelectionBox(intWhat : integer;IMGWhat : TImage);
    procedure RestoreSelectedImages;
    procedure ResetELementNumbers;

    procedure Init;
    procedure CreateImage(IMGSource : TImage; intX : integer; intY : integer);
    procedure DeleteImage(strName : string);
    procedure CopyImage;
    procedure PasteImage;
    procedure SavePython;
    procedure LoadPython;
    procedure ClearIDE;
    procedure ClearSelectionArray;
    procedure FindImagesInSelectionBox;
    //PANForm events - Tkinter Window
    procedure PANForm_DragDrop(Sender, Source: TObject; X, Y: Integer);
    procedure PANForm_DragOver(Sender, Source: TObject; X, Y: Integer;
      State: TDragState; var Accept: Boolean);
    //for moving PANForm around the IDE with the mouse
    procedure PANForm_MouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure PANForm_MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
    procedure PANForm_MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure PANForm_Click(Sender: TObject);

  public
    { Public declarations }
  end;


var
  frmMain                 : TfrmMain;
  //mode button captions - have to be declared here as Delphi won't allow it in the class vars!
  aryModes                : array [1..2] of string = ('Mode - Drag Control','Mode - Select Control');
//  aryProperties           : TaryProperties;

implementation
//drag/drop image show subs/funcs
constructor TclsDragObject.Create(ctlControl: TControl);
begin
  inherited;
  CilstImageList:=TImageList.Create(nil);
end;

destructor TclsDragObject.Destroy;
begin
  CilstImageList.Free;
  inherited;
end;

function TclsDragObject.GetDragImages: TDragImageList;
begin
   Result := CilstImageList;
end;

{$R *.dfm}

procedure TclsDragObject.StartDrag(graGraphic: TGraphic;pntPoint:TPoint;ctlDragSource:TControl);
var
 bmpTemp:TBitMap;

begin
CctlDragSource := ctlDragSource;
bmpTemp:=TBitMap.Create;

  try
    CilstImageList.Width := graGraphic.Width;
    CilstImageList.Height := graGraphic.Height;
    bmpTemp.Width := graGraphic.Width;
    bmpTemp.Height := graGraphic.Height;
    bmpTemp.Canvas.Draw(0,0,graGraphic);
    CilstImageList.Add(bmpTemp,nil);
  finally
    bmpTemp.Free;
  end;

 CilstImageList.SetDragImage(0,pntPoint.x,pntPoint.y)
end;

procedure TfrmMain.ImageStartDrag(Sender: TObject; var DragObject: TDragObject);
{
  Created 26/04/2026 By Roger Williams

  copied from stackoverflow and modified

}
var
 pntpoint:TPoint;

begin
    //set these to zero in original was mouse pos now top left of selected image
    pntPoint.X:=0;
    pntPoint.Y:=0;

    if Assigned(clsDragObject) then clsDragObject.Free;

    clsDragObject := TclsDragObject.Create(TImage(Sender));
    clsDragObject.StartDrag(TImage(Sender).Picture.Graphic,pntPoint,TImage(Sender));
    DragObject := clsDragObject;
end;


procedure TfrmMain.ImageMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
   blnImageMoving:=true;
   intImageMovingX:=X;
   intImageMovingY:=Y;
end;

procedure TfrmMain.ImageMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
{
  Created 28/04/2026 By Roger Williams

  if an image selected moves it
  shows current X/Y pos in status bar

}
var
 pntTemp : TPoint;
begin

 if blnImageMoving then
 begin
   TImage(Sender).Left:=(TImage(Sender).Left - intImageMovingX) + X;
   TImage(Sender).Top:=(TImage(Sender).Top - intImageMovingY) + Y;

   //update properties form with X/Y changes
   if clsControls.clsCurrentFormControl <> nil then
   begin
     clsControls.clsCurrentFormControl.Left:=TImage(Sender).Left;
     clsControls.clsCurrentFormControl.Top:=TImage(Sender).Top;
     frmProps.UpdateForm;
   end;
 end;

 GetCursorPos(pntTemp);
 intCurX:=pntTemp.X;
 intCurY:=pntTemp.Y;

 //check if mouse pointer near edge of image
// if InRange(intCurX, TImage(Sender).Left, TImage(Sender).Left + TImage(Sender).Width) then
//    if InRange(intCurY, TImage(Sender).Top, TImage(Sender).Top + 40) then
//       blnMouseAtImageEdge:=true;



 self.STBStatus.Panels[3].Text:='Col: ' + IntToStr(intCurX) + ' Row: ' + IntToStr(intCurY);
end;

procedure TfrmMain.ImageMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  blnImageMoving:=false;
end;


procedure TfrmMain.ImageDblClick(Sender: TObject);
{
  Created 28/04/2026 By Roger Williams

  shows properties for image 'control'

}

begin
//  if clsControls <> nil then
//  begin
//     lstProperties:=clsControls.ReturnPropertiesByImageName(strSelected);
//     clsControls.SetControl(clsFormControl);
//     frmProps.Show(lstProperties, clsFormControl.intIndex);
//  end;
end;

procedure TfrmMain.ImageClick(Sender: TObject);
{
  Created 28/04/2026 By Roger Williams

  captures selected image so used can move it about

}

begin
  //set image to selected
  IMGSelected.Free;
  IMGSelected:=TImage.Create(self);
  IMGSelected.AutoSize:=true;
  IMGSelected.Name:='IMGSelected';
  IMGSelected.Left:=TImage(Sender).Left;
  IMGSelected.Top:=TImage(Sender).Top;
  IMGSelected.Picture.Assign(TImage(Sender).Picture);
  strSelected:=TImage(Sender).Name;
  IMGSelected.Tag:=TImage(Sender).Tag;
  //update selected control class in clsControls
  clsFormControl:=clsControls.GetControl(clsControls.GetIndexByImageName(strSelected));
  intSelectedIndex:= clsControls.clsCurrentFormControl.intComponentIndex;
  //show properties
  clsControls.SetControl(clsFormControl);
  clsControls.ReturnProperties(clsControls.clsCurrentFormControl.intIndex);
  frmProps.Show(clsFormControl.intIndex);
  //show name in statusbar
  self.STBStatus.Panels[2].Text:='Selected Image: ' + TImage(Sender).Name;
end;

procedure TfrmMain.PANElementsDragDrop(Sender, Source: TObject; X, Y: Integer);
begin
  if clsDragObject.DragSource is TImage then
    TImage(clsDragObject.DragSource).Parent := TPanel(Sender);
end;

procedure TfrmMain.PANForm_DragDrop(Sender, Source: TObject; X, Y: Integer);
begin
  if clsDragObject.DragSource is TImage then
   // TImage(clsDragObject.DragSource).Parent := TPanel(Sender);
     CreateImage(TImage(clsDragObject.CctlDragSource),X,Y);
end;

procedure TfrmMain.PANForm_DragOver(Sender: TObject; Source: TObject; X: Integer; Y: Integer; State: TDragState; var Accept: Boolean);
begin
//set to not accept as default
//Accept:=false;

if clsDragObject.CctlDragSource is TImage then
begin
   if (clsDragObject.CctlDragSource.Name = 'IMGFormE') and (blnHasForm) then
      Accept:=false
   else
      Accept:=True;
end;
end;

procedure TfrmMain.PANForm_MouseDown(Sender: TObject; Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
begin
  blnDragging := true;
  intPANFormX:=X;
  intPANFormY:=Y;
end;

procedure TfrmMain.PANForm_MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
begin
  if blnDragging then
  begin
    self.PANForm.Left := (self.PANForm.Left - intPANFormX) + X;
    self.PANForm.Top :=(self.PANForm.Top - intPANFormY) + Y;
  end;
end;

procedure TfrmMain.PANForm_MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
begin
     blnDragging := false;
end;

procedure TfrmMain.PANForm_Click(Sender: TObject);
begin
  //update selected control class in clsControls
  clsFormControl:=clsControls.GetControl(1);
  intSelectedIndex:= 1;
  //show properties
  clsControls.SetControl(clsFormControl);
  strSelected:=clsControls.clsCurrentFormControl.Name;
  clsControls.ReturnProperties(clsControls.clsCurrentFormControl.intIndex);
  frmProps.Show(clsFormControl.intIndex);
end;

procedure TfrmMain.ClearSelectionArray;
begin
 Setlength(arySelImagesImage,1);
 intSelImagesPos:=1;
end;

procedure TfrmMain.FindImagesInSelectionBox;
{
  Created 28/04/2026 By Roger Williams

  looks for any images inside rctSelRect
  if all same type add to selected array

  gets top left pixel colour of first image
  this is used after multi select in TMRFlashSelected

}
var
 intNum   : integer;
 ctlTemp  : TControl;
 strTemp  : string;

 rctTemp:TRect;

begin
intSelImagesPos := 1;
ClearSelectionArray;

for intNum := 0 to self.ControlCount -1 do
begin
  ctlTemp := self.Controls[intNum];

  // Check if the control's bounding rectangle intersects with SearchRect
  if IntersectRect(rctTemp, ctlTemp.BoundsRect, rctSelRect) then
  begin
    if intSelImagesPos = 1 then
       intSelImageType:=TImage(ctlTemp).Tag;

    //only select images of same type
    if intSelImageType=TImage(ctlTemp).Tag then
    begin
      if ctlTemp.Name <> strTemp then
      begin
         Setlength(arySelImagesImage, intSelImagesPos+1);

         //store actual image before drawing "selection box" around it
         arySelImagesImage[intSelImagesPos]:=TImage(ctlTemp);
         //draw "selection box"
         DrawImageSelectionBox(TImage(ctlTemp));
         Inc(intSelImagesPos);
         strTemp:=ctlTemp.Name;
       end;
    end;
  end;
end;

 //get first pixel of first image
  if intSelImagesPos <> 1 then
      clrFirstPixel:=arySelImagesImage[1].Canvas.Pixels[0,0];
end;

procedure Tfrmmain.ResetELementNumbers;
begin
 intEditNbr:=1;
 intCheckBoxNbr:=1;
 intRadioButtonNbr:=1;
 intButtonNbr:=1;
 intLabelNbr:=1;
 intListBoxNbr:=1;
 intComboBoxNbr:=1;
 intScrollBarNbr:=1;
 intPanelNbr:=1;
 intGroupBoxNbr:=1;
 intTreeViewNbr:=1;
 intListViewNbr:=1;
 intSpinEditNbr:=1;
end;

procedure TfrmMain.Init;
{
  Created 24/03/2026 By Roger Williams

  creates toolbox and initialises variables

}

begin
 self.Top:=0;
 self.Left:=0;
  //reset element numbers
 ResetELementNumbers;
 self.STBStatus.Panels[6].Text := aryModes[1];
//create and show toolbox form
// frmTools:=TfrmToolBox.Create(self);
 //position to right of form
 //frmTools.Left:=self.Left+ self.Width-7;

 //configure so selected images show when dragged
 ControlStyle := ControlStyle + [csDisplayDragImage];
 self.PANElements.ControlStyle := ControlStyle + [csDisplayDragImage];
   
 self.IMGButtonE.OnStartDrag:=ImageStartDrag;
 self.IMGLabelE.OnStartDrag:=ImageStartDrag;
 self.IMGEditBoxE.OnStartDrag:=ImageStartDrag;
 self.IMGSpinEditE.OnStartDrag:=ImageStartDrag;
 self.IMGCheckBoxE.OnStartDrag:=ImageStartDrag;
 self.IMGRadioButtonE.OnStartDrag:=ImageStartDrag;
 self.IMGListBoxE.OnStartDrag:=ImageStartDrag;
 self.IMGComboBoxE.OnStartDrag:=ImageStartDrag;
 self.IMGScrollBarE.OnStartDrag:=ImageStartDrag;
 self.IMGPanelE.OnStartDrag:=ImageStartDrag;
 self.IMGGroupBoxE.OnStartDrag:=ImageStartDrag;
 self.IMGTreeViewE.OnStartDrag:=ImageStartDrag;
 self.IMGListViewE.OnStartDrag:=ImageStartDrag;
 self.IMGFormE.OnStartDrag:=ImageStartDrag;

 //create properties form but dont enable
 frmProps:=TfrmProperties.Create(self.PANProperties);
 frmProps.Parent:=self.PANProperties;
 frmProps.Top:=0;
 frmProps.Left:=0;
 self.PANProperties.Enabled:=false;
end;

procedure TfrmMain.DeleteImage(strName : string);
{
   Modified 29/04/2026 By Roger Williams

   also deletes any multi selected images

   Created 24/03/2026 By Roger Williams

   deletes image in strName

   VARS

   strName - image to delete

}
var
 intNum : integer;

begin
intNum:= 0;

if strName <> '' then
 while intNum <> self.ControlCount do
 begin
   if self.Controls[intNum] is TImage then
   begin
     if self.Controls[intNum].Name = strName then
     begin
       self.Controls[intNum].Free;
       intNum:=self.ControlCount-1;
     end;
   end;

   Inc(intNum);
 end;

//multi select delete
if strName = '' then
begin
   for intNum := 1 to intSelImagesPos -1 do
   begin
     self.RemoveControl(arySelImagesImage[intNum]);
   end;
end;

end;

procedure TfrmMain.CopyImage;
{
   Created 24/03/2026 By Roger Williams

   copies IMGSelected

   sets unique name which is current name with number +1
   e.g. IMGGrass1 -> IMGGrass2

   Note: does not alter intButtonNbr etc PASTE does that!
}
var
 intNum  : integer;
 intPos  : integer;
 intErr  : integer;
 strTemp : string;

begin
if IMGSelected = nil then exit;

  IMGTemp:=TImage.Create(self);
  IMGTemp.AutoSize:=true;
  IMGTemp.Tag:=IMGSelected.Tag;
  IMGTemp.Picture.Assign(IMGSelected.Picture);
  //set unique name which is current name with number +1
  //e.g. IMGGrass1 -> IMGGrass2
  intNum:=1;

  while intNum <> Length(strSelected) +1 do
  begin
   if TryStrToInt(strSelected[intNum], intErr) = true then
   begin
      if strTemp = '' then
         intPos:=intNum-1;   //store end of letters in name

      strTemp:=strTemp+strSelected[intNum];
    end;

   Inc(IntNum);
  end;

  intNum:=StrToInt(strTemp);
  Inc(intNum);
  strTemp:=Copy(strSelected,1,intPos)+IntToStr(intNum);
  IMGTemp.Name:=strTemp;
end;


procedure TfrmMain.PasteImage;
{
   Created 24/03/2026 By Roger Williams

   pastes copy of IMGSelected adds name with unique number
   based on existing images of type e.g. IMGrass2

}

var
 strTemp : string;

begin
 if IMGTemp <> nil then
    if Pos('IMG',IMGTemp.Name) <> 0 then
    begin
       //set events
       IMGTemp.OnClick:=ImageClick;
       IMGTemp.OnMouseDown:=ImageMouseDown;
       IMGTemp.OnMouseMove:=ImageMouseMove;
       IMGTemp.OnMouseUp:=ImageMouseUp;
       //set position add to form
       IMGTemp.Left:=intCurX;
       IMGTemp.Top:=intCurY;
       IMGTemp.Parent:=self;


       case IMGSelected.Tag of
       0:
         Inc(intButtonNbr);
       1:
         Inc(intEditNbr);
       2:
         Inc(intCheckBoxNbr);
       3:
         Inc(intRadioButtonNbr);
       4:
         Inc(intListBoxNbr);
       5:
         Inc(intComboBoxNbr);
       6:
         Inc(intScrollBarNbr);
       7:
         Inc(intPanelNbr);
       8:
         Inc(intGroupBoxNbr);
       9:
         Inc(intTreeViewNbr);
       10:
         Inc(intListViewNbr);
       11:
         Inc(intLabelNbr);
     end;
    end;
end;

procedure TfrmMain.DrawImageSelectionBox(IMGWhat : TImage);
var
  rctTemp: TRect;

begin
  rctTemp := Rect(IMGWhat.Left+2, IMGWhat.Top-1, IMGWhat.left+2 + IMGWhat.Width -2, IMGWhat.Top +2 + IMGWhat.Height -2);


  try
    IMGWhat.Picture.Bitmap.Canvas.Brush.Style:=bsSolid;
    IMGWhat.Picture.Bitmap.Canvas.Pen.Color:=clRed;
    IMGWhat.Picture.Bitmap.Canvas.Pen.Width:=2;
    IMGWhat.Picture.Bitmap.Canvas.Brush.Color:=clRed; //clGray;
    IMGWhat.Picture.Bitmap.Canvas.FillRect(rctTemp);
    IMGWhat.Picture.Bitmap.Canvas.DrawFocusRect(rctTemp);
  finally
   // IMGWhat.Picture.Bitmap.Canvas.EndScene;
  end;

  IMGWhat.Repaint;
end;

procedure TfrmMain.RemoveImageSelectionBox(intWhat : integer;IMGWhat : TImage);
begin
 IMGWhat.Assign(arySelImagesImage[intWhat]);
end;

procedure TfrmMain.ClearIDE;
{
   Created 06/05/2026 By Roger Williams

   clears form panel by deleting all the image controls

}
var
 intNum : integer;

begin
intNum:= 0;

 while intNum <> PANForm.ControlCount do
 begin
   if PANForm.Controls[intNum] is TImage then
   begin
     PANForm.Controls[intNum].Free;
     //reset counter to make sure finding all images
     intNum:=-1;
   end;

   Inc(intNum);
 end;

 //reset element numbers
ResetElementNumbers;
end;


procedure TfrmMain.SavePython;
{
   Created 06/05/2026 By Roger Williams

   saves images as a Tkinter file using data in clsControls

}
var
 filSave        : TextFile;
 strTemp        : string;
 strFont        : string;
 strName        : string;
 strValue       : string;
 strData        : string;
 intNum         : integer;
 intFont        : integer;
 intImageNbr    : integer;
 dlgSave        : TSaveDialog;
 clsFormControl : TclsFormControl;


 function FormatDate : string;
 {
     Created 08/05/2026 By Roger Williams

     returns todays date with padded zeroes if day/month < 10

 }
 var
   strDay   : string;
   strMonth : string;

 begin
  strDay:=Now.Day.ToString.Format('%.2d',[Now.Day]);
  strMonth:=Now.Month.ToString.Format('%.2d',[Now.Month]);
  result:=strDay+'\'+strMonth+'\'+ Now.Year.ToString;
 end;

 function FormatControlType : string;
 {
     Created 14/05/2026 By Roger Williams

     returns clsFormControl.ControlType with first letter as captial

 }
 var
   strTemp : string;

 begin
   strTemp:= clsFormControl.ControlType;
   strTemp:= strTemp.Substring(0,1).ToUpper + strTemp.Substring(1,strTemp.Length);
   result:=strTemp;
 end;

 function ProcessIfColor : boolean;
 {
    Created 24/05/2026 By Roger Williams

    if property name is a colour

 }

  var
   blnOk       : boolean;
   intColour   : integer;
   intNum      : integer;
   clrTemp     : TColor;

 begin
  blnOk:=false;

  if (strTemp.IndexOf('color') <> -1) or
     (strTemp.IndexOf('background') <> -1) or
     (strTemp.IndexOf('foreground') <> -1) then
    begin
       //check if strValue is a hex number NOT a string e.g. clBlack
       TryStrToInt(strValue,intNum);

       if intNum <> 0 then
       begin
         blnOk:=true;
         intColour:=ColorToRGB(StrToInt(strValue));

         strData:=strData + strTemp + ' = "#' + GetRValue(intColour).ToString +  GetGValue(intColour).ToString +
                  GetBValue(intColour).ToString +'", ';
       end
      else
       begin
         blnOk:=true;
         clrTemp:=StringToColor(strValue);
         intColour:=ColorToRGB(clrTemp);

         strData:=strData + strTemp + ' = "#' + GetRValue(intColour).ToString +  GetGValue(intColour).ToString +
                  GetBValue(intColour).ToString +'", ';
       end;
    end;

  result:=blnOk;
 end;


begin
if clsControls = nil then
   exit;
if clsControls.intControlPos = 0 then
   exit;

strTemp:=InputBox('Enter Programmer','Programmers Name','');

dlgSave:=TSaveDialog.Create(self);
dlgSave.Title:='Save TKinter File';
dlgSave.DefaultExt:='py';
dlgSave.Filter:='Python Tkinter File|*.py';
dlgSave.Options:=[ofOverwritePrompt];

if dlgSave.Execute then
begin
  AssignFile(filSave,dlgSave.FileName);
  Rewrite(filSave);

  //write header
  writeln(filSave,'from tkinter import *');
  writeln(filSave,'from tkinter import ttk');
  writeln(filSave,'from tkinter import PhotoImage');
  writeln(filSave,'import tkinter.font as Tkfont');
  writeln(filSave,'from tkinter.scrolledtext import ScrolledText');
  writeln(filSave,'');

  writeln(filSave,'#');
  writeln(filSave,'#File Generated By RogTKinterIDE');
  writeln(filSave,'#');

  if strTemp <> '' then
     writeln(filSave,'#Created '+ FormatDate + ' ' +strTemp)
  else
     writeln(filSave,'#Created '+ FormatDate);

  writeln(filSave,'#');

  //get PANForms properties
  clsFormControl:=clsControls.GetControl(1);

  //create PANForm
  writeln(filSave,'#Create form');
  writeln(filSave, '');
  writeln(filSave,'tkWindow = Tk()');
  writeln(filSave,'tkWindow.title("' + clsFormControl.Text + '")');
  //set size and x/y position
  writeln(filSave,'tkWindow.geometry("' + clsFormControl.Width.ToString + 'x' + clsFormControl.Height.ToString +
          '+' + clsFormControl.Left.ToString + '+' + clsFormControl.Top.ToString + '")');
  writeln(filSave,'tkWindow.resizable(0,0)  #make fixed size');

  //get PANForm properties
  clsControls.ReturnProperties(1);

  //used to create 1x1 pixel image for controls that normally
  //measure height in lines and width in chars makes it easier to position controls
  intImageNbr:=1;

  //for unique font controls
  intFont:=1;

  //go through rest of controls and write their properties
  for intNum := 2 to clsControls.intControlPos -1 do
  begin
   strFont:='';
   clsFormControl:=clsControls.GetControl(intNum);
   //get properties
   clsControls.ReturnProperties(clsFormControl.intIndex);
   //create inital control creation code some controls use tkWindow other
   //have to use ttk also need to rename custom type listview to treeview
   //for the python code to work!

   if (clsFormControl.ControlType = 'combobox') or (clsFormControl.ControlType = 'labelframe') or
      (clsFormControl.ControlType = 'treeview') or (clsFormControl.ControlType = 'listview')  then
      begin
         if clsFormControl.ControlType = 'listview' then
            strData:=clsFormControl.Name + ' = ttk.Treeview(tkWindow,'
         else
            strData:=clsFormControl.Name + ' = ttk.' + FormatControlType + '(tkWindow,';
      end
   else
      strData:=clsFormControl.Name + ' = ' + FormatControlType + '(tkWindow,';

   //get type
   if clsFormControl.ControlType = 'button' then
   {
      activebackground: Background color when the button is under the cursor.
      activeforeground: Foreground color when the button is under the cursor.
      anchor: Specifies the position of the content (text or image) inside the button.
      bd or borderwidth: Width of the border around the outside of the button
      bg or background: Normal background color.
      command: Function or method to be called when the button is clicked.
      cursor: Selects the cursor to be shown when the mouse is over the button.
      text: Text displayed on the button.
      disabledforeground: Foreground color is used when the button is disabled.
      fg or foreground: Normal foreground (text) color.
      font: Text font to be used for the button's label.
      height: Height of the button in text lines
      highlightbackground: Color of the focus highlight when the widget does not have focus.
      highlightcolor: The color of the focus highlight when the widget has focus.
      highlightthickness: Thickness of the focus highlight.
      image: Image to be displayed on the button (instead of text).
      justify: tk.LEFT to left-justify each line; tk.CENTER to center them; or tk.RIGHT to right-justify.
      overrelief: The relief style to be used while the mouse is on the button; default relief is tk.RAISED.
      padx, pady: padding left and right of the text. / padding above and below the text.
      width: Width of the button in letters (if displaying text) or pixels (if displaying an image).
      underline: Default is -1, underline=1 would underline the second character of the button's text.
      width: Width of the button in letters
      wraplength: If this value is set to a positive number, the text lines will be wrapped to fit within this length.

      button = tk.Button(root,
                   text="Click Me",
                   command=button_clicked,
                   activebackground="blue",
                   activeforeground="white",
                   anchor="center",
                   bd=3,
                   bg="lightgray",
                   cursor="hand2",
                   disabledforeground="gray",
                   fg="black",
                   font=("Arial", 12),
                   height=2,
                   highlightbackground="black",
                   highlightcolor="green",
                   highlightthickness=2,
                   justify="center",
                   overrelief="raised",
                   padx=10,
                   pady=5,
                   width=15,
                   wraplength=100)



   }

   //create above string for our control
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython')  then
        if (strTemp <> 'name') and (strTemp <> 'type') then
         if (strTemp <> 'top') and (strTemp <> 'left') and
            (strTemp <> 'fontsize') and (strTemp <> 'font') then
          //handle strings differently from numbers
          if (strTemp = 'borderwidth') or
             (strTemp = 'height') or (strTemp = 'width')  or
             (strTemp = 'wraplength') or
             (strTemp = 'padx') or (strTemp = 'pady') then
             begin
               if strValue <> '' then
                  strData:=strData + strTemp + '=' + strValue + ', ';
             end
          else
             begin
               if strValue <> '' then
                  if not ProcessIfColor then
                     strData:=strData + strTemp + '="' + strValue +'", ';
             end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'label' then
        {
        text: The text displayed on the label.
        image: Displays an image on the label.
        bg: Sets the background color.
        fg: Sets the text color.
        font: Specifies the font style and size.
        width: Sets the width of the label.
        height: Sets the height of the label.
        padx: Adds horizontal padding.
        pady: Adds vertical padding.
        relief: Sets the border style (FLAT, RAISED, SUNKEN, etc.).

      lbl = tk.Label(root,
                     text="Welcome to Tkinter",
                     font=("Arial", 16, "bold"),
                     bg="lightblue",
                     fg="darkblue",
                     width=20,
                     height=2,
                     relief="raised")
      }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
        if (strTemp <> 'name') and (strTemp <> 'type') then
         if (strTemp <> 'top') and (strTemp <> 'left') then
          //handle strings differently from numbers
          if (strTemp = 'borderwidth')or
             (strTemp = 'height') or (strTemp = 'width')  or
             (strTemp = 'wraplength') or
             (strTemp = 'padx') or (strTemp = 'pady') then
             begin
               if strValue <> '' then
                  strData:=strData + strTemp + '=' + strValue + ', ';
             end
          else
             begin
               if strValue <> '' then
                  if not ProcessIfColor then
                     strData:=strData + strTemp + '="' + strValue +'", ';
             end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');

   end;


   if clsFormControl.ControlType = 'spinbox' then
   {

   }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
        if (strTemp <> 'name') and (strTemp <> 'type') then
         if (strTemp <> 'top') and (strTemp <> 'left') then
          //handle strings differently from numbers
          if (strTemp = 'height') or (strTemp = 'width')  or
             (strTemp = 'wraplength') or
             (strTemp = 'min') or (strTemp = 'max') then
             begin
               if strValue <> '' then
                  if (strTemp = 'min') or (strTemp = 'max') then
                     begin
                       if strTemp = 'min' then strData:=strData + 'from_=' + strValue + ', ';
                       if strTemp = 'max' then strData:=strData + 'to_=' + strValue + ', ';
                     end
                    else
                     strData:=strData + strTemp + '=' + strValue + ', ';
             end
          else
             begin
               if strValue <> '' then
                  if not ProcessIfColor then
                     strData:=strData + strTemp + '="' + strValue +'", ';
             end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'text' then
   {
    root - root window.
    bg - background colour
    fg - foreground colour
    bd - border of widget.
    height - height of the widget.
    width - width of the widget.
    font - Font type of the text.
    cursor - The type of the cursor to be used.
    insetofftime - The time in milliseconds for which the cursor blink is off.
    insertontime - the time in milliseconds for which the cursor blink is on.
    padx - horizontal padding.
    pady - vertical padding.
    state - defines if the widget will be responsive to mouse or keyboards movements.
    highlightthickness - defines the thickness of the focus highlight.
    insertionwidth - defines the width of insertion character.
    relief - type of the border which can be SUNKEN, RAISED, GROOVE and RIDGE.
    yscrollcommand - to make the widget vertically scrollable.
    xscrollcommand - to make the widget horizontally scrollable.
    Text(root, height = 5, width = 52)
   }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);
//     text = ScrolledText(tkWindow, width=80,  height=8)
//text.pack(padx = 10, pady=10,  fill=tk.BOTH, side=tk.LEFT, expand=True)


     //add any scrollbars
     clsControls.dictProperties.TryGetValue('yscrollcommand',strValue);

     if strValue = '-1' then
     begin
       //if vertical only use scrollbox instead of text
       clsControls.dictProperties.TryGetValue('xscrollcommand',strValue);

       if strValue = '' then
       begin
              strData:=clsFormControl.Name + ' = ScrolledText(tkWindow,';
       end;
     end;

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);
//     //see if any scroll bars
//     clsControls.dictProperties.TryGetValue('yscrollcommand',strValue);
//
//     if strValue <> '' then
//     begin
//       writeln(filSave,'# Create scrollbar for text widget');    //scrollable_widget.yview
//       writeln(filSave,strValue + ' = ttk.Scrollbar(tkWindow, orient=VERTICAL, command=self.txt.yview)');
//       writeln(filSave,strValue + '.pack(side=LEFT, fill=BOTH)');
//       strData:=strData + ' ,yscrollcommand=' + strValue + '.set, ';
//     end;
//
//     clsControls.dictProperties.TryGetValue('xscrollcommand',strValue);
//
//     if strValue <> '' then
//     begin
//       writeln(filSave,'# Create scrollbar for text widget');
//       writeln(filSave, strValue + ' = ttk.Scrollbar(tkWindow, orient=tk.HORIZONTAL, command=self.txt.xview)');
//       writeln(filSave,strValue + '.pack(side=RIGHT, fill=Y)');
//       strData:=strData + ' ,xscrollcommand=' + strValue + '.set, ';
//     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
         if (strTemp <> 'yscrollcommand') and (strTemp <> 'xscrollcommand') then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left')  then
              //handle strings differently from numbers
              if (strTemp = 'borderwidth') or (strTemp = 'fontsize') or
                 (strTemp = 'height') or (strTemp = 'width')  or
                 (strTemp = 'highlightthickness') or
                 (strTemp = 'padx') or (strTemp = 'pady') or
                 (strTemp = 'insertionwidth') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);

     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'listbox' then
    {
      root - root window.
      bg - background colour
      fg - foreground colour
      bd - border
      height - height of the widget.
      width - width of the widget.
      font - Font type of the text.
      highlightcolor - The colour of the list items when focused.
      yscrollcommand - for scrolling vertically.
      xscrollcommand - for scrolling horizontally.
      cursor - The cursor on the widget which can be an arrow, a dot etc.

      listbox = Listbox(top, height = 10,
                  width = 15,
                  bg = "grey",
                  activestyle = 'dotbox',
                  font = "Helvetica",
                  fg = "yellow")


     }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

      //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);
     //see if any scroll bars
     clsControls.dictProperties.TryGetValue('yscrollcommand',strValue);

     if strValue <> '' then
     begin
       writeln(filSave,'# Create scrollbar for text widget');
       writeln(filSave,strValue + ' = ttk.Scrollbar(tkWindow, orient=tk.VERTICAL)');
       writeln(filSave,strValue + '.pack(side=tk.LEFT, fill=tk.BOTH)');
       strData:=strData + ' ,yscrollcommand=' + strValue + '.set, ';
     end;

     clsControls.dictProperties.TryGetValue('xscrollcommand',strValue);

     if strValue <> '' then
     begin
       writeln(filSave,'# Create scrollbar for text widget');
       writeln(filSave, strValue + ' = ttk.Scrollbar(tkWindow, orient=tk.HORIZONTAL)');
       writeln(filSave,strValue + '.pack(side=tk.RIGHT, fill=tk.Y)');
       strData:=strData + ' ,xscrollcommand=' + strValue + '.set, ';
     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
         if (strTemp <> 'yscrollcommand') and (strTemp <> 'xscrollcommand') then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'bd') or
                 (strTemp = 'height') or (strTemp = 'width')  or
                 (strTemp = 'insertionwidth') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';
     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');

     //add fake item
     writeln(filSave,'#create fake item');
     writeln(filSave,clsFormControl.Name + '.insert(1, "Test Item")');
   end;

   if clsFormControl.ControlType = 'checkbutton' then
   {
    checkbutton = tk.Checkbutton(root, text="Enable Feature", variable=var,
                             onvalue=1, offvalue=0, command=on_button_toggle)

    # Setting options for the Checkbutton
    checkbutton.config(bg="lightgrey", fg="blue", font=("Arial", 12),
                       selectcolor="green", relief="raised", padx=10, pady=5)
   }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
        if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width')  or
                 (strTemp = 'wraplength') or
                 (strTemp = 'padx') or (strTemp = 'pady') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'radiobutton' then
   {
       Radiobutton(master, text = text, variable = v,
                value = value, indicator = 0,
                background = "light blue").pack(fill = X, ipady = 5)

   }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //create image so measurements are in pixels
//     writeln(filSave,'IMGPixel' + intImageNbr.ToString + ' = PhotoImage(width=1, height=1)');
//     strData:=strData + 'image=IMGPixel' + intImageNbr.ToString + ', compound=LEFT, ';
//     Inc(intImageNbr);

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
       if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width')  or
                 (strTemp = 'wraplength') or
                 (strTemp = 'padx') or (strTemp = 'pady') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';

     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'frame' then
    {

    frame = create_widget(
    window, tk.Frame,
    bg='lightblue', bd=3, cursor='hand2',
    height=100, width=200,
    highlightcolor='red',
    highlightbackground='black',
    highlightthickness=2,
    relief=tk.RAISED)
    }
   begin
     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
        if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left')  then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';
     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'labelframe' then
{
     # This will create a LabelFrame
     label_frame = LabelFrame(root, text='This is Label Frame')


}
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
        if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left')  then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';
     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'combobox' then
   {

    # Combobox creation
n = tk.StringVar()
monthchoosen = ttk.Combobox(window, width = 27, textvariable = n)

# Adding combobox drop down list
monthchoosen['values'] = (' January',
                          ' February',
                          ' March',
                          ' April',
                          ' May',
                          ' June',
                          ' July',
                          ' August',
                          ' September',
                          ' October',
                          ' November',
                          ' December')


   }
   begin
     //if font property set create font to use with JUST this control
     clsControls.dictProperties.TryGetValue('font',strFont);

     if strFont <> '' then
     begin
        //create unique font
        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
        //clsFormControl.Name
     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
        if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                      if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
     if strFont <> '' then
     begin
        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
        Inc(intFont);
     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-2);
     strData:=strData + ')';
     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
   end;

   if clsFormControl.ControlType = 'treeview' then
    {
     # Creating treeview window
      treeview = ttk.Treeview(app)

           treeview.insert('', '0', 'item1',
                      text ='GeeksforGeeks')

      # Inserting child
      treeview.insert('', '1', 'item2',
                      text ='Computer Science')
    }
    begin
     //if font property set create font to use with JUST this control
//     clsControls.dictProperties.TryGetValue('font',strFont);
//
//     if strFont <> '' then
//     begin
//        //create unique font
//        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
//        //clsFormControl.Name
//     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
        if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
               //       if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
//     if strFont <> '' then
//     begin
//        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
//        Inc(intFont);
//     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-1);
     strData:=strData + ')';
     //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
     //create root and child item - fake data
     writeln(filSave,clsFormControl.Name + '.insert("","end","rootnode",text="Root")');
     writeln(filSave,clsFormControl.Name + '.insert("rootnode","end","childnode",text="Child")');
    end;

  if clsFormControl.ControlType = 'listview' then
    {
      tree = ttk.Treeview(frmMain)
      #create columns
      tree["columns"] = ("productfamily", "uom")   #by default there is always one column these are extras
      tree.column("#0", width=250, stretch=False)  #default column (first) is always called: #0
      tree.column("productfamily", width=150)
      tree.column("uom", width=150)
      #set column headers
      tree.heading("#0", text="Item ID", anchor="w")
      tree.heading("productfamily", text="Product Family", anchor="w")
      tree.heading("uom", text="UOM", anchor="w")
      #put on form
      tree.place(x=0, y=150, anchor="w", height=200)
    }
    begin
     //if font property set create font to use with JUST this control
   //  clsControls.dictProperties.TryGetValue('font',strFont);

//     if strFont <> '' then
//     begin
//        //create unique font
//        writeln(filSave,'fntFont' + intFont.ToString + '=' + clsFormControl.strFontPython);
//        //clsFormControl.Name
//     end;

     //write create code
     for strTemp in clsControls.dictProperties.Keys do
     begin
      clsControls.dictProperties.TryGetValue(strTemp,strValue);
         if (strTemp <> 'fontpython') and (strTemp <> 'fontsize') and (strTemp <> 'font')  then
          if (strTemp <> 'name') and (strTemp <> 'type') then
             if (strTemp <> 'top') and (strTemp <> 'left') then
              //handle strings differently from numbers
              if (strTemp = 'height') or (strTemp = 'width') then
                 begin
                   if strValue <> '' then
                      strData:=strData + strTemp + '=' + strValue + ', ';
                 end
              else
                 begin
                   if strValue <> '' then
                   //   if not ProcessIfColor then
                         strData:=strData + strTemp + '="' + strValue +'", ';
                 end;
     end;

     //if font add
//     if strFont <> '' then
//     begin
//        strData:=strData + 'font = fntFont' + intFont.ToString +', ';
//        Inc(intFont);
//     end;

     //strip end , and add )
     strData:=strData.Substring(0,strData.Length-1);
     strData:=strData + ')';
      //write to file
     writeln(filSave,'#'+clsFormControl.Name);
     writeln(filSave,strData);
     //place on form
     writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
             clsFormControl.Top.ToString + ',anchor=W)');
     writeln(filSave,'');
     //create root and child item - fake data
     writeln(filSave,clsFormControl.Name + '.insert("","0","rootnode",text="Root")');
     writeln(filSave,clsFormControl.Name + '.insert("","1","childnode",text="Child")');
     //writeln(filSave,strData);
     //create a fake column with header
     writeln(filSave,'#create columns');
     writeln(filSave,clsFormControl.Name + '["columns"]= ("column2") #by default there is always one column this is extra');
     writeln(filSave,clsFormControl.Name + '.column("#0", width=250, stretch=False)  #default column (first) is always called: #0');
     writeln(filSave,clsFormControl.Name + '.column("column2", width=250, stretch=False)');
     writeln(filSave,'#set column headers');
     writeln(filSave,clsFormControl.Name + '.heading("#0", text="First Column", anchor="w")');
     writeln(filSave,clsFormControl.Name + '.heading("column2", text="Second Column", anchor="w")');
    end;

   if clsFormControl.ControlType = 'scrollbar' then
   {
    activebackground: This option is used to represent the background color of the widget when it has the focus.
    bg: This option is used to represent the background color of the widget.
    bd: This option is used to represent the border width of the widget.
    command: This option can be set to the procedure associated with the list which can be called each time when the scrollbar is moved.
    cursor: In this option, the mouse pointer is changed to the cursor type set to this option which can be an arrow, dot, etc.
    elementborderwidth: This option is used to represent the border width around the arrow heads and slider. The default value is -1.
    Highlightbackground: This option is used to focus highlighcolor when the widget doesn't have the focus.
    highlighcolor: This option is used to focus highlighcolor when the widget has the focus.
    highlightthickness: This option is used to represent the thickness of the focus highlight.
    jump: This option is used to control the behavior of the scroll jump. If it set to 1, then the callback is called when the user releases the mouse button.
    orient: This option can be set to HORIZONTAL or VERTICAL depending upon the orientation of the scrollbar.
    repeatdelay: This option tells the duration up to which the button is to be pressed before the slider starts moving in that direction repeatedly. The default is 300 ms.
    repeatinterval: The default value of the repeat interval is 100.
    takefocus: You can tab the focus through a scrollbar widget
    troughcolor: This option is used to represent the color of the trough.
    width: This option is used to represent the width of the scrollbar.

    scroll_bar = Scrollbar(root)
   }
   begin

   end;

//     for strTemp in clsControls.dictProperties.Keys do
//     begin
//
//      clsControls.dictProperties.TryGetValue(strTemp,strValue);
//
//      if strName = 'command' then
//         writeln(filSave,clsFormControl.Name + '.command("' + strValue + '")');
//      if strName = 'text' then
//         writeln(filSave,clsFormControl.Name + '.text("' + strValue + '")');
//
//         {
//         IMGButton1 = Button(tkWindow,width=129, height=48, text="IMGButton1")
//         IMGButton1.place(x=250, y=339,anchor=W)
//
//          tkWindow.geometry("400x400+10+10")
//                     }
//        //write create code
//       writeln(filSave,clsFormControl.Name + ' = ' + clsFormControl.ControlType + '(tkWindow,width=' +
//               clsFormControl.Width.ToString + ', height=' + clsFormControl.Height.ToString+')');
//       writeln(filSave,clsFormControl.Name + '.place(x=' + clsFormControl.Left.ToString + ', y=' +
//               clsFormControl.Top.ToString + ',anchor=W)');
//
//
     end;

    //show form
    writeln(filSave,'#show form');
    writeln(filSave,'tkWindow.mainloop()');
    CloseFile(filSave);
    ShowMessage('Saved!');
  end;
end;

procedure TfrmMain.RestoreSelectedImages;
{
   Created 29/04/2026 By Roger Williams

   restores the images in arySelImagesPos by changing all pixels
   with clLime to clrFirstPixel

}
var
 intNum  : integer;
 intX    : integer;
 intY    : integer;
begin
self.TMRFlashSelected.Enabled:=false;

 for intNum := 1 to intSelImagesPos -1 do
 begin
   for intY := 0 to arySelImagesImage[intNum].Height do
   begin
     for intX := 0 to arySelImagesImage[intNum].Width do
     begin
       if arySelImagesImage[intNum].Canvas.Pixels[intX,intY] = clLime then
          arySelImagesImage[intNum].Canvas.Pixels[intX,intY] := clrFirstPixel;
     end;
   end;
 end;
end;

procedure TfrmMain.LoadPython;
{
   Created 06/05/2026 By Roger Williams

   Loads Tkinter file



}
var
 filLoad : TextFile;
 strTemp : string;
 intNum  : integer;
 dlgLoad : TOpenDialog;
 IMGTemp : TImage;

begin
dlgLoad:=TOpenDialog.Create(self);
dlgLoad.Title:='Load Python TKinter File';
dlgLoad.DefaultExt:='lvl';
dlgLoad.Filter:='Level File|*.lvl';

if dlgLoad.Execute then
begin
  ClearIDE;
  AssignFile(filLoad,dlgLoad.FileName);
  Reset(filLoad);

//  readln(filLoad,strLevelName);
//  readln(filLoad,strTemp);
//  intLevelNbr:=StrToInt(strTemp);
//  readln(filLoad,strNeighbour);

  //read image details and create them
//  while not eof(filLoad) do
//  begin
//     //get image name
//     readln(filLoad,strTemp);
//     //create image
//     IMGTemp:=TImage.Create(self);
//     IMGTemp.Name:=strTemp;
//     IMGTemp.Visible:=true;
//     //get tag for image type
//     readln(filLoad,strTemp);
//     IMGTemp.Tag:=StrToInt(strTemp);
//     //get: left, top, height and width
//     readln(filLoad,strTemp);
//     IMGTemp.Left:=StrToInt(strTemp);
//     readln(filLoad,strTemp);
//     IMGTemp.Top:=StrToInt(strTemp);
//     readln(filLoad,strTemp);
//     IMGTemp.Height:=StrToInt(strTemp);
//     readln(filLoad,strTemp);
//     IMGTemp.Width:=StrToInt(strTemp);
//     //set images data from frmToolbox image
//     case IMGTemp.Tag of
//       0:
//         IMGTemp.Picture.Assign(self.IMGGrassE.Picture);
//       1:
//         IMGTemp.Picture.Assign(self.IMGHedgeE.Picture);
//       2:
//         IMGTemp.Picture.Assign(self.IMGFlower1E.Picture);
//       3:
//         IMGTemp.Picture.Assign(self.IMGFlower2E.Picture);
//       4:
//         IMGTemp.Picture.Assign(self.IMGFlower3E.Picture);
//       5:
//         IMGTemp.Picture.Assign(self.IMGNeighbourE.Picture);
//       6:
//         IMGTemp.Picture.Assign(self.IMGGardenerE.Picture);
//       7:
//         IMGTemp.Picture.Assign(self.IMGDogE.Picture);
//       8:
//         IMGTemp.Picture.Assign(self.IMGPlayerE.Picture);
//     end;
//
//     IMGTemp.AutoSize:=true;
//     //see if image type is NOT grass/hedge/flower if so bring to top
//     if IMGTemp.Tag > 4 then
//        IMGTemp.BringToFront;
//
//     //assign events so can be moved around screen
//     IMGTemp.OnClick:=ImageClick;
//     IMGTemp.OnMouseDown:=ImageMouseDown;
//     IMGTemp.OnMouseMove:=ImageMouseMove;
//     IMGTemp.OnMouseUp:=ImageMouseUp;
//
//     //set parent last to show image
//     IMGTemp.Parent:=self;
//  end;
//
//  CloseFile(filLoad);
//
//  self.STBStatus.Panels[0].Text:=strLevelName;
//  self.STBStatus.Panels[1].Text:='Level Number: ' + IntToStr(intLevelNbr);
//  self.STBStatus.Panels[4].Text:='Neighbour: ' + strNeighbour;
end;

end;

procedure TfrmMain.CreateImage(IMGSource : TImage; intX : integer; intY : integer);
{
  Created 24/03/2026 By Roger Williams

  creates an image control from dragged image

}

var
  intNum  : integer;

begin
//check if adding form inside IDE boundaries
if blnHasForm = false then
begin
   if intX >= CNST_INT_IDEWIDTH then
      exit;
   if intX <= self.Left then
      exit;
   if intY >= CNST_INT_IDEHEIGHT then
      exit;
   if intY + self.IMGFormE.Height >= CNST_INT_IDEHEIGHT then
      exit;
   if intY <= self.top then
      exit;
end;

//check form exists!
if (not Assigned(PANForm)) and (IMGSource.Name <> self.IMGFormE.Name) then
   exit;


if IMGSource.Name = 'IMGFormE' then
   if blnHasForm = false then
   begin
      //turn off form drag drop
      self.DragMode:=dmManual;
      self.IMGFormE.DragMode:=dmManual;
      blnHasForm := true;
      //create "form" panel
      PANForm:=TPanel.Create(self);
      PANForm.Parent:=self;
      PANForm.Height:=400;
      PANForm.Width:=400;
      PANForm.Top:=10;
      PANForm.Left:=10;
      PANForm.Name:='PANForm';
      PANForm.Caption:='';
      PANForm.ControlStyle := ControlStyle + [csDisplayDragImage];
      PANForm.DragMode:=dmAutomatic;
      //create events
      PANForm.OnDragDrop:=PANForm_DragDrop;
      PANForm.OnDragOver:=PANForm_DragOver;
      PANForm.OnMouseDown:=PANForm_MouseDown;
      PANForm.OnMouseMove:=PANForm_MouseMove;
      PANForm.OnMouseUp:=PANForm_MouseUp;
      PANForm.OnClick:=PANForm_Click;
      PANForm.OnDblClick:=ImageDblClick;
      //create controls class
      CreateControlsClass;
      //add PANForm to it
      clsControls.MainForm:=PANForm;
      //set control basic properties
      clsFormControl:=TclsFormControl.Create;
      clsFormControl.Name:='frmMain';
      clsFormControl.ControlType:='form';
      clsFormControl.Text:='My Form';
      clsFormControl.Left:=0;
      clsFormControl.Top:=0;
      clsFormControl.Height:=400;
      clsFormControl.Width:=400;
      clsFormControl.FontSize:=Application.DefaultFont.Size;
      clsFormControl.Font:=Application.DefaultFont.Name;
      clsFormControl.intComponentIndex:=self.FindChildControl(PANForm.Name).ComponentIndex;
      clsControls.CreateControl(clsFormControl);
      //activate properties form
      self.PANProperties.Enabled:=true;
      //show properties
      frmProps.UpdateControlsCombobox;
      clsControls.ReturnProperties(1);
      clsControls.SetControl(clsFormControl);
      frmProps.Show(clsFormControl.intIndex);
      exit;
   end
   else
      exit;


//check not trying to add control outside of PANForm
if IMGSource.Width + intX > PANForm.Width then
   exit;
if intX < 1 then
   exit;
if IMGSource.Height + intY > PANForm.Height then
   exit;
if intY < 2 then
   exit;

 IMGTemp:=TImage.Create(self);
 IMGTemp.Picture.Assign(IMGSource.Picture);
 IMGTemp.AutoSize:=true;
 IMGTemp.Left:=intX-IMGTemp.Left;
 IMGTemp.Top:=intY;

 //set image type e.g. grass/dog etc
 IMGTemp.Tag:=IMGSource.Tag;
 //set events
 IMGTemp.OnClick:=ImageClick;
 IMGTemp.OnDblClick:=ImageDblClick;
 IMGTemp.OnMouseDown:=ImageMouseDown;
 IMGTemp.OnMouseMove:=ImageMouseMove;
 IMGTemp.OnMouseUp:=ImageMouseUp;

 //create control class to store properties
 clsFormControl:=TclsFormControl.Create;

 //determine name
 if IMGSource.Name = 'IMGButtonE' then
 begin
  IMGTemp.Name:='IMGButton'+IntToStr(intButtonNbr);
  clsFormControl.ControlType:='button';
  clsFormControl.Text:='Button'+IntToStr(intButtonNbr);
  clsFormControl.Name:='btn'+IntToStr(intButtonNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match button image
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(204,204,204));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intButtonNbr);
 end;

 if IMGSource.Name = 'IMGLabelE' then
 begin
  IMGTemp.Name:='IMGLabel'+IntToStr(intLabelNbr);
  clsFormControl.ControlType:='label';
  clsFormControl.Text:='Label'+IntToStr(intLabelNbr);
  clsFormControl.Name:='lbl'+IntToStr(intLabelNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(240,240,240));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intLabelNbr);
 end;

 if IMGSource.Name = 'IMGEditBoxE' then
 begin
  IMGTemp.Name:='IMGEdit'+IntToStr(intEditNbr);
  clsFormControl.ControlType:='text';
  clsFormControl.Text:='text'+IntToStr(intEditNbr);
  clsFormControl.Name:='txt'+IntToStr(intEditNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(255,255,255));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intEditNbr);
 end;

 if IMGSource.Name = 'IMGSpinEditE' then
 begin
  IMGTemp.Name:='IMGSpinEdit'+IntToStr(intSpinEditNbr);
  clsFormControl.ControlType:='spinbox';
  clsFormControl.Text:='0';     //set to zero as number box
  clsFormControl.Name:='sbox'+IntToStr(intSpinEditNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(255,255,255));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intSpinEditNbr);
 end;

 if IMGSource.Name = 'IMGCheckBoxE' then
 begin
  IMGTemp.Name:='IMGCheckBox'+IntToStr(intCheckBoxNbr);
  clsFormControl.ControlType:='checkbutton';
  clsFormControl.Text:='checkbutton'+IntToStr(intCheckBoxNbr);
  clsFormControl.Name:='chk'+IntToStr(intCheckBoxNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(240,240,240));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intCheckBoxNbr);
 end;

 if IMGSource.Name = 'IMGRadioButtonE' then
 begin
  IMGTemp.Name:='IMGRadioButton'+IntToStr(intRadioButtonNbr);
  clsFormControl.ControlType:='radiobutton';
  clsFormControl.Text:='radiobutton'+IntToStr(intRadioButtonNbr);
  clsFormControl.Name:='rad'+IntToStr(intRadioButtonNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(240,240,240));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intRadioButtonNbr);
 end;

 if IMGSource.Name = 'IMGListBoxE' then
 begin
  IMGTemp.Name:='IMGListBox'+IntToStr(intListBoxNbr);
  clsFormControl.ControlType:='listbox';
  clsFormControl.Name:='lb'+IntToStr(intListBoxNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  Inc(intListBoxNbr);
 end;

  if IMGSource.Name = 'IMGComboBoxE' then
 begin
  IMGTemp.Name:='IMGComboBox'+IntToStr(intComboBoxNbr);
  clsFormControl.ControlType:='combobox';
  clsFormControl.Text:='combobox'+IntToStr(intComboBoxNbr);
  clsFormControl.Name:='cmb'+IntToStr(intComboBoxNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  Inc(intComboBoxNbr);
 end;

  if IMGSource.Name = 'IMGScrollBoxE' then
 begin
  IMGTemp.Name:='IMGScrollBox'+IntToStr(intScrollBarNbr);
  clsFormControl.ControlType:='scrollbar';
  clsFormControl.Name:='scr'+IntToStr(intScrollBarNbr);
  Inc(intScrollBarNbr);
 end;

  if IMGSource.Name = 'IMGPanelE' then
 begin
  IMGTemp.Name:='IMGPanel'+IntToStr(intPanelNbr);
  clsFormControl.ControlType:='frame';
  clsFormControl.Name:='fra'+IntToStr(intPanelNbr);
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(240,240,240));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intPanelNbr);
 end;

  if IMGSource.Name = 'IMGGroupBoxE' then
 begin
  IMGTemp.Name:='IMGGroupBox'+IntToStr(intGroupBoxNbr);
  clsFormControl.ControlType:='labelframe';
  clsFormControl.Text:='labelframe'+IntToStr(intGroupBoxNbr);
  clsFormControl.Name:='lfra'+IntToStr(intGroupBoxNbr);
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(240,240,240));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intGroupBoxNbr);
 end;

  if IMGSource.Name = 'IMGTreeViewE' then
 begin
  IMGTemp.Name:='IMGTreeView'+IntToStr(intTreeViewNbr);
  clsFormControl.ControlType:='treeview';
  clsFormControl.Name:='tv'+IntToStr(intTreeViewNbr);;
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(255,255,255));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intTreeViewNbr);
 end;

 if IMGSource.Name = 'IMGListViewE' then
 begin
  IMGTemp.Name:='IMGListView'+IntToStr(intListViewNbr);
  //not a Tkinter type BUT needed for save to ensure correct formatting
  clsFormControl.ControlType:='listview';
  clsFormControl.Name:='tvc'+IntToStr(intListViewNbr);;
  clsFormControl.FontSize:=Application.DefaultFont.Size;
  clsFormControl.Font:=Application.DefaultFont.Name;
  clsFormControl.strFontPython:='Tkfont.Font(family="' + clsFormControl.Font + '", size=' + clsFormControl.FontSize.ToString + ')';
  //set to match image colour
  clsFormControl.ActiveBackgroundColour:=ColorToString(RGB(255,255,255));
  clsFormControl.TextColour:=ColorToString(RGB(0,0,0));
  Inc(intListViewNbr);
 end;

 //default send to back
 IMGTemp.SendToBack;

 IMGTemp.Visible:=true;
 IMGTemp.Parent:=PANForm;
 IMGAdded:=IMGTemp;

 //set control basic properties
 clsFormControl.ImageName:=IMGTemp.Name;
 clsFormControl.Left:=IMGTemp.Left;
 clsFormControl.Top:=IMGTemp.Top;

 //these control measure height in lines and width in chars!
 if (clsFormControl.ControlType='listbox') or (clsFormControl.ControlType='combobox') or
    (clsFormControl.ControlType='text') or (clsFormControl.ControlType='button') or
    (clsFormControl.ControlType='checkbutton') or (clsFormControl.ControlType='radiobutton') then
     begin
       clsFormControl.Height:=1;
       //estimated char widths based on image
       //button 21 listbox 26  combobox  18  text 19  checkbutton 11  radiobutton 16  label 6
       if clsFormControl.ControlType='button' then
          clsFormControl.Width:=21;
       if clsFormControl.ControlType='listbox' then
          clsFormControl.Width:=26;
       if clsFormControl.ControlType='combobox' then
          clsFormControl.Width:=18;
       if clsFormControl.ControlType='text' then
          clsFormControl.Width:=19;
       if clsFormControl.ControlType='checkbutton' then
          clsFormControl.Width:=11;
       if clsFormControl.ControlType='radiobutton' then
          clsFormControl.Width:=16;
       if clsFormControl.ControlType='label' then
          clsFormControl.Width:=6;
     end
    else
     begin
       clsFormControl.Height:=IMGTemp.Height;
       clsFormControl.Width:=IMGTemp.Width;
     end;

 //get control index from PANForms
 for intNum :=0  to PANForm.ControlCount -1 do
     if TImage(PANForm.Controls[intNum]).Name = IMGTemp.Name then
        clsFormControl.intComponentIndex:=intNum;

 clsFormControl.ImageOnForm:=IMGTemp;
 clsControls.CreateControl(clsFormControl);

 //show properties
 frmProps.UpdateControlsCombobox;
 clsControls.ReturnProperties(clsFormControl.intIndex);
 clsControls.SetControl(clsFormControl);
 frmProps.Show(clsFormControl.intIndex);
end;


//****form events etc***

procedure TfrmMain.TMRFlashSelectedTimer(Sender: TObject);
{
   Created 28/04/2026 By Roger Williams

   "flashes" the images in arySelImagesPos by changing all pixels
   with clrFirstPixel to clLime

}
var
 intNum  : integer;
 intX    : integer;
 intY    : integer;

begin

 for intNum := 1 to intSelImagesPos -1 do
 begin
   for intY := 0 to arySelImagesImage[intNum].Height do
   begin
     for intX := 0 to arySelImagesImage[intNum].Width do
     begin
     if arySelImagesImage[intNum].Canvas.Pixels[intX,intY] = clrFirstPixel then
        arySelImagesImage[intNum].Canvas.Pixels[intX,intY] := clLime
     else
       if arySelImagesImage[intNum].Canvas.Pixels[intX,intY] = clLime then
          arySelImagesImage[intNum].Canvas.Pixels[intX,intY] := clrFirstPixel;
     end;
   end;
 end;
end;

procedure TfrmMain.FormClose(Sender: TObject; var Action: TCloseAction);
begin
 frmProps.Free;
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
 Init;
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
if Assigned(clsDragObject) then clsDragObject.Free;
end;

procedure TfrmMain.FormDragDrop(Sender, Source: TObject; X, Y: Integer);
begin
 CreateImage(TImage(clsDragObject.CctlDragSource),X,Y);
end;

procedure TfrmMain.FormDragOver(Sender, Source: TObject; X, Y: Integer;
  State: TDragState; var Accept: Boolean);
begin
//set to not accept as default
Accept:=false;

if clsDragObject.CctlDragSource is TImage then
   Accept:=true;
end;

procedure TfrmMain.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
if IMGSelected <> nil then
begin
   if (Shift = [ssAlt]) and (Key = VK_RETURN) then
   begin
     if clsControls <> nil then
     begin
        clsControls.ReturnPropertiesByImageName(strSelected);
        clsControls.SetControl(clsFormControl);
        frmProps.Show(clsFormControl.intIndex);
     end;
   end;

   if Pos('IMG',IMGSelected.Name) <> 0 then
   begin
     if Key = VK_Left then
     begin
        self.PANForm.Left:=self.PANForm.Left-1;

        if self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left < 0 then
           self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left:=0;

     end;

     if Key = VK_Right then
     begin
        self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left:=self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left+1;

        if self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left > self.PANForm.Width - self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Width then
           self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Left:=self.PANForm.Width - self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Width;
     end;

     if Key = VK_Up then
     begin
        self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top:=self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top-1;

        if self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top < self.Top then
           self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top:=0;
     end;

     if Key = VK_Down then
     begin
        self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top:=self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top+1;

        if self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top > self.PANForm.Height - self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Height then
           self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Top:= self.PANForm.Height - self.PANForm.Controls[clsControls.clsCurrentFormControl.intComponentIndex].Height;
     end;

//     if Key = VK_Delete then
//        self.MNUDelete.Click;

     if Key = VK_ESCAPE then
        RestoreSelectedImages;
  end;
end;

if IMGSelected = nil then
begin
  //check if mouse outside of level area
  if intCurX > CNST_INT_IDEWIDTH then
     exit;

  if intCurY > CNST_INT_IDEHEIGHT then
     exit;

    if Key = VK_SHIFT then
    begin
       if self.STBStatus.Panels[6].Text = aryModes[2] then
       begin
        self.DragMode:=dmAutomatic;
        self.PANForm.DragMode:=dmAutomatic;
        self.STBStatus.Panels[6].Text := aryModes[1];
       end
      else
       if self.STBStatus.Panels[6].Text = aryModes[1] then
       begin
        self.DragMode:=dmManual;
        self.PANForm.DragMode:=dmManual;
        self.STBStatus.Panels[6].Text := aryModes[2];
       end;
    end;
   end;

   if strSelected = self.PANForm.Name then
   begin
     if Key = VK_Left then
     begin
        self.PANForm.Left:=self.PANForm.Left-1;

        if self.PANForm.Left < 0 then
           self.PANForm.Left:=0;

     end;

     if Key = VK_Right then
     begin
        self.PANForm.Left:=self.PANForm.Left+1;

        if self.PANForm.Left > self.PANForm.Width - self.PANForm.Width then
           self.PANForm.Left:=self.PANForm.Width - self.PANForm.Width;
     end;

     if Key = VK_Up then
     begin
        self.PANForm.Top:=self.PANForm.Top-1;

        if self.PANForm.Top < self.Top then
           self.PANForm.Top:=0;
     end;

     if Key = VK_Down then
     begin
        self.PANForm.Top:=self.PANForm.Top+1;

        if self.PANForm.Top > self.PANForm.Height - self.PANForm.Height then
           self.PANForm.Top:= self.PANForm.Height - self.PANForm.Height;
     end;
   end;
end;

procedure TfrmMain.FormMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
 if Button = mbLeft then
 begin
   intSelX := X;
   intSelY := Y;
   blnSelecting := True;
 end;
end;

procedure TfrmMain.FormMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
var
  intWidth: Integer;
  intHeight: Integer;

begin
intCurX:=X;
intCurY:=Y;
self.STBStatus.Panels[3].Text:='Col: ' + IntToStr(X) + ' Row: ' + IntToStr(Y);

  if blnSelecting then
  begin
    self.Canvas.DrawFocusRect(rctSelRect);
    intWidth := X - intSelX;
    intHeight := Y - intSelY;

    rctSelRect := Bounds(
      Min(intSelX, intSelX + intWidth), Min(intSelY, intSelY + intHeight), Abs(intWidth), Abs(intHeight));
    self.Canvas.DrawFocusRect(rctSelRect);
  end;

end;

procedure TfrmMain.FormMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
if Button = mbLeft then
begin
  blnSelecting := False;

  //find images inside selection box add to selected array
  FindImagesInSelectionBox;
  //reset mode back to drag/drop
  self.DragMode:=dmAutomatic;
  self.STBStatus.Panels[6].Text := aryModes[1];

  //clear selection box coords
  rctSelRect.Width:=0;
  rctSelRect.Height:=0;
  rctSelRect.Left:=0;
  rctSelRect.Top:=0;
  rctSelRect.TopLeft.X:=0;
  rctSelRect.TopLeft.Y:=0;
  rctSelRect.BottomRight.X:=0;
  rctSelRect.BottomRight.Y:=0;
  //remove selection box
  self.Refresh;

  //"flash" multi select items
  self.TMRFlashSelected.Enabled:=true;
end;

if Button = mbRight then
   RestoreSelectedImages;
end;

procedure TfrmMain.MNUExitClick(Sender: TObject);
begin
 self.Close;
end;

procedure TfrmMain.MNUNewClick(Sender: TObject);
begin
 if PANForm <> nil then
 begin
    PANForm.Free;
    self.PANProperties.Enabled:=false;
    //reactivate form dragndrog
    self.DragMode:=dmManual;
    self.IMGFormE.DragMode:=dmAutomatic;
    blnHasForm:=false;
    ResetELementNumbers;
 end;
end;

procedure TfrmMain.MNUSaveClick(Sender: TObject);
begin
 SavePython;
end;

end.
