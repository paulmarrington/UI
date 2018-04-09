#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Askowl.Sprites;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.U2D;

public sealed class SpritesExample : MonoBehaviour {
  public SpriteAtlas SpriteAtlas;

  [UsedImplicitly]
  public void SpritesCacheTest() {
    Assert.NotNull(anObject: SpriteAtlas);

    SpriteCache cache = SpriteCache.Atlas(atlas: SpriteAtlas);

    Assert.IsTrue(condition: cache.Sprite.ContainsKey(key: "Attack_1"),
                  message: "Attack_1 missing");

    Assert.IsFalse(condition: cache.Sprite.ContainsKey(key: "Attack_6"),
                   message: "Attack_6 exists");

    Assert.AreEqual(expected: cache.Sprite[key: "Attack_1"].name, actual: "Attack_1",
                    message: "Sprite Name incorrect");

    string sprites = string.Join(
      separator: ", ",
      value: new List<string>(collection: cache.Sprite.Keys)
            .Select(selector: x => x.ToString()).ToArray());

    Debug.Log(message: "Atlas contains: " + sprites);
    Debug.Log(message: "Sprites.Cache works as expected");
  }

  [UsedImplicitly]
  public void SpritesContentsTest() {
    Assert.NotNull(anObject: SpriteAtlas);

    SpriteCache cache   = SpriteCache.Atlas(atlas: SpriteAtlas);
    Sprite      attack1 = cache.Sprite[key: "Attack_1"];

    Texture2D texture1A = Askowl.Sprites.Contents.Texture(sprite: attack1);
    Assert.NotNull(anObject: texture1A);

    Texture2D texture1B = Askowl.Sprites.Contents.Texture(atlas: SpriteAtlas, name: "Attack_1");
    Assert.NotNull(anObject: texture1B);

    Assert.AreEqual(expected: texture1A.imageContentsHash, actual: texture1B.imageContentsHash);

    Debug.Log(message: "Sprites.Contents works as expected");
  }
}
#endif