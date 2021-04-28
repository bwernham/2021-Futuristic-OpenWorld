using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamZoom : MonoBehaviour
{
    public CinemachineFreeLook cam;
    void Update()
    {
        if (Input.GetAxis ("Mouse ScrollWheel") > 0)
        {
            cam.
        }
        else if (Input.GetAxis ("Mouse ScrollWheel") < 0)
        {
            cam.m_Lens.FieldOfView++;
        }
    }
}
