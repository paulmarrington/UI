using UnityEngine;

namespace Askowl {
  namespace Sprites {
    /// <a href=""></a> //#TBD#//
    public class FullScreen : MonoBehaviour {
      [SerializeField] private bool overFill = true;

      private void Awake() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // ReSharper disable once PossibleNullReferenceException
        var main = Camera.main;
        if (main == null) return;
        var     tran         = transform;
        float   cameraHeight = main.orthographicSize * 2;
        Vector2 cameraSize   = new Vector2(x: main.aspect * cameraHeight, y: cameraHeight);
        Vector2 spriteSize   = spriteRenderer.sprite.bounds.size;
        Vector3 scale        = tran.localScale;
        float   xScale       = cameraSize.x / spriteSize.x;
        float   yScale       = cameraSize.y / spriteSize.y;

        scale *= (overFill ? (xScale > yScale) : (xScale < yScale)) ? xScale : yScale;

        tran.position   = Vector2.zero; // Optional
        tran.localScale = scale;
      }
    }
  }
}