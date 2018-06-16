#if UNITY_EDITOR && AskowlUI
using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;
using Askowl;
using UnityEngine.UI;

internal class RuntimeTests : PlayModeTests {
  private IEnumerator Setup() { yield return LoadScene("Askowl-UI-Examples"); }

  [UnityTest]
  public IEnumerator ScrollerExampleTest() {
    yield return Setup();
    yield return PushButton("Scroller");

    Transform content = FindGameObject("Scroller Content").transform;
    float     first = content.transform.position.x;
    yield return null;

    Assert.AreNotEqual(first, content.transform.position.x);
  }

  [UnityTest]
  public IEnumerator SpriteCacheExampleTest() {
    yield return Setup();
    yield return PushButton("Sprites.Cache");

    LogAssert.Expect(LogType.Log, new Regex(
                       @"Atlas contains: Attack_\d, Attack_\d, Attack_\d, Attack_\d, Attack_\d"));

    LogAssert.Expect(LogType.Log, "Sprites.Cache works as expected");
  }

  [UnityTest]
  public IEnumerator SpriteContentsExampleTest() {
    yield return Setup();
    yield return PushButton("Sprites.Contents");

    LogAssert.Expect(LogType.Log, "Sprites.Contents works as expected");
  }

  [UnityTest]
  public IEnumerator SpriteFullScreenTest() {
    yield return Setup();

    SpriteRenderer spriteRenderer = Component<SpriteRenderer>("SpriteToFill");
    Assert.NotNull(spriteRenderer);

    float   cameraHeight = Camera.main.orthographicSize * 2;
    Vector2 cameraSize = new Vector2(x: Camera.main.aspect * cameraHeight, y: cameraHeight);
    Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

    Assert.IsTrue(
      (Math.Abs((spriteSize * spriteRenderer.transform.localScale).x - cameraSize.x) < 0.1) ||
      (Math.Abs((spriteSize * spriteRenderer.transform.localScale).y - cameraSize.y) < 0.1));
  }

  [UnityTest, Timeout(10000)]
  public IEnumerator DialogExampleTest() {
    yield return Setup();

    Canvas canvas = Component<Canvas>("Dialog Example");
    Assert.IsTrue(!canvas.isActiveAndEnabled);
    yield return PushButton("Dialog Example Button");

    while (!canvas.isActiveAndEnabled) yield return null;

    Button yes = Component<Button>("Dialog Example", "Yes");
    yield return PushButton(yes);

    LogAssert.Expect(LogType.Log, "Affirmative");
  }

  [UnityTest, Timeout(10000)]
  public IEnumerator FaderExampleTest() {
    yield return Setup();

    Canvas canvas = Component<Canvas>("Fader Example");
    Assert.IsTrue(!canvas.isActiveAndEnabled);
    yield return PushButton("Fader Example Button");

    while (!canvas.isActiveAndEnabled) yield return null;

    Button yes = Component<Button>("Fader Example", "Yes");

    float start = Time.realtimeSinceStartup;
    yield return PushButton(yes);

    while (canvas.isActiveAndEnabled) yield return null;

    Assert.Greater(Time.realtimeSinceStartup - start, 0.5);
  }
}
#endif