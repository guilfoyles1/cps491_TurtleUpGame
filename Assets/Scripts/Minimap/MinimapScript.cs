<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        }
    }
}


=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        }
    }
}


>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
