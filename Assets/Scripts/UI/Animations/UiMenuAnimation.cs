using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;

    [Header("Animation Settings")]
    public float animationDuration = 0.3f;
    public bool justUseFade = false;

    void Awake()
    {
        // اگر CanvasGroup وجود نداشت خودش بساز
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        // وقتی فعال میشه → افکت ورود
        StopAllCoroutines();
        StartCoroutine(ShowEffect());
    }

    public void Close()
    {
        // وقتی می‌خوای پنل بسته بشه → افکت خروج
        StopAllCoroutines();
        StartCoroutine(HideEffect());
    }

    IEnumerator ShowEffect()
    {
        if (justUseFade)
        {
            // فقط fade
            canvasGroup.alpha = 0;
            float t = 0;
            while (t < animationDuration)
            {
                t += Time.unscaledDeltaTime;
                float progress = t / animationDuration;
                canvasGroup.alpha = progress;
                yield return null;
            }
            canvasGroup.alpha = 1;
        }
        else
        {
            // fade + scale
            transform.localScale = Vector3.zero;
            canvasGroup.alpha = 0;

            float t = 0;
            while (t < animationDuration)
            {
                t += Time.unscaledDeltaTime;
                float progress = t / animationDuration;

                transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, progress);
                canvasGroup.alpha = progress;

                yield return null;
            }

            transform.localScale = originalScale;
            canvasGroup.alpha = 1;
        }
    }

    IEnumerator HideEffect()
    {
        if (justUseFade)
        {
            // فقط fade
            float startAlpha = canvasGroup.alpha;
            float t = 0;
            while (t < animationDuration)
            {
                t += Time.unscaledDeltaTime;
                float progress = t / animationDuration;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
                yield return null;
            }
            canvasGroup.alpha = 1; // reset برای دفعات بعد
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            // fade + scale
            float t = 0;
            Vector3 startScale = transform.localScale;
            float startAlpha = canvasGroup.alpha;

            while (t < animationDuration)
            {
                t += Time.unscaledDeltaTime;
                float progress = t / animationDuration;

                transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                yield return null;
            }

            transform.localScale = originalScale;
            canvasGroup.alpha = 1;
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
