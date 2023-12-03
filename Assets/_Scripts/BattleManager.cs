using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BattleManager;

public class BattleManager : MonoBehaviour
{

    private  BattleManager instance;

    public static BattleManager Instance { get; private set; }

    public int maxCost = 10;
    public int turnCost = 0;
    [SerializeField]
    private int curCost = 0;
    public bool isZoom = false;

    public int CurCost 
    { get { return curCost; } 
        set
        { 
            curCost = value;
            UiManager.Instance.CostDotRefresh();
        } 
    }

    public int battleTurn =0;

   
    public List<CardQue> Actionlist = new List<CardQue>();

    [Serializable]
    public struct CardQue
    {
        public Card card;
        public int targetnum;

        public CardQue(Card card, int targetNum) => (this.card, this.targetnum) = (card, targetNum);
    }

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

    public void AddTarget(int targetnum)
    {
        CardQue cardque = new CardQue(SelectedCard, targetnum);
        Actionlist.Add(cardque);
        SelectedCard.GetComponent<CardBtn>().Selected();
        SelectedCard = null;
    }

    public void RemoveTarget(Card card)
    {
        foreach(CardQue cardque in Actionlist )
        {
            if(cardque.card == card)
            {
                CurCost += cardque.card.cost;
                Actionlist.Remove(cardque);
                break;
            }
        }

    }



}
