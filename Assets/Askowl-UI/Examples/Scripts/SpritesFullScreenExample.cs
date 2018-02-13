using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesFullScreenExample : Sprites.FullScreen {

  // Use this for initialization
  void Start() {
		
  }
	
  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown("space")) {
      gameObject.SetActive(false);
    }
  }

  public void ShowFullScreen() {
    if (gameObject.activeSelf) {
      gameObject.SetActive(false);
    } else {
      gameObject.SetActive(true);
    }
  }
}
