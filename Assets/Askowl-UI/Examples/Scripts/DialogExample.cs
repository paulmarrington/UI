using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogExample : MonoBehaviour {
  Dialog dialog;

  // Use this for initialization
  void Start() {
  }
	
  // Update is called once per frame
  void Update() {
		
  }

  public void ActivateButtonPressed() {
    StartCoroutine(ActivateDialog());
  }

  IEnumerator ActivateDialog() {
    Dialog dialog = Dialog.Instance("Dialog Example");
    yield return dialog.Activate(
      "<color=#ff000088>Now</color> is the time for all good <b>men</b> to come to the aid of the <i>party</i>",
      "Yes Sir",
      "Not Now");
    if (dialog.action == "Yes") {
      Debug.Log("Affirmative");
    } else if (dialog.action == "No") {
      Debug.Log("Negative");
    } else {
      Debug.LogError("Unexpected button: '" + dialog.action + "'");
    }
  }
}
