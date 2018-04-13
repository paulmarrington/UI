namespace Askowl {
  namespace Sprites {
    using System;
    using JetBrains.Annotations;
    using UnityEngine;
    using UnityEngine.U2D;

    public static class Contents {
      public static Texture2D Texture([CanBeNull] Sprite sprite) {
        if (sprite == null) return null;

        if (!sprite.packed) {
          return sprite.texture; // not in atlas
        }

        int x      = (int) Math.Ceiling(a: sprite.textureRect.x);
        int y      = (int) Math.Ceiling(a: sprite.textureRect.y);
        int width  = (int) Math.Ceiling(a: sprite.textureRect.width);
        int height = (int) Math.Ceiling(a: sprite.textureRect.height);

        Texture2D texture = new Texture2D(width: width, height: height);
        texture.SetPixels(sprite.texture.GetPixels(x, y, width, height));
        texture.Apply();
        return texture;
      }

      public static Texture2D Texture([NotNull] SpriteAtlas atlas, string name) {
        return Texture(sprite: atlas.GetSprite(name));
      }

      public static void SetTexture(UITexture uiTexture, SpriteAtlas atlas, string name) {
        Sprite sprite = atlas.GetSprite(name);
        if (sprite == null) return;

        uiTexture.mainTexture = sprite.texture;
        uiTexture.uvRect      = sprite.textureRect;
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
}