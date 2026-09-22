program RogTkinterIDE;

uses
  Vcl.Forms,
  Form_Main in 'Form_Main.pas' {frmMain},
  Form_ToolBox in 'Form_ToolBox.pas' {frmToolBox},
  Form_Properties in 'Form_Properties.pas' {frmProperties},
  clsTkinterFormControls in 'clsTkinterFormControls.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
