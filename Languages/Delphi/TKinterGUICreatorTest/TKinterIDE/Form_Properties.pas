unit Form_Properties;

interface
{
 Modified 13/05/2026 By Roger Williams
 added CMBControls

 Created 07/05/2026 By Roger Williams

 used by main form to show control properties

 clsControls has a global dictionary with properties
 use name property in list to get clsFormControl
 or use clsFormControls intIndex property
 from global clsControls class for editing
 uses clsControls for updating the changes

}

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs,
  clsTkinterFormControls, Vcl.StdCtrls, Vcl.ExtCtrls, Vcl.Samples.Spin;

{
 all properties available:

      //custom internal properties
      ControlType
      Name
      ImageName
      ImageOnForm
      ImageForPythonControl

      //widget properties
      Command
      Text
      Min
      Max
      Left
      Top
      Height
      Width
      Font
      Justify
      Relief
      Anchor
      OverRelief
      Orient
      FontSize
      PadX
      PadY
      BorderWidth
      HighlightThickness
      WrapLength
      RepeatDelay
      RepeatInterval
      OnValue
      OffValue
      InsetOffTime
      InsertOnTime
      Underline
      ActiveBackgroundColour
      BackgroundColour
      ForegroundColour
      ActiveForegroundColour
      DisabledForegroundColour
      DisabledBackgroundColour
      TextColour
      SelectColour
      HighlightBackgroundColour
      HighlightColour



}
type
  TfrmProperties = class(TForm)
    SPTSplitter: TSplitter;
    PANProperty: TPanel;
    LBLColourABackground: TLabel;
    LBLColourAForeground: TLabel;
    LBLColourDForeground: TLabel;
    LBLText: TLabel;
    LBLColourText: TLabel;
    LBLCommand: TLabel;
    LBLHeight: TLabel;
    LBLWidth: TLabel;
    LBLTop: TLabel;
    LBLLeft: TLabel;
    LBlName: TLabel;
    LBLJustify: TLabel;
    LBLFont: TLabel;
    PANValues: TPanel;
    CMBColourABackground: TColorBox;
    CMBColourAForeground: TColorBox;
    CMBColourDForeground: TColorBox;
    EDTText: TEdit;
    CMBColourText: TColorBox;
    EDTCommand: TEdit;
    SEDTHeight: TSpinEdit;
    SEDTTop: TSpinEdit;
    SEDTLeft: TSpinEdit;
    EDTName: TEdit;
    CMBJustify: TComboBox;
    CMBFont: TComboBox;
    PANControls: TPanel;
    CMBControls: TComboBox;
    LBLControls: TLabel;
    SEDTWidth: TSpinEdit;
    LBLFontSize: TLabel;
    SEDTFontSize: TSpinEdit;
    LBLColourDBackground: TLabel;
    CMBColourDBackground: TColorBox;
    LBLMinimumValue: TLabel;
    LBLMaximumValue: TLabel;
    SEDTMinimumValue: TSpinEdit;
    SEDTMaximumValue: TSpinEdit;
    LBLColourBackground: TLabel;
    LBLColourForeground: TLabel;
    CMBColourBackground: TColorBox;
    CMBColourForeground: TColorBox;
    LBLAnchor: TLabel;
    CMBAnchor: TComboBox;
    LBLColourHighlightBackground: TLabel;
    CMBColourHighlightBackground: TColorBox;
    LBLColourHighlight: TLabel;
    CMBColourHighlight: TColorBox;
    LBLHighlightThickness: TLabel;
    SEDTHighlightThickness: TSpinEdit;
    LBLOverRelief: TLabel;
    CMBOverRelief: TComboBox;
    LBLPadX: TLabel;
    SEDTPadX: TSpinEdit;
    LBLPadY: TLabel;
    SEDTPadY: TSpinEdit;
    LBLRelief: TLabel;
    CMBRelief: TComboBox;
    LBLColourSelect: TLabel;
    CMBColourSelect: TColorBox;
    LBLWrapLength: TLabel;
    LBLWrap: TLabel;
    SEDTWrapLength: TSpinEdit;
    CMBWrap: TComboBox;
    LBLBorderWidth: TLabel;
    SEDTBorderWidth: TSpinEdit;
    LBLScrollBarX: TLabel;
    CHKScrollBarY: TCheckBox;
    procedure FormCreate(Sender: TObject);
    procedure CMBFontDrawItem(Control: TWinControl; Index: Integer; Rect: TRect;
      State: TOwnerDrawState);
    procedure CMBControlsChange(Sender: TObject);
    procedure CHKScrollBarYClick(Sender: TObject);

  private
    { Private declarations }
    procedure Init;
    procedure OnPropertyChange(Sender: TObject);
    procedure OnPropertyExit(Sender: TObject);
    procedure OnKeyDown(Sender: TObject; var Key: Word; Shift: TShiftState);
    procedure MoveResizeControlWindow(intWhat : integer; intValue : integer);

  public
    { Public declarations }
    procedure UpdateControlsCombobox;
    procedure UpdateControl;
    procedure Show(intIndex : integer);
    procedure UpdateForm;
    procedure UpdateControlText;
  end;

var
  frmProperties: TfrmProperties;

implementation

{$R *.dfm}

procedure TfrmProperties.MoveResizeControlWindow(intWhat : integer; intValue : integer);
{
 Created 24/05/2026 By Roger Williams

 moves/resizes panel on IDE that represents the "main form" the user
 drags controls onto

 makes sure "main form" does not exceed IDE boundaries as stated in
 unit clsTkinterFormControls

 VARS

 intWhat  - action: 1 - left  2 - top  3 - height  4 - width
 intValue - amount to apply

}

begin
 if intWhat = 1 then
    if intValue > 0 then
       if intValue + clsControls.MainForm.Width < clsTkinterFormControls.CNST_INT_IDE_WIDTH then
          clsControls.MainForm.Left:=intValue;

 if intWhat = 2 then
    if intValue > 0 then
       if intValue + clsControls.MainForm.Height < clsTkinterFormControls.CNST_INT_IDE_HEIGHT then
          clsControls.MainForm.Height:=intValue;
 if intWhat = 3 then
 begin
   if intValue < clsTkinterFormControls.CNST_INT_IDE_HEIGHT then
      clsControls.MainForm.Height:=intValue;
 end;

 if intWhat = 4 then
 begin
   if intValue < clsTkinterFormControls.CNST_INT_IDE_WIDTH then
      clsControls.MainForm.Width:=intValue;
 end;

