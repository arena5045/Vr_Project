using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [Serializable]
    public struct monsterPart
    {
        public string PartName;
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

}
