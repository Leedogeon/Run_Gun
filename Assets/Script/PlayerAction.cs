using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator anim;
    private float MoveForward;
    private float MoveRight;
    public float speed = 5f;

    private bool isGrounded = false;
    private bool jDown;
    public float jumpPower = 5f;
    public Vector2 turn;







    public float jumpForce = 5f; // ���� ��
    public float jumpDistance = 3f; // X������ �̵��� �Ÿ�
    public float jumpDuration = 1f; // �����ϴ� �� �ɸ��� �ð�
    private float jumpStartTime; // ���� ���� �ð�
    private bool isJumping; // ���� ������ Ȯ��
    private Vector3 startPosition; // ���� ���� ��ġ
    private Vector3 targetPosition; // ���� ��ǥ ��ġ
    private bool GoSide;


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
        //Jump();

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            StartJump();
        }

        // ���� ���� ��
        if (isJumping && GoSide)
        {
            UpdateJump();
        }

    }

    void GetInput()
    {
        MoveRight = Input.GetAxis("Horizontal");
        MoveForward = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", MoveForward);
        anim.speed = speed*.3f;
        jDown = Input.GetButtonDown("Jump");
        anim.SetBool("Jump", jDown);
    }

    void Move()
    {

        transform.Translate(Vector3.forward * MoveForward * Time.deltaTime * speed);
        
    }
    void StartJump()
    {
        if (MoveRight != 0) GoSide = true;
        // �����ϱ�
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // ���� ���� �ð��� ��ġ ����
        jumpStartTime = Time.time;
        startPosition = transform.position;

        bool LR = true;
        if (MoveRight > 0) LR = true;
        else if(MoveRight < 0) LR = false;

        targetPosition = new Vector3(startPosition.x + jumpDistance*(LR ? 1f:-1f), startPosition.y, startPosition.z);

        isJumping = true;
    }

    void UpdateJump()
    {
        float t = (Time.time - jumpStartTime) / jumpDuration; // ���� �ð� ����
        if (t > 1f)
        {
            t = 1f; // ������ 1�� �ʰ����� �ʵ��� ����
            isJumping = false; // ���� ����
            isGrounded = false; // ���� ���� ����
        }

        // �ڿ������� X�� �̵�
        Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);
        newPosition.y = transform.position.y; // Y���� ����

        // Z������ �̵��� ���ÿ� ó��
        newPosition.z = transform.position.z + (MoveForward * Time.deltaTime * speed *.1f);

        transform.position = newPosition; // ��ġ ������Ʈ
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���� ��Ҵ��� Ȯ��
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            GoSide = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // ������ �������� ��
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

}
