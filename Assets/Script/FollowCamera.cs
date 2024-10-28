using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Initialize PlayerPrefab;
    public Transform Target;
    public Vector3 offset;

    void Start()
    {
        PlayerPrefab = FindObjectOfType<Initialize>();
        Target = PlayerPrefab.PlayerInstance.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Target.position.z);
    }
    void OnDisable()
    {
        // 다른 화면으로 전환할 때 커서 복원
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
