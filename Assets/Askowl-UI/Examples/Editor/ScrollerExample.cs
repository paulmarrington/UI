using JetBrains.Annotations;
using UnityEngine;

public sealed class ScrollerExample : MonoBehaviour {
  public GameObject Viewport;
  public GameObject Content;
  public int        PixelsPerSecond = 100;

  private Scroller scroller;
  private bool     active;

  private void Start() {
    scroller = new Scroller(viewport: Viewport.GetComponent<RectTransform>(),
                            content: Content.GetComponent<RectTransform>()) {
      step = {x = -1, y = 1}
    };

    // moves left to right
    // moves bottom to top
  }

  private void Update() {
    if (active) {
      if (!scroller.Step(scale: PixelsPerSecond * Time.fixedUnscaledDeltaTime)) {
        Viewport.SetActive(value: false);
        active = false;
      }
    }
  }

  [UsedImplicitly]
  public void ButtonPressed() {
    Viewport.SetActive(value: true);
    active = true;
  }
}