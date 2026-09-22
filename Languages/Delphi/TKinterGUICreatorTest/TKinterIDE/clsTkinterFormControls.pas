unit clsTkinterFormControls;

interface
uses
  System.SysUtils, System.Classes, VCL.Graphics, System.Generics.Collections,
  VCL.ExtCtrls;


type
{
 Modified 13/05/2026 By Roger Williams

 clsFromControl class now has new property: ImageOnMainForm
 which represents the actual image in the IDE the control class relates too

 uses dictionaries for returning control class properties


 Created 07/06/2026 By Roger Williams

 contains BASIC control properties

 form control has extra properties:

 type      - button etc.
 imagename - name of image on main form that represents the control

 clsControls has property: intIndex which refers to a unique index number for the
 form control which equals its position in aryControls!

}

  TclsFormControl = class   //control on form/form
    private
      strName                      : string;
      strCommand                   : string;
      strText                      : string;
      strType                      : string;
      strImageName                 : string;
      strFont                      : string;
      strJustify                   : string;
      strRelief                    : string;
      strImageForPythonControl     : string;
      strAnchor                    : string;
      strOverRelief                : string;
      strOrient                    : string;
      strActiveBackgroundColour    : string;
      strBackgroundColour          : string;
      strForegroundColour          : string;
      strActiveForegroundColour    : string;
      strDisabledForegroundColour  : string;
      strDisabledBackgroundColour  : string;
      strTextColour                : string;
      strHighlightBackgroundColour : string;
      strHighlightColour           : string;
      strSelectColour              : string;
      strWrap                      : string;
//      strScrollBarX                : string;
//      strScrollBarY                : string;
      intFontSize                  : integer;
      intPadX                      : integer;
      intPadY                      : integer;
      intBorderWidth               : integer;
      intHighlightThickness        : integer;
      intWrapLength                : integer;
      intRepeatDelay               : integer;
      intRepeatInterval            : integer;
      intOnValue                   : integer;
      intOffValue                  : integer;
      intInsetOffTime              : integer;
      intInsertOnTime              : integer;
      intLeft                      : integer;
      intTop                       : integer;
      intHeight                    : integer;
      intWidth                     : integer;
      intMin                       : integer;
      intMax                       : integer;
      blnUnderline                 : boolean; //* unused
      blnScrollBarY                : boolean;
      IMGOnMainForm                : TImage;

      procedure SetImageName(strWhat : string);
      procedure SetControlName(strWhat : string);
      procedure SetControlText(strWhat : string);
      procedure SetControlType(strWhat : string);
      procedure SetCommand(strWhat : string);
      procedure SetLeft(intWhat : integer);
      procedure SetTop(intWhat : integer);
      procedure SetHeight(intWhat : integer);
      procedure SetMin(intWhat : integer);
      procedure SetMax(intWhat : integer);
      procedure SetWidth(intWhat : integer);
      procedure SetFont(strWhat : string);
      procedure SetJustify(strWhat : string);
      procedure SetActiveBackgroundColour(strWhat : string);
      procedure SetBackgroundColour(strWhat : string);
      procedure SetForegroundColour(strWhat : string);
      procedure SetActiveForegroundColour(strWhat : string);
      procedure SetDisabledBackgroundColour(strWhat : string);
      procedure SetDisabledForegroundColour(strWhat : string);
      procedure SetTextColour(strWhat : string);
      procedure SetWrap(strWhat : string);
      procedure SetRelatedImage(IMGWhat : TImage);
      procedure SetAnchor(strWhat : string);
      procedure SetOverRelief(strWhat : string);
      procedure SetOrient(strWhat : string);
      procedure SetFontSize(intValue : integer);
      procedure SetPadX(intValue : integer);
      procedure SetPadY(intValue : integer);
      procedure SetBorderWidth(intValue : integer);
      procedure SetHighlightThickness(intValue : integer);
      procedure SetWrapLength(intValue : integer);
      procedure SetRepeatDelay(intValue : integer);
      procedure SetRepeatInterval(intValue : integer);
      procedure SetOnValue(intValue : integer);
      procedure SetOffValue(intValue : integer);
      procedure SetInsertOffTime(intValue : integer);
      procedure SetInsertOnTime(intValue : integer);
      procedure SetUnderline(blnOk : boolean);
      procedure SetHighlightBackgroundColour(strWhat : string);
      procedure SetHighlightColour (strWhat : string);
      procedure SetImageForPythonControl(strWhat : string);
      procedure SetSelectColour(strWhat : string);
      procedure SetRelief(strWhat : string);
   //   procedure SetScrollBarX(strWhat : string);
      procedure SetScrollBarY(blnValue : boolean);

    public
      //used for recording position in array
      intIndex                              : integer;
      //index in mainforms components array
      intComponentIndex                     : integer;
      //contains font name and size for python write
      strFontPython                         : string;

      //custom internal properties
      property ControlType                  : string read strType write SetControlType;
      property Name                         : string read strName write SetControlName;
      property ImageName                    : string read strImageName write SetImageName;
      property ImageOnForm                  : TImage read IMGOnMainForm write SetRelatedImage;
      property ImageForPythonControl        : string read strImageForPythonControl write SetImageForPythonControl;
      //widget properties
      property Command                      : string read strCommand write SetCommand;
      property Text                         : string read strText write SetControlText;
      property Min                          : integer read intMin write SetMin;
      property Max                          : integer read intMax write SetMax;
      property Left                         : integer read intLeft write SetLeft;
      property Top                          : integer read intTop write SetTop;
      property Height                       : integer read intHeight write SetHeight;
      property Width                        : integer read intWidth write SetWidth;
      property Font                         : string read strFont write SetFont;
      property Justify                      : string read strJustify write SetJustify;
      property Relief                       : string read strRelief write SetRelief;
      property Anchor                       : string read strAnchor write SetAnchor;
      property Wrap                         : string read strWrap write SetWrap;
      property OverRelief                   : string read strOverRelief write SetOverRelief;
      property Orient                       : string read strOrient write SetOrient;
      property FontSize                     : integer read intFontSize write SetFontSize;
      property PadX                         : integer read intPadX write SetPadX;
      property PadY                         : integer read intPadY write SetPadY ;
      property BorderWidth                  : integer read intBorderWidth write SetBorderWidth;
      property HighlightThickness           : integer read intHighlightThickness write SetHighlightThickness;
      property WrapLength                   : integer read intWrapLength write SetWrapLength;
      property RepeatDelay                  : integer read intRepeatDelay write SetRepeatDelay;
      property RepeatInterval               : integer read intRepeatInterval write SetRepeatInterval;
      property OnValue                      : integer read intOnValue write SetOnValue;
      property OffValue                     : integer read intOffValue write SetOffValue;
      property InsertOffTime                : integer read intInsetOffTime write SetInsertOffTime;
      property InsertOnTime                 : integer read intInsertOnTime write SetInsertOnTime;
      property Underline                    : boolean read blnUnderline write SetUnderline;
   //   property ScrollBarX                   : string read strScrollBarX write SetScrollBarX;
      property ScrollBarY                   : boolean read blnScrollBarY write SetScrollBarY;
      //colours
      property ActiveBackgroundColour       : string read strActiveBackgroundColour write SetActiveBackgroundColour;
      property BackgroundColour             : string read strBackgroundColour write SetBackgroundColour;
      property ForegroundColour             : string read strForegroundColour write SetForegroundColour;
      property ActiveForegroundColour       : string read strActiveForegroundColour write SetActiveForegroundColour;
      property DisabledForegroundColour     : string read strDisabledForegroundColour write SetDisabledForegroundColour;
      property DisabledBackgroundColour     : string read strDisabledBackgroundColour write SetDisabledBackgroundColour;
      property TextColour                   : string read strTextColour write SetTextColour;
      property SelectColour                 : string read strSelectColour write SetSelectColour;
      property HighlightBackgroundColour    : string read strHighlightBackgroundColour write SetHighlightBackgroundColour;
      property HighlightColour              : string read strHighlightColour write SetHighlightColour;

   end;

