using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public GameObject Target;
    protected float Speed = 1000f;
    protected Transform Player;
    protected float StartX;
    protected Rigidbody BulletRigid;
    protected bool HasHit = false;
    public abstract void Move();
    public abstract void OnHit(Collider other);

    void Start()
    {
        GameObject PlayerInstance = Initialize.Instance.GetPlayerInstance();
        StartX = PlayerInstance.transform.position.x;

        BulletRigid = this.GetComponent<Rigidbody>();

        if (Target == null)
            BulletRigid.AddForce(Vector3.forward * 1000f, ForceMode.Acceleration);
    }

    void Update()
    {
        if (Target != null)
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!HasHit&& other.tag == "Enemy")
        {
            HasHit = true;
            Debug.Log("타겟과 트리거 충돌 발생!");
            OnHit(other);
        }
    }
}

