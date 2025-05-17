namespace CorebrainCLIAPI;

public class CorebrainSettings {
  public string PythonPath { get; set; } = "../Corebrain-CS/corebrain/venv/Scripts/python";
  public string ScriptPath { get; set; } = "../Corebrain-CS/corebrain/corebrain/cli";
  public bool Verbose { get; set; } = false;
}
