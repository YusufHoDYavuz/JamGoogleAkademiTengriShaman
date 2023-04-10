using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCheck : MonoBehaviour
{
    public GameObject missionPanel;
    public GameObject missionCompletedPanel;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (Singleton.Instance.isDeadWater && Singleton.Instance.isDeadEarth && Singleton.Instance.isDeadFire && Singleton.Instance.isDeadWater)
            {
                missionCompletedPanel.SetActive(true);
            }
            missionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Singleton.Instance.isDeadWater && Singleton.Instance.isDeadEarth && Singleton.Instance.isDeadFire && Singleton.Instance.isDeadWater)
            {
                missionCompletedPanel.SetActive(false);
            }
            missionPanel.SetActive(false);
        }
    }
}
