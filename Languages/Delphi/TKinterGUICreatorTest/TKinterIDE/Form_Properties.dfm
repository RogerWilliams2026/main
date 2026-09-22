object frmProperties: TfrmProperties
  Left = 0
  Top = 0
  BorderIcons = []
  BorderStyle = bsSingle
  Caption = 'Properties'
  ClientHeight = 854
  ClientWidth = 298
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  Visible = True
  OnCreate = FormCreate
  TextHeight = 15
  object SPTSplitter: TSplitter
    Left = 161
    Top = 41
    Width = 2
    Height = 813
    Beveled = True
    ResizeStyle = rsLine
    ExplicitTop = 0
    ExplicitHeight = 731
  end
  object PANProperty: TPanel
    Left = 0
    Top = 41
    Width = 161
    Height = 813
    Align = alLeft
    BevelOuter = bvNone
    TabOrder = 0
    object LBLColourABackground: TLabel
      Left = 0
      Top = 408
      Width = 142
      Height = 15
      Caption = 'Active Background Colour:'
    end
    object LBLColourAForeground: TLabel
      Left = 0
      Top = 435
      Width = 140
      Height = 15
      Caption = 'Active Foreground Colour:'
    end
    object LBLColourDForeground: TLabel
      Left = 0
      Top = 462
      Width = 152
      Height = 15
      Caption = 'Disabled Foreground Colour:'
    end
    object LBLText: TLabel
      Left = 1
      Top = 28
      Width = 24
      Height = 15
      Caption = 'Text:'
    end
    object LBLColourText: TLabel
      Left = 0
      Top = 515
      Width = 63
      Height = 15
      Caption = 'Text Colour:'
    end
    object LBLCommand: TLabel
      Left = 1
      Top = 51
      Width = 60
      Height = 15
      Caption = 'Command:'
    end
    object LBLHeight: TLabel
      Left = 1
      Top = 117
      Width = 39
      Height = 15
      Caption = 'Height:'
    end
    object LBLWidth: TLabel
      Left = 1
      Top = 143
      Width = 35
      Height = 15
      Caption = 'Width:'
    end
    object LBLTop: TLabel
      Left = 1
      Top = 170
      Width = 22
      Height = 15
      Caption = 'Top:'
    end
    object LBLLeft: TLabel
      Left = 1
      Top = 194
      Width = 23
      Height = 15
      Caption = 'Left:'
    end
    object LBlName: TLabel
      Left = 1
      Top = 6
      Width = 35
      Height = 15
      Caption = 'Name:'
    end
    object LBLJustify: TLabel
      Left = 1
      Top = 216
      Width = 36
      Height = 15
      Caption = 'Justify:'
    end
    object LBLFont: TLabel
      Left = 1
      Top = 237
      Width = 27
      Height = 15
      Caption = 'Font:'
    end
    object LBLFontSize: TLabel
      Left = 1
      Top = 256
      Width = 50
      Height = 15
      Caption = 'Font Size:'
    end
    object LBLColourDBackground: TLabel
      Left = 0
      Top = 488
      Width = 154
      Height = 15
      Caption = 'Disabled Background Colour:'
    end
    object LBLMinimumValue: TLabel
      Left = 0
      Top = 298
      Width = 83
      Height = 15
      Caption = 'Minimun Value:'
    end
    object LBLMaximumValue: TLabel
      Left = -2
      Top = 321
      Width = 89
      Height = 15
      Caption = 'Maximum Value:'
    end
    object LBLColourBackground: TLabel
      Left = -1
      Top = 349
      Width = 106
      Height = 15
      Caption = 'Background Colour:'
    end
    object LBLColourForeground: TLabel
      Left = 0
      Top = 378
      Width = 104
      Height = 15
      Caption = 'Foreground Colour:'
    end
    object LBLAnchor: TLabel
      Left = 0
      Top = 536
      Width = 42
      Height = 15
      Caption = 'Anchor:'
    end
    object LBLColourHighlightBackground: TLabel
      Left = 0
      Top = 557
      Width = 159
      Height = 15
      Caption = 'Highlight Background Colour:'
    end
    object LBLColourHighlight: TLabel
      Left = 0
      Top = 589
      Width = 92
      Height = 15
      Caption = 'Highlight Colour:'
    end
    object LBLHighlightThickness: TLabel
      Left = 0
      Top = 621
      Width = 107
      Height = 15
      Caption = 'Highlight Thickness:'
    end
    object LBLOverRelief: TLabel
      Left = 0
      Top = 653
      Width = 60
      Height = 15
      Caption = 'Over Relief:'
    end
    object LBLPadX: TLabel
      Left = 0
      Top = 680
      Width = 30
      Height = 15
      Caption = 'PadX:'
    end
    object LBLPadY: TLabel
      Left = 0
      Top = 709
      Width = 30
      Height = 15
      Caption = 'PadY:'
    end
    object LBLRelief: TLabel
      Left = 0
      Top = 733
      Width = 32
      Height = 15
      Caption = 'Relief:'
    end
    object LBLColourSelect: TLabel
      Left = 0
      Top = 754
      Width = 73
      Height = 15
      Caption = 'Select Colour:'
    end
    object LBLWrapLength: TLabel
      Left = 0
      Top = 775
      Width = 71
      Height = 15
      Caption = 'Wrap Length:'
    end
    object LBLWrap: TLabel
      Left = 0
      Top = 796
      Width = 31
      Height = 15
      Caption = 'Wrap:'
    end
    object LBLBorderWidth: TLabel
      Left = 0
      Top = 277
      Width = 73
      Height = 15
      Caption = 'Border Width:'
    end
    object LBLScrollBarX: TLabel
      Left = 0
      Top = 73
      Width = 94
      Height = 15
      Caption = 'ScrollBar X Name:'
    end
  end
  object PANValues: TPanel
    Left = 163
    Top = 41
    Width = 135
    Height = 813
    Align = alClient
    BevelOuter = bvNone
    TabOrder = 1
    object CMBColourABackground: TColorBox
      Left = 0
      Top = 410
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 0
    end
    object CMBColourAForeground: TColorBox
      Left = 0
      Top = 440
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 1
    end
    object CMBColourDForeground: TColorBox
      Left = 0
      Top = 467
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 2
    end
    object EDTText: TEdit
      Left = 0
      Top = 23
      Width = 131
      Height = 23
      TabOrder = 3
    end
    object CMBColourText: TColorBox
      Left = 0
      Top = 519
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 4
    end
    object EDTCommand: TEdit
      Left = 0
      Top = 46
      Width = 131
      Height = 23
      TabOrder = 5
    end
    object SEDTHeight: TSpinEdit
      Left = 0
      Top = 116
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 6
      Value = 0
    end
    object SEDTTop: TSpinEdit
      Left = 0
      Top = 168
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 7
      Value = 0
    end
    object SEDTLeft: TSpinEdit
      Left = 0
      Top = 194
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 8
      Value = 0
    end
    object EDTName: TEdit
      Left = 0
      Top = 0
      Width = 131
      Height = 23
      TabOrder = 9
    end
    object CMBJustify: TComboBox
      Left = 0
      Top = 217
      Width = 113
      Height = 23
      Style = csDropDownList
      TabOrder = 10
      Items.Strings = (
        'LEFT'
        'CENTER'
        'RIGHT')
    end
    object CMBFont: TComboBox
      Left = 0
      Top = 239
      Width = 130
      Height = 23
      Style = csDropDownList
      DropDownWidth = 250
      TabOrder = 11
      OnDrawItem = CMBFontDrawItem
    end
    object SEDTWidth: TSpinEdit
      Left = 0
      Top = 142
      Width = 57
      Height = 24
      MaxLength = 4
      MaxValue = 9999
      MinValue = 0
      TabOrder = 12
      Value = 0
    end
    object SEDTFontSize: TSpinEdit
      Left = 0
      Top = 262
      Width = 57
      Height = 24
      MaxValue = 72
      MinValue = 0
      TabOrder = 13
      Value = 0
    end
    object CMBColourDBackground: TColorBox
      Left = 0
      Top = 493
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 14
    end
    object SEDTMinimumValue: TSpinEdit
      Left = 0
      Top = 296
      Width = 57
      Height = 24
      MaxValue = 72
      MinValue = 0
      TabOrder = 15
      Value = 0
    end
    object SEDTMaximumValue: TSpinEdit
      Left = 0
      Top = 322
      Width = 100
      Height = 24
      MaxValue = 72
      MinValue = 0
      TabOrder = 16
      Value = 0
    end
    object CMBColourBackground: TColorBox
      Left = 0
      Top = 352
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 17
    end
    object CMBColourForeground: TColorBox
      Left = 0
      Top = 381
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 18
    end
    object CMBAnchor: TComboBox
      Left = 0
      Top = 540
      Width = 113
      Height = 23
      Style = csDropDownList
      TabOrder = 19
      Items.Strings = (
        'CENTER'
        'N'
        'NE'
        'E'
        'SE'
        'S'
        'SW'
        'W'
        'NW')
    end
    object CMBColourHighlightBackground: TColorBox
      Left = 0
      Top = 569
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 20
    end
    object CMBColourHighlight: TColorBox
      Left = 0
      Top = 597
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 21
    end
    object SEDTHighlightThickness: TSpinEdit
      Left = 0
      Top = 625
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 22
      Value = 0
    end
    object CMBOverRelief: TComboBox
      Left = 0
      Top = 655
      Width = 113
      Height = 23
      Style = csDropDownList
      TabOrder = 23
      Items.Strings = (
        'FLAT'
        'RAISED'
        'SUNKEN'
        'GROOVE'
        'RIDGE')
    end
    object SEDTPadX: TSpinEdit
      Left = 0
      Top = 678
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 24
      Value = 0
    end
    object SEDTPadY: TSpinEdit
      Left = 0
      Top = 707
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 25
      Value = 0
    end
    object CMBRelief: TComboBox
      Left = 0
      Top = 732
      Width = 113
      Height = 23
      Style = csDropDownList
      TabOrder = 26
      Items.Strings = (
        'FLAT'
        'RAISED'
        'SUNKEN'
        'GROOVE'
        'RIDGE')
    end
    object CMBColourSelect: TColorBox
      Left = 0
      Top = 756
      Width = 101
      Height = 22
      DropDownWidth = 140
      TabOrder = 27
    end
    object SEDTWrapLength: TSpinEdit
      Left = 0
      Top = 779
      Width = 57
      Height = 24
      MaxValue = 9999
      MinValue = 0
      TabOrder = 28
      Value = 0
    end
    object CMBWrap: TComboBox
      Left = 0
      Top = 804
      Width = 113
      Height = 23
      Style = csDropDownList
      TabOrder = 29
      Items.Strings = (
        'CHAR'
        'WORD')
    end
    object SEDTBorderWidth: TSpinEdit
      Left = 0
      Top = 274
      Width = 57
      Height = 24
      MaxValue = 72
      MinValue = 0
      TabOrder = 30
      Value = 0
    end
    object CHKScrollBarY: TCheckBox
      Left = 4
      Top = 69
      Width = 89
      Height = 25
      Caption = 'ScrollBar Y'
      TabOrder = 31
      OnClick = CHKScrollBarYClick
    end
  end
  object PANControls: TPanel
    Left = 0
    Top = 0
    Width = 298
    Height = 41
    Align = alTop
    TabOrder = 2
    object LBLControls: TLabel
      Left = 4
      Top = 13
      Width = 45
      Height = 15
      Caption = 'Controls'
    end
    object CMBControls: TComboBox
      Left = 64
      Top = 10
      Width = 212
      Height = 23
      DropDownCount = 12
      DropDownWidth = 240
      TabOrder = 0
      OnChange = CMBControlsChange
    end
  end
end
