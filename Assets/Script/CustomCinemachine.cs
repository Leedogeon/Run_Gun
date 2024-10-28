using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemCustomCinemachineachine : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    void Start()
    {
        GameObject PlayerInstance = Initialize.Instance.GetPlayerInstance();
        freeLookCamera = GetComponent<CinemachineFreeLook>();

        if (PlayerInstance != null)
        {
            Transform playerCameraRoot = PlayerInstance.transform.Find("PlayerCameraRoot");
            freeLookCamera.Follow = PlayerInstance.transform;
            freeLookCamera.LookAt = playerCameraRoot;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
