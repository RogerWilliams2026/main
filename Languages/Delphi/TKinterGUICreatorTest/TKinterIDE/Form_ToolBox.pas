unit Form_ToolBox;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ExtCtrls;

type
  TfrmToolBox = class(TForm)
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
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmToolBox: TfrmToolBox;

implementation

{$R *.dfm}

end.
