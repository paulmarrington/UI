using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
  public Text message;
  public Button[] buttons;

  private string buttonPressed;
  private Canvas canvas;

  /*
   * When I try and cache dialog loaded in OnEnable, the reference becomes destroyed.
   * Peculiar since it has the same ID. Probably something to do with it being a prefab.
   * The solution/workaround I chose was to find it when I need it.
   */
  static public Dialog Instance(string gameObjectName) {
    GameObject go = GameObject.Find(gameObjectName);
    if (go == null) {
      Debug.LogError("Scene requires GameObject '" + gameObjectName + "'");
      return null;
    } else {
      Dialog dialog = go.GetComponent<Dialog>();
      if (dialog == null) {
        Debug.LogError("GameObject '" + gameObjectName + "' must have a Dialog script attached");
      }
      return dialog;
    }
  }

  public void Start() {
    canvas = gameObject.GetComponent<Canvas>();
    message.text = "";
    foreach (Button button in buttons) {
      button.GetComponentInChildren<Text>().text = "";
    }
  }

  public void Buttons(params string[] buttonTexts) {
    for (int i = 0; i < buttons.Length; i++) {
      if ((i < buttonTexts.Length) && (buttonTexts [i] != null) && (buttonTexts [i].Length > 0)) {
        buttons [i].gameObject.SetActive(true);
        buttons [i].GetComponentInChildren<Text>().text = buttonTexts [i];
      } else {
        buttons [i].gameObject.SetActive(false);
      }
    }
  }

  public void PressButton(Button button) {
    buttonPressed = button.name;
  }

  public void Show(string text, params string[] buttonTexts) {
    Buttons(buttonTexts);
    buttonPressed = null;
    message.text = text;
    canvas.enabled = true;
  }

  public IEnumerator Activate(string text, params string[] buttonTexts) {
    Show(text, buttonTexts);
    return Wait();
  }

  public void Hide() {
    canvas.enabled = false;
  }

  public IEnumerator Wait() {
    while (buttonPressed == null) {
      yield return null;
    }
    Hide();
  }

  public string action { get { return buttonPressed; } }
}
