#if UNITY_EDITOR && AskowlUI
using Askowl;
using UnityEngine;

// ReSharper disable MissingXmlDoc

public sealed class ScrollerExample : MonoBehaviour {
  public GameObject viewport;

  public GameObject content;

  public int pixelsPerSecond = 100;

  private Scroller scroller;
  private bool     active;

  private void Start() =>
    scroller = new Scroller(
      viewport: viewport.GetComponent<RectTransform>(),
      content: content.GetComponent<RectTransform>()) {
      StepSize = {x = -1, y = 1} // moves left to right, bottom to top
    };

  private void Update() {
    if (!active || scroller.Step(pixelsPerSecond)) return;

    viewport.SetActive(value: false);
    active = false;
  }

  public void ButtonPressed() {
    viewport.SetActive(value: true);
    active = true;
  }
}
#endif