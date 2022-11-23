using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpslider;
    public Slider Rslider;




    public void SetHUD(EnemyController unit)
    {
        nameText.text = unit.enenyScriptableObject.Enemy_Name;
        hpslider.maxValue = unit.enenyScriptableObject.Max_HP;
        hpslider.value = unit.enenyScriptableObject.Current_HP;
        Rslider.maxValue = 100;
        Rslider.value = 100-unit.enenyScriptableObject.Courage;
    }
    public void upEnemyHP(EnemyController unit)
    {
        hpslider.value = unit.enenyScriptableObject.Current_HP;
    }
    public void upPlayerHP(EnemyController unit)
    {
        hpslider.value = unit.enenyScriptableObject.Current_HP;
    }



}
