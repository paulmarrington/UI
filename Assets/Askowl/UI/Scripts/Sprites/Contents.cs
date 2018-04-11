namespace Askowl {
  namespace Sprites {
    using System;
    using JetBrains.Annotations;
    using UnityEngine;
    using UnityEngine.U2D;

    public static class Contents {
      public static Texture2D Texture([NotNull] Sprite sprite) {
        Debug.LogWarning("**** Contents:10 (sprite!=null)=" + (sprite != null) +
                         "  #### DELETE-ME #### 10/4/18 9:38 PM"); //#DM#//

        if (!sprite.packed) {
          return sprite.texture; // not in atlas
        }

        int       x       = (int) Math.Ceiling(a: sprite.textureRect.x);
        int       y       = (int) Math.Ceiling(a: sprite.textureRect.y);
        int       width   = (int) Math.Ceiling(a: sprite.textureRect.width);
        int       height  = (int) Math.Ceiling(a: sprite.textureRect.height);
        Texture2D texture = new Texture2D(width: width, height: height);

        texture.SetPixels(
          colors: sprite.texture.GetPixels(x: x, y: y, blockWidth: width, blockHeight: height));

        texture.Apply();
        return texture;
      }

      public static Texture2D Texture([NotNull] SpriteAtlas atlas, string name) {
        Debug.LogWarning("**** Contents:28 atlas.name=" + atlas.name +
                         "  #### DELETE-ME #### 10/4/18 9:33 PM"); //#DM#//

        Debug.LogWarning("**** Contents:28 name=" + name +
                         "  #### DELETE-ME #### 10/4/18 9:33 PM"); //#DM#//

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
}