end;

procedure TfrmProperties.UpdateControlText;
{
 Created 18/05/2026 By Roger Williams

 draws new text on image in clsControls.clsCurrentFormControl

}
var
  intNum   : integer;
  intX     : integer;
  intY     : integer;
  clrTemp  : TColor;
  rctTemp  : TRect;
  strTemp  : string;

begin
if (clsControls.clsCurrentFormControl.ControlType = 'button') or
   (clsControls.clsCurrentFormControl.ControlType = 'label') or
   (clsControls.clsCurrentFormControl.ControlType = 'text') or
   (clsControls.clsCurrentFormControl.ControlType = 'listbox') or
   (clsControls.clsCurrentFormControl.ControlType = 'combobox') then
begin
 //get first pixel colour (need that to flood fill the control)
 clrTemp:=StringToColor(clsControls.clsCurrentFormControl.ActiveBackgroundColour);

 //flood fill
 clsControls.clsCurrentFormControl.ImageOnForm.Canvas.Brush.Color:=clrTemp;

 for intY := 0 to clsControls.clsCurrentFormControl.ImageOnForm.Height do
 begin

   for intX := 0 to clsControls.clsCurrentFormControl.ImageOnForm.Width do
   begin
     clsControls.clsCurrentFormControl.ImageOnForm.Canvas.Pixels[intX,intY]:=clrTemp;
   end;
 end;

 //write new text
// clsControls.clsCurrentFormControl.ImageOnForm.Canvas.Pen.Color:=StringToColor(clsControls.clsCurrentFormControl.TextColour);
 rctTemp:=clsControls.clsCurrentFormControl.ImageOnForm.ClientRect;
 strTemp:=self.EDTText.Text;
 //@ font colour not working!
 clsControls.clsCurrentFormControl.ImageOnForm.Canvas.Font.Color:=StringToColor(clsControls.clsCurrentFormControl.TextColour);
 clsControls.clsCurrentFormControl.ImageOnForm.Canvas.Font.Size:=self.SEDTFontSize.Value;
 clsControls.clsCurrentFormControl.ImageOnForm.Canvas.TextRect(rctTemp, strTemp, [tfSingleLine, tfVerticalCenter, tfCenter]);
end;

if clsControls.clsCurrentFormControl.ControlType = 'groupbox' then
begin

end;

end;

procedure TfrmProperties.OnKeyDown(Sender: TObject; var Key: Word; Shift: TShiftState);
{
 Created 22/05/2026 By Roger Williams

 stops key presses

}
begin
 key:=0;
end;

procedure TfrmProperties.Init;
{
 Created 10/05/2026 By Roger Williams

 sets property controls onchange handler to OnPropertyChange
 populate fonts combobox

}
var
  intNum   : integer;

begin
//populate fonts combobox
for intNum := 0 to Screen.Fonts.Count - 1 do
begin
    if Pos('@',Screen.Fonts[intNum]) = 0 then
       self.CMBFont.Items.Add(Screen.Fonts[intNum]);
end;

//set custom events
 self.EDTText.OnChange:=OnPropertyChange;
 self.EDTCommand.OnChange:=OnPropertyChange;
 self.EDTName.OnChange:=OnPropertyChange;
 self.CMBColourABackground.OnChange:=OnPropertyChange;
 self.CMBColourAForeground.OnChange:=OnPropertyChange;
 self.CMBColourDForeground.OnChange:=OnPropertyChange;
 self.CMBColourText.OnChange:=OnPropertyChange;
 self.CMBJustify.OnChange:=OnPropertyChange;
 self.CMBFont.OnChange:=OnPropertyChange;
 self.SEDTFontSize.OnChange:=OnPropertyChange;
 self.SEDTHeight.OnChange:=OnPropertyChange;
 self.SEDTTop.OnChange:=OnPropertyChange;
 self.SEDTLeft.OnChange:=OnPropertyChange;
 self.SEDTWidth.OnChange:=OnPropertyChange;
 self.EDTText.OnExit:=OnPropertyExit;
 self.SEDTMinimumValue.OnChange:=OnPropertyChange;
 self.SEDTMaximumValue.OnChange:=OnPropertyChange;
 self.CMBColourBackground.OnChange:=OnPropertyChange;
 self.CMBColourForeground.OnChange:=OnPropertyChange;
 self.CMBAnchor.OnChange:=OnPropertyChange;
 self.SEDTBorderWidth.OnChange:=OnPropertyChange;
 self.CMBColourHighlightBackground.OnChange:=OnPropertyChange;
 self.CMBColourHighlight.OnChange:=OnPropertyChange;
 self.CMBOverRelief.OnChange:=OnPropertyChange;
 self.SEDTPadX.OnChange:=OnPropertyChange;
 self.SEDTPadY.OnChange:=OnPropertyChange;
 self.CMBColourSelect.OnChange:=OnPropertyChange;
 self.SEDTWrapLength.OnChange:=OnPropertyChange;
 self.CMBWrap.OnChange:=OnPropertyChange;
 self.CHKScrollBarY.OnClick:=CHKScrollBarYClick;
// self.EDTScrollBarY.OnChange:=OnPropertyChange;
// self.EDTScrollBarX.OnChange:=OnPropertyChange;

 //make comboboxes readonly
self.CMBColourABackground.OnKeyDown:=OnKeyDown;
self.CMBColourAForeground.OnKeyDown:=OnKeyDown;
self.CMBColourDForeground.OnKeyDown:=OnKeyDown;
self.CMBColourText.OnKeyDown:=OnKeyDown;
self.CMBJustify.OnKeyDown:=OnKeyDown;
self.CMBFont.OnKeyDown:=OnKeyDown;
self.CMBControls.OnKeyDown:=OnKeyDown;
self.CMBColourDBackground.OnKeyDown:=OnKeyDown;
self.CMBColourBackground.OnKeyDown:=OnKeyDown;
self.CMBColourForeground.OnKeyDown:=OnKeyDown;
self.CMBAnchor.OnKeyDown:=OnKeyDown;
self.CMBColourHighlightBackground.OnKeyDown:=OnKeyDown;
self.CMBColourHighlight.OnKeyDown:=OnKeyDown;
self.CMBOverRelief.OnKeyDown:=OnKeyDown;
self.CMBRelief.OnKeyDown:=OnKeyDown;
self.CMBColourSelect.OnKeyDown:=OnKeyDown;
self.CMBWrap.OnKeyDown:=OnKeyDown;
end;

