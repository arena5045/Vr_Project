using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using static BattleManager;

public class BattleManager : MonoBehaviour
{

    private BattleManager instance;

    public static BattleManager Instance { get; private set; }

    public int maxCost = 10;
    public int turnCost = 0;
    [SerializeField]
    private int curCost = 0;
    public bool isZoom = false;
    public GameObject monsterOb;
    public Monster monster;

    public int CurCost
    { get { return curCost; }
        set
        {
            curCost = value;
            UiManager.Instance.CostDotRefresh();

        }
    }

    public int battleTurn = 0;


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
        if (instance == null)
        {
            Instance = this;
            monster = monsterOb.GetComponent<Monster>();    
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        maxCost = 10;
        turnCost = 5;
        CurCost = 5;

        TurnStart();
    }

    void TurnStart()
    {
        battleTurn++;
        if(turnCost < maxCost)
        {
            turnCost++;
        }
        CurCost = turnCost;

        foreach(GameObject card in CardList)
        {
            card.GetComponent<Card>().SettingUi();
        }
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

            if (!card.cardbtn.isSelected && card.cost > curCost)
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

        for (int i = 0; i < Actionlist.Count; i++)
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

    public void StartBattleTurn()
    {
        if(Actionlist.Count == 0)
        {
            return;
        }

        StartCoroutine(PlayerTurnCorutine());
    }


    IEnumerator PlayerTurnCorutine()
    {
        UiManager.Instance.MainPannel.GetComponent<TrackedDeviceGraphicRaycaster>().enabled = false;
        PlayerManager.Instance.pm.MovePos();
        yield return new WaitForSeconds(1f);

        while (Actionlist.Count !=0) 
        { 


           if(monster.parts[Actionlist[0].targetnum].Active)
            {
                monster.Damaged(Actionlist[0].targetnum, Actionlist[0].card.Value);
            }
           else //만약이미 타겟이 죽은경우
            {
                List<int> RandTarget = new List<int>();

                for (int i = 0; i < monster.parts.Count; i++)
                {
                    if (monster.parts[i].Active)//살아있는 파츠 색출
                    {
                        print(i+"번파츠 살아있음");
                        RandTarget.Add(i); //살아있는 파츠 번호 추가
                    }
                }
                if (RandTarget.Count>0)
                {
                    int randomT = UnityEngine.Random.Range(0, RandTarget.Count);
                    monster.Damaged(RandTarget[randomT], Actionlist[0].card.Value);
                    print("대상이 이미 죽었으므로" + monster.parts[RandTarget[randomT]].PartName + "에게" + Actionlist[0].card.Value + "만큼 데미지");
                }
                else //다죽었다는 소리
                {
                    print("여기에 게임클리어 내용");
                    
                }
            }
           
            Actionlist[0].card.gameObject.SetActive(false);
            Actionlist.RemoveAt(0);

            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(MonsterTurnCourtine());
    }

    IEnumerator MonsterTurnCourtine()
    {
        PlayerManager.Instance.pm.MovePos();
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < monster.parts.Count; i++)
        {
            PlayerManager.Instance.player.Damaged(monster.parts[i].NomalAttackDamage);


            yield return new WaitForSeconds(1f);
        }
        TurnStart();
        UiManager.Instance.MainPannel.GetComponent<TrackedDeviceGraphicRaycaster>().enabled = true;
    }



}
