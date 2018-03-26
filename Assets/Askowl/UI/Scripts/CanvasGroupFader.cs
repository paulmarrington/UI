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

    [CanBeNull]
    // ReSharper disable once UnusedMember.Global
    public static Coroutine FadeIn([NotNull] Canvas canvas) {
      CanvasGroupFader canvasGroupFader = canvas.GetComponent<CanvasGroupFader>();
      if (canvasGroupFader != null) return canvasGroupFader.FadeIn();

      canvas.enabled = true;
      return null;
    }

    [CanBeNull]
    // ReSharper disable once UnusedMember.Global
    public static Coroutine FadeOut([NotNull] Canvas canvas) {
      CanvasGroupFader canvasGroupFader = canvas.GetComponent<CanvasGroupFader>();
      if (canvasGroupFader != null) return canvasGroupFader.FadeOut();

      canvas.enabled = false;
      return null;
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

    private void SetActive(bool active) {
      if (canvas != null) {
        canvas.enabled = active;
      } else {
        gameObject.SetActive(active);
      }
    }

    private IEnumerator FadeInCanvasGroup() {
      SetActive(true);
      yield return FadeCanvasGroup(startAlpha: lowAlpha, endAlpha: highAlpha);
    }

    private IEnumerator FadeOutCanvasGroup() {
      yield return FadeCanvasGroup(startAlpha: highAlpha, endAlpha: lowAlpha);

      if (lowAlpha < 0.1) SetActive(false);
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