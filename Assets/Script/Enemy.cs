using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Transform Target;
    public float approachSpeed = 5f;
    private bool chasing;
    private float distanceZ;

    private Renderer Color;

    private bool CanAttack = false;
    private float AttackCoolDown = 2f;
    private float AttackReady = 0f;

    private GameObject Bullet;
    private Transform Gun;
    private void Start()
    {
        GameObject PlayerInstance = Initialize.Instance.GetPlayerInstance();
        Target = PlayerInstance.transform;
        chasing = false;
        Color = GetComponent<Renderer>();
        Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color.material.color = randomColor;
        Gun = transform.Find("Gun");
        Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }
    private void Update()
    {
        if(Target != null)
        {
            if(!chasing)
            {

                distanceZ = transform.position.z - Target.position.z;
                if (distanceZ > 10)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.z = Mathf.Lerp(transform.position.z, Target.position.z, approachSpeed * Time.deltaTime);
                    transform.position = newPosition;
                }

                if (distanceZ <= 10) chasing = true;
            }

            if(chasing)
            {
                CanAttack = true;
                Vector3 newPosition = transform.position;
                newPosition.z = Target.position.z + 10;
                transform.position = newPosition;
            }

            if(CanAttack)
            {
                AttackReady += Time.deltaTime;

                if(AttackReady >= AttackCoolDown)
                {
                    AttackReady = 0f;
                    GameObject NewBullet = Instantiate(Bullet, Gun.transform.position, Bullet.transform.rotation);
                    NewBullet.AddComponent<EnemyBullet>();
                    EnemyBullet BulletScript = NewBullet.GetComponent<EnemyBullet>();
                    BulletScript.Target = Target.gameObject;
                    BulletScript.HitPoint = Target.gameObject.transform.Find("HitPoint").transform.position;
                }
            }

        }
    }
}
