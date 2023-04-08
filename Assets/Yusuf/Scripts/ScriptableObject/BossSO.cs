using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Boss", fileName = "Boss")]
public class BossSO : ScriptableObject
{
    public string bossName;
    public GameObject skill;
}
