using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private UiManager instance;
    public static UiManager Instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
    }


    public TMP_Text costText;
    public GameObject[] costmark;
    public GameObject[] Hpbar;
    public GameObject MainPannel;

    // Start is called before the first frame update
    void Start()
    {
        UiRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        HpbarRefressh();
    }


    public void UiRefresh()
    {
        costText.text = $"{BattleManager.Instance.CurCost}/{BattleManager.Instance.turnCost}";
        for (int i = 0; i < Hpbar.Length; i++)
        { int k = BattleManager.Instance.monster.GetComponent<Monster>().parts.Count;
            if (i < k)
            {
                Hpbar[i].SetActive(true);
            }
            else
            { Hpbar[i].SetActive(false); }

        }

        CostDotRefresh();
    }


    public void HpbarRefressh()
    {
        for(int i =0; i < BattleManager.Instance.monster.GetComponent<Monster>().parts.Count; i++) 
        {
            Hpbar[i].GetComponentInChildren<Slider>().value = BattleManager.Instance.monster.GetComponent<Monster>().parts[i].hpUi.GetComponentInChildren<Slider>().value;
        }
    }


    public void CostDotRefresh()
    {
        costText.text = $"{BattleManager.Instance.CurCost}/{BattleManager.Instance.turnCost}";

        for (int i = 0; i < BattleManager.Instance.turnCost; i++)
        {
            if(i< BattleManager.Instance.CurCost)
            {
                costmark[i].GetComponent<Costmark>().AtciveCost();
            }
            else
            {
                costmark[i].GetComponent<Costmark>().DeactiveCost();
            }
        }
    }
}
