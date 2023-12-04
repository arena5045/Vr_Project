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



    public void Damaged(int partsnum, int value)
    {
        monsterPart part = parts[partsnum];
        if (part.hp <= value)//�״� �Ǹ�
        {
            value = part.hp;
            part.hp -= value;
            part.Dead();
        }
         else
        {
            part.hp -= value;
        }

       
        parts[partsnum] = part;
        print(part.PartName + "�� " + value + "��ŭ�� ������!");

        if(partsnum != 0) //��üģ�Ծƴϸ� ��ü�� ������
        {
            Damaged(0, value);
        }
    }

}
