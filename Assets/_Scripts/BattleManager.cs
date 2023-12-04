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
    public GameObject monster;

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
    public List<GameObject> CardList = new List<GameObject>();

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
        SelectedCard.OrderSetting(true, Actionlist.Count);
        SelectedCard.GetComponent<CardBtn>().Selected();
        SelectedCard = null;

        for (int i = 0; i < CardList.Count; i++)
        {
            Card card = CardList[i].GetComponent<Card>();
            Debug.Log(!card.cardbtn.isSelected +" 이거와 이거 "+(card.cost > curCost));

            if (!card.cardbtn.isSelected && card.cost>curCost)
            {
                card.SetAble(false);
            }
            else
            {
                card.SetAble(true);
            }
        }
     }

    public void RemoveTarget(Card card)
    {
        
        for(int i=0; i< Actionlist.Count; i++)
        {
            if (Actionlist[i].card == card)
            {
                CurCost += Actionlist[i].card.cost;
                Actionlist[i].card.OrderSetting(false, 0);
                Actionlist[i].card.GetComponent<CardBtn>().isSelected = false;
                Actionlist.Remove(Actionlist[i]);

                if (i == Actionlist.Count) //뺀카드가 마지막일경우 종료
                    break;
            }

            Actionlist[i].card.OrderSetting(true, i + 1);
        }


        for (int i = 0; i < CardList.Count; i++)
        {
            Card hcard = CardList[i].GetComponent<Card>();
            //Debug.Log(!hcard.cardbtn.isSelected + " 이거와 이거 " + (hcard.cost > curCost));

            if (!hcard.cardbtn.isSelected && hcard.cost > curCost)
            {
                hcard.SetAble(false);
            }
            else
            {
                hcard.SetAble(true);
            }
        }

    }

}
