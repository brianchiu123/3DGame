using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using TMPro;

public class GameSceneUIManager : MonoBehaviour
{
    // references
    public static GameSceneUIManager SUIM_Instance; // singleton

    [SerializeField] PlayableDirector Director;

    /*[SerializeField] GameObject MainMenuSceneUI, BattleSceneUI;
    [SerializeField] GameObject TechTreeWindow, CharacterStatsWindow, OptionsWindow;*/

    [SerializeField] GameObject VictorySelectionUI, LoseSelectionUI;

    [Serializable] public struct UITimelineClips
    {
        public PlayableAsset EnterMainMenuFromStart, EnterMainMenuFromEndBattle, EnterMainMenuFromGameOver;
        public PlayableAsset EnterFirstEvoFromMainMenu;
        public PlayableAsset EnterPrepFromMainMenu, EnterPrepFromFirstEvo, EnterPrepFromEndBattle;
        public PlayableAsset EnterBattleFromPrep;
        public PlayableAsset EnterEndBattleSelecFromBattle;
        public PlayableAsset EnterGameOverFromEndBattle; // game over mean the game end ( victory ).

        public PlayableAsset EnterSkillTreeWindow, ExitSkillTreeWindow;
    }
    public UITimelineClips UItimelineClips;

    [Serializable] public struct ConstantPlayerStatsUI
    {
        public TMP_Text text_HP, text_ATK, text_DF, text_SPD, text_Courage;
    }
    [SerializeField] ConstantPlayerStatsUI PlayerStatsUI;

    // values
    



    void Awake()
    {
        if (SUIM_Instance == null)
            SUIM_Instance = this;
        else
            Destroy(gameObject);
        // singleton
    }

    bool TryPlayTimeline()
    {
        if ( Director.state == PlayState.Playing )
        {
            Debug.Log("The timelineclip is still playing.");
            return false;
        }

        return true;
    }

    public bool PlayTimeline(PlayableAsset clip)
    {
        if ( !TryPlayTimeline() )
        {
            return false;
        }

        if ( clip == null )
        {
            Debug.LogWarning("The timeline clip doesn't exist!");
            return false;
        }

        Director.Play(clip);
        return true;
    }

    public void Button_EnterSkillTreeWindow()
    {
        PlayTimeline(UItimelineClips.EnterSkillTreeWindow);
    }

    public void Button_ExitSkillTreeWindow()
    {
        PlayTimeline(UItimelineClips.ExitSkillTreeWindow);
    }

    public void SetUpConstantPlayerStatsUI()
    {
        PlayerStatsUI.text_HP.text = PlayerStatsManager.PSM_Instance.playerStats.stats._life.ToString();
        PlayerStatsUI.text_ATK.text = PlayerStatsManager.PSM_Instance.playerStats.stats._attack.ToString();
        PlayerStatsUI.text_DF.text = PlayerStatsManager.PSM_Instance.playerStats.stats._defense.ToString();
        PlayerStatsUI.text_SPD.text = PlayerStatsManager.PSM_Instance.playerStats.stats._mobility.ToString();
        PlayerStatsUI.text_Courage.text = PlayerStatsManager.PSM_Instance.playerStats.stats._courage.ToString();

    }

    public void SetUpPrepUI()
    {
        // set up preparation info.
    }

    public void SetUpBattleSceneUI()
    {
        // set up battle scene info.
    }

    public void SetupEndBattleResultUI(bool WinorLose)
    {
        VictorySelectionUI.SetActive(false);
        LoseSelectionUI.SetActive(false);
        if (WinorLose)
        {
            VictorySelectionUI.SetActive(true);
        }
        else
        {
            LoseSelectionUI.SetActive(true);
        }
    }

}
