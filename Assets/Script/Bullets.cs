using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Move()
    {
        if (Target != null)
        {
            // Ÿ�� ���� ���
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            // �Ѿ��� Ÿ�� �������� �̵�
            BulletRigid.velocity = direction * Speed*10f * Time.deltaTime;
        }
    }

    public override void OnHit()
    {
        Destroy(gameObject);  // ��: �Ѿ��� Ÿ�ٰ� Ʈ���� �浹�ϸ� �Ѿ��� �ı�
        Destroy(Target); // Ÿ�ٵ� �ı�
    }
}

public class EnemyBullet : Bullet
{
    public override void Move()
    {
        if (Target != null)
        {
            // Ÿ�� ���� ���
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            // �Ѿ��� Ÿ�� �������� �̵�
            transform.position += direction * Speed * Time.deltaTime;
        }
    }

    public override void OnHit()
    {
        Destroy(gameObject);  // ��: �Ѿ��� Ÿ�ٰ� Ʈ���� �浹�ϸ� �Ѿ��� �ı�
    }
}
