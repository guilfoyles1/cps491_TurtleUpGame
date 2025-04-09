<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;

public class PlayerBinDetector : MonoBehaviour
{
    public bool isNearBin { get; private set; }
    public string currentBinTag { get; private set; }

    [SerializeField] GameObject glassBin;
    [SerializeField] GameObject metalBin;
    [SerializeField] GameObject plasticBin;
    [SerializeField] GameObject paperBin;
    [SerializeField] GameObject trashBin;

    private Dictionary<string, GameObject> binMap;

    void Start()
    {
        binMap = new Dictionary<string, GameObject>
        {
            { "GlassBin", glassBin },
            { "MetalBin", metalBin },
            { "PlasticBin", plasticBin },
            { "PaperBin", paperBin },
            { "TrashBin", trashBin }
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (binMap.ContainsKey(other.tag))
        {
            currentBinTag = other.tag;
            isNearBin = true;
            SetOnlyCurrentBinActive(currentBinTag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isNearBin && other.tag == currentBinTag)
        {
            isNearBin = false;
            currentBinTag = null;
            SetOnlyCurrentBinActive(""); // Hide all
        }
    }

    private void SetOnlyCurrentBinActive(string tag)
    {
        foreach (var bin in binMap.Values)
        {
            if (bin != null)
                bin.SetActive(false);
        }

        if (binMap.ContainsKey(tag))
            binMap[tag].SetActive(true);
    }
}
=======
using System.Collections.Generic;
using UnityEngine;

public class PlayerBinDetector : MonoBehaviour
{
    public bool isNearBin { get; private set; }
    public string currentBinTag { get; private set; }

    [SerializeField] GameObject glassBin;
    [SerializeField] GameObject metalBin;
    [SerializeField] GameObject plasticBin;
    [SerializeField] GameObject paperBin;
    [SerializeField] GameObject trashBin;

    private Dictionary<string, GameObject> binMap;

    void Start()
    {
        binMap = new Dictionary<string, GameObject>
        {
            { "GlassBin", glassBin },
            { "MetalBin", metalBin },
            { "PlasticBin", plasticBin },
            { "PaperBin", paperBin },
            { "TrashBin", trashBin }
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (binMap.ContainsKey(other.tag))
        {
            currentBinTag = other.tag;
            isNearBin = true;
            SetOnlyCurrentBinActive(currentBinTag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isNearBin && other.tag == currentBinTag)
        {
            isNearBin = false;
            currentBinTag = null;
            SetOnlyCurrentBinActive(""); // Hide all
        }
    }

    private void SetOnlyCurrentBinActive(string tag)
    {
        foreach (var bin in binMap.Values)
        {
            if (bin != null)
                bin.SetActive(false);
        }

        if (binMap.ContainsKey(tag))
            binMap[tag].SetActive(true);
    }
}
>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
