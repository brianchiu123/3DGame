using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Trait")]
public class TraitSO : ScriptableObject
{
    public int evoCost;
    public Sprite icon;
    public new string name;
    [TextArea] public string description;

    [Serializable]
    public struct Modifier
    {
        public bool isMultiplier;
        public int life, attack, defense, mobility, courage;    
    }

    public Modifier statsModifier;

    public AbilitySO[] actives, passives;
}
