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
        for (int i = 0; i < parts.Count; i++) //각 파츠에 이름이랑 번호 할당
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
        if (part.hp <= value)//죽는 피면
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
        print(part.PartName + "에 " + value + "만큼의 데미지!");

        if(partsnum != 0) //본체친게아니면 본체도 데미지
        {
            Damaged(0, value);
        }
    }

}
