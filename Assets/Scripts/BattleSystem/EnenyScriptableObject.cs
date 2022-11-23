using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Configuration",menuName ="ScriptableOnject/Enemy Configuration")]
public class EnenyScriptableObject : ScriptableObject
{
    // Start is called before the first frame update
    public string Enemy_Name ;
    public int Max_HP = 100;
    public int Current_HP = 100;
    public int Attack = 20;
    public int Defense = 20;
    public int Mobility = 20;
    public int Courage = 30;
    public int[] skills =new int[5];

}
