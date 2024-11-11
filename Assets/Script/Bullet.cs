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
            // Ÿ�� ���� ���
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            // �Ѿ��� Ÿ�� �������� �̵�
            transform.position += direction * Speed * Time.deltaTime;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
        {
            Debug.Log("Ÿ�ٰ� Ʈ���� �浹 �߻�!");
            Destroy(gameObject);  // ��: �Ѿ��� Ÿ�ٰ� Ʈ���� �浹�ϸ� �Ѿ��� �ı�
            Destroy(Target);
        }
    }
}
