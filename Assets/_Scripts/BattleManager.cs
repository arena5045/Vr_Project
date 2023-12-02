using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private  BattleManager instance;

    public static BattleManager Instance { get; private set; }

    public int maxCost = 10;
    public int turnCost = 0;
    [SerializeField]
    private int curCost = 0;

    public int CurCost 
    { get { return curCost; } 
        set
        { 
            curCost = value;
            UiManager.Instance.CostDotRefresh();
        } 
    }

    public int battleTurn =0;

    List<Tuple<Card, int>> cardlist;
    Tuple<Card, int> cardTuple;

    public Card SelectedCard;
    public int SelectedParts;

    private void Awake()
    {
        if(instance == null)
        {
            Instance = this;
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        maxCost = 10;
        turnCost = 5;
        curCost = 5;

        TurnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void TurnStart()
    {
        battleTurn++;
        turnCost++;
        curCost = turnCost;
    }
    public void AddCard(Card card)
    { 
        SelectedCard = card;
        CurCost -= card.cost; 
    }

    public void RemoveCard()
    {
        CurCost += SelectedCard.cost;
        SelectedCard = null;
    }
}
