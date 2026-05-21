using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour
{
    public float fadeDuration = 0.25f;

    private CanvasGroup cg;

    private Coroutine fadeRoutine;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(1));
    }

    public void FadeOut()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(0));
    }

    IEnumerator Fade(float target)
    {
        float start = cg.alpha;

        float time = 0;

        if (target == 1)
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;

            float t = time / fadeDuration;

            // smoothstep
            t = t * t * (3f - 2f * t);

            cg.alpha = Mathf.Lerp(start, target, t);

            yield return null;
        }

        cg.alpha = target;

        if (target == 0)
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;

            gameObject.SetActive(false);
        }
    }
}