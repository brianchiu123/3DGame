using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Creature")]
public class CreatureSO : ScriptableObject
{
    public int Health;

    [HideInInspector] public int _Health;

    // A "Creature" is defined by its stats and abilities(actives & passives).
    [Serializable]
    public struct Stats
    {
        public int life, attack, defense, mobility, courage;

        [HideInInspector] public int _life, _attack, _defense, _mobility, _courage;

        public void Set(int life, int attack, int defense, int mobility, int courage)
        {
            this.life = life;
            this.attack = attack;
            this.defense = defense;
            this.mobility = mobility;
            this.courage = courage;
        }

        public void Modify(TraitSO.Modifier modifier, bool isMultiplier = false)
        {
            Debug.Log("Stats is modified");

            if (isMultiplier)
            {
                life *= modifier.life;
                attack *= modifier.attack;
                defense *= modifier.defense;
                mobility *= modifier.mobility;
                courage *= modifier.courage;
            }
            else
            {
                life += modifier.life;
                attack += modifier.attack;
                defense += modifier.defense;
                mobility += modifier.mobility;
                courage += modifier.courage;
            }
        }

        public void Initialize()
        {
            this.Set(_life, _attack, _defense, _mobility, _courage);
        }

        public void Backup()
        {
            _life = life;
            _attack = attack;
            _defense = defense;
            _mobility = mobility;
            _courage = courage;
        }
    }

    public Stats stats;

    public List<AbilitySO> actives, passives;

    void OnEnable()
    {
        stats.Backup();

        _Health = Health;
    }

    public void ClearAbilities()
    {
        actives.Clear();
        passives.Clear();
    }

    public void Initialize()
    {
        stats.Initialize();

        Health = _Health;
    }
}
