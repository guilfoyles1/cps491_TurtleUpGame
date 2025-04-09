using UnityEngine;

public class SeaSFXTrigger : MonoBehaviour
{
    [SerializeField] AudioSource seaAudioSource;
    [SerializeField] float fadeSpeed = .25f; // Speed at which volume changes
    private float targetVolume = 0f;

    public float playerSetVolume;

    private bool inArea = false;
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
            targetVolume = playerSetVolume; // Set target volume to full
            inArea = true;
        }
    }

    public void setVolume()
    {
        if (inArea)
        {
            seaAudioSource.volume = playerSetVolume;
            targetVolume = playerSetVolume;
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0f; // Set target volume to zero
            inArea = false;
        }
    }
}
