using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    [Header("SFX")]
    public AudioClip touchSFX;
    public AudioClip mergeSFX;
    public AudioClip hammerSFX;
    public AudioClip magnetSFX;
    public AudioClip bombSFX;
    public AudioClip swapSFX;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ================= SCENE MUSIC =================
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Game")
        {
            PlayMusic(gameplayMusic);
        }
        else
        {
            musicSource.Stop();
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip &&musicSource.isPlaying)
            return;

        musicSource.Stop();

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = 0.6f;

        musicSource.Play();
    }

    // ================= SFX =================
    public void PlayTouch() => PlaySFX(touchSFX);
    public void PlayMerge() => PlaySFX(mergeSFX);
    public void PlayHammer() => PlaySFX(hammerSFX);
    public void PlayMagnet() => PlaySFX(magnetSFX);
    public void PlayBomb() => PlaySFX(bombSFX);
    public void PlaySwap() => PlaySFX(swapSFX);

    void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }
}
