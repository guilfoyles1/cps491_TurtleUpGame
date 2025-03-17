using UnityEngine;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource musicSource; // Assign an AudioSource for music playback
    public AudioClip[] musicTracks; // Assign music tracks in the Inspector

    public float minPauseTime = 10f; // Minimum pause time (in seconds) between tracks
    public float maxPauseTime = 30f; // Maximum pause time (in seconds) between tracks

    private bool isPlaying = false;

    void Start()
    {
        StartCoroutine(PlayMusicLoop());
    }

    private IEnumerator PlayMusicLoop()
    {
        while (true) // Infinite loop for continuous music
        {
            if (!isPlaying && musicTracks.Length > 0)
            {
                isPlaying = true;
                AudioClip newTrack = GetRandomTrack();
                musicSource.clip = newTrack;
                musicSource.Play();

                Debug.Log("Playing Track: " + newTrack.name);

                // Wait for the song to finish
                yield return new WaitForSeconds(newTrack.length);

                isPlaying = false;

                // Wait for a random pause before playing the next song
                float pauseDuration = Random.Range(minPauseTime, maxPauseTime);
                Debug.Log("Pausing for " + pauseDuration + " seconds...");
                yield return new WaitForSeconds(pauseDuration);
            }
            yield return null; // Wait for the next frame to check again
        }
    }

    private AudioClip GetRandomTrack()
    {
        return musicTracks[Random.Range(0, musicTracks.Length)];
    }
}
