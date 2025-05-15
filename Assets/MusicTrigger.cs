using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip scaryMusic;
    public AudioClip normalMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(scaryMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(normalMusic);
        }
    }
}
