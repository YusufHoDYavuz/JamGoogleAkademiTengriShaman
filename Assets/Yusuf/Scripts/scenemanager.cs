using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(4);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Singleton.Instance.isDeadWater && Singleton.Instance.isDeadEarth && Singleton.Instance.isDeadFire && Singleton.Instance.isDeadWater)
            {
                SceneManager.LoadScene(5);
            }
        }
    }
}
