using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Move()
    {
        Vector3 direction = transform.forward;
        if (Target != null)
        {
            // 타겟 방향 계산
            direction = (Target.transform.position - transform.position).normalized;

            // 총알을 타겟 방향으로 이동
            BulletRigid.velocity = direction * Speed*10f * Time.deltaTime;
        }
        else BulletRigid.velocity = direction * Speed * 10f * Time.deltaTime;
    }

    public override void OnHit(Collider other)
    {
        Destroy(gameObject);  // 예: 총알이 타겟과 트리거 충돌하면 총알을 파괴
        Destroy(other.gameObject); // 타겟도 파괴
    }
}

public class EnemyBullet : Bullet
{
    public override void Move()
    {
        if (Target != null)
        {
            // 타겟 방향 계산
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            // 총알을 타겟 방향으로 이동
            transform.position += direction * Speed * Time.deltaTime;
        }
    }

    public override void OnHit(Collider other)
    {
        Destroy(gameObject);  // 예: 총알이 타겟과 트리거 충돌하면 총알을 파괴
    }
}
