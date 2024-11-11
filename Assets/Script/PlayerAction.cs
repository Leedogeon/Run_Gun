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


    public float jumpForce = 5f; // ���� ��
    public float jumpDistance = 3f; // X������ �̵��� �Ÿ�
    public float jumpDuration = 1f; // �����ϴ� �� �ɸ��� �ð�
    private float jumpStartTime; // ���� ���� �ð�
    private bool isJumping; // ���� ������ Ȯ��
    private Vector3 startPosition; // ���� ���� ��ġ
    private Vector3 targetPosition; // ���� ��ǥ ��ġ
    private bool GoSide;

    private Transform Drone;
    private GameObject Bullet;

    public GameObject Target;
    public float detectionRange = 20f;       // Ž�� �Ÿ�
    public Vector3 boxSize = new Vector3(1, 4, 1); // Y���� ���� Ž�� �ڽ��� ũ��
    private LayerMask targetLayer;            // Ž���� ���̾� (���� �ִ� ���̾�)


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

        // ���� ���� ��
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


    void Attack()
    {


        GameObject NewBullet = Instantiate(Bullet, Drone.transform.position, Bullet.transform.rotation);
        Bullet BulletScript = NewBullet.GetComponent<Bullet>();
        BulletScript.Target = Target;
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * detectionRange / 2, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxSize.x, boxSize.y, detectionRange));
    }
}
