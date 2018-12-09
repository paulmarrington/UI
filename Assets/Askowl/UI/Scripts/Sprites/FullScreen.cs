namespace Askowl {
  namespace Sprites {
    using UnityEngine;

    /// <a href=""></a> //#TBD#//
    public class FullScreen : MonoBehaviour {
      [SerializeField] private bool overFill = true;

      private void Awake() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // ReSharper disable once PossibleNullReferenceException
        float   cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize   = new Vector2(x: Camera.main.aspect * cameraHeight, y: cameraHeight);
        Vector2 spriteSize   = spriteRenderer.sprite.bounds.size;
        Vector3 scale        = transform.localScale;
        float   xScale       = cameraSize.x / spriteSize.x;
        float   yScale       = cameraSize.y / spriteSize.y;

        scale *= (overFill ? (xScale > yScale) : (xScale < yScale)) ? xScale : yScale;

        transform.position   = Vector2.zero; // Optional
        transform.localScale = scale;
      }
    }
  }
}