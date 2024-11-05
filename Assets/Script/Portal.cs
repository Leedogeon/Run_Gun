using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag ("Player"))
        {
            LoadScene();    
        }
    }
    void LoadScene()
    {
        string curScene = SceneManager.GetActiveScene().name;
        if(curScene == "3DScene")
            SceneManager.LoadScene("2DScene");
        else SceneManager.LoadScene("3DScene");
               
    }
}
