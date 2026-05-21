using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LoopVideoFix : MonoBehaviour
{
    private VideoPlayer vp;

    [Header("UI Fade")]
    [SerializeField] private CanvasGroup uiCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();

        vp.playOnAwake = false;
        vp.isLooping = false;

        // an ui khi video chua san sang
        if (uiCanvasGroup != null)
            uiCanvasGroup.alpha = 0f;

        vp.prepareCompleted += OnPrepared;
        vp.loopPointReached += OnVideoEnd;
    }

    void Start()
    {
        vp.Prepare();
    }

    void OnPrepared(VideoPlayer source)
    {
        source.Play();

        if (uiCanvasGroup != null)
            StartCoroutine(FadeUI());
    }

    IEnumerator FadeUI()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            uiCanvasGroup.alpha =
                Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        uiCanvasGroup.alpha = 1f;
    }

    void OnVideoEnd(VideoPlayer source)
    {
        source.time = 0;
        source.Play();
    }

    void OnDestroy()
    {
        vp.prepareCompleted -= OnPrepared;
        vp.loopPointReached -= OnVideoEnd;
    }
}