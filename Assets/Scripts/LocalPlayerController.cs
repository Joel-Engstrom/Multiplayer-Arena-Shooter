using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class LocalPlayerController : NetworkBehaviour
{
    public Camera PlayerCam;
    public CinemachineFreeLook CineMachineCamera;

    void FixedUpdate()
    {
        //PlayerCam = GetComponentInChildren<Camera>();
        //CineMachineCamera = GetComponentInChildren<CinemachineFreeLook>();

        if (isLocalPlayer)
        {
            PlayerCam.gameObject.SetActive(true);
            CineMachineCamera.gameObject.SetActive(true);
        }
    }
}
