using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Monster : MonoBehaviour
{
    [Serializable]
    public struct monsterPart
    {
        public string PartName;
        public int maxhp;
        public int hp;
        public bool Active;

        public int NomalAttackDamage;
        public int SuperAttackDamage;

        public GameObject targettingUi;
        public GameObject hpUi;
        public GameObject transRoot;

        public void Dead()
        {
             Active = false;
             print(Active);
             targettingUi.SetActive(false);
             hpUi.SetActive(false);
        }
    }

    
    public List<monsterPart> parts;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < parts.Count; i++) //�� ������ �̸��̶� ��ȣ �Ҵ�
        {
            TargettingCanvas tc = parts[i].targettingUi.GetComponentInChildren<TargettingCanvas>();
            if (tc)
            {
                tc.partname = parts[i].PartName;
                tc.partnum = i;
            }
        }

    }


    private void Update()
    {
        foreach (monsterPart part in parts) 
        {
          part.hpUi.GetComponentInChildren<Slider>().value = (float)part.hp/part.maxhp;
        }
    }



    public void Damaged(int partsnum, Card card, bool effective)
    {
            monsterPart part = parts[partsnum];
            if (part.hp <= card.Value)//�״� �Ǹ�
            {
                card.Value = part.hp;
                part.hp -= card.Value;
                part.Dead();
            }
            else
            {
                part.hp -= card.Value;
            }
            parts[partsnum] = part;
            print(part.PartName + "�� " + card.Value + "��ŭ�� ������!");


        if(card.data != null && effective)
        { 
                GameObject hiteffect = Instantiate(card.data.effect, parts[partsnum].transRoot.transform);
                Destroy(hiteffect, card.data.casttime);
        }

        if(partsnum != 0) //��üģ�Ծƴϸ� ��ü�� ������
        {
            Damaged(0, card, false);
        }
    }

    public void DamagedAll(Card card, bool effective)
    {

            for(int i =0; i< parts.Count; i++)
            {
                monsterPart part = parts[i];
                if (part.hp <= card.Value)//�״� �Ǹ�
                {
                    card.Value = part.hp;
                    part.hp -= card.Value;
                    part.Dead();
                }
                else
                {
                    part.hp -= card.Value;
                }
                parts[i] = part;
                print(part.PartName + "�� " + card.Value + "��ŭ�� ������!");

            if (i != 0) //��üģ�Ծƴϸ� ��ü�� ������
            {
                Damaged(0, card, false);
            }
        }

        GameObject hiteffect = Instantiate(card.data.effect, gameObject.transform);
        hiteffect.transform.localPosition = new Vector3(0, 3, 0);
        Destroy(hiteffect, card.data.casttime);

    }

}
