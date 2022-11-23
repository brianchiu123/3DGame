using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class TheTree : MonoBehaviour
{
    public static TheTree tree;

    [SerializeField] CreatureSO player;

    [SerializeField] TextMeshProUGUI evoPointsFeild;

    public int playerEvoPoint;

    Node[] allNodes;
    readonly List<Node> nodesApproached = new List<Node>();

    void Awake()
    {
        if (tree == null) tree = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        allNodes = transform.GetComponentsInChildren<Node>();

        foreach (Node node in allNodes)
        {
            if (node.state == Node.State.Approached)
            {
                nodesApproached.Add(node);
            }
            else
            {
            node.ApproachingEvent += OnNodeApproached;
            }
        }
    }

    void OnNodeApproached(Node node)
    {
        nodesApproached.Add(node);
    }

    public static void GainEvoPoint(int points)
    {
        tree.playerEvoPoint += points;

        UpdatePlayerEvoPoints();
    }

    public static void UpdatePlayerEvoPoints()
    {
        tree.evoPointsFeild.text = "Evolution Points: " + tree.playerEvoPoint;
    }

    public static void RandomDiscover(int number)   // Give player some random evolve options to chose from
    {
        if (tree.nodesApproached.Count < number) number = tree.nodesApproached.Count;

        for (int i = 0; i < number; i++)
        {
            Node randomNode = tree.nodesApproached[Random.Range(0, tree.nodesApproached.Count)];

            randomNode.Discover();

            tree.nodesApproached.Remove(randomNode);
        }
    }

    void OnDestroy()
    {
        player.Initialize();
        player.ClearAbilities();
    }
}