type
{
 Created 07/05/2026 By Roger Williams

 used by forms to read/write control properties

 for returning control properties populates multi dim array with VALID
 properties for that control type e.g. button has command label does NOT

 has public property: mainform which represents PANForm on frmMain which is used
 in the IDE to house the control images the user drags

}
  TclsControls = class
     private
        aryControls             : array of TclsFormControl; //form controls
        intPropertiesPos        : integer;
        PANForm                 : TPanel;

        constructor Create;
        procedure SetMainForm(PANTemp : TPanel);
     public
       intControlPos            : integer;
       lstControls              : TStringList; //used by frmProps cmbcontrols
       //used by all forms to access currently selected control properties
       clsCurrentFormControl    : TclsFormControl;
       dictProperties           : TDictionary<string,string>;

       procedure ClearControls;
       procedure UpdateControl(intIndex : integer);
       procedure DeleteControl(intIndex : integer);
       procedure SetControl(clsFormControl : TclsFormControl);

       procedure ReturnProperties(intIndex : integer);
       procedure ReturnPropertiesByImageName(strName : string);


       function GetControl(intIndex : integer) : TclsFormControl;
       function CreateControl(clsFormControl : TclsFormControl) : boolean;
       function NotInArray(clsFormControl : TclsFormControl) : boolean;
       function GetIndexByControlName(strWhat : string) : integer;
       function GetIndexByImageName(strWhat : string) : integer;

       property MainForm        : TPanel read PANForm write SetMainForm;
  end;

//public shared procedure used by frmMain to create public clsControls class
procedure CreateControlsClass;

//public shared vars used by frmMain and frmProperties
const
  CNST_INT_IDE_HEIGHT = 809;
  CNST_INT_IDE_WIDTH = 1080;

var
  clsControls : TclsControls;

implementation

constructor TclsControls.Create;
begin
  intControlPos:=1;
  dictProperties:=TDictionary<string,string>.Create;
  lstControls:=TStringList.Create;
end;

//****class  TclsControls****


procedure TclsControls.ClearControls;
{
 Created 07/05/2026 By Roger Williams

 clears controls array


}
begin
 intControlPos:=1;
 Setlength(aryControls,0);
end;

function TclsControls.NotInArray(clsFormControl : TclsFormControl): Boolean;
{
 Created 07/05/2026 By Roger Williams

 checks if control already exists

 VARS

 clsFormControl : control class to find

}
var
  intNum    : integer;
  blnFound  : boolean;

