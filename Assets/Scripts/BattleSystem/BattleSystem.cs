using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, ACTION ,PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{
    //skillscript skillscript;

    public GameObject WinUI;
    public GameObject LoseUI;

    public Sprite[] MonsterImages; 
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform enemyBattleStation;

    public BattleState state;


    public Text dialogueText;
    public Image  Monster;
    public BattleHUD enemyHUD;


    public int playerMove = 0;
    public int enemyMove = 0;
    public int playeraction = 0;

    public int gamelevel = 0;

    public Text Enemy_HPText;
    public Text Enemy_COText;
    public Slider Enemy_hpslider;
    public Slider Enemy_Rslider;

    [SerializeField] public TextMeshProUGUI Player_HPText;
    [SerializeField] public TextMeshProUGUI Player_COText;
    public Slider Player_hpslider;
    public Slider Player_Rslider;

    public int truedamage;
    public int skilldamage;
    public float ATratio ;
    public float decreaseDamage ;
    public float ratiodecreaseDamage; 

    public float Coconstant;
    public float takeCo;
    public float reverseCo;
    public float trueCo;
    public float Coratio;

    public float static_ratio;



    [SerializeField] AbilitySO now_ability;

    [SerializeField] CreatureSO testplayer,testenemy;
    [SerializeField] CreatureSO[] enemylist;
    public struct BattleStats
    {
        public int HP;
        public int ATK;
        public int DEF;
        public int MO;
        public int CO;
        public int Now_HP;
        public int Now_CO;
    }

    public  BattleStats playerBattleStats;
    public  BattleStats enemyBattleStats;


    public void StartBattle()
    {

        WinUI.SetActive(false);
        LoseUI.SetActive(false);
        int rand_enemy =  Random.Range(0,5);
        playerMove = 0;
        enemyMove = 0;
        Monster.sprite = MonsterImages[gamelevel];
        testenemy = enemylist[gamelevel];
        // set player battle stats
        playerBattleStats.HP  = testplayer.stats._life;
        playerBattleStats.ATK = testplayer.stats._attack;
        playerBattleStats.DEF = testplayer.stats._defense;
        playerBattleStats.MO  = testplayer.stats._mobility;
        playerBattleStats.CO  = testplayer.stats._courage;
        playerBattleStats.Now_HP  = testplayer.Health;
        playerBattleStats.Now_CO  = 100 - playerBattleStats.CO;

        // set enemy battle stats
        enemyBattleStats.HP  = testenemy.stats._life;
        enemyBattleStats.ATK = testenemy.stats._attack;
        enemyBattleStats.DEF = testenemy.stats._defense;
        enemyBattleStats.MO  = testenemy.stats._mobility;
        enemyBattleStats.CO  = testenemy.stats._courage;
        enemyBattleStats.Now_HP  = testenemy.Health;
        enemyBattleStats.Now_CO  = 100 - enemyBattleStats.CO;



        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {   
        SetEnemyStats(enemyBattleStats);
        SetPlayerStats(playerBattleStats);

        dialogueText.text = "A wild Monster approaches...";

        //enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
    }

    IEnumerator Actointurn()
    {

            if (playerMove == 1 && enemyMove == 1){
                playerMove = 0;
                enemyMove = 0;
            }
            if (enemyBattleStats.Now_HP <= 0 || enemyBattleStats.Now_CO  >= 100 ){
                won();
            }
            else if (playerBattleStats.Now_HP <= 0 || playerBattleStats.Now_CO  >= 100){
                lose();
            }


            else if (playerBattleStats.MO >= enemyBattleStats.MO && playerMove == 0|| enemyMove == 1){

                dialogueText.text = "Player Turn...";
                yield return new WaitForSeconds(2f);
                state = BattleState.PLAYERTURN;
                playerMove = 1;
                Playerturn();
            }
            else if(enemyMove == 0){
                dialogueText.text = "Enemy Turn...";
                yield return new WaitForSeconds(2f);
                enemyMove = 1;
                state = BattleState.ENEMYTURN;
                StartCoroutine(enemyturn());
            }

        
    }

    void won(){
        gamelevel +=1;
        if(gamelevel >=4){
            gamelevel = 4;
        }
        testplayer.Health = playerBattleStats.Now_HP;
        WinUI.SetActive(true);
        //GameStateManager.TestButton_EndBattle(true);
    }

    void lose(){
        LoseUI.SetActive(true);
        //GameStateManager.TestButton_EndBattle(false);
    }

    void Playerturn()
    {   

        dialogueText.text = "Choose an action...";

        
    }
    IEnumerator enemyturn()
    {
        now_ability = testenemy.actives[0];

        enemy_ability(now_ability);
        yield return new WaitForSeconds(1f);

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
    }

    public void skill_1()
    {   


        if(state == BattleState.PLAYERTURN){
            if (testplayer.actives.Count > 0){
                StartCoroutine(using_active_1());
            }
            else{
                dialogueText.text = "No Skill";
            }
        }


    }
    public void skill_2()
    {   
        if(state == BattleState.PLAYERTURN){
            if (testplayer.actives.Count > 1){
            StartCoroutine(using_active_2());
            }
            else{
                dialogueText.text = "No Skill";
            }
        }
    }
    public void skill_3()
    {   
        if(state == BattleState.PLAYERTURN){
            if (testplayer.actives.Count > 2){
            StartCoroutine(using_active_3());
            }
            else{
                dialogueText.text = "No Skill";
            }
        }
    }
    public void skill_4()
    {   
        if(state == BattleState.PLAYERTURN){
            if (testplayer.actives.Count > 3){
            StartCoroutine(using_active_4());
            }
            else{
                dialogueText.text = "No Skill";
            }
        }
    }
    public void skill_5()
    {   
        if(state == BattleState.PLAYERTURN){
            if (testplayer.actives.Count > 4){
            StartCoroutine(using_active_5());
            }
            else{
                dialogueText.text = "No Skill";
            }
        }
    }

    IEnumerator using_skill_1()
    {
        dialogueText.text = "Use Skill1............";
        dialogueText.text = testplayer.actives[0].description;
        yield return new WaitForSeconds(2f);       
  
    }

    IEnumerator using_active_1()
    {

        now_ability = testplayer.actives[0];

        use_ability(now_ability);
        yield return new WaitForSeconds(2f);       

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
        
    }

    IEnumerator using_active_2()
    {

        now_ability = testplayer.actives[1];

        use_ability(now_ability);
        yield return new WaitForSeconds(2f);       

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
        
    }

    IEnumerator using_active_3()
    {

        now_ability = testplayer.actives[2];

        use_ability(now_ability);
        yield return new WaitForSeconds(2f);       

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
        
    }

    IEnumerator using_active_4()
    {

        now_ability = testplayer.actives[3];

        use_ability(now_ability);
        yield return new WaitForSeconds(2f);       

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
        
    }

    IEnumerator using_active_5()
    {

        now_ability = testplayer.actives[4];

        use_ability(now_ability);
        yield return new WaitForSeconds(2f);       

        state = BattleState.ACTION;
        StartCoroutine(Actointurn());
        
    }

    IEnumerator updatestate()
    {   
        yield return new WaitForSeconds(1f);

    }

    public void SetEnemyStats(BattleStats Unit){

        Enemy_HPText.text = Unit.Now_HP.ToString();
        Enemy_COText.text = Unit.Now_CO.ToString();
        Enemy_hpslider.maxValue = Unit.HP;
        Enemy_hpslider.value = Unit.Now_HP;
        Enemy_Rslider.maxValue = 100;
        Enemy_Rslider.value = Unit.Now_CO;
    }
    public void SetPlayerStats(BattleStats Unit){

        Player_HPText.text = Unit.Now_HP.ToString() + "/" + Unit.HP.ToString();
        Player_COText.text = Unit.Now_CO.ToString() + "/" + "100";
        Player_hpslider.maxValue = Unit.HP;
        Player_hpslider.value = Unit.Now_HP;
        Player_Rslider.maxValue = 100;
        Player_Rslider.value = Unit.Now_CO;
    }

    public void use_ability(AbilitySO now_ab){

        dialogueText.text = now_ab.description;
        if (now_ab.effect.type == AbilitySO.Effect.Type.damaging){

            skilldamage = now_ab.effect.coefficient.baseAmount + (int)(now_ab.effect.coefficient.baseAmount * (now_ab.effect.coefficient.coefLife + now_ab.effect.coefficient.coefAttack + now_ab.effect.coefficient.coefDefense + now_ab.effect.coefficient.coefMobility + now_ab.effect.coefficient.cofeCourage));
            ATratio = (float)playerBattleStats.ATK/(float)enemyBattleStats.DEF;
            decreaseDamage =0.1f * (float)enemyBattleStats.DEF;
            ratiodecreaseDamage = 100.0f/(100.0f+(float)enemyBattleStats.DEF);

            truedamage = (int)((skilldamage *ATratio-decreaseDamage )*ratiodecreaseDamage);

            truedamage = (now_ab.effect.coefficient.baseAmount);
            Debug.Log(truedamage);

            enemyBattleStats.Now_HP -= truedamage;
            Debug.Log(enemyBattleStats.Now_HP);
            SetEnemyStats(enemyBattleStats);
        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.intimidating){

  
            Coratio = (float)playerBattleStats.Now_CO/(float)enemyBattleStats.Now_CO;
            trueCo = (int)((float)now_ab.effect.coefficient.baseAmount*(float)Coratio);

            Debug.Log(trueCo);

            enemyBattleStats.Now_CO += (int)trueCo;
            Debug.Log(enemyBattleStats.Now_CO);
            SetEnemyStats(enemyBattleStats);

        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.healing){
            Debug.Log("Heal");
            playerBattleStats.Now_HP += now_ab.effect.coefficient.baseAmount;
            if(playerBattleStats.Now_HP > playerBattleStats.HP){
                playerBattleStats.Now_HP = playerBattleStats.HP;
            }
        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.statistic){
            Debug.Log("statistic");

            static_ratio = 1.0f + now_ab.effect.statsChange.ratio;


            if(now_ab.effect.target ==  AbilitySO.Effect.Target.self){
                if(now_ab.effect.statsChange.targetStats == AbilitySO.Effect.StatsChange.Stats.Attack){
                    playerBattleStats.ATK = (int)(playerBattleStats.ATK * static_ratio);
                }
            }
        }

    }

    public void enemy_ability(AbilitySO now_ab){

        dialogueText.text = now_ab.description;
        if (now_ab.effect.type == AbilitySO.Effect.Type.damaging){

            skilldamage = now_ab.effect.coefficient.baseAmount + (int)(now_ab.effect.coefficient.baseAmount * (now_ab.effect.coefficient.coefLife + now_ab.effect.coefficient.coefAttack + now_ab.effect.coefficient.coefDefense + now_ab.effect.coefficient.coefMobility + now_ab.effect.coefficient.cofeCourage));
            ATratio = (float)enemyBattleStats.ATK/(float)playerBattleStats.DEF;
            decreaseDamage =0.1f * (float)playerBattleStats.DEF;
            ratiodecreaseDamage = 100.0f/(100.0f+(float)playerBattleStats.DEF);

            truedamage = (int)((skilldamage *ATratio-decreaseDamage )*ratiodecreaseDamage);
            Debug.Log(skilldamage);
            Debug.Log(ATratio);
            Debug.Log(decreaseDamage);
            Debug.Log(ratiodecreaseDamage);

            playerBattleStats.Now_HP -= truedamage;
            Debug.Log(playerBattleStats.Now_HP);
            SetPlayerStats(playerBattleStats);
        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.intimidating){
            Coratio = (float)enemyBattleStats.Now_CO/(float)playerBattleStats.Now_CO;
            trueCo = (int)((float)now_ab.effect.coefficient.baseAmount*(float)Coratio);

            Debug.Log(trueCo);

            playerBattleStats.Now_CO += (int)trueCo;

            Debug.Log(playerBattleStats.Now_CO);
            SetPlayerStats(playerBattleStats);

        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.healing){
            Debug.Log("Heal");
            enemyBattleStats.Now_HP += now_ab.effect.coefficient.baseAmount;
            if(enemyBattleStats.Now_HP > enemyBattleStats.HP){
                enemyBattleStats.Now_HP = enemyBattleStats.HP;
            }
        }
        else if(now_ab.effect.type == AbilitySO.Effect.Type.statistic){
            Debug.Log("statistic");

            static_ratio = 1.0f + now_ab.effect.statsChange.ratio;


            if(now_ab.effect.target ==  AbilitySO.Effect.Target.self){
                if(now_ab.effect.statsChange.targetStats == AbilitySO.Effect.StatsChange.Stats.Attack){
                    enemyBattleStats.ATK = (int)(enemyBattleStats.ATK * static_ratio);
                }
            }
        }

    }


}