procedure TfrmProperties.UpdateControlsCombobox;
{
 Created 13/05/2026 By Roger Williams

 populates CMBControls

}
var
    strTemp  : string;

begin
//populate CMBControls from clsControls.lstControls
self.CMBControls.Items.Clear;

if clsControls <> nil then
   for strTemp in clsControls.lstControls do
   begin
     self.CMBControls.Items.Add(strTemp);
   end;

self.CMBControls.Sorted:=True;
end;

procedure TfrmProperties.OnPropertyChange(Sender: TObject);
{
 Created 10/05/2026 By Roger Williams

 global onchange handler

 populates clsFormControl with property change

}
   function ConvertCharWidthToPixels(intSize : integer; intFontSize: Integer): integer;
    {
      Created 26/05/2026 By Roger Williams

      converts pixel width of control with text in it too char length
      when spinedit for width is changed

      for:

      label
      combobox
      text
      button
      checkbutton
      radiobutton

      VARS

      intSize      - SEDTWidth.Value
      intFontSize  - font size

    }
    var
     intChars   : integer;
     intPixels  : integer;
     intNum     : integer;

    begin
      intChars:=intSize;
      //left indent
      intPixels:=3 + (intFontSize +1);

      for intNum := 1 to intSize do
          intPixels:=intPixels + (intFontSize -3);

  //    intPixels:=intPixels + (intFontSize * intSize);              //intChars * (intFontSize +1 div 2);
      //pad to allow for control text indent at start and end of control
   //   intPixels:=intPixels+4;       //-intChars * 2;
      result:=intPixels;
    end;


   procedure ReDrawControlImage;
   {


     used after height/width change
   }

   var
     bmpTemp   : TBitmap;
     intHeight : integer;

   begin
     bmpTemp:=TBitmap.Create;
   //  bmpTemp.Height:=self.SEDTHeight.Value;
   //  bmpTemp.Width:=self.SEDTWidth.Value;
     if self.SEDTWidth.Value <> 19 then
        bmpTemp.Width:=ConvertCharWidthToPixels(self.SEDTWidth.Value, clsControls.clsCurrentFormControl.FontSize)
     else
        bmpTemp.Width:=108;

     //get image height
     intHeight:=clsControls.clsCurrentFormControl.ImageOnForm.Height;

     if clsControls.clsCurrentFormControl.ControlType = 'text' then
        if self.SEDTHeight.Value > 1 then
           bmpTemp.Height:=26 + (self.SEDTHeight.Value * (clsControls.clsCurrentFormControl.FontSize +1)) +4
        else
           bmpTemp.Height:=26;
//     else
//        bmpTemp.Height:=(clsControls.clsCurrentFormControl.FontSize +1) * self.SEDTHeight.Value +4;

     bmpTemp.Canvas.StretchDraw(Rect(0,0,bmpTemp.Width, bmpTemp.Height),clsControls.clsCurrentFormControl.ImageOnForm.Picture.Graphic);
     clsControls.clsCurrentFormControl.ImageOnForm.Picture.Assign(bmpTemp);
     bmpTemp.FreeImage;
     bmpTemp.Free;

     //if groupbox/button/label redraw text
     if (clsControls.clsCurrentFormControl.ControlType = 'button') or
        (clsControls.clsCurrentFormControl.ControlType = 'label')  or
        (clsControls.clsCurrentFormControl.ControlType = 'text') or
        (clsControls.clsCurrentFormControl.ControlType = 'spinbox')
     then
        UpdateControlText;
   end;


