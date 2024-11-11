using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target;
    private float Speed = 100f;
    private Transform Player;
    private float StartX;
    Rigidbody BulletRigid;
    void Start()
    {
        GameObject PlayerInstance = Initialize.Instance.GetPlayerInstance();
        //Player = PlayerInstance.transform;
        StartX = PlayerInstance.transform.position.x;




        BulletRigid = this.GetComponent<Rigidbody>();
        
        if(Target == null)
            BulletRigid.AddForce(Vector3.forward * 1000f, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            // 타겟 방향 계산
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            // 총알을 타겟 방향으로 이동
            transform.position += direction * Speed * Time.deltaTime;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
        {
            Debug.Log("타겟과 트리거 충돌 발생!");
            Destroy(gameObject);  // 예: 총알이 타겟과 트리거 충돌하면 총알을 파괴
            Destroy(Target);
        }
    }
}
