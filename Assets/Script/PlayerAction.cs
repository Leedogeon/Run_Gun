using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Transform Drone;
    private GameObject Bullet;

    public GameObject Target;
    public float detectionRange = 20f;       // 탐지 거리
    public Vector3 boxSize = new Vector3(1, 4, 1); // Y축을 넓힌 탐지 박스의 크기
    private LayerMask targetLayer;            // 탐지할 레이어 (적이 있는 레이어)


    private bool Is3D;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ViewPoint();

        targetLayer = LayerMask.GetMask("Enemy");
        Drone = transform.Find("Drone");
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }
    void ViewPoint()
    {
        string curScene = SceneManager.GetActiveScene().name;
        if (curScene == "3DScene")
            Is3D = true;
        else Is3D = false;
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

        if(Input.GetButtonDown("Fire"))
        {
            Attack();
        }




        if (Physics.BoxCast(transform.position, boxSize/2, transform.forward, out RaycastHit hit, transform.rotation, detectionRange, targetLayer))
        {
            Target = hit.collider.gameObject;
        }
    }

    void GetInput()
    {
        /*MoveRight = Is3D ? Input.GetAxis("Horizontal") : Input.GetAxis("Vertical")*-1;*/
        MoveRight = Is3D ? Input.GetAxis("Horizontal") : 0;
        MoveForward = Is3D ? Input.GetAxis("Vertical") : Input.GetAxis("Horizontal");
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


    void Attack()
    {


        GameObject NewBullet = Instantiate(Bullet, Drone.transform.position, Bullet.transform.rotation);
        Bullet BulletScript = NewBullet.GetComponent<Bullet>();
        BulletScript.Target = Target;
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * detectionRange / 2, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, detectionRange));
    }
}
