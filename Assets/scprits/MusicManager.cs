using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Header("Музыка")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || audioSource.clip == clip)
            return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }
    public void PlayGamePlayMusic()
    {
        PlayGameplayMusic();
    }
}
