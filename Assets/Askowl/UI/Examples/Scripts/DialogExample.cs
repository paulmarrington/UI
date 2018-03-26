using System.Collections;
using Askowl;
using JetBrains.Annotations;
using UnityEngine;

public sealed class DialogExample : MonoBehaviour {
  private Dialog dialog;

  [UsedImplicitly]
  public void DialogButtonPressed() { StartCoroutine(routine: ActivateDialog("Dialog Example")); }

  [UsedImplicitly]
  public void FaderButtonPressed() { StartCoroutine(routine: ActivateDialog("Fader Example")); }

  private static IEnumerator ActivateDialog(string dialogName) {
    Dialog dialogInstance = Dialog.Instance(dialogName);
    if (dialogInstance == null) yield break;

    yield return dialogInstance.Activate(
      "<color=#ff000088>Now</color> is the time for all good <b>men</b> to come to the aid of the <i>party</i>",
      "Yes Sir",
      "Not Now");

    Debug.Log("Button pressed - " + dialogInstance.Action);

    switch (dialogInstance.Action) {
      case "Yes":
        Debug.Log(message: "Affirmative");
        break;
      case "No":
        Debug.Log(message: "Negative");
        break;
      default:
        Debug.LogError(message: "Unexpected button: '" + dialogInstance.Action + "'");
        break;
    }
  }
}