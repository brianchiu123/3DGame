using TMPro;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Node> ApproachingEvent;
    public event Action ArrivalEvent;

    public enum State
    {
        Unknown,    // Not approached yet
        Approached, // All condition nodes Arrived or excelled
        Discovered, // Player can evolve into this trait from now on
        Arrived,    // Player obtain this trait and skill
    }

    public State state = State.Unknown;

    [SerializeField] TraitSO trait;

    [SerializeField] Node[] conditions;

    TextMeshProUGUI nameField;

    Image iconField;

    static CreatureSO player;

    void Awake()
    {
        nameField = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        iconField = transform.Find("IconMask").Find("Icon").GetComponent<Image>();

        if (trait.icon) iconField.sprite = trait.icon;
        else iconField.gameObject.SetActive(false);

        player = Resources.Load<CreatureSO>("Scriptable Objects/Creatures/Player/PlayerStats");
    }

    void Start()
    {
        if (conditions.Length == 0)
        {
            Discover();
            Arrive();
        }
        else
        {
            foreach (Node condition in conditions)
            {
                condition.ArrivalEvent += OnConditionArrived;
            }
        }

        // Draw lines
        foreach (Node condition in conditions)
        {
            PathDrawer.DrawPath(gameObject, transform.localPosition - condition.transform.localPosition);
        }
    }

    public void OnChosen()  // Called when player click on it
    {
        if (state == State.Discovered && TheTree.tree.playerEvoPoint >= trait.evoCost)
        {
            TheTree.tree.playerEvoPoint -= trait.evoCost;
            TheTree.UpdatePlayerEvoPoints();

            Arrive();
        }
    }

    void Approach()
    {
        state = State.Approached;

        ApproachingEvent?.Invoke(this);
    }

    public void Discover()
    {
        state = State.Discovered;

        nameField.text = trait.name;
    }

    void Arrive()
    {
        state = State.Arrived;
        ArrivalEvent?.Invoke();  // If the Arrival event has any subscriber, invoke it.

        GetComponent<Image>().color = new Color(0.6496f, 0.7f, 0.448f, 1f);

        player.stats.Modify(trait.statsModifier, trait.statsModifier.isMultiplier);
        
        if (trait.actives.Length != 0)
        {
            player.actives.AddRange(trait.actives);
        }
        if (trait.passives.Length != 0)
        {
            player.passives.AddRange(trait.passives);
        }
    }


    void OnConditionArrived()
    {
        if (state == State.Unknown)
        {
            foreach (Node condition in conditions) if ((int)condition.state < 3) return;

            Approach();   // All condition nodes Arrived or excelled
        }
    }


    #region Event Triggers
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (state)
        {
            case State.Unknown: 
                TooltipSystem.Show("-未知-", "");
                break;

            case State.Approached:
                TooltipSystem.Show("-即將發現-", "");
                break;

            default:
                TooltipSystem.Show("演化成本：" + trait.evoCost, trait.description);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
    #endregion
}
