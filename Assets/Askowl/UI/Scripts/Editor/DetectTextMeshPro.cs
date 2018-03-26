namespace Askowl {
  using UnityEditor;

  [InitializeOnLoad]
  public sealed class DetectTextMeshPro : DefineSymbols {
    static DetectTextMeshPro() {
      bool usable = HasFolder("TextMesh Pro");
      AddOrRemoveDefines(addDefines: usable, named: "TextMeshPro");
    }
  }
}