begin
blnFound:=true;

  for intNum := 1 to intControlPos -1 do
  begin
    if aryControls[intNum].strName = clsFormControl.strName then
    begin
      blnFound:=false;
    end;

  end;

result:=blnFound;
end;

function TclsControls.CreateControl(clsFormControl : TclsFormControl) : boolean;
{
 Created 07/05/2026 By Roger Williams

 creates passed control in the controls array

 checks if control already exists if not adds to array

 VARS

 clsFormControl : control class to create

}

begin
if NotInArray(clsFormControl) then
   begin
     //set unique index
     clsFormControl.intIndex := intControlPos;
     //add to array
     Setlength(aryControls,intControlPos +1);
     aryControls[intControlPos]:=clsFormControl;
     Inc(intControlPos);
     //set to selected
     clsCurrentFormControl:=clsFormControl;
     //add to controls list
     lstControls.Add(clsFormControl.Name);
   end;
end;

procedure TclsControls.SetControl(clsFormControl: TclsFormControl);
{
 Created 10/05/2026 By Roger Williams

 sets clsCurrentFormControl to passed control class

 VARS

 clsFormControl : control class to set to current

}
begin
 //set to selected
 clsCurrentFormControl:=clsFormControl;
end;

procedure TclsControls.DeleteControl(intIndex: Integer);
{
 Created 07/05/2026 By Roger Williams

 finds passed control name in the controls array

 deletes it and repacks array and renumbers indexes

}
var
  intNum           : integer;
  intPos           : integer;
  clsFormControl   : TclsFormControl;
  aryControlsTemp  : array of TclsFormControl; //form controls

begin
//set temp array
SetLength(aryControlsTemp,intControlPos);
intPos:=1;

 for clsFormControl in aryControls do
 begin
   if clsFormControl <> nil then //element 0 always nil
     if clsFormControl.intIndex <> intIndex then
     begin
       aryControlsTemp[intPos] := clsFormControl;
       Inc(intPos);
     end;
 end;

Dec(intControlPos);
SetLength(aryControls,intControlPos);

//renumber indexes
for intNum := 1 to intControlPos -1 do
begin
    aryControls[intNum]:=aryControlsTemp[intNum];
    aryControls[intNum].intIndex := intNum;
end;
end;

procedure TclsControls.UpdateControl(intIndex : integer);
{
 Created 07/05/2026 By Roger Williams

 finds passed control index in the controls array

 replaces it with current selected control class

 VARS

 clsFormControl : control class to update

}

var
 intNum              : integer;
 clsFormControlFind  : TclsFormControl;

begin
  for clsFormControlFind in aryControls do
  begin
    if clsFormControlFind <> nil then
       if clsFormControlFind.intIndex = clsCurrentFormControl.intIndex then
       begin
         //update
         aryControls[clsFormControlFind.intIndex]:=clsCurrentFormControl;
       end;
  end;
end;



function TclsControls.GetControl(intIndex : integer): TclsFormControl;
{
 Created 07/05/2026 By Roger Williams

 populates a TclsFormControl with specified control index
 used by ReturnProperties function primarily


 VARS
 intIndex  - control to find

 RETURNS

 clsFormControl : control class to get properties for

}
var
 clsFormControl        : TclsFormControl;
 clsFormControlFound   : TclsFormControl;
 intNum                : integer;

begin
  for clsFormControl in aryControls do
  begin
    if clsFormControl <> nil then //element 0 always nil
       if clsFormControl.intIndex = intIndex then
       begin
         clsFormControlFound:=clsFormControl;
       end;
  end;

  result:=clsFormControlFound;
end;

procedure TclsControls.SetMainForm(PANTemp: TPanel);
{
  Created 06/05/2026 By Roger Williams

  sets panel that represents the mainform

}
begin
  PANForm:=PANTemp;
end;

procedure TclsControls.ReturnProperties(intIndex : integer);
{
 Created 07/05/2026 By Roger Williams

 populates dictProperties with VALID properties for passed control name

 e.g. button has command property label does NOT

 uses format:

 <property>:<value>

 VARS

 intIndex : index in control class to get properties for

 all properties available:

 activebackgroundcolour
 activeforegroundcolour
 DisabledForegroundColour
 textcolour
 justify
 font
 text
 name
 command
 height
 width
 top
 left


}
var
 clsFormControlFound  : TclsFormControl;

begin
clsFormControlFound:=GetControl(intIndex);

