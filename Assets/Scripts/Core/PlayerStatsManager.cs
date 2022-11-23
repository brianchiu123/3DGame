using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerStatsManager : MonoBehaviour
{
    //Reference
    public static PlayerStatsManager PSM_Instance; // singleton

    //Values
    [SerializeField] CreatureSO NewPlayerStatsTemplate;
    public CreatureSO playerStats;

    void Awake()
    {
        if (PSM_Instance == null)
            PSM_Instance = this;
        else
            Destroy(gameObject);
        // singleton
    }

    public void CreateNewPlayerStats()
    {
        playerStats = Instantiate(NewPlayerStatsTemplate);
        Debug.Log("Create new player stats.");
    }

    public void LoadPlayerStats()
    {
        if (playerStats == null)
        {
            playerStats = Resources.Load<CreatureSO>("Scriptable Objects/Creatures/Player/PlayerStats");
        }
        Debug.Log("Load player stats.");
    }

}
