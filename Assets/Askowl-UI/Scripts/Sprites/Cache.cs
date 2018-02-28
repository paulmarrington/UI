using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Linq;

namespace Sprites {
  using JetBrains.Annotations;

  public sealed class Cache {
    private static readonly Dictionary<string, Cache> Atlases = new Dictionary<string, Cache>();

    public readonly Dictionary<string, Sprite> Sprite = new Dictionary<string, Sprite>();

    public static Cache Atlas([NotNull] SpriteAtlas atlas) {
      if (Atlases.ContainsKey(key: atlas.name)) return Atlases[key: atlas.name];

      Cache cache = new Cache(atlas: atlas);
      Atlases.Add(key: atlas.name, value: cache);

      return Atlases[key: atlas.name];
    }

    private Cache([NotNull] SpriteAtlas atlas) {
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