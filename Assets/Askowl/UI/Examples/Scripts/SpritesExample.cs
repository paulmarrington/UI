#if UNITY_EDITOR && AskowlUI
using System.Collections.Generic;
using System.Linq;
using Askowl.Sprites;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.U2D;

// ReSharper disable MissingXmlDoc

public sealed class SpritesExample : MonoBehaviour {
  public SpriteAtlas spriteAtlas;

  public void SpritesCacheTest() {
    Assert.NotNull(anObject: spriteAtlas);

    SpriteCache cache = SpriteCache.Atlas(atlas: spriteAtlas);

    Assert.IsTrue(
      condition: cache.Sprite.ContainsKey(key: "Attack_1"),
      message: "Attack_1 missing");

    Assert.IsFalse(
      condition: cache.Sprite.ContainsKey(key: "Attack_6"),
      message: "Attack_6 exists");

    Assert.AreEqual(
      expected: cache.Sprite[key: "Attack_1"].name, actual: "Attack_1",
      message: "Sprite Name incorrect");

    string sprites = string.Join(
      separator: ", ",
      value: new List<string>(collection: cache.Sprite.Keys)
            .Select(selector: x => x.ToString()).ToArray());

    Debug.Log("Atlas contains: " + sprites);
    Debug.Log("Sprites.Cache works as expected");
  }

  public void SpritesContentsTest() {
    Assert.NotNull(anObject: spriteAtlas);

    SpriteCache cache   = SpriteCache.Atlas(atlas: spriteAtlas);
    Sprite      attack1 = cache.Sprite[key: "Attack_1"];

    Texture2D texture1A = Contents.Texture(sprite: attack1);
    Assert.NotNull(anObject: texture1A);

    Texture2D texture1B = Contents.Texture(atlas: spriteAtlas, name: "Attack_1");
    Assert.NotNull(anObject: texture1B);

    Assert.AreEqual(expected: texture1A.imageContentsHash, actual: texture1B.imageContentsHash);

    Debug.Log("Sprites.Contents works as expected");
  }
}
#endif