begin
 //filter by control type
 if Sender is TColorBox then
 begin
   //filter for exact property and set value in clsFormControl
   if TComboBox(Sender).Name = 'CMBColourBackground' then
      clsControls.clsCurrentFormControl.BackgroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourForeground' then
      clsControls.clsCurrentFormControl.ForegroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourABackground' then
      clsControls.clsCurrentFormControl.ActiveBackgroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourAForeground' then
      clsControls.clsCurrentFormControl.ActiveForegroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourDForeground' then
      clsControls.clsCurrentFormControl.DisabledForegroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourDBackground' then
      clsControls.clsCurrentFormControl.DisabledBackgroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourText' then
      clsControls.clsCurrentFormControl.TextColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourHighlightBackground' then
      clsControls.clsCurrentFormControl.HighlightBackgroundColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourHighlight' then
      clsControls.clsCurrentFormControl.HighlightColour:=ColorToString(TColorBox(Sender).Color);
   if TComboBox(Sender).Name = 'CMBColourSelect' then
      clsControls.clsCurrentFormControl.SelectColour:=ColorToString(TColorBox(Sender).Color);
  end;

 if Sender is TComboBox then
 begin
   if TComboBox(Sender).Name = 'CMBAnchor' then
      clsControls.clsCurrentFormControl.Anchor:=TComboBox(Sender).Text;
   if TComboBox(Sender).Name = 'CMBJustify' then
      clsControls.clsCurrentFormControl.Justify:=TComboBox(Sender).Text;
   if TComboBox(Sender).Name = 'CMBFont' then
   begin
      //make compound property value from font and fontsize
      clsControls.clsCurrentFormControl.Font:='Tkfont.Font(family="' + TComboBox(Sender).Text + '", size=' + self.SEDTFontSize.Value.ToString + ')';
   end;
   if TComboBox(Sender).Name = 'CMBOverRelief' then
      clsControls.clsCurrentFormControl.OverRelief:=TComboBox(Sender).Text;
   if TComboBox(Sender).Name = 'CMBRelief' then
      clsControls.clsCurrentFormControl.Relief:=TComboBox(Sender).Text;
   if TComboBox(Sender).Name = 'CMBWrap' then
      clsControls.clsCurrentFormControl.Wrap:=TComboBox(Sender).Text;
 end;

 if sender is TCheckBox then
    clsControls.clsCurrentFormControl.ScrollBarY:=TCheckBox(Sender).Checked;

 if Sender is TEdit then
 begin
   if TEdit(Sender).Name = 'EDTName' then
      clsControls.clsCurrentFormControl.Name:=TEdit(Sender).Text;
   if TEdit(Sender).Name = 'EDTText' then
   begin
      clsControls.clsCurrentFormControl.Text:=TEdit(Sender).Text;
      UpdateControlText;
   end;
   if TEdit(Sender).Name = 'EDTCommand' then
      clsControls.clsCurrentFormControl.Command:=TEdit(Sender).Text;
 //  if TEdit(Sender).Name = 'EDTScrollBarX' then
 //     clsControls.clsCurrentFormControl.ScrollBarX:=TEdit(Sender).Text;
 end;

 if Sender is TSpinEdit then
 begin
    if TSpinEdit(Sender).Name = 'SEDTBorderWidth' then
       clsControls.clsCurrentFormControl.BorderWidth:=TSpinEdit(Sender).Value;
    if TSpinEdit(Sender).Name = 'SEDTHighlightThickness' then
       clsControls.clsCurrentFormControl.HighlightThickness:=TSpinEdit(Sender).Value;
    if TSpinEdit(Sender).Name = 'SEDTMinimumValue' then
       clsControls.clsCurrentFormControl.Min:=TSpinEdit(Sender).Value;
    if TSpinEdit(Sender).Name = 'SEDTMaximumValue' then
       clsControls.clsCurrentFormControl.Max:=TSpinEdit(Sender).Value;

    if TSpinEdit(Sender).Name = 'SEDTHeight' then
    begin
       if clsControls.clsCurrentFormControl.Name <> 'frmMain' then
       begin
         clsControls.clsCurrentFormControl.Height:=TSpinEdit(Sender).Value;
         //resize image in IDE
         clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=false;
         ReDrawControlImage;
         clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=true;
       end
      else
        MoveResizeControlWindow(3,TSpinEdit(Sender).Value);
    end;

    if TSpinEdit(Sender).Name = 'SEDTWidth' then
    begin
       if clsControls.clsCurrentFormControl.Name <> 'frmMain' then
       begin
         clsControls.clsCurrentFormControl.Width:=TSpinEdit(Sender).Value;
         //resize image in IDE
         clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=false;
         ReDrawControlImage;
         clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=true;
       end
      else
        MoveResizeControlWindow(4,TSpinEdit(Sender).Value);
    end;

    if TSpinEdit(Sender).Name = 'SEDTTop' then
    begin
       if clsControls.clsCurrentFormControl.Name <> 'frmMain' then
       begin
         clsControls.clsCurrentFormControl.Top:=TSpinEdit(Sender).Value;
          //move image in IDE
         clsControls.clsCurrentFormControl.ImageOnForm.Top:=clsControls.clsCurrentFormControl.Top;
       end
      else
        MoveResizeControlWindow(2,TSpinEdit(Sender).Value);
    end;

    if TSpinEdit(Sender).Name = 'SEDTLeft' then
    begin
       if clsControls.clsCurrentFormControl.Name <> 'frmMain' then
       begin
         clsControls.clsCurrentFormControl.Left:=TSpinEdit(Sender).Value;
         //move image in IDE
         clsControls.clsCurrentFormControl.ImageOnForm.Left:=clsControls.clsCurrentFormControl.Left;
       end
      else
        MoveResizeControlWindow(1,TSpinEdit(Sender).Value);
    end;

    if TSpinEdit(Sender).Name = 'SEDTFontSize' then
    begin
       //make compound property value from font and fontsize
       clsControls.clsCurrentFormControl.Font:='Tkfont.Font(family="' + self.CMBFont.Text + '", size=' + self.SEDTFontSize.Value.ToString + ')';
       clsControls.clsCurrentFormControl.FontSize:=TSpinEdit(Sender).Value;
       //update font size on image
       clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=false;
       ReDrawControlImage;
       clsControls.clsCurrentFormControl.ImageOnForm.AutoSize:=true;
    end;

    if TSpinEdit(Sender).Name = 'SEDTPadX' then
       clsControls.clsCurrentFormControl.PadX:=TSpinEdit(Sender).Value;
    if TSpinEdit(Sender).Name = 'SEDTPadY' then
       clsControls.clsCurrentFormControl.PadY:=TSpinEdit(Sender).Value;
    if TSpinEdit(Sender).Name = 'SEDTWrapLength' then
       clsControls.clsCurrentFormControl.WrapLength:=TSpinEdit(Sender).Value;

 end;

end;

procedure TfrmProperties.UpdateControl;
{
 Created 10/05/2026 By Roger Williams

 global onexit handler

 updates clsControls with clsCurrentFormControl

}
begin
 //update global component class
 clsControls.UpdateControl(clsControls.clsCurrentFormControl.intIndex);
 //update CMBControls incase user renamed control
 UpdateControlsComboBox;
end;

procedure TfrmProperties.UpdateForm;
{
 Created 18/05/2026 By Roger Williams

 called by main form when selected image is moved or resized
 posts changes to control properties on this form and updates clsCurrentFormControl


}

var
 strValue : string;
 strName  : string;
 strData  : string;
 strTemp  : string;
 intNum   : integer;


begin
//disable onchange/onexit
self.EDTText.OnChange:=nil;
self.EDTCommand.OnChange:=nil;
self.EDTName.OnChange:=nil;
self.CMBColourBackground.OnChange:=nil;
self.CMBColourForeground.OnChange:=nil;
self.CMBColourABackground.OnChange:=nil;
self.CMBColourAForeground.OnChange:=nil;
self.CMBColourDForeground.OnChange:=nil;
self.CMBColourText.OnChange:=nil;
self.CMBJustify.OnChange:=nil;
self.CMBFont.OnChange:=nil;
self.SEDTFontSize.OnChange:=nil;
self.SEDTHeight.OnChange:=nil;
self.SEDTTop.OnChange:=nil;
self.SEDTLeft.OnChange:=nil;
self.SEDTWidth.OnChange:=nil;
self.EDTText.OnExit:=nil;
self.CMBAnchor.OnChange:=nil;
self.SEDTBorderWidth.OnChange:=nil;
self.CMBColourHighlightBackground.OnChange:=nil;
self.CMBColourHighlight.OnChange:=nil;
self.CMBOverRelief.OnChange:=nil;
self.SEDTPadX.OnChange:=nil;
self.SEDTPadY.OnChange:=nil;
self.CMBColourSelect.OnChange:=nil;
self.SEDTWrapLength.OnChange:=nil;
self.CMBWrap.OnChange:=nil;
self.CHKScrollBarY.OnClick:=nil;
//self.EDTScrollBarX.OnChange:=nil;
//self.EDTScrollBarY.OnChange:=nil;

