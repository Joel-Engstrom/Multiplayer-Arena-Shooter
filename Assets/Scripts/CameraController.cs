using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook Cam;

    private InputDevice Controller = InputManager.ActiveDevice;

    private float CameraYValue;
    private float CameraXValue;

    private void Update()
    {
        //Uppdatera vilken kontroller som används.
        Controller = InputManager.ActiveDevice;
        //Spara Värdena på X och Y till kameran.
        CameraXValue = Controller.RightStickX.Value;
        CameraYValue = Controller.RightStickY.Value;
        //Uppdatera kamerans position.
        Cam.m_XAxis.m_InputAxisValue = CameraXValue;
        Cam.m_YAxis.m_InputAxisValue = CameraYValue;
    }
}
