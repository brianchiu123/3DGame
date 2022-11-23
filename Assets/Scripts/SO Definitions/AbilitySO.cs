using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    public string title;

    [TextArea] public string description;

    [Serializable]
    public struct Effect
    {
        public enum Type { damaging, healing, intimidating, statistic, controlling };
        public Type type;

        public enum Target { Opponent, self };
        public Target target;

        // For damaging, healing, intimidating
        [Serializable]
        public struct Coefficient  
        {
            public int baseAmount;
            public float coefLife, coefAttack, coefDefense, coefMobility, cofeCourage;
        }

        [Tooltip("For damaging, healing or intimidating effect")]
        public Coefficient coefficient;

        // For statistic
        [Serializable]
        public struct StatsChange
        {
            public enum Stats { Life, Attack, Defense, Mobility, Courage }
            public Stats targetStats;
            [Tooltip("Positive means Buff, negative means Nerf.")]
            public float ratio;    // Positive means Buff, negative means Nerf.
        }

        [Tooltip("For statistic effect")]
        public StatsChange statsChange;

        // For controlling
        public enum Control { None, ImmuneToDamage }

        public Control control;
    }

    public Effect effect;
}