if clsFormControlFound <> nil then
begin
   intPropertiesPos:=1;
   clsControls.dictProperties.Clear;

   if clsFormControlFound.strType = 'button' then
   begin
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

   }
      //custom properties
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('justify',clsFormControlFound.strJustify);
      dictProperties.Add('overrelief',clsFormControlFound.strOverRelief);
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('command',clsFormControlFound.strCommand);
      dictProperties.Add('anchor',clsFormControlFound.strAnchor);
      dictProperties.Add('borderwidth',clsFormControlFound.intBorderWidth.ToString);
      dictProperties.Add('wraplength',clsFormControlFound.intWrapLength.ToString);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('padx',clsFormControlFound.intPadX.ToString);
      dictProperties.Add('pady',clsFormControlFound.intPadY.ToString);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'label' then
   begin
  {
        text: The text displayed on the label.
        bg: Sets the background color.
        fg: Sets the text color.
        font: Specifies the font style and size.
        width: Sets the width of the label.
        height: Sets the height of the label.
        padx: Adds horizontal padding.
        pady: Adds vertical padding.
        relief: Sets the border style (FLAT, RAISED, SUNKEN, etc.).
        wraplength - when to start word wrap in chars
  }
      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('wraplength',clsFormControlFound.intWrapLength.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('padx',clsFormControlFound.intPadX.ToString);
      dictProperties.Add('pady',clsFormControlFound.intPadY.ToString);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'spinbox' then
   begin
   {
    activebackground: This option used to represent the background color when the slider and arrowheads is under the cursor.
    bg: This option used to represent the normal background color displayed behind the label and indicator.
    bd: This option used to represent the size of the border around the indicator and the default value is 2 pixels.
    command: This option is associated with a function to be called when the state is changed.
    cursor: By using this option, the mouse cursor will change to that pattern when it is over the type.
    disabledforeground: This option used to represent the foreground color of the widget when it is disabled..
    disabledbackground: This option used to represent the background color of the widget when it is disabled..
    font: This option used to represent the font used for the text.
    fg: This option used to represent the color used to render the text.
    format: This option used to formatting the string and it's has no default value.
    from_: This option used to represent the minimum value.
    justify: This option used to control how the text is justified: CENTER, LEFT, or RIGHT.
    relief: This option used to represent the type of the border and It's default value is set to SUNKEN.
    repeatdelay: This option is used to control the button auto repeat and its default value is in milliseconds.
    repeatinterval: This option is similar to repeatdelay.
    state: This option used to represent the represents the state of the widget and its default value is NORMAL.
    textvariable: This option used to control the behaviour of the widget text.
    to: It specify the maximum limit of the widget value. The other is specified by the from_ option.
    validate: This option is used to control how the widget value is validated.
    validatecommand: This option is associated to the function callback which is used for the validation of the widget content.
    values: This option used to represent the tuple containing the values for this widget.
    vcmd: This option is same as validation command.
    width: This option is used to represents the width of the widget.
    wrap: This option wraps up the up and down button the Spinbox.
    xscrollcommand: This options is set to the set() method of scrollbar to make this widget horizontally scrollable.
   }

      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('text','0');     //set to zero as number
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
   //   dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('justify',clsFormControlFound.strJustify);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('min',clsFormControlFound.intMin.ToString);
      dictProperties.Add('max',clsFormControlFound.intMax.ToString);
      dictProperties.Add('wrap',clsFormControlFound.strWrap);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'text' then
   begin
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
    wrap - line break set to WORD or CHAR
    yscrollcommand - to make the widget vertically scrollable.
    xscrollcommand - to make the widget horizontally scrollable.
   }
     //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
    //  dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('wrap',clsFormControlFound.strWrap);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('padx',clsFormControlFound.intPadX.ToString);
      dictProperties.Add('pady',clsFormControlFound.intPadY.ToString);
      dictProperties.Add('highlightthickness',clsFormControlFound.intHighlightThickness.ToString);
   //   dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
   //   dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
    //  dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
   //   dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
    //  dictProperties.Add('xscrollcommand',clsFormControlFound.strScrollBarX);
      dictProperties.Add('yscrollcommand',clsFormControlFound.blnScrollBarY.ToString);


      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'listbox' then
   begin
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
   }
      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('highlightcolor', clsFormControlFound.strHighlightColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
      dictProperties.Add('xscrollcommand',clsFormControlFound.strFont);
      dictProperties.Add('yscrollcommand',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'frame' then
   begin
   {
    bg: Sets the background color of the frame.
    bd: Sets the width of the border around the frame (default is 2 pixels).
    cursor: Changes the mouse cursor when it moves over the frame.
    height: Sets the height of the frame.
    width: Sets the width of the frame.
    relief: Defines the border style (FLAT, RAISED, SUNKEN, etc.).
    highlightcolor: Sets the color of the focus border when the frame has focus.
    highlightbackground: Sets the color of the focus border when the frame does not have focus.
    highlightthickness: Sets the thickness (width) of the focus highlight border.
   }
     //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
  //    dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
  //    dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
  //    dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
  //    dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
  //    dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('highlightcolor', clsFormControlFound.strHighlightColour);
      dictProperties.Add('highlightbackground', clsFormControlFound.strHighlightBackgroundColour);
      dictProperties.Add('highlightthickness', clsFormControlFound.intHighlightThickness.ToString);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'labelframe' then
   begin
   {
      bg The normal background color displayed behind the label and indicator.
      bd The size of the border around the indicator. Default is 2 pixels.
      cursor If you set this option to a cursor name (arrow, dot etc.), the mouse cursor will change to that pattern when it is over the checkbutton.
      font The vertical dimension of the new frame.
      height The vertical dimension of the new frame.
      labelanchor Specifies where to place the label.
      highlightbackground  Color of the focus highlight when the frame does not have focus.
      highlightcolor Color shown in the focus highlight when the frame has the focus.
      highlightthickness Thickness of the focus highlight.
      relief With the default value, relief=FLAT, the checkbutton does not stand out from its background. You may set this option to any of the other styles.
      text Specifies a string to be displayed inside the widget.
      width Specifies the desired width for the window.
   }
     //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('labelanchor',clsFormControlFound.strAnchor);
//      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
//      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
//      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
//      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
//      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
//      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('highlightcolor', clsFormControlFound.strHighlightColour);
      dictProperties.Add('highlightbackground', clsFormControlFound.strHighlightBackgroundColour);
      dictProperties.Add('highlightthickness', clsFormControlFound.intHighlightThickness.ToString);
//      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
//      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;

   if clsFormControlFound.strType = 'checkbutton' then
   begin
   {
    activebackground: This option used to represent the background color when the checkbutton is under the cursor.
    activeforeground: This option used to represent the foreground color when the checkbutton is under the cursor.
    bg: This option used to represent the normal background color displayed behind the label and indicator.
    bitmap: This option used to display a monochrome image on a button.
    bd: This option used to represent the size of the border around the indicator and the default value is 2 pixels.
    command: This option is associated with a function to be called when the state of the checkbutton is changed.
    cursor: By using this option, the mouse cursor will change to that pattern when it is over the checkbutton.
    disabledforeground: The foreground color used to render the text of a disabled checkbutton. The default is a stippled version of the default foreground color.
    font: This option used to represent the font used for the text.
    fg: This option used to represent the color used to render the text.
    height: This option used to represent the number of lines of text on the checkbutton and it's default value is 1.
    highlightcolor: This option used to represent the color of the focus highlight when the checkbutton has the focus.
    image: This option used to display a graphic image on the button.
    justify: This option used to control how the text is justified: CENTER, LEFT, or RIGHT.
    offvalue: The associated control variable is set to 0 by default if the button is unchecked. We can change the state of an unchecked variable to some other one.
    onvalue: The associated control variable is set to 1 by default if the button is checked. We can change the state of the checked variable to some other one.
    padx: This option used to represent how much space to leave to the left and right of the checkbutton and text. It's default value is 1 pixel.
    pady: This option used to represent how much space to leave above and below the checkbutton and text. It's default value is 1 pixel.
    relief: The type of the border of the checkbutton. It's default value is set to FLAT.
    selectcolor: This option used to represent the color of the checkbutton when it is set. The Default is selectcolor="red".
    selectimage: The image is shown on the checkbutton when it is set.
    state: It represents the state of the checkbutton. By default, it is set to normal. We can change it to DISABLED to make the checkbutton unresponsive. The state of the checkbutton is ACTIVE when it is under focus.
    text: This option used use newlines ("\n") to display multiple lines of text.
    underline: This option used to represent the index of the character in the text which is to be underlined. The indexing starts with zero in the text.
    variable: This option used to represents the associated variable that tracks the state of the checkbutton.
    width: This option used to represents the width of the checkbutton. and also represented in the number of characters that are represented in the form of texts.
    wraplength: This option will be broken text into the number of pieces.
   }
      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('justify',clsFormControlFound.strJustify);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('padx',clsFormControlFound.intPadX.ToString);
      dictProperties.Add('pady',clsFormControlFound.intPadY.ToString);
      dictProperties.Add('wraplength',clsFormControlFound.intWrapLength.ToString);
      dictProperties.Add('selectcolor', clsFormControlFound.strSelectColour);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
       dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');

   end;


   if clsFormControlFound.strType = 'radiobutton' then
   begin
   {
      activebackground The background color when the mouse is over the radiobutton.
      activeforeground The foreground color when the mouse is over the radiobutton.
      anchor If the widget inhabits a space larger than it needs, this option specifies where the radiobutton will sit in that space. The default is anchor=CENTER.
      bg The normal background color behind the indicator and label.
      bitmap To display a monochrome image on a radiobutton, set this option to a bitmap.
      borderwidth The size of the border around the indicator part itself. Default is 2 pixels.
      command A procedure to be called every time the user changes the state of this radiobutton.
      cursor If you set this option to a cursor name (arrow, dot etc.), the mouse cursor will change to that pattern when it is over the radiobutton.
      font The font used for the text.
      fg The color used to render the text.
      height The number of lines (not pixels) of text on the radiobutton. Default is 1.
      highlightbackground The color of the focus highlight when the radiobutton does not have focus.
      highlightcolor The color of the focus highlight when the radiobutton has the focus.
      image To display a graphic image instead of text for this radiobutton, set this option to an image object.
      justify If the text contains multiple lines, this option controls how the text is justified: CENTER (the default), LEFT, or RIGHT.
      padx How much space to leave to the left and right of the radiobutton and text. Default is 1.
      pady How much space to leave above and below the radiobutton and text. Default is 1.
      relief Specifies the appearance of a decorative border around the label. The default is FLAT; for other values.
      selectcolor The color of the radiobutton when it is set. Default is red.
      selectimage If you are using the image option to display a graphic instead of text when the radiobutton is cleared, you can set the selectimage option to a different image that will be displayed when the radiobutton is set.
      state The default is state=NORMAL, but you can set state=DISABLED to gray out the control and make it unresponsive. If the cursor is currently over the radiobutton, the state is ACTIVE.
      text The label displayed next to the radiobutton. Use newlines ("\n") to display multiple lines of text.
      textvariable To slave the text displayed in a label widget to a control variable of class StringVar, set this option to that variable.
      underline You can display an underline (_) below the nth letter of the text, counting from 0, by setting this option to n. The default is underline=-1, which means no underlining.
      value When a radiobutton is turned on by the user, its control variable is set to its current value option. If the control variable is an IntVar, give each radiobutton in the group a different integer value option. If the control variable is aStringVar, give each radiobutton a different string value option.
      variable The control variable that this radiobutton shares with the other radiobuttons in the group. This can be either an IntVar or a StringVar.
      width Width of the label in characters (not pixels!). If this option is not set, the label will be sized to fit its contents.
      wraplength You can limit the number of characters in each line by setting this option to the desired number. The default value, 0, means that lines will be broken only at newlines.
   }
       //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('relief',clsFormControlFound.strRelief);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('justify',clsFormControlFound.strJustify);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('padx',clsFormControlFound.intPadX.ToString);
      dictProperties.Add('pady',clsFormControlFound.intPadY.ToString);
      dictProperties.Add('anchor',clsFormControlFound.strAnchor);
      dictProperties.Add('wraplength',clsFormControlFound.intWrapLength.ToString);
      dictProperties.Add('selectcolor', clsFormControlFound.strSelectColour);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');


   end;


   if clsFormControlFound.strType = 'combobox' then
   begin
   {
    state – normal, readonly, or disabled. Readonly prevents typing.
    values – list of choices, can be strings, ints, etc.
    width – width of the widget in characters
    height – number of menu items visible before scrolling
    font – font family and size string
    foreground – text color
    justify – left/right/center alignment of text
    background – background color of input field
   }
      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      dictProperties.Add('justify',clsFormControlFound.strJustify);
      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
      dictProperties.Add('background', clsFormControlFound.strBackgroundColour);
      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
      dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');


   end;

   //also handle internal name for multi column version: listview
   if (clsFormControlFound.strType = 'treeview') or (clsFormControlFound.strType = 'listview') then
   begin
   {
    activebackground: This option used to represent the background color when the slider and arrowheads is under the cursor.
    bg: This option used to represent the normal background color displayed behind the label and indicator.
    bd: This option used to represent the size of the border around the indicator and the default value is 2 pixels.
    fg The color used to render the text.
    disabledforeground: This option used to represent the foreground color of the widget when it is disabled..
    disabledbackground: This option used to represent the background color of the widget when it is disabled..
    height
    width: This option is used to represents the width of the widget.
   }

      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
//      dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
//      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
//      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
//      dictProperties.Add('justify',clsFormControlFound.strJustify);
//      dictProperties.Add('activebackground', clsFormControlFound.strActiveBackgroundColour);
//      dictProperties.Add('activeforeground', clsFormControlFound.strActiveForegroundColour);
//      dictProperties.Add('disabledforeground', clsFormControlFound.strDisabledForegroundColour);
//      dictProperties.Add('disabledbackground', clsFormControlFound.strDisabledBackgroundColour);
//      dictProperties.Add('foreground', clsFormControlFound.strTextColour);
//      dictProperties.Add('font',clsFormControlFound.strFont);
      //make compound property value from font and fontsize
      //buttonFont = font.Font(family='Tahoma', size=20, underline=1)
     // dictProperties.Add('fontpython','Tkfont.Font(family="' + clsFormControlFound.Font + '", size=' + clsFormControlFound.FontSize.ToString + ')');
   end;

   if clsFormControlFound.strType = 'form' then
   begin
   {

   }
      //custom properties
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('name',clsFormControlFound.strName);
     // dictProperties.Add('fontsize',clsFormControlFound.intFontSize.ToString);
      //standard properties
      dictProperties.Add('title',clsFormControlFound.strText);
      dictProperties.Add('left',clsFormControlFound.intLeft.ToString);
      dictProperties.Add('top',clsFormControlFound.intTop.ToString);
      dictProperties.Add('height',clsFormControlFound.intHeight.ToString);
      dictProperties.Add('width',clsFormControlFound.intWidth.ToString);
      //make compound property value from font and fontsize
    //  dictProperties.Add('font',clsFormControlFound.Font + ', (' + clsFormControlFound.FontSize.ToString + ') ');
   end;


end;

end;

procedure TclsControls.ReturnPropertiesByImageName(strName : string);
{
 Created 07/05/2026 By Roger Williams

 populates dictProperties with VALID properties for passed image name

 e.g. button has command property label does NOT

 uses format:

 <property>:<value>

 VARS

 strName  - image name to find in aryControls


 all properties available:

 activebackgroundcolour
 activeforegroundcolour
 DisabledForegroundColour
 textcolour
 justify
 font
 text
 name
 command
 height
 width
 top
 left


}
var
 clsFormControlFound  : TclsFormControl;

  function GetControlForImage : TclsFormControl;
  {
   Created 07/05/2026 By Roger Williams

   populates a TclsFormControl with specified data
   used by ReturnPropertiesByImageName function primarily

   RETURNS

   clsFormControl : control class to get properties for

  }
  var
   clsFormControl  : TclsFormControl;
   blnOk           : boolean;

begin
  blnOk:=false;

    for clsFormControl in aryControls do
    begin
      if clsFormControl <> nil then //element 0 always nil
        if clsFormControl.ImageName = strName then
        begin
          blnOk:=true;
        end;
    end;

 if blnOk then
    result:=clsFormControl
 else
    result:=nil;
end;


begin
clsFormControlFound:=GetControlForImage;

if clsFormControlFound <> nil then
begin
 ReturnProperties(clsFormControlFound.intIndex);

 {
   intPropertiesPos:=1;
   clsControls.dictProperties.Clear;

   if clsFormControlFound.strType = 'button' then
   begin
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('command',clsFormControlFound.strCommand);
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('left',IntToStr(clsFormControlFound.intLeft));
      dictProperties.Add('top',IntToStr(clsFormControlFound.intTop));
      dictProperties.Add('height',IntToStr(clsFormControlFound.intHeight));
      dictProperties.Add('width',IntToStr(clsFormControlFound.intWidth));
   end;

   if clsFormControlFound.strType = 'form' then
   begin
      dictProperties.Add('name',clsFormControlFound.strName);
      dictProperties.Add('text',clsFormControlFound.strText);
      dictProperties.Add('type',clsFormControlFound.strType);
      dictProperties.Add('left',IntToStr(clsFormControlFound.intLeft));
      dictProperties.Add('top',IntToStr(clsFormControlFound.intTop));
      dictProperties.Add('height',IntToStr(clsFormControlFound.intHeight));
      dictProperties.Add('width',IntToStr(clsFormControlFound.intWidth));

   end;    }
end;

end;

function TclsControls.GetIndexByImageName(strWhat : string) : integer;
{
  Created 25/05/2026 By Roger Williams

  returns clsFormComponent.intIndex for passed control name
  used by frmProps when CMBControls is changed

  VARS

  strWhat - control to get component index of

}
var
   clsFormControl  : TclsFormControl;
   intNum          : integer;

begin
  for clsFormControl in aryControls do
  begin
    if clsFormControl <> nil then //element 0 always nil
      if clsFormControl.ImageName = strWhat then
      begin
       //set current control
       clsCurrentFormControl:=aryControls[clsFormControl.intIndex];
      end;
  end;

 result:=clsCurrentFormControl.intIndex;
end;

function TclsControls.GetIndexByControlName(strWhat : string) : integer;
{
  Created 13/05/2026 By Roger Williams

  returns clsFormComponent.intIndex for passed control name
  used by frmProps when CMBControls is changed

  VARS

  strWhat - control to get component index of

}
var
   clsFormControl  : TclsFormControl;
   intNum          : integer;

begin
  for clsFormControl in aryControls do
  begin
    if clsFormControl <> nil then //element 0 always nil
      if clsFormControl.Name = strWhat then
      begin
       //set current control
       clsCurrentFormControl:=aryControls[clsFormControl.intIndex];
      end;
  end;

 result:=clsCurrentFormControl.intIndex;
end;

//*********end clsControls*******



//****TclsFormControl ****
procedure TclsFormControl.SetControlType(strWhat : string);
{
 Created 07/05/2026 By Roger Williams

 sets control type e.g. button

}

begin
  strType:= strWhat;
end;

procedure TclsFormControl.SetRelatedImage(IMGWhat : TImage);
{
 Created 13/05/2026 By Roger Williams

 sets image which is used in IDE to represent control to user

}

begin
 IMGOnMainForm:=IMGWhat;
end;

procedure TclsFormControl.SetControlName(strWhat : string);
{
 Created 07/05/2026 By Roger Williams

 sets control name

}

begin
  strName := strWhat;
end;

procedure TclsFormControl.SetImageName(strWhat : string);
{
 Created 07/05/2026 By Roger Williams

 sets image name that links control to image on main for,

}

begin
  strImageName := strWhat;
end;

procedure TclsFormControl.SetFont(strWhat : string);
{
 Created 10/05/2026 By Roger Williams

 sets font name

}

begin
  strFont := strWhat;
end;

procedure TclsFormControl.SetJustify(strWhat : string);
{
 Created 10/05/2026 By Roger Williams

 sets justify type

}

begin
  strJustify := strWhat;
end;

procedure TclsFormControl.SetCommand(strWhat : string);
{
 Created 07/05/2026 By Roger Williams

 sets control command i.e. button click event

}

begin
  strCommand := strWhat;
end;


procedure TclsFormControl.SetControlText(strWhat : string);
{
 Created 07/05/2026 By Roger Williams

 sets control text

}

begin
  strText := strWhat;
end;

procedure TclsFormControl.SetLeft(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control left

}

begin
  intLeft := intWhat;
end;

procedure TclsFormControl.SetTop(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control top

}

begin
  intTop := intWhat;
end;

procedure TclsFormControl.SetMin(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control min value allowed

}

begin
  intMin:= intWhat;
end;

procedure TclsFormControl.SetMax(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control max value allowed

}

begin
  intMax:= intWhat;
end;

procedure TclsFormControl.SetHeight(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control height

}

begin
  intHeight := intWhat;
end;

procedure TclsFormControl.SetWidth(intWhat : integer);
{
 Created 07/05/2026 By Roger Williams

 sets control width

}

begin
  intWidth := intWhat;
end;

procedure TclsFormControl.SetActiveBackgroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls ActiveBackgroundColour

}

begin
 strActiveBackgroundColour:=strWhat;
end;

procedure TclsFormControl.SetBackgroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls BackgroundColour

}

