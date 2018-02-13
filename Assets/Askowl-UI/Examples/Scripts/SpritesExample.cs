using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.U2D;
using System.Linq;

public class SpritesExample : MonoBehaviour {
  public SpriteAtlas spriteAtlas;

  public void SpritesCacheTest() {
    Assert.NotNull(spriteAtlas);

    Sprites.Cache cache = Sprites.Cache.Atlas(spriteAtlas);

    Assert.IsTrue(cache.sprite.ContainsKey("Attack_1"), "Attack_1 missing");
    Assert.IsFalse(cache.sprite.ContainsKey("Attack_6"), "Attack_6 exists");

    Assert.AreEqual(cache.sprite ["Attack_1"].name, "Attack_1", "Sprite Name incorrect");

    string sprites = string.Join(", ", new List<string> (cache.sprite.Keys).Select(x => x.ToString()).ToArray());
    Debug.Log("Atlas contains: " + sprites);
    Debug.Log("Sprites.Cache works as expected");
  }

  public void SpritesContentsTest() {
    Assert.NotNull(spriteAtlas);

    Sprites.Cache cache = Sprites.Cache.Atlas(spriteAtlas);
    Sprite attack1 = cache.sprite ["Attack_1"];

    Texture2D texture1a = Sprites.Contents.Texture(attack1);
    Assert.NotNull(texture1a);

    Texture2D texture1b = Sprites.Contents.Texture(spriteAtlas, "Attack_1");
    Assert.NotNull(texture1b);

    Assert.AreEqual(texture1a.imageContentsHash, texture1b.imageContentsHash);

    Debug.Log("Sprites.Contents works as expected");
  }
}
