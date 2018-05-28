namespace Askowl {
  using System.Collections;
  using JetBrains.Annotations;
  using UnityEngine;

  public sealed class CanvasGroupFader : MonoBehaviour {
    // ReSharper disable once RedundantDefaultMemberInitializer
    [SerializeField] private float lowAlpha          = 0;
    [SerializeField] private float highAlpha         = 1;
    [SerializeField] private float overTimeInSeconds = 1;

    private Canvas             canvas;
    private CanvasGroup        canvasGroup;
    private CanvasGroupFader[] childCanvasGroupFaders;

    private void Awake() {
      canvas      = GetComponent<Canvas>();
      canvasGroup = GetComponent<CanvasGroup>();
      if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
      canvasGroup.alpha      = 0;
      childCanvasGroupFaders = GetComponentsInChildren<CanvasGroupFader>();
    }

    public static Coroutine FadeIn(Canvas canvas) {
      CanvasGroupFader[] canvasGroupFaders = canvas.GetComponentsInChildren<CanvasGroupFader>();
      canvas.enabled = true;
      return (canvasGroupFaders.Length == 0) ? null : canvasGroupFaders[0].FadeIn();

      return null;
    }

    public static Coroutine FadeOut(Canvas canvas) {
      CanvasGroupFader[] canvasGroupFaders = canvas.GetComponentsInChildren<CanvasGroupFader>();
      canvas.enabled = false;
      return (canvasGroupFaders.Length == 0) ? null : canvasGroupFaders[0].FadeOut();
    }

    private Coroutine FadeIn() { return StartCoroutine(FadeInChildCanvasGroups()); }

    private Coroutine FadeOut() { return StartCoroutine(FadeOutChildCanvasGroups()); }

    private IEnumerator FadeInChildCanvasGroups() {
      for (int i = 0; i < childCanvasGroupFaders.Length; i++) {
        yield return childCanvasGroupFaders[i].FadeInCanvasGroup();
      }
    }

    private IEnumerator FadeOutChildCanvasGroups() {
      for (int i = childCanvasGroupFaders.Length - 1; i >= 0; i--) {
        yield return childCanvasGroupFaders[i].FadeOutCanvasGroup();
      }
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