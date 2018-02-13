# UI Support
[TOC]
> Read the code in the Examples Folder.

## Scroller

A ```Scroller``` is a class that allows a content 2D rectangular region to move through a viewport rectangle in a defined direction and at a defined speed.

It is easier to show than explain. Open the Askowl-Lib-Example scene and press the ***Scroller Example*** button. A green rectangle representing the viewport will appear. Wait, and another button will drift from the lower right to the upper left.

To have the content visible only in the viewport, give the viewport panel a ***Rect Mask 2D*** component.

The MonoBehaviour ```Start``` method creates a new scroller from the RectTransform of the components provided. I then set the stepping so that the content will move up and left at 45 degrees. Using pixels per second means that the speed will vary drastically between displays of different resolutions.

```C#
public class ScrollerExample : MonoBehaviour {

  public GameObject viewport;
  public GameObject content;
  public int pixelsPerSecond = 100;

  Scroller scroller;
  bool active = false;

  void Start() {
    scroller = new Scroller (
        viewport.GetComponent<RectTransform>(),
        content.GetComponent<RectTransform>());
    scroller.step.x = -1; // moves left to right
    scroller.step.y = 1; // moves bottom to top
  }

  void Update() {
    if (active) {
      if (!scroller.Step(pixelsPerSecond * Time.fixedUnscaledDeltaTime)) {
        viewport.SetActive(false);
        active = false;
      }
    }
  }

  public void ButtonPressed() {
    viewport.SetActive(true);
    active = true;
  }
}
```

To have the content follow a non-linear path, recalculate the step after each update. Note that ```scroller.Step``` returns true while the content approaches or is in the viewport.

## Sprites

### Cache

The Cache class is all about a SpriteAtlas, and a memory leak in Android. Many 2D games use a series of images to create animation. 

Due to sprite compression limitations (width and height must be a power of two), keeping the sprite in the project as individual files causes a bloated app. When it exceeds 100Mb, the Google Play Store refuses to accept it.

One solution is to combine all the images into a SpriteAtlas. Unity3D does this automatically. Just give it some files or directories and get a single file to load.

Now, if we have a character walking along at 60 frames a second, we are loading 60 sprites from the atlas in a second while cycling through the images that make us the walk animation.

Surely the SpriteAtlas is optimised. It may well be, but with Unity3D 2017 this creates a memory leak in the Native Android (C++) heap. The Java heap is ok. Only when we drop to the C++ layer deep inside the Android kernel, do things go awry. It is probably due to optimisation for graphics display - and may very well be hardware specific.

The symptom is an app that quits and goes back to the Android front page without any message at any level. It is just as if you had pressed the quit button if there was one. Android is behaving itself. Android terminates an app if it is taking too much native heap. Usually, this is reasonably transparent to the user with background apps. Switching back to them takes a bit longer. What the user sees depends on the quality of state persistence. When Android terminates a foreground application, then it is a tiny bit more obvious.

Don't ask how long it took me to track this little fellow down. Fortunately caching the sprites resolves the issue.

```C#
public SpriteAtlas spriteAtlas;

Sprite.Cache atlas;

void Start() {
  atlas = Sprite.Cache.Atlas(spriteAtlas);
}

Sprite getSprite(string name) {
  if (atlas.sprites.Contains(name) {
    return atlas.sprites[name];
  }
  return null;
}
```

### Contents

In most normal cases sprites are provided as-is for the 2D application. There are cases where you need the texture the asset contains using `sprite.texture`. For one in an atlas, however, this will point the atlas. Internal sprite-aware processes use the member `TextureRect` for the offset. Great, but a pure `Texture2D` does not have an offset. The example below extracts a texture by reference or atlas/name.

```C#
    Sprites.Cache cache = Sprites.Cache.Atlas(spriteAtlas);
    Sprite attack1 = cache.sprite ["Attack_1"];

    Texture2D texture1a = Sprites.Contents.Texture(attack1);
    Assert.NotNull(texture1a);

    Texture2D texture1b = Sprites.Contents.Texture(spriteAtlas, "Attack_1");
    Assert.NotNull(texture1b);

    Assert.AreEqual(texture1a.imageContentsHash, texture1b.imageContentsHash);
```