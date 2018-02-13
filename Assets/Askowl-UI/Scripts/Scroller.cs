using UnityEngine;
using System.Collections;

public class Scroller {
  public RectTransform viewport;
  public RectTransform content;
  public Vector2 step = new Vector2 (0, 0);

  private Vector3 startPosition;
  private bool approaching;
  private Rect viewportRect;

  public Scroller(RectTransform viewport, RectTransform content) {
    this.viewport = viewport;
    this.content = content;
    startPosition = content.position;
    viewportRect = getWorldRect(viewport);
    Reset();
  }

  private Rect getWorldRect(RectTransform transform) {
    Vector3[] corners = new Vector3[4];
    transform.GetWorldCorners(corners);
    return new Rect (
      corners [0].x,
      corners [0].y,
      corners [2].x - corners [0].x,
      corners [2].y - corners [0].y);
  }

  public void Reset() {
    content.position = startPosition;
    approaching = true;
  }

  public bool Step(float scale) {
    Vector3 offset = content.position;
    offset.x += step.x * scale;
    offset.y += step.y * scale;
    content.position = offset;
    if (viewportRect.Overlaps(getWorldRect(content))) {
      approaching = false;
    } else if (!approaching) {
      Reset();
      return false;
    }
    return true;
  }
}