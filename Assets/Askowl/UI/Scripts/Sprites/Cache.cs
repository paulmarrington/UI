using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace Askowl {
  namespace Sprites {
    /// <a href=""></a> //#TBD#//
    public sealed class SpriteCache {
      private static readonly Dictionary<string, SpriteCache> atlases = new Dictionary<string, SpriteCache>();

      /// <a href=""></a> //#TBD#//
      public readonly Dictionary<string, Sprite> Sprite = new Dictionary<string, Sprite>();

      /// <a href=""></a> //#TBD#//
      public static SpriteCache Atlas(SpriteAtlas atlas) {
        if (atlases.ContainsKey(key: atlas.name)) return atlases[key: atlas.name];

        SpriteCache cache = new SpriteCache(atlas: atlas);
        atlases.Add(key: atlas.name, value: cache);

        return atlases[key: atlas.name];
      }

      private SpriteCache(SpriteAtlas atlas) {
        Sprite[] sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(sprites: sprites);

        foreach (Sprite entry in sprites) {
          int index = entry.name.IndexOf(value: "(", comparisonType: StringComparison.Ordinal);

          if (index >= 0) {
            entry.name = entry.name.Substring(startIndex: 0, length: index);
          }

          Sprite[key: entry.name] = entry;
        }
      }

      /// <a href=""></a> //#TBD#//
      public override string ToString()
        => string.Join(separator: ",", value: Sprite.Select(selector: d => d.Key).ToArray());
    }
  }
}