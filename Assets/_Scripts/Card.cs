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
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        nametext.text = cardName;
        costtext.text = cost.ToString();
        valuetext.text = Value.ToString();

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
