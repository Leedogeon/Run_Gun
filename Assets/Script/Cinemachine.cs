using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinemachine : MonoBehaviour
{
    void Start()
    {
        // 커서 숨기기 및 잠금
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // 다른 화면으로 전환할 때 커서 복원
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
