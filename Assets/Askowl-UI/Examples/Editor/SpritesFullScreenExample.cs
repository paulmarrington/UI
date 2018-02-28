using JetBrains.Annotations;
using UnityEngine;

public sealed class SpritesFullScreenExample : Sprites.FullScreen {
  private void Update() {
    if (Input.GetKeyDown(name: "space")) {
      gameObject.SetActive(value: false);
    }
  }

  [UsedImplicitly]
  public void ShowFullScreen() { gameObject.SetActive(value: !gameObject.activeSelf); }
}