begin
 strBackgroundColour:=strWhat;
end;

procedure TclsFormControl.SetForegroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls ForegroundColour

}

begin
 strForegroundColour:=strWhat;
end;


procedure TclsFormControl.SetDisabledForegroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls DisabledForegroundColour

}

begin
 strDisabledForegroundColour:=strWhat;
end;

procedure TclsFormControl.SetDisabledBackgroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls DisabledBackgroundColour

}

begin
 strDisabledBackgroundColour:=strWhat;
end;



procedure TclsFormControl.SetActiveForegroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls ActiveForegroundColour

}

begin
 strActiveForegroundColour:=strWhat;
end;

procedure TclsFormControl.SetTextColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls TextColour

}

begin
 strTextColour:=strWhat;
end;

procedure TclsFormControl.SetSelectColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls SelectColour

}

begin
 strSelectColour:=strWhat;
end;

procedure TclsFormControl.SetHighlightBackgroundColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls HighlightBackground

}

begin
 strHighlightBackgroundColour:=strWhat;
end;

procedure TclsFormControl.SetHighlightColour(strWhat: string);
{
 Created 10/05/2026 By Roger Williams

 sets controls HighlightColour

}

begin
 strHighlightColour:=strWhat;
end;

procedure TclsFormControl.SetAnchor(strWhat : string);
{
 Created 14/05/2026 By Roger Williams

 set Anchor

}
begin
 strAnchor:=strWhat;
