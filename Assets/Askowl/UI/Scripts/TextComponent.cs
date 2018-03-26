namespace Askowl {
  using System;
  using UnityEngine;
  using UnityEngine.UI;
#if TextMeshPro
  using TMPro;
#endif

  public sealed class TextComponent : MonoBehaviour {
#if TextMeshPro
    private TextMeshProUGUI tmpComponent;
#endif
    private Text textComponent;

    private Func<string>   getText;
    private Action<string> setText;

    private void Awake() {
      textComponent = GetComponent<Text>();

      if (textComponent != null) {
        getText = () => textComponent.text;
        setText = (value) => textComponent.text = value;
      }
#if TextMeshPro
      tmpComponent = GetComponent<TextMeshProUGUI>();

      if (tmpComponent != null) {
        getText = () => tmpComponent.text;
        setText = (value) => tmpComponent.text = value;
      }
#endif
    }

    public string text { get { return getText(); } set { setText(value); } }
  }
}