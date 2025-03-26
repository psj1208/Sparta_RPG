using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] Image hp;
    [SerializeField] Image mp;
    [SerializeField] Image exp;

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] TextMeshProUGUI goldAmount;

    public void ChangeHp(float curhp,float maxhp)
    {
        hp.fillAmount = curhp / maxhp;
    }

    public void ChangeMp(float curmp,float maxmp)
    {
        mp.fillAmount = curmp / maxmp;
    }

    public void ChangeExp(float curExp,float maxExp)
    {
        exp.fillAmount = curExp / maxExp;
    }

    public void ChangeStageTxt(int num)
    {
        stageText.text = num.ToString();
    }

    public void ChangeGoldTxt(int num)
    {
        goldAmount.text = num.ToString();
    }
}