end;

procedure TclsFormControl.SetWrap(strWhat : string);
{
 Created 14/05/2026 By Roger Williams

 set wrap

}
begin
 strWrap:=strWhat;
end;

procedure TclsFormControl.SetOverRelief(strWhat : string);
{
 Created 14/05/2026 By Roger Williams

  set OverRelief

}
begin
 strOverRelief:=strWhat;
end;

procedure TclsFormControl.SetOrient(strWhat : string);
{
 Created 14/05/2026 By Roger Williams

   set Orient

}
begin
 strOrient:=strWhat;
end;


procedure TclsFormControl.SetFontSize(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

  set FontSize

}
begin
 intFontSize:=intValue;
end;


procedure TclsFormControl.SetPadX(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

   set PadX

}
begin
 intPadX:=intValue;
end;


procedure TclsFormControl.SetPadY(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

  set PadY

}
begin
 intPadY:=intValue;
end;


procedure TclsFormControl.SetBorderWidth(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

    set BorderWidth

}
begin
 intBorderWidth:=intValue;
end;


procedure TclsFormControl.SetHighlightThickness(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

   set HighlightThickness

}
begin
 intHighlightThickness:=intValue;
end;


procedure TclsFormControl.SetWrapLength(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

   set WrapLength

}
begin
 intWrapLength:=intValue;
