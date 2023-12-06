using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
using static BattleManager;
using static UnityEngine.Rendering.DebugUI;

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

    public List<CardData> Deck = new List<CardData>();


    public List<int[]> BuffList = new List<int[]>();
    public int[] buff = new int[2];
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
        turnCost = 3;
        curCost = 3;

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

        if (BuffList.Count > 0)
        {
            List<int[]> deadbuff = new List<int[]>();
            for(int i =0; i< BuffList.Count; i++)
            {
                BuffList[i][0]--;

                if (BuffList[i][0] <= 0)
                {
                    deadbuff.Add(BuffList[i]);
                    print(i+"��° ���� ��������");
                }
            }

            if (deadbuff.Count > 0)
            {
                for(int i = 0; i < deadbuff.Count; i++)
                { 
                    BuffList.Remove(deadbuff[i]);
                }
            }
  
        }

        foreach (GameObject card in CardList)
        {
            card.GetComponent<Card>().SettingUi();
        }


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

                if (i == Actionlist.Count) //��ī�尡 �������ϰ�� ����
                    break;
            }

            Actionlist[i].card.OrderSetting(true, i + 1);
        }


        for (int i = 0; i < CardList.Count; i++)
        {
            Card hcard = CardList[i].GetComponent<Card>();
            //Debug.Log(!hcard.cardbtn.isSelected + " �̰ſ� �̰� " + (hcard.cost > curCost));

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
            switch(Actionlist[0].card.data.type)
            {
                case CardData.Type.Active:
                    if (Actionlist[0].targetnum == -1)//��ü�����̸�
                    {
                        monster.DamagedAll(Actionlist[0].card, true);
                    }

                    else if (monster.parts[Actionlist[0].targetnum].Active)
                    {
                        monster.Damaged(Actionlist[0].targetnum, Actionlist[0].card, true);
                    }
                    else //�����̹� Ÿ���� �������
                    {
                        List<int> RandTarget = new List<int>();

                        for (int i = 0; i < monster.parts.Count; i++)
                        {
                            if (monster.parts[i].Active)//����ִ� ���� ����
                            {
                                print(i + "������ �������");
                                RandTarget.Add(i); //����ִ� ���� ��ȣ �߰�
                            }
                        }
                        if (RandTarget.Count > 0)
                        {
                            int randomT = UnityEngine.Random.Range(0, RandTarget.Count);
                            monster.Damaged(RandTarget[randomT], Actionlist[0].card, true);
                            print("����� �̹� �׾����Ƿ�" + monster.parts[RandTarget[randomT]].PartName + "����" + Actionlist[0].card.Value + "��ŭ ������");
                        }
                        else //���׾��ٴ� �Ҹ�
                        {
                            print("���⿡ ����Ŭ���� ����");

                        }
                    }

                    break;
                case CardData.Type.Buff:
                    buff[0] = Actionlist[0].card.data.duration_turn;
                    buff[1] = Actionlist[0].card.Value;

                    BuffList.Add(buff);
                    buff = new int[2];

                    //����Ʈ ����
                    GameObject buffeffect = Instantiate(Actionlist[0].card.data.effect,PlayerManager.Instance.playerOb.transform);
                    Destroy(buffeffect, Actionlist[0].card.data.casttime);

                    //������ ����
                    foreach (GameObject card in CardList)
                    {
                        card.GetComponent<Card>().ValueSetting();
                    }

                    break;
                case CardData.Type.Debuff:
                    break;
                case CardData.Type.heal:
                    PlayerManager.Instance.player.Heal(Actionlist[0].card.Value);
                    GameObject healffect = Instantiate(Actionlist[0].card.data.effect, PlayerManager.Instance.playerOb.transform);
                    Destroy(healffect, Actionlist[0].card.data.casttime);
                    break;

            }
            float casttime = Actionlist[0].card.data.casttime;

            Actionlist[0].card.data = null; //ī�� ������ �襙!
            Actionlist[0].card.gameObject.SetActive(false); //

            Actionlist.RemoveAt(0);

            yield return new WaitForSeconds(casttime);
        }

        StartCoroutine(MonsterTurnCourtine());
    }

    IEnumerator MonsterTurnCourtine()
    {
        PlayerManager.Instance.pm.MovePos();
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < monster.parts.Count; i++)
        {
            if (monster.parts[i].Active)
            {
                monster.AttackAnim(i + 1);
                UiManager.Instance.MonsAttackPannelSet(true, monster.parts[i].NomalAttackName);
                yield return new WaitForSeconds(0.5f);
                PlayerManager.Instance.player.Damaged(monster.parts[i].NomalAttackDamage);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(1.5f);
            UiManager.Instance.MonsAttackPannelSet(false,"");
            if (monster.isDead)
            {
                yield break;
            }
        }
        TurnStart();
        UiManager.Instance.MainPannel.GetComponent<TrackedDeviceGraphicRaycaster>().enabled = true;
    }



}
