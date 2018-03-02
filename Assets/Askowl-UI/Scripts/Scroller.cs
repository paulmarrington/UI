using JetBrains.Annotations;
using UnityEngine;

public sealed class Scroller {
  private readonly RectTransform content;

  public Vector2 StepSize = new Vector2(x: 0, y: 0);

  private readonly Vector3 startPosition;
  private          bool    approaching;
  private          Rect    viewportRect;

  public Scroller([NotNull] RectTransform content, [NotNull] RectTransform viewport) {
    this.content  = content;
    startPosition = content.position;
    viewportRect  = getWorldRect(transform: viewport);
    Reset();
  }

  private Rect getWorldRect([NotNull] RectTransform transform) {
    Vector3[] corners = new Vector3[4];
    transform.GetWorldCorners(fourCornersArray: corners);

    return new Rect(
      x: corners[0].x,
      y: corners[0].y,
      width: corners[2].x  - corners[0].x,
      height: corners[2].y - corners[0].y);
  }

  private void Reset() {
    content.position = startPosition;
    approaching      = true;
  }

  public bool Step(float scale) {
    Vector3 offset = content.position;
    offset.x         += StepSize.x * scale;
    offset.y         += StepSize.y * scale;
    content.position =  offset;

    if (viewportRect.Overlaps(other: getWorldRect(transform: content))) {
      approaching = false;
    } else if (!approaching) {
      Reset();
      return false;
    }

    return true;
  }
}