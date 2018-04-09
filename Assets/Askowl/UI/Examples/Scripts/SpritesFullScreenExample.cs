#if UNITY_EDITOR
using Askowl.Sprites;
using JetBrains.Annotations;
using UnityEngine;

public sealed class SpritesFullScreenExample : FullScreen {
  private void Update() {
    if (Input.GetKeyDown(name: "space")) {
      gameObject.SetActive(value: false);
    }
  }

  [UsedImplicitly]
  public void ShowFullScreen() { gameObject.SetActive(value: !gameObject.activeSelf); }
}
#endif