#if UNITY_EDITOR && AskowlUI
using Askowl.Sprites;
using UnityEngine;

// ReSharper disable MissingXmlDoc

public sealed class SpritesFullScreenExample : FullScreen {
  private void Update() {
    if (Input.GetKeyDown(name: "space")) {
      gameObject.SetActive(value: false);
    }
  }

  public void ShowFullScreen() => gameObject.SetActive(value: !gameObject.activeSelf);
}
#endif