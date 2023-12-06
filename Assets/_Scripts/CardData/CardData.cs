using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Object/카드", order = 0)]

public class CardData : ScriptableObject
{
    public enum Type
    {
        none,
        Active,
        Buff,
        Debuff,
        heal
    }

    public enum Element
    {
        none,
        fire,
        water,
        thunder,
        leaf,
        light,
        dark

    }
    public string cardName;
    public int cardCode;
    public bool isAll;

    public Sprite cardImg;
    public GameObject effect;
    public AudioClip sound;

    public float casttime;

    public Type type;
    public Element element;

    public int cost;
    public int value;
    public int duration_turn;
    [Header("만약 잔존 효과가 있을시 ")]
    public int duration_value;
    [Header("카드 설명")]
    [TextArea] public string explanation;


}
