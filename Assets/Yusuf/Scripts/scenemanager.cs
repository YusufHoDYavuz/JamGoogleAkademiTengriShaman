using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private bool isInfoPanel;

    [SerializeField] private GameObject forController;

    void Start()
    {
        Destroy(forController, 10);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Singleton.Instance.isDeadWater && Singleton.Instance.isDeadEarth && Singleton.Instance.isDeadFire && Singleton.Instance.isDeadWater)
            {
                SceneManager.LoadScene(5);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isInfoPanel = !isInfoPanel;
            infoPanel.SetActive(isInfoPanel);
        }
    }
}
