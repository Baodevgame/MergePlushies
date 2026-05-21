using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashLoader : MonoBehaviour
{
    public string nextSceneName = "MainMenu";

    public CanvasGroup logoCanvasGroup;

    public float fadeInTime = 1f;
    public float fadeOutTime = 1f;
    public float waitAfterLoad = 0.5f;

    void Start()
    {
        logoCanvasGroup.alpha = 0f;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // Fade In Logo
        yield return StartCoroutine(Fade(0f, 1f, fadeInTime));

        // Load Scene Async
        AsyncOperation op =
            SceneManager.LoadSceneAsync(nextSceneName);

        op.allowSceneActivation = false;

        // cho load gan xong
        while (op.progress < 0.9f)
        {
            yield return null;
        }

        // cho them
        yield return new WaitForSeconds(waitAfterLoad);

        // Fade Out Logo
        yield return StartCoroutine(Fade(1f, 0f, fadeOutTime));

        // chuyen scene
        op.allowSceneActivation = true;
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            logoCanvasGroup.alpha =
                Mathf.Lerp(from, to, t / duration);

            yield return null;
        }

        logoCanvasGroup.alpha = to;
    }
}