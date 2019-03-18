using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class LocalPlayerController : NetworkBehaviour
{
    public List<GameObject> objectsToActivate;

    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            foreach (GameObject item in objectsToActivate)
            {
                item.SetActive(true);
            }
        }
    }
}
