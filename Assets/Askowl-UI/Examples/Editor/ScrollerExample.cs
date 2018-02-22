using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerExample : MonoBehaviour {

  public GameObject viewport;
  public GameObject content;
  public int pixelsPerSecond = 100;

  Scroller scroller;
  bool active = false;

  void Start() {
    scroller = new Scroller (viewport.GetComponent<RectTransform>(), content.GetComponent<RectTransform>());
    scroller.step.x = -1; // moves left to right
    scroller.step.y = 1; // moves bottom to top
  }

  void Update() {
    if (active) {
      if (!scroller.Step(pixelsPerSecond * Time.fixedUnscaledDeltaTime)) {
        viewport.SetActive(false);
        active = false;
      }
    }
  }

  public void ButtonPressed() {
    viewport.SetActive(true);
    active = true;
  }
}