//populate each control for each property in list
 for strName in clsControls.dictProperties.Keys do
 begin
  //populate property controls and show
  if strName = 'backgroundcolour' then
  begin
     self.CMBColourBackground.Color:=StringToColor(clsControls.clsCurrentFormControl.BackgroundColour);
  end;
  if strName = 'foregroundcolour' then
  begin
     self.CMBColourForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.ForegroundColour);
  end;
  if strName = 'activebackgroundcolour' then
  begin
     self.CMBColourABackground.Color:=StringToColor(clsControls.clsCurrentFormControl.ActiveBackgroundColour);
  end;
  if strName = 'activeforegroundcolour' then
  begin
     self.CMBColourAForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.ActiveForegroundColour);
  end;
  if strName = 'disabledforegroundcolour' then
  begin
     self.CMBColourDForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.disabledforegroundcolour);
  end;
  if strName = 'disabledbackgroundcolour' then
  begin
     self.CMBColourDbackground.Color:=StringToColor(clsControls.clsCurrentFormControl.disabledbackgroundcolour);
  end;
  if strName = 'textcolour' then
  begin
     self.CMBColourText.Color:=StringToColor(clsControls.clsCurrentFormControl.TextColour);
  end;
  if strName = 'justify' then
  begin
     self.CMBJustify.Text:=clsControls.clsCurrentFormControl.Justify;
  end;
  if strName = 'font' then
  begin
     self.CMBFont.Text:=clsControls.clsCurrentFormControl.Font;
  end;
  if strName = 'fontsize' then
  begin
     self.SEDTFontSize.Value:=clsControls.clsCurrentFormControl.FontSize;
  end;
  if strName = 'name' then
  begin
     self.EDTName.Text:=clsControls.clsCurrentFormControl.Name;
  end;
  if strName = 'text' then
  begin
     self.EDTText.Text:=clsControls.clsCurrentFormControl.Text;
  end;
  if strName = 'command' then
  begin
     self.EDTCommand.Text:=clsControls.clsCurrentFormControl.Command;
  end;
  if strName = 'height' then
  begin
     self.SEDTHeight.Value:=clsControls.clsCurrentFormControl.Height;
  end;
  if strName = 'width' then
  begin
     self.SEDTWidth.Value:=clsControls.clsCurrentFormControl.Width;
  end;
  if strName = 'top' then
  begin
     self.SEDTTop.Value:=clsControls.clsCurrentFormControl.Top;
  end;
  if strName = 'left' then
  begin
     self.SEDTLeft.Value:=clsControls.clsCurrentFormControl.Left;
  end;
  if strName = 'mix' then
  begin
     self.SEDTMinimumValue.Value:=clsControls.clsCurrentFormControl.Min;
  end;
  if strName = 'max' then
  begin
     self.SEDTMaximumValue.Value:=clsControls.clsCurrentFormControl.Max;
  end;
  if strName = 'borderwidth' then
  begin
     self.SEDTBorderWidth.Value:=clsControls.clsCurrentFormControl.BorderWidth;
  end;
  if strName = 'highlightbackgroundcolour' then
  begin
     self.CMBColourHighlightBackground.Color:=StringToColor(clsControls.clsCurrentFormControl.HighlightBackgroundColour);
  end;
  if strName = 'highlightcolour' then
  begin
     self.CMBColourHighlight.Color:=StringToColor(clsControls.clsCurrentFormControl.HighlightColour);
  end;
  if strName = 'hightlightthickness' then
  begin
     self.SEDTHighlightThickness.Value:=clsControls.clsCurrentFormControl.HighlightThickness;
  end;
  if strName = 'overrelief' then
  begin
     self.CMBOverRelief.Text:=clsControls.clsCurrentFormControl.OverRelief;
  end;
  if strName = 'padx' then
  begin
     self.SEDTPadX.Value:=clsControls.clsCurrentFormControl.PadX;
  end;
  if strName = 'pady' then
  begin
     self.SEDTPadY.Value:=clsControls.clsCurrentFormControl.PadY;
  end;
  if strName = 'relief' then
  begin
     self.CMBRelief.Text:=clsControls.clsCurrentFormControl.Relief;
  end;
  if strName = 'selectcolour' then
  begin
     self.CMBColourSelect.Color:=StringToColor(clsControls.clsCurrentFormControl.SelectColour);
  end;
  if strName = 'wraplength' then
  begin
     self.SEDTWrapLength.Value:=clsControls.clsCurrentFormControl.WrapLength;
  end;
  if strName = 'wrap' then
  begin
     self.CMBWrap.Text:=clsControls.clsCurrentFormControl.Wrap;
  end;
  if strName = 'yscrollcommand' then
  begin
    self.CHKScrollBarY.Checked:=clsControls.clsCurrentFormControl.ScrollBarY;
  end;

 end;


 //post changes to clsCurrentFormControl
 UpdateControl;

 //re-enable onchange/onedit
 self.EDTText.OnChange:=OnPropertyChange;
 self.EDTCommand.OnChange:=OnPropertyChange;
 self.EDTName.OnChange:=OnPropertyChange;
 self.CMBColourBackground.OnChange:=OnPropertyChange;
 self.CMBColourForeground.OnChange:=OnPropertyChange;
 self.CMBColourABackground.OnChange:=OnPropertyChange;
 self.CMBColourAForeground.OnChange:=OnPropertyChange;
 self.CMBColourDForeground.OnChange:=OnPropertyChange;
 self.CMBColourText.OnChange:=OnPropertyChange;
 self.CMBJustify.OnChange:=OnPropertyChange;
 self.CMBFont.OnChange:=OnPropertyChange;
 self.SEDTFontSize.OnChange:=OnPropertyChange;
 self.SEDTHeight.OnChange:=OnPropertyChange;
 self.SEDTTop.OnChange:=OnPropertyChange;
 self.SEDTLeft.OnChange:=OnPropertyChange;
 self.SEDTWidth.OnChange:=OnPropertyChange;
 self.EDTText.OnExit:=OnPropertyExit;
 self.CMBAnchor.OnChange:=OnPropertyChange;
 self.SEDTBorderWidth.OnChange:=OnPropertyChange;
 self.CMBColourHighlightBackground.OnChange:=OnPropertyChange;
 self.CMBColourHighlight.OnChange:=OnPropertyChange;
 self.CMBOverRelief.OnChange:=OnPropertyChange;
 self.SEDTPadX.OnChange:=OnPropertyChange;
 self.SEDTPadY.OnChange:=OnPropertyChange;
 self.CMBColourSelect.OnChange:=OnPropertyChange;
 self.SEDTWrapLength.OnChange:=OnPropertyChange;
 self.CMBWrap.OnChange:=OnPropertyChange;
 self.CHKScrollBarY.OnClick:=CHKScrollBarYClick;
