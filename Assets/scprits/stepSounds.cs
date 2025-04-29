using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D))]
public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepClip;
    public float stepInterval = 0.4f;

    private float stepTimer;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        footstepSource = footstepSource ?? GetComponent<AudioSource>();
        footstepSource.playOnAwake = false;
        footstepSource.loop = false;
        stepTimer = stepInterval;
    }

    void Update()
    {
        // Если двигаемся
        if (rb.velocity.magnitude > 0.1f && footstepClip != null)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                footstepSource.PlayOneShot(footstepClip);
                stepTimer = 0f;
            }
        }
        else
        {
            // сброс, чтобы при старте движения сразу заиграл шаг
            stepTimer = stepInterval;
        }
    }
}