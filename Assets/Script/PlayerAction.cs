using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator anim;
    private float MoveForward;
    private float MoveRight;
    public float speed = 5f;

    public Vector2 turn;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    void GetInput()
    {
        MoveRight = Input.GetAxis("Horizontal");
        MoveForward = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", MoveForward);
        anim.SetFloat("Direction", MoveRight);
        anim.speed = speed*.3f;
    }

    void Move()
    {
        transform.Translate(Vector3.forward * MoveForward * Time.deltaTime * speed);
        transform.Translate(Vector3.right * MoveRight * Time.deltaTime * speed);
    }
    void Turn()
    {

        turn.x += Input.GetAxisRaw("Mouse X");
        turn.y += Input.GetAxisRaw("Mouse Y");
        //turn.y = Mathf.Clamp(turn.y, -30, MaxY);

        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }

}
