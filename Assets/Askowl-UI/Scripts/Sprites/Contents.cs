using UnityEngine;
using UnityEngine.U2D;

namespace Sprites {
  using JetBrains.Annotations;

  public static class Contents {
    public static Texture2D Texture([NotNull] Sprite sprite) {
      if (!sprite.packed) {
        return sprite.texture; // not in atlas
      }

      int       x       = (int) System.Math.Ceiling(a: sprite.textureRect.x);
      int       y       = (int) System.Math.Ceiling(a: sprite.textureRect.y);
      int       width   = (int) System.Math.Ceiling(a: sprite.textureRect.width);
      int       height  = (int) System.Math.Ceiling(a: sprite.textureRect.height);
      Texture2D texture = new Texture2D(width: width, height: height);
      texture.SetPixels(colors: sprite.texture.GetPixels(x: x, y: y, blockWidth: width, blockHeight: height));
      texture.Apply();
      return texture;
    }

    public static Texture2D Texture([NotNull] SpriteAtlas atlas, string name) {
      return Texture(sprite: atlas.GetSprite(name: name));
    }

    [NotNull, UsedImplicitly]
    public static string ToString([NotNull] Sprite sprite, string separator = ", ") {
      string s = separator;

      return
        "Sprite name="     + sprite.name + s + "border=" + sprite.border + s + "bounds=" +
        sprite.bounds      +
        s                  + "packed=" + sprite.packed + s + "packingMode=" +
        sprite.packingMode +
        s                  + "packingRotation=" + sprite.packingRotation + s + "pivot=" +
        sprite.pivot       +
        s                  + "pixelsPerUnit="     + sprite.pixelsPerUnit + s + "rect=" +
        sprite.rect        + s                    +
        "textureRect="     + sprite.textureRect   +
        s                  + "textureRectOffset=" + sprite.textureRectOffset;
    }
  }
}