﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
  public Text message;
  public Button[] buttons;

  private string buttonPressed;
  private Canvas canvas;

  public void Start () {
    canvas = gameObject.GetComponent<Canvas> ();
    message.text = "";
    foreach (Button button in buttons) {
      button.GetComponentInChildren<Text> ().text = "";
    }
  }

  public void Buttons (params string[] buttonTexts) {
    for (int i = 0; i < buttonTexts.Length; i++) {
      if (i < buttons.Length) {
        buttons [i].gameObject.SetActive (buttonTexts [i] != null && buttonTexts [i].Length > 0);
        if (buttons [i].enabled) {
          buttons [i].GetComponentInChildren<Text> ().text = buttonTexts [i];
        }
      }
    }
  }

  public void PressButton (Button button) {
    buttonPressed = button.name;
  }

  public void Show (string text, params string[] buttonTexts) {
    Buttons (buttonTexts);
    buttonPressed = null;
    message.text = text;
    canvas.enabled = true;
  }

  public IEnumerator Activate (string text, params string[] buttonTexts) {
    Show (text, buttonTexts);
    return Wait ();
  }

  public void Hide () {
    canvas.enabled = false;
  }

  public IEnumerator Wait () {
    while (buttonPressed == null) {
      yield return null;
    }
    Hide ();
  }

  public string action { get { return buttonPressed; } }
}