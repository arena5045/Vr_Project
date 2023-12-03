using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        UiRefresh();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UiRefresh()
    {
        costText.text = $"{BattleManager.Instance.CurCost}/{BattleManager.Instance.turnCost}";
        CostDotRefresh();
    }


    public void CostDotRefresh()
    {
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
