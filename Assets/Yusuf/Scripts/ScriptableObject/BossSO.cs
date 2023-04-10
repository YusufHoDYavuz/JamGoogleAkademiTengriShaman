using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boss", fileName = "Boss")]
public class BossSO : ScriptableObject
{
    public string bossName;
    public GameObject skill;
    public int bossHealth;
    public float damageAmount;
    public float skillCounterAmount;
    public float skillRange;
    public float skillDistance;
    public float hitDistance;
    public float stoppingDistance;

}
