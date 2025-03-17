using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class GroundChecker : MonoBehaviour
{
    public AudioSource walkSrc;   // For step sounds
    public AudioSource ambientSrc; // For background ambient noise

    public AudioClip grassWalkSFX;
    public AudioClip sandWalkSFX;
    public AudioClip woodWalkSFX;

    public AudioClip natureSFX; // For Grass
    public AudioClip seaSFX;    // For Sand & Wood

    public Tilemap[] tilemaps; // Assign all ground Tilemaps in the Inspector
    private string currentGroundTag = "Default"; // Default ground type
    private Coroutine fadeCoroutine; // Used to handle volume fading

    void Start()
    {
        if (ambientSrc != null)
        {
            ambientSrc.loop = true;
        }
    }

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Vector3Int tilePosition;

        foreach (Tilemap tilemap in tilemaps)
        {
            tilePosition = tilemap.WorldToCell(transform.position); // Convert to Tilemap grid position
            TileBase tile = tilemap.GetTile(tilePosition); // Get the tile at that position

            if (tile != null) // If a tile exists in this Tilemap, use its tag
            {
                string newGroundTag = tilemap.gameObject.tag;

                if (newGroundTag != currentGroundTag) // Only switch if the ground changes
                {
                    currentGroundTag = newGroundTag;
                    StartAmbientTransition();
                }

                return;
            }
        }

        currentGroundTag = "Default"; // No tile detected
    }

    public void PlayWalkSound()
    {
        if (!walkSrc.isPlaying)
        {
            walkSrc.clip = GetClipForSurface();
            walkSrc.pitch = 1.6f;
            walkSrc.Play();
        }
    }

    public void StopWalkSound()
    {
        walkSrc.Stop();
    }

    private AudioClip GetClipForSurface()
    {
        switch (currentGroundTag)
        {
            case "Grass": return grassWalkSFX;
            case "Sand": return sandWalkSFX;
            case "Wood": return woodWalkSFX;
            default: return grassWalkSFX; // Default to grass sound
        }
    }

    private void StartAmbientTransition()
    {
        AudioClip newAmbientClip = null;

        if (currentGroundTag == "Grass")
        {
            newAmbientClip = natureSFX;
        }
        else if (currentGroundTag == "Sand" || currentGroundTag == "Wood")
        {
            newAmbientClip = seaSFX;
        }

        if (newAmbientClip != null && ambientSrc.clip != newAmbientClip)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine); // Stop any existing fade transition

            fadeCoroutine = StartCoroutine(FadeToNewAmbient(newAmbientClip));
        }
    }

    private IEnumerator FadeToNewAmbient(AudioClip newClip)
    {
        float fadeDuration = 1.5f; // Adjust fade time for smoother transition
        float startVolume = ambientSrc.volume;

        // Fade out old sound
        while (ambientSrc.volume > 0.05f) // Avoid cutting off abruptly
        {
            ambientSrc.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // ✅ Instead of stopping, we just change the clip and let it loop
        ambientSrc.clip = newClip;
        ambientSrc.loop = true; // ✅ Ensure ambient sound loops
        ambientSrc.Play();

        // Fade in new sound
        while (ambientSrc.volume < startVolume)
        {
            ambientSrc.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        ambientSrc.volume = startVolume; // Ensure final volume is set correctly
    }
}