// self.EDTScrollBarX.OnChange:=OnPropertyChange;
// self.EDTScrollBarY.OnChange:=OnPropertyChange;
end;


procedure TfrmProperties.OnPropertyExit(Sender: TObject);
{
 Created 10/05/2026 By Roger Williams

 global onexit handler

 updates clsControls with clsCurrentFormControl

}
begin
 //update global component class
 clsControls.UpdateControl(clsControls.clsCurrentFormControl.intIndex);
 //update CMBControls incase user renamed control
 UpdateControlsComboBox;
end;

procedure TfrmProperties.Show(intIndex : integer);
{
 Created 07/05/2026 By Roger Williams

 called by main form when control image clicked or PANForm clicked
 goes through dictProperties and shows properties in there

 VARS

 intIndex       - used to get clsFormControl from clsControls for editing

}

var
 strValue     : string;
 strName      : string;
 strData      : string;
 strTemp      : string;
 intNum       : integer;
 intControlY  : integer;
 intLabelY    : integer;
 intHeight    : integer;

 procedure HideControls;
 var
   intNum   : integer;

 begin
   for intNum := 0 to self.PANProperty.ControlCount -1 do
   begin
    self.PANProperty.Controls[intNum].Hide;
   end;

   for intNum := 0 to self.PANValues.ControlCount -1 do
   begin
    self.PANValues.Controls[intNum].Hide;
   end;
 end;

begin
//disable onchange/onexit
self.EDTText.OnChange:=nil;
self.EDTCommand.OnChange:=nil;
self.EDTName.OnChange:=nil;
self.CMBColourBackground.OnChange:=nil;
self.CMBColourForeground.OnChange:=nil;
self.CMBColourABackground.OnChange:=nil;
self.CMBColourAForeground.OnChange:=nil;
self.CMBColourDForeground.OnChange:=nil;
self.CMBColourText.OnChange:=nil;
self.CMBJustify.OnChange:=nil;
self.CMBFont.OnChange:=nil;
self.SEDTFontSize.OnChange:=nil;
self.SEDTHeight.OnChange:=nil;
self.SEDTTop.OnChange:=nil;
self.SEDTLeft.OnChange:=nil;
self.SEDTWidth.OnChange:=nil;
self.EDTText.OnExit:=nil;
self.SEDTMinimumValue.OnChange:=nil;
self.SEDTMaximumValue.OnChange:=nil;
self.CMBAnchor.OnChange:=nil;
self.SEDTBorderWidth.OnChange:=nil;
self.CMBColourHighlightBackground.OnChange:=nil;
self.CMBColourHighlight.OnChange:=nil;
self.CMBOverRelief.OnChange:=nil;
self.SEDTPadX.OnChange:=nil;
self.SEDTPadY.OnChange:=nil;
self.CMBColourSelect.OnChange:=nil;
self.SEDTWrapLength.OnChange:=nil;
self.CMBWrap.OnChange:=nil;
self.CHKScrollBarY.OnClick:=nil;
//self.EDTScrollBarX.OnChange:=nil;
//self.EDTScrollBarY.OnChange:=nil;

HideControls;
//first get class for passed control
clsControls.clsCurrentFormControl:=clsControls.GetControl(intIndex);
intControlY:=0;
intLabelY:=6;
intHeight:=24+intLabelY; //spaces the controls

