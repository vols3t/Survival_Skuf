using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip gamePlayMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayGamePlayMusic()
    {
        PlayMusic(gamePlayMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}