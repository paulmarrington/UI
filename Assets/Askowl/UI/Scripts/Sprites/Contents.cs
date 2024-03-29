﻿using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.U2D;

namespace Askowl {
  namespace Sprites {
    /// <a href=""></a> //#TBD#//
    public static class Contents {
      /// <a href=""></a> //#TBD#//
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

      /// <a href=""></a> //#TBD#//
      public static Texture2D Texture([NotNull] SpriteAtlas atlas, string name) =>
        Texture(sprite: atlas.GetSprite(name));

      /// <a href=""></a> //#TBD#//
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