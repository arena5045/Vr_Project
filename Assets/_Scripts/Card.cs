using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum CardTyep
    {
        Active,
        Buff,
        DeBuff,
        Heal
    }

    public CardData data;

    public string cardName;
    public CardTyep type;
    public int cost;
    public int Value;
    public Sprite illust;

    public TMP_Text nametext;
    public Image[] typeimg;
    public Image cardImg;
    public TMP_Text costtext;
    public TMP_Text valuetext;

    public GameObject orderImage;
    public TMP_Text orderText;

    public GameObject DisalbeImage;
    public CardBtn cardbtn;


    private void Awake()
    {
        cardbtn = GetComponent<CardBtn>();
    }
    private void Start()
    {
        SettingUi();
    }

    public void SettingUi()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        if (data != null)
        {
            cardName = data.cardName;
            nametext.text = cardName;
            cost = data.cost;
            costtext.text = cost.ToString();
            ValueSetting();

            cardImg.sprite = data.cardImg;
        }
        else
        {
            nametext.text = cardName;
            costtext.text = cost.ToString();
            valuetext.text = Value.ToString();
        }

        orderImage.SetActive(false);
        SetAble(true);
        cardbtn.SetRefresh();

        /*
          cardImg.sprite = illust;
          switch(type)
         {
             case CardTyep.Active:
                 break;
             case CardTyep.Buff:
                 break;
             case CardTyep.DeBuff:
                 break;
             case CardTyep.Heal:
                 break;
         }
        */

    }

    public void ValueSetting()
    {
        Value = data.value;

        if (BattleManager.Instance.BuffList.Count >= 0)
        {
            foreach (int[] value in BattleManager.Instance.BuffList)
            {
                if (data.type == CardData.Type.Active)
                {
                    Value += (value[1] * cost);
                }
            }
        }

        valuetext.text = Value.ToString();

    }
    public void OrderSetting(bool isOn, int ordercount)
    {
        orderImage.SetActive(isOn);

        if (isOn)
        { orderText.text = ordercount.ToString(); }
    }


    public void SetAble(bool isAble)
    {
            gameObject.GetComponent<Button>().enabled = isAble;
            DisalbeImage.SetActive(!isAble);
    }


}
