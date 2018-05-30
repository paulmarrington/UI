using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;
using CustomAsset;
using UnityEngine.UI;

public class NewTestScript : PlayModeTests {
  private IEnumerator Setup() { yield return LoadScene("Askowl-UI-Examples"); }

  [UnityTest]
  public IEnumerator ScrollerExampleTest() {
    yield return Setup();
    yield return PushButton("Scroller");

    throw new NotImplementedException();
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
    yield return PushButton("Full Screen Example Button");

    throw new NotImplementedException();
  }

  [UnityTest]
  public IEnumerator DialogExampleTest() {
    yield return Setup();
    yield return PushButton("Dialog Example Button");

    throw new NotImplementedException();
  }

  [UnityTest]
  public IEnumerator FaderExampleTest() {
    yield return Setup();
    yield return PushButton("Fader ExampleButton");

    throw new NotImplementedException();
  }
}