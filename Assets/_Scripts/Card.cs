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



    private void Start()
    {
        SettingUi();
    }

    void SettingUi()
    {
        nametext.text = cardName;
        costtext.text = cost.ToString();
        valuetext.text = Value.ToString();

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

}
