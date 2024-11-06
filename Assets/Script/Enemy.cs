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

    private void Start()
    {
        GameObject PlayerInstance = Initialize.Instance.GetPlayerInstance();
        Target = PlayerInstance.transform;
        chasing = false;
        if (Target == null) print("test");
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
                Vector3 newPosition = transform.position;
                newPosition.z = Target.position.z + 10;
                transform.position = newPosition;
            }
            


        }


        


    }
}
