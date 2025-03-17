using UnityEngine;

public class SeaSFXTrigger : MonoBehaviour
{
    public AudioSource seaAudioSource; // Assign in Inspector
    public float fadeSpeed = .25f; // Speed at which volume changes
    private float targetVolume = 0f; // Desired volume level

    void Start()
    {
        if (seaAudioSource != null)
        {
            seaAudioSource.volume = 0f; // Start muted
            seaAudioSource.loop = true; // Ensure it loops
            seaAudioSource.Play(); // Start playing immediately but muted
        }
    }

    void Update()
    {
        if (seaAudioSource != null)
        {
            // Smoothly move volume towards targetVolume
            seaAudioSource.volume = Mathf.MoveTowards(seaAudioSource.volume, targetVolume, fadeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = .175f; // Set target volume to full
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0f; // Set target volume to zero
        }
    }
}
