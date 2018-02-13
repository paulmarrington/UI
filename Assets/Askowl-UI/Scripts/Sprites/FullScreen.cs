using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprites {
  public class FullScreen : MonoBehaviour {
    void Awake() {
      Debug.Log("FullScreen Awake");
      SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

      float cameraHeight = Camera.main.orthographicSize * 2;
      Vector2 cameraSize = new Vector2 (Camera.main.aspect * cameraHeight, cameraHeight);
      Vector2 spriteSize = spriteRenderer.sprite.bounds.size;   
      Vector3 scale = transform.localScale;
      float xScale = cameraSize.x / spriteSize.x;
      Debug.Log("xScale=" + xScale + " from camera=" + cameraSize.x + " and sprite=" + spriteSize.x);
      float yScale = cameraSize.y / spriteSize.y;
      scale *= (xScale > yScale) ? xScale : yScale;
      Debug.Log("old: " + transform.localScale + ", new: " + scale);
      transform.position = Vector2.zero; // Optional
      transform.localScale = scale;
    }
  }
}