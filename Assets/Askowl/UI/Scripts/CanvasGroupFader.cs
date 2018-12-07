namespace Askowl {
  using System.Collections;
  using UnityEngine;

  /// <a href=""></a> //#TBD#//
  public sealed class CanvasGroupFader : MonoBehaviour {
    // ReSharper disable once RedundantDefaultMemberInitializer
    [SerializeField] private float lowAlpha = 0;
    [SerializeField] private float highAlpha = 1;
    [SerializeField] private float overTimeInSeconds = 1;

    private CanvasGroup canvasGroup;
    private CanvasGroupFader[] childCanvasGroupFaders;

    private void Awake() {
      canvasGroup = GetComponent<CanvasGroup>();
      if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
      canvasGroup.alpha = 0;
      childCanvasGroupFaders = GetComponentsInChildren<CanvasGroupFader>();
    }

    /// <a href=""></a> //#TBD#//
    public static Coroutine FadeIn(Canvas canvas) {
      CanvasGroupFader[] canvasGroupFaders = canvas.GetComponentsInChildren<CanvasGroupFader>();
      canvas.enabled = true;
      return canvasGroupFaders.Length == 0 ? null : canvasGroupFaders[0].FadeIn();
    }

    /// <a href=""></a> //#TBD#//
    public static Coroutine FadeOut(Canvas canvas) {
      CanvasGroupFader[] canvasGroupFaders = canvas.GetComponentsInChildren<CanvasGroupFader>();

      if (canvasGroupFaders.Length == 0) {
        canvas.enabled = false;
        return null;
      }

      return canvasGroupFaders[0].FadeOutAll(canvas);
    }

    private Coroutine FadeIn() => StartCoroutine(FadeInChildCanvasGroups());

    private Coroutine FadeOutAll(Canvas canvas) => StartCoroutine(FadeOutChildCanvasGroups(canvas));

    private IEnumerator FadeInChildCanvasGroups() {
      for (var i = 0; i < childCanvasGroupFaders.Length; i++) {
        yield return childCanvasGroupFaders[i].FadeInCanvasGroup();
      }
    }

    private IEnumerator FadeOutChildCanvasGroups(Canvas canvas) {
      for (int i = childCanvasGroupFaders.Length - 1; i >= 0; i--) {
        yield return childCanvasGroupFaders[i].FadeOutCanvasGroup();
      }

      canvas.enabled = false;
    }

    private IEnumerator FadeInCanvasGroup() {
      yield return FadeCanvasGroup(startAlpha: lowAlpha, endAlpha: highAlpha);
    }

    private IEnumerator FadeOutCanvasGroup() {
      yield return FadeCanvasGroup(startAlpha: highAlpha, endAlpha: lowAlpha);
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha) {
      float startTime = Time.time, percentageComplete = 0;

      while (percentageComplete < 1) {
        float timeElapsed = Time.time - startTime;
        percentageComplete = timeElapsed / overTimeInSeconds;
        float currentValue = Mathf.Lerp(a: startAlpha, b: endAlpha, t: percentageComplete);
        canvasGroup.alpha = currentValue;
        yield return new WaitForFixedUpdate();
      }
    }
  }
}