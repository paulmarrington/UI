using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Linq;

namespace Sprites {
  public class Cache {

    static Dictionary<string, Cache> atlases = new Dictionary<string, Cache> ();

    public Dictionary<string, Sprite> sprite = new Dictionary<string, Sprite> ();

    public static Cache Atlas(SpriteAtlas atlas) {
      if (!atlases.ContainsKey(atlas.name)) {
        Cache cache = new Cache (atlas);
        atlases.Add(atlas.name, cache);
      }
      return atlases [atlas.name];
    }

    Cache(SpriteAtlas atlas) {
      Sprite[] sprites = new Sprite[atlas.spriteCount];
      atlas.GetSprites(sprites);
      foreach (Sprite entry in sprites) {
        int index = entry.name.IndexOf("(");
        if (index >= 0) {
          entry.name = entry.name.Substring(0, index);
        }
        sprite [entry.name] = entry;
      }
    }

    public override String ToString() {
      return String.Join(",", sprite.Select(d => d.Key).ToArray());
    }
  }
}