end;


procedure TclsFormControl.SetRepeatDelay(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

 set RepeatDelay

}
begin
 intRepeatDelay:=intValue;
end;


procedure TclsFormControl.SetRepeatInterval(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

  set RepeatInterval

}
begin
 intRepeatInterval:=intValue;
end;


procedure TclsFormControl.SetOnValue(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

   set OnValue

}
begin
 intOnValue:=intValue;
end;


procedure TclsFormControl.SetOffValue(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

  set OffValue

}
begin
 intOffValue:=intValue;
end;


procedure TclsFormControl.SetInsertOffTime(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

  set InsetOffTime

}
begin
 intInsetOffTime:=intValue;
end;


procedure TclsFormControl.SetInsertOnTime(intValue : integer);
{
 Created 14/05/2026 By Roger Williams

   set InsertOnTime

}
begin
 intInsertOnTime:=intValue;
end;


procedure TclsFormControl.SetUnderline(blnOk : boolean);
{
 Created 14/05/2026 By Roger Williams

    set Underline

}
begin
 blnUnderline:=blnok;
end;

//procedure TclsFormControl.SetScrollBarX(strWhat : string);
//{
// Created 26/05/2026 By Roger Williams
//
//    set strScrollBarX
//
//}
//begin
// strScrollBarX:=strWhat;
//end;


procedure TclsFormControl.SetScrollBarY(blnValue : boolean);
{
 Created 26/05/2026 By Roger Williams

    set strScrollBarY

}
begin
 blnScrollBarY:=blnValue;
end;

procedure TclsFormControl.SetImageForPythonControl(strWhat : string);
{
 Created 14/05/2026 By Roger Williams

   set ImageForPythonControl

}
begin
 strImageForPythonControl:=strWhat;
end;


procedure TclsFormControl.SetRelief(strWhat : string);
{
 Created 14/05/2026 By Roger Williams


 set Relief
}
begin
 strRelief:=strWhat;
end;







//*********end clsFormControl*******




//public procedure not in a class that CREATES clsControls so forms can use
//a SINGLE public copy
procedure CreateControlsClass;
begin
  clsControls:=TclsControls.Create;
end;
end.
