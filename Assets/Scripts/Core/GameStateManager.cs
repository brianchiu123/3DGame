using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // reference
    public static GameStateManager GSM_Instance; // singleton
    public BattleSystem BaSystem;
    public GameObject WinUI;
    public GameObject LoseUI;
    // values
    enum GameState { WaitForStart, MainMenu, FirstEvolution, Preparation, Battle, EndBattleSelection, GameOver }
    [SerializeField] GameState currentState = GameState.WaitForStart;

    void Awake()
    {
        if (GSM_Instance == null)
            GSM_Instance = this;
        else
            Destroy(gameObject);
        // singleton

        //_SceneUIManager = GameSceneUIManager.SUIM_Instance; // although it is singletoned, still using it as this.
    }

    void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    void ChangeGameState(GameState newState)
    {
        if (currentState == newState)
        {
            Debug.LogWarning("Shouldn't change to same state! Something is wrong!");
            return;
        }

        // This define all the things that should do in every state changing flow.
        switch ( currentState ) 
        {
            case GameState.WaitForStart: // exit from game start.

                switch ( newState )
                {
                    case GameState.MainMenu: // enter main menu.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterMainMenuFromStart))
                            return;

                        // do sth.

                        currentState = GameState.MainMenu;
                        break;
                }

                break;

            case GameState.MainMenu: // exit from main menu.

                switch ( newState )
                {
                    case GameState.FirstEvolution: // enter first evolution.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterFirstEvoFromMainMenu))
                            return;

                        // do sth.

                        currentState = GameState.FirstEvolution;
                        break;
                    case GameState.Preparation: // enter preparation.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterPrepFromMainMenu))
                            return;

                        // do sth.
                        GameSceneUIManager.SUIM_Instance.SetUpConstantPlayerStatsUI();

                        currentState = GameState.Preparation;
                        break;
                }

                break;

            case GameState.FirstEvolution: // exit from first evolution.

                switch ( newState )
                {
                    case GameState.Preparation: // enter preparation.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterPrepFromFirstEvo))
                            return;

                        // do sth.

                        GameSceneUIManager.SUIM_Instance.SetUpConstantPlayerStatsUI();

                        currentState = GameState.Preparation;
                        break;
                }

                break;

            case GameState.Preparation: // exit from preparation.

                switch (newState)
                {
                    case GameState.Battle: // enter battle.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterBattleFromPrep))
                            return;

                        // do sth.
                        BaSystem.StartBattle();

                        currentState = GameState.Battle;
                        break;
                }

                break;

            case GameState.Battle: // exit from battle.

                switch (newState)
                {
                    case GameState.EndBattleSelection: // enter end battle selection.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterEndBattleSelecFromBattle))
                            return;

                        // do sth.
                        // Here should load battle ending information! To decide to show which result UI.

                        currentState = GameState.EndBattleSelection;
                        break;
                }

                break;

            case GameState.EndBattleSelection: // exit from end battle selection.

                switch (newState)
                {
                    case GameState.Preparation: // enter preparation.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterPrepFromEndBattle))
                            return;

                        // do sth.
                        GameSceneUIManager.SUIM_Instance.SetUpConstantPlayerStatsUI();

                        currentState = GameState.Preparation;
                        break;
                    case GameState.MainMenu: // enter main menu.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterMainMenuFromEndBattle))
                            return;

                        // do sth.

                        currentState = GameState.MainMenu;
                        break;
                    case GameState.GameOver: // enter game over.
                        if (!GameSceneUIManager.SUIM_Instance.PlayTimeline(GameSceneUIManager.SUIM_Instance.UItimelineClips.EnterGameOverFromEndBattle))
                            return;

                        // do sth.

                        currentState = GameState.GameOver;
                        break;
                }

                break;

            case GameState.GameOver: // exit from gameover.

                switch (newState)
                {
                    case GameState.MainMenu: // enter main menu.



                        // do sth.

                        //currentState = GameState.MainMenu;
                        break;
                }

                break;
        }
    }

    

    public void Button_NewGame()
    {
        PlayerStatsManager.PSM_Instance.CreateNewPlayerStats();

        ChangeGameState(GameState.FirstEvolution);
    }

    public void Button_Continue()
    {
        PlayerStatsManager.PSM_Instance.LoadPlayerStats();

        GameSceneUIManager.SUIM_Instance.SetUpPrepUI();

        ChangeGameState(GameState.Preparation);
    }

    public void Button_QuitGame()
    {
        // save ?

        Debug.Log("Quit the game.");

        Application.Quit();
    }

    public void Button_EnterBattle()
    {
        GameSceneUIManager.SUIM_Instance.SetUpBattleSceneUI();

        ChangeGameState(GameState.Battle);
    }

    public void TestButton_EndBattle(bool WinorLose)
    {   
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        GameSceneUIManager.SUIM_Instance.SetupEndBattleResultUI(WinorLose);

        ChangeGameState(GameState.EndBattleSelection);
    }

    public void Button_BackToPrep_EatMeat()
    {
        GameSceneUIManager.SUIM_Instance.SetUpPrepUI();

        // update tech tree info!
        TheTree.GainEvoPoint(2);
        TheTree.RandomDiscover(3);

        ChangeGameState(GameState.Preparation);
    }

    public void Button_BackToPrep_EatVeg()
    {
        GameSceneUIManager.SUIM_Instance.SetUpPrepUI();

        // update tech tree info!
        TheTree.GainEvoPoint(3);
        TheTree.RandomDiscover(3);

        ChangeGameState(GameState.Preparation);
    }

    public void Button_BackToMenu()
    {
        ChangeGameState(GameState.MainMenu);
    }

    public void TestButton_RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
