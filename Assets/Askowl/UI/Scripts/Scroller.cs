using UnityEngine;

namespace Askowl {
  /// <a href=""></a> //#TBD#//
  public sealed class Scroller {
    private RectTransform content;

    /// <a href=""></a> //#TBD#//
    public Vector2 StepSize = new Vector2(x: 0, y: 0);

    private Vector3 startPosition;
    private bool    approaching;
    private Rect    viewportRect;

    /// <a href=""></a> //#TBD#//
    public Scroller(GameObject content, GameObject viewport) =>
      Reset(content.GetComponent<RectTransform>(), viewport.GetComponent<RectTransform>());

    /// <a href=""></a> //#TBD#//
    public Scroller(RectTransform content, RectTransform viewport) => Reset(content, viewport);

    private Rect getWorldRect(RectTransform transform) {
      Vector3[] corners = new Vector3[4];
      transform.GetWorldCorners(fourCornersArray: corners);

      return new Rect(
        x: corners[0].x,
        y: corners[0].y,
        width: corners[2].x  - corners[0].x,
        height: corners[2].y - corners[0].y);
    }

    /// <a href=""></a> //#TBD#//
    public void Reset(RectTransform contentTransform, RectTransform viewport) {
      content       = contentTransform;
      startPosition = contentTransform.position;
      viewportRect  = getWorldRect(transform: viewport);
      Reset();
    }

    /// <a href=""></a> //#TBD#//
    public void Reset() {
      content.position = startPosition;
      approaching      = true;
    }

    /// <a href=""></a> //#TBD#//
    public bool Step(int pixelsPerSecond) => Step(pixelsPerSecond * Time.fixedUnscaledDeltaTime);

    /// <a href=""></a> //#TBD#//
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
}