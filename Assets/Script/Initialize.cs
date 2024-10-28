using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerInstance;
    // Start is called before the first frame update
    void Start()
    {
        Player = Resources.Load<GameObject>("Prefabs/unitychan");
        if (Player != null)
        {
            PlayerInstance = Instantiate(Player, new Vector3(0, 1, 0), Quaternion.identity);
        
        }
    }

}
