using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] musicTracks;

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
