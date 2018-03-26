using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Linq;

namespace Askowl {
  namespace Sprites {
    using JetBrains.Annotations;

    public sealed class SpriteCache {
      private static readonly Dictionary<string, SpriteCache> Atlases =
        new Dictionary<string, SpriteCache>();

      public readonly Dictionary<string, Sprite> Sprite = new Dictionary<string, Sprite>();

      public static SpriteCache Atlas([NotNull] SpriteAtlas atlas) {
        if (Atlases.ContainsKey(key: atlas.name)) return Atlases[key: atlas.name];

        SpriteCache cache = new SpriteCache(atlas: atlas);
        Atlases.Add(key: atlas.name, value: cache);

        return Atlases[key: atlas.name];
      }

      private SpriteCache([NotNull] SpriteAtlas atlas) {
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

      public override string ToString() {
        return string.Join(separator: ",", value: Sprite.Select(selector: d => d.Key).ToArray());
      }
    }
  }
}