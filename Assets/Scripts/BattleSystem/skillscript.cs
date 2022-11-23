using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillscript : MonoBehaviour
{
    public void skillaction(int skill_id,EnemyController target_unit,EnemyController my_unit,BattleHUD HUD)
    {
        List<int> arr = new List<int>(3);
        if (skill_id == 1){
            arr[0]=-20;
            arr[1]=0;
            arr[2]=0;
        }
        else if (skill_id == 2){
            arr[0]=0;
            arr[1]=20;
            arr[2]=0;
        }
        else if (skill_id == 3){
            arr[0]=0;
            arr[1]=-20;
            arr[2]=0;
        }
        else if (skill_id == 4){
            arr[0]=0;
            arr[1]=0;
            arr[2]=20;
        }
        target_unit.enenyScriptableObject.Current_HP += arr[0];
        my_unit.enenyScriptableObject.Current_HP += arr[2];
        HUD.Rslider.value += arr[1];
    }


}
