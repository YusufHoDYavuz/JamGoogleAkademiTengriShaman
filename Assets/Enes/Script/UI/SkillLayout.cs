using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLayout : MonoBehaviour
{
    [SerializeField] GameObject firstSkillImage, secondSkillImage, thirdSkillImage, fourthSkillImage;

    public bool fireBossDeath, waterBossDeath, airBossDeath, earthBossDeath;

    private void Update()
    {
        if (fireBossDeath) { firstSkillImage.SetActive(true); }
        if (waterBossDeath) {  secondSkillImage.SetActive(true); }
        if (airBossDeath) { thirdSkillImage.SetActive(true); }
        if (earthBossDeath) {  fourthSkillImage.SetActive(true); }
    }
}
