using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase killCam;
    
    public void EnableKillCam()
    {
        killCam.Priority = 20;
    }
}
