using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    public List<string> Tags = new List<string>();
    private void Start()
    {
        Tags.Add("Floor");
    }
    void OnTriggerEnter(Collider other)
    {
        print("Tag : " + other.gameObject.name);

        for (int i = 0; i < Tags.Count; i++)
        {
            if (other.CompareTag(Tags[i]))
            {
                if (Tags[i] == "Wall") Destroy(other.gameObject, 3f);
                else Destroy(other.gameObject);
            }
        }

    }
}
