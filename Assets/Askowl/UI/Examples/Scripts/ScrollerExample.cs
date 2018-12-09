#if UNITY_EDITOR && AskowlUI
using Askowl;
using UnityEngine;

/// <a href=""></a> //#TBD#//
public sealed class ScrollerExample : MonoBehaviour {
  /// <a href=""></a> //#TBD#//
  public GameObject Viewport;

  /// <a href=""></a> //#TBD#//
  public GameObject Content;

  /// <a href=""></a> //#TBD#//
  public int PixelsPerSecond = 100;

  private Scroller scroller;
  private bool     active;

  private void Start() {
    scroller = new Scroller(viewport: Viewport.GetComponent<RectTransform>(),
                            content: Content.GetComponent<RectTransform>()) {
      StepSize = {x = -1, y = 1} // moves left to right, bottom to top
    };
  }

  private void Update() {
    if (!active || scroller.Step(PixelsPerSecond)) return;

    Viewport.SetActive(value: false);
    active = false;
  }


  /// <a href=""></a> //#TBD#//
  public void ButtonPressed() {
    Viewport.SetActive(value: true);
    active = true;
  }
}
#endif