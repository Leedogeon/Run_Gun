using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinemachine : MonoBehaviour
{
    void Start()
    {
        // Ŀ�� ����� �� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // �ٸ� ȭ������ ��ȯ�� �� Ŀ�� ����
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
