using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public sealed class DialogExample : MonoBehaviour {
  private Dialog dialog;

  [UsedImplicitly]
  public void ActivateButtonPressed() { StartCoroutine(routine: ActivateDialog()); }

  private static IEnumerator ActivateDialog() {
    Dialog dialogInstance = Dialog.Instance(gameObjectName: "Dialog Example");

    yield return dialogInstance.Activate(
      "<color=#ff000088>Now</color> is the time for all good <b>men</b> to come to the aid of the <i>party</i>",
      "Yes Sir",
      "Not Now");

    switch (dialogInstance.action) {
      case "Yes":
        Debug.Log(message: "Affirmative");
        break;
      case "No":
        Debug.Log(message: "Negative");
        break;
      default:
        Debug.LogError(message: "Unexpected button: '" + dialogInstance.action + "'");
        break;
    }
  }
}