using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource musicSource; // Assign in Inspector
    public AudioClip[] musicTracks; // Assign multiple tracks or just one

    private int currentTrackIndex = 0;

    void Start()
    {
        if (musicTracks.Length > 0 && musicSource != null)
        {
            musicSource.loop = false; // Disable looping for individual tracks
            musicSource.clip = musicTracks[currentTrackIndex];
            musicSource.Play();
            Invoke("PlayNextTrack", musicSource.clip.length);
        }
    }

    void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        musicSource.clip = musicTracks[currentTrackIndex];
        musicSource.Play();
        Invoke("PlayNextTrack", musicSource.clip.length);
    }
}
