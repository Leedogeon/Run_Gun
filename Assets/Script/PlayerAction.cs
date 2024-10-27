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







    public float jumpForce = 5f; // 점프 힘
    public float jumpDistance = 3f; // X축으로 이동할 거리
    public float jumpDuration = 1f; // 점프하는 데 걸리는 시간
    private float jumpStartTime; // 점프 시작 시간
    private bool isJumping; // 점프 중인지 확인
    private Vector3 startPosition; // 점프 시작 위치
    private Vector3 targetPosition; // 점프 목표 위치
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

        // 점프 중일 때
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
        // 점프하기
        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // 점프 시작 시간과 위치 설정
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
        float t = (Time.time - jumpStartTime) / jumpDuration; // 점프 시간 비율
        if (t > 1f)
        {
            t = 1f; // 비율이 1을 초과하지 않도록 조정
            isJumping = false; // 점프 종료
            isGrounded = false; // 땅에 닿지 않음
        }

        // 자연스러운 X축 이동
        Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);
        newPosition.y = transform.position.y; // Y값은 유지

        // Z축으로 이동도 동시에 처리
        newPosition.z = transform.position.z + (MoveForward * Time.deltaTime * speed *.1f);

        transform.position = newPosition; // 위치 업데이트
    }

    void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿았는지 확인
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            GoSide = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 땅에서 떨어졌을 때
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

}