//show and populate each control for each property in list
 for strName in clsControls.dictProperties.Keys do
 begin
  //populate property controls and show
  //make sure below list is in the order you want them appear on the form
  //
  if strName = 'name' then
  begin
     self.EDTName.Text:=clsControls.clsCurrentFormControl.Name;
     self.EDTName.Top:=intControlY;
     self.LBLName.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.EDTName.Visible:=true;
     self.LBLName.Visible:=true;
  end;

  if strName = 'activebackgroundcolour' then
  begin
     self.CMBColourABackground.Color:=StringToColor(clsControls.clsCurrentFormControl.ActiveBackgroundColour);
     self.CMBColourABackground.Top:=intControlY;
     self.LBLColourABackground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourABackground.Visible:=true;
     self.LBLColourABackground.Visible:=true;
  end;
  if strName = 'activeforegroundcolour' then
  begin
     self.CMBColourAForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.ActiveForegroundColour);
     self.CMBColourAForeground.Top:=intControlY;
     self.LBLColourAForeground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourAForeground.Visible:=true;
     self.LBLColourAForeground.Visible:=true;
  end;

  if strName = 'backgroundcolour' then
  begin
     self.CMBColourBackground.Color:=StringToColor(clsControls.clsCurrentFormControl.BackgroundColour);
     self.CMBColourBackground.Top:=intControlY;
     self.LBLColourBackground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourBackground.Visible:=true;
     self.LBLColourBackground.Visible:=true;
  end;

  if strName = 'foregroundcolour' then
  begin
     self.CMBColourForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.ForegroundColour);
     self.CMBColourForeground.Top:=intControlY;
     self.LBLColourForeground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourForeground.Visible:=true;
     self.LBLColourForeground.Visible:=true;
  end;

  if strName = 'disabledbackgroundcolour' then
  begin
     self.CMBColourDBackground.Color:=StringToColor(clsControls.clsCurrentFormControl.DisabledBackgroundColour);
     self.CMBColourDBackground.Top:=intControlY;
     self.LBLColourDBackground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourDBackground.Visible:=true;
     self.LBLColourDBackground.Visible:=true;
  end;

  if strName = 'disabledforegroundcolour' then
  begin
     self.CMBColourDForeground.Color:=StringToColor(clsControls.clsCurrentFormControl.DisabledForegroundColour);
     self.CMBColourDForeground.Top:=intControlY;
     self.LBLColourDForeground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourDForeground.Visible:=true;
     self.LBLColourDForeground.Visible:=true;
  end;

  if strName = 'highlightbackgroundcolour' then
  begin
     self.CMBColourHighlightBackground.Color:=StringToColor(clsControls.clsCurrentFormControl.HighlightBackgroundColour);
     self.CMBColourHighlightBackground.Top:=intControlY;
     self.LBLColourHighlightBackground.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourHighlightBackground.Visible:=true;
     self.LBLColourHighlightBackground.Visible:=true;
  end;

  if strName = 'highlightcolour' then
  begin
     self.CMBColourHighlight.Color:=StringToColor(clsControls.clsCurrentFormControl.HighlightColour);
     self.CMBColourHighlight.Top:=intControlY;
     self.LBLColourHighlight.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourHighlight.Visible:=true;
     self.LBLColourHighlight.Visible:=true;
  end;

  if strName = 'selectcolour' then
  begin
     self.CMBColourSelect.Color:=StringToColor(clsControls.clsCurrentFormControl.SelectColour);
     self.CMBColourSelect.Top:=intControlY;
     self.LBLColourSelect.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourSelect.Visible:=true;
     self.LBLColourSelect.Visible:=true;
  end;

  if strName = 'textcolour' then
  begin
     self.CMBColourText.Color:=StringToColor(clsControls.clsCurrentFormControl.TextColour);
     self.CMBColourText.Top:=intControlY;
     self.LBLColourText.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBColourText.Visible:=true;
     self.LBLColourText.Visible:=true;
  end;

  if strName = 'text' then
  begin
     self.EDTText.Text:=clsControls.clsCurrentFormControl.Text;
     self.EDTText.Top:=intControlY;
     self.LBLText.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.EDTText.Visible:=true;
     self.LBLText.Visible:=true;
  end;

  if strName = 'anchor' then
  begin
     self.CMBAnchor.Text:=clsControls.clsCurrentFormControl.Anchor;
     self.CMBAnchor.Top:=intControlY;
     self.LBLAnchor.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBAnchor.Visible:=true;
     self.LBLAnchor.Visible:=true;
  end;

  if strName = 'font' then
  begin
     self.CMBFont.Text:=clsControls.clsCurrentFormControl.Font;
     self.CMBFont.Top:=intControlY;
     self.LBLFont.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBFont.Visible:=true;
     self.LBLFont.Visible:=true;
  end;

  if strName = 'fontsize' then
  begin
     self.SEDTFontSize.Value:=clsControls.clsCurrentFormControl.FontSize;
     self.SEDTFontSize.Top:=intControlY;
     self.LBLFontSize.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTFontSize.Visible:=true;
     self.LBLFontSize.Visible:=true;
  end;

  if strName = 'height' then
  begin
     self.SEDTHeight.Value:=clsControls.clsCurrentFormControl.Height;
     self.SEDTHeight.Top:=intControlY;
     self.LBLHeight.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTHeight.Visible:=true;
     self.LBLHeight.Visible:=true;
  end;

  if strName = 'width' then
  begin
     self.SEDTWidth.Value:=clsControls.clsCurrentFormControl.Width;
     self.SEDTWidth.Top:=intControlY;
     self.LBLWidth.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTWidth.Visible:=true;
     self.LBLWidth.Visible:=true;
  end;

  if strName = 'left' then
  begin
     self.SEDTLeft.Value:=clsControls.clsCurrentFormControl.Left;
     self.SEDTLeft.Top:=intControlY;
     self.LBLLeft.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTLeft.Visible:=true;
     self.LBLLeft.Visible:=true;
  end;

  if strName = 'top' then
  begin
     self.SEDTTop.Value:=clsControls.clsCurrentFormControl.Top;
     self.SEDTTop.Top:=intControlY;
     self.LBLTop.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTTop.Visible:=true;
     self.LBLTop.Visible:=true;
  end;

  if strName = 'borderwidth' then
  begin
     self.SEDTBorderWidth.Value:=clsControls.clsCurrentFormControl.BorderWidth;
     self.SEDTBorderWidth.Top:=intControlY;
     self.LBLBorderWidth.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTBorderWidth.Visible:=true;
     self.LBLBorderWidth.Visible:=true;
  end;

  if strName = 'highlightthickness' then
  begin
     self.SEDTHighlightThickness.Value:=clsControls.clsCurrentFormControl.HighlightThickness;
     self.SEDTHighlightThickness.Top:=intControlY;
     self.LBLHighlightThickness.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTHighlightThickness.Visible:=true;
     self.LBLHighlightThickness.Visible:=true;
  end;

  if strName = 'command' then
  begin
     self.EDTCommand.Text:=clsControls.clsCurrentFormControl.Command;
     self.EDTCommand.Top:=intControlY;
     self.LBLCommand.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.EDTCommand.Visible:=true;
     self.LBLCommand.Visible:=true;
  end;

  if strName = 'justify' then
  begin
     self.CMBJustify.Text:=clsControls.clsCurrentFormControl.Justify;
     self.CMBJustify.Top:=intControlY;
     self.LBLJustify.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBJustify.Visible:=true;
     self.LBLJustify.Visible:=true;
  end;

  if strName = 'overrelief' then
  begin
     self.CMBOverRelief.Text:=clsControls.clsCurrentFormControl.OverRelief;
     self.CMBOverRelief.Top:=intControlY;
     self.LBLOverrelief.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBOverRelief.Visible:=true;
     self.LBLOverRelief.Visible:=true;
  end;

  if strName = 'relief' then
  begin
     self.CMBRelief.Text:=clsControls.clsCurrentFormControl.Relief;
     self.CMBRelief.Top:=intControlY;
     self.LBLRelief.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBRelief.Visible:=true;
     self.LBLRelief.Visible:=true;
  end;

  if strName = 'max' then
  begin
     self.SEDTMaximumValue.Value:=clsControls.clsCurrentFormControl.Max;
     self.SEDTMaximumValue.Top:=intControlY;
     self.LBLMaximumValue.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTMaximumValue.Visible:=true;
     self.LBLMaximumValue.Visible:=true;
  end;

  if strName = 'min' then
  begin
     self.SEDTMinimumValue.Value:=clsControls.clsCurrentFormControl.Min;
     self.SEDTMinimumValue.Top:=intControlY;
     self.LBLMinimumValue.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTMinimumValue.Visible:=true;
     self.LBLMinimumValue.Visible:=true;
  end;

  if strName = 'padx' then
  begin
     self.SEDTPadX.Value:=clsControls.clsCurrentFormControl.PadX;
     self.SEDTPadX.Top:=intControlY;
     self.LBLPadX.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTPadX.Visible:=true;
     self.LBLPadX.Visible:=true;
  end;

  if strName = 'pady' then
  begin
     self.SEDTPadY.Value:=clsControls.clsCurrentFormControl.PadY;
     self.SEDTPadY.Top:=intControlY;
     self.LBLPadY.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTPadY.Visible:=true;
     self.LBLPadY.Visible:=true;
  end;

  if strName = 'wraplength' then
  begin
     self.SEDTWrapLength.Value:=clsControls.clsCurrentFormControl.WrapLength;
     self.SEDTWrapLength.Top:=intControlY;
     self.LBLWrapLength.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.SEDTWrapLength.Visible:=true;
     self.LBLWrapLength.Visible:=true;
  end;

  if strName = 'wrap' then
  begin
     self.CMBWrap.Text:=clsControls.clsCurrentFormControl.Wrap;
     self.CMBWrap.Top:=intControlY;
     self.LBLWrap.Top:=intControlY+intLabelY;
     intControlY:=intControlY+intHeight;
     self.CMBWrap.Visible:=true;
     self.LBLWrap.Visible:=true;
  end;

  if strName = 'xscrollcommand' then
  begin
