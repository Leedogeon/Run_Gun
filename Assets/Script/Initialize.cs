using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public static Initialize Instance { get; private set; }
    public GameObject PlayerInstance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetPlayerInstance(GameObject player)
    {
        PlayerInstance = player; // 플레이어 인스턴스 설정
    }

    public GameObject GetPlayerInstance()
    {
        return PlayerInstance; // 플레이어 인스턴스 반환
    }
    void Start()
    {
        GameObject PlayerPrefab = Resources.Load<GameObject>("Prefabs/unitychan");
        if (PlayerPrefab != null)
        {
            PlayerInstance = Instantiate(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity);

        }
    }

}
