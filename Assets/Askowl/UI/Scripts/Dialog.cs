// Copyright 2018 (C) paul@marrington.net http://www.askowl.net/unity-packages

using Decoupled;

namespace Askowl {
  using System.Collections;
  using JetBrains.Annotations;
  using UnityEngine;
  using UnityEngine.UI;

  /// <inheritdoc />
  /// <summary>
  /// Script support for a custom dialog-box prefab.
  /// </summary>
  public sealed class Dialog : MonoBehaviour {
    [SerializeField] private Textual  message;
    [SerializeField] private Button[] buttons;

    private Canvas canvas;

    /*
     * When I try and cache dialog loaded in OnEnable, the reference becomes destroyed.
     * Peculiar since it has the same ID. Probably something to do with it being a prefab.
     * The solution/workaround I chose was to find it when I need it.
     */
    /// <summary>
    ///
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <returns></returns>
    public static Dialog Instance(string gameObjectName) {
      GameObject go = GameObject.Find(name: gameObjectName);

      if (go == null) {
        Debug.LogError(message: "Scene requires GameObject '" + gameObjectName + "'");
        return null;
      } else {
        Dialog dialog = go.GetComponent<Dialog>();

        if (dialog == null) {
          Debug.LogError("GameObject '" + gameObjectName + "' must have a Dialog script attached");
        }

        return dialog;
      }
    }

    private void Start() {
      canvas         = gameObject.GetComponent<Canvas>();
      canvas.enabled = false;
      message.text   = "";

      foreach (Button button in buttons) {
        button.GetComponentInChildren<Textual>().text = "";
      }
    }

    private void Buttons(){//params string[] buttonTexts) {
      for (int i = 0; i < buttons.Length; i++) {
        if ((i < buttonTexts.Length) && (buttonTexts[i] != null) && (buttonTexts[i].Length > 0)) {
          buttons[i].gameObject.SetActive(value: true);
          buttons[i].GetComponentInChildren<Textual>().text = buttonTexts[i];
        } else {
          buttons[i].gameObject.SetActive(value: false);
        }
      }
    }

    /// <summary>
    /// Called by prefab when a button on the dialog-box is pressed
    /// </summary>
    /// <param name="button">Name of button component in prefab</param>
    
    public void PressButton(Button button) { Action = button.name; }

    private void Show(string text, ){//params string[] buttonTexts) {
      Buttons(buttonTexts: buttonTexts);
      Action       = null;
      message.text = text;
      CanvasGroupFader.FadeIn(canvas);
    }

    /// <summary>
    /// Coroutine to display a dialog box and only return once we have user interaction.
    /// </summary>
    /// <param name="text">Text to display in the dialog box</param>
    /// <param name="buttonTexts">List of the text to put on the buttons - assigned in the same order as the buttons are in the prefab</param>
    /// <returns></returns>
    public IEnumerator Activate(string text, ){//params string[] buttonTexts) {
      Show(text, buttonTexts);
      return Wait();
    }

    private void Hide() { CanvasGroupFader.FadeOut(canvas); }

    private IEnumerator Wait() {
      while (Action == null) {
        yield return null;
      }

      Hide();
    }

    /// <summary>
    /// Result of a button press - being the name of the button component (not the text on it)
    /// </summary>
    public string Action { get; private set; }
  }
}