//     self.EDTScrollBarX.Text:=clsControls.clsCurrentFormControl.ScrollBarX;
//     self.EDTScrollBarX.Top:=intControlY;
//     self.LBLScrollBarX.Top:=intControlY+intLabelY;
//     intControlY:=intControlY+intHeight;
//     self.EDTScrollBarX.Visible:=true;
//     self.LBLScrollBarX.Visible:=true;
  end;

  if strName = 'yscrollcommand' then
  begin
     self.CHKScrollBarY.Checked:=clsControls.clsCurrentFormControl.ScrollBarY;
     self.CHKScrollBarY.Top:=intControlY;
     intControlY:=intControlY+intHeight;
     self.CHKScrollBarY.Visible:=true;
  end;
 end;

 //re-enable onchange/onedit
 self.EDTText.OnChange:=OnPropertyChange;
 self.EDTCommand.OnChange:=OnPropertyChange;
 self.EDTName.OnChange:=OnPropertyChange;
 self.CMBColourBackground.OnChange:=OnPropertyChange;
 self.CMBColourForeground.OnChange:=OnPropertyChange;
 self.CMBColourABackground.OnChange:=OnPropertyChange;
 self.CMBColourAForeground.OnChange:=OnPropertyChange;
 self.CMBColourDForeground.OnChange:=OnPropertyChange;
 self.CMBColourText.OnChange:=OnPropertyChange;
 self.CMBJustify.OnChange:=OnPropertyChange;
 self.CMBFont.OnChange:=OnPropertyChange;
 self.SEDTFontSize.OnChange:=OnPropertyChange;
 self.SEDTHeight.OnChange:=OnPropertyChange;
 self.SEDTTop.OnChange:=OnPropertyChange;
 self.SEDTLeft.OnChange:=OnPropertyChange;
 self.SEDTWidth.OnChange:=OnPropertyChange;
 self.EDTText.OnExit:=OnPropertyExit;
 self.SEDTMinimumValue.OnChange:=OnPropertyChange;
 self.SEDTMaximumValue.OnChange:=OnPropertyChange;
 self.CMBAnchor.OnChange:=OnPropertyChange;
 self.SEDTBorderWidth.OnChange:=OnPropertyChange;
 self.CMBColourHighlightBackground.OnChange:=OnPropertyChange;
 self.CMBColourHighlight.OnChange:=OnPropertyChange;
 self.CMBOverRelief.OnChange:=OnPropertyChange;
 self.SEDTPadX.OnChange:=OnPropertyChange;
 self.SEDTPadY.OnChange:=OnPropertyChange;
 self.CMBColourSelect.OnChange:=OnPropertyChange;
 self.SEDTWrapLength.OnChange:=OnPropertyChange;
 self.CMBWrap.OnChange:=OnPropertyChange;
 //self.EDTScrollBarX.OnChange:=OnPropertyChange;
 self.CHKScrollBarY.OnClick:=CHKScrollBarYClick;
end;



//***form events etc***

procedure TfrmProperties.CHKScrollBarYClick(Sender: TObject);
begin
//update property
OnPropertyChange(Sender);
end;

procedure TfrmProperties.CMBControlsChange(Sender: TObject);
begin
 //get properties
 Show(clsControls.GetIndexByControlName(self.CMBControls.Text));
end;

procedure TfrmProperties.CMBFontDrawItem(Control: TWinControl; Index: Integer;
  Rect: TRect; State: TOwnerDrawState);
begin
  self.CMBFont.Canvas.FillRect(Rect);
  self.CMBFont.Canvas.Font.Name := self.CMBFont.Items[Index];
  self.CMBFont.Canvas.TextOut(Rect.Left + 2, Rect.Top + 2, self.CMBFont.Items[Index]);
end;

procedure TfrmProperties.FormCreate(Sender: TObject);
begin
 Init;
end;
end.
