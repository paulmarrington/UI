using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public sealed class Dialog : MonoBehaviour {
  [SerializeField] private Text     message;
  [SerializeField] private Button[] buttons;

  private Canvas canvas;

  /*
   * When I try and cache dialog loaded in OnEnable, the reference becomes destroyed.
   * Peculiar since it has the same ID. Probably something to do with it being a prefab.
   * The solution/workaround I chose was to find it when I need it.
   */
  [CanBeNull]
  public static Dialog Instance(string gameObjectName) {
    GameObject go = GameObject.Find(name: gameObjectName);

    if (go == null) {
      Debug.LogError(message: "Scene requires GameObject '" + gameObjectName + "'");
      return null;
    } else {
      Dialog dialog = go.GetComponent<Dialog>();

      if (dialog == null) {
        Debug.LogError(message: "GameObject '" + gameObjectName +
                                "' must have a Dialog script attached");
      }

      return dialog;
    }
  }

  public void Start() {
    canvas       = gameObject.GetComponent<Canvas>();
    message.text = "";

    foreach (Button button in buttons) {
      button.GetComponentInChildren<Text>().text = "";
    }
  }

  private void Buttons(params string[] buttonTexts) {
    for (int i = 0; i < buttons.Length; i++) {
      if ((i < buttonTexts.Length) && (buttonTexts[i] != null) && (buttonTexts[i].Length > 0)) {
        buttons[i].gameObject.SetActive(value: true);
        buttons[i].GetComponentInChildren<Text>().text = buttonTexts[i];
      } else {
        buttons[i].gameObject.SetActive(value: false);
      }
    }
  }

  [UsedImplicitly]
  public void PressButton([NotNull] Button button) { Action = button.name; }

  private void Show(string text, params string[] buttonTexts) {
    Buttons(buttonTexts: buttonTexts);
    Action         = null;
    message.text   = text;
    canvas.enabled = true;
  }

  public IEnumerator Activate(string text, params string[] buttonTexts) {
    Show(text: text, buttonTexts: buttonTexts);
    return Wait();
  }

  private void Hide() { canvas.enabled = false; }

  private IEnumerator Wait() {
    while (Action == null) {
      yield return null;
    }

    Hide();
  }

  public string Action { get; private set; }
}