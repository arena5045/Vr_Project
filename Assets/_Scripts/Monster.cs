using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [Serializable]
    public struct monsterPart
    {
        public string PartName;
        public int maxhp;
        public int hp;

        public GameObject targettingUi;
        public GameObject hpUi;
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
}
