using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class GroundChecker : MonoBehaviour
{
    public GameObject player;
    public AudioSource walkSrc;   // For step sounds
    public AudioClip grassWalkSFX;
    public AudioClip sandWalkSFX;
    public AudioClip woodWalkSFX;

    public AudioClip natureSFX; // For Grass
    public AudioClip seaSFX;    // For Sand

    public Tilemap[] tilemaps; // Assign all ground Tilemaps in the Inspector
    private string currentGroundTag = "Default"; // Default ground type

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        foreach (Tilemap tilemap in tilemaps)
        {
            Vector3Int tilePosition = tilemap.WorldToCell(player.transform.position); // Convert to Tilemap grid position
            TileBase tile = tilemap.GetTile(tilePosition); // Get the tile at that position

            if (tile != null) // If a tile exists in this Tilemap, use its tag
            {
                string newGroundTag = tilemap.gameObject.tag;

                if (newGroundTag != currentGroundTag) // Only switch if the ground changes
                {
                    currentGroundTag = newGroundTag;
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

}
