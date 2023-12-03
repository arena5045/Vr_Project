using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargettingCanvas : MonoBehaviour, IPointerClickHandler
{
    public string partname;
    public int partnum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTest()
    {

        Debug.Log("클릭테스트");


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(BattleManager.Instance.SelectedCard != null) //카드 선택 상태면
        {
            BattleManager.Instance.AddTarget(partnum); //부위랑 카드를 타겟으로 지정

            PlayerManager.Instance.pm.MovePos();
        }
    }
}
