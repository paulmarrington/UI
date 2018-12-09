#if UNITY_EDITOR && AskowlUI
using Askowl.Sprites;
using UnityEngine;

/// <a href=""></a> //#TBD#//
public sealed class SpritesFullScreenExample : FullScreen {
  private void Update() {
    if (Input.GetKeyDown(name: "space")) {
      gameObject.SetActive(value: false);
    }
  }


  /// <a href=""></a> //#TBD#//
  public void ShowFullScreen() => gameObject.SetActive(value: !gameObject.activeSelf);
}
#endif