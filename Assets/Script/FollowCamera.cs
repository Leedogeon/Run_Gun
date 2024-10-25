using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;
    public float pLerp = .02f;
    public float rLerp = .01f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offset, pLerp);
        transform.LookAt(Target);
        transform.position = Target.position + offset;
    }
}
