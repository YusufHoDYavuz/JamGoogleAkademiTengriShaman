using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCheck : MonoBehaviour
{
    public GameObject missionPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionPanel.SetActive(false);
        }
    }
}
