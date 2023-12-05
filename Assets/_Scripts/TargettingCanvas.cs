using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargettingCanvas : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
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

        Debug.Log("Ŭ���׽�Ʈ");


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(BattleManager.Instance.SelectedCard != null) //ī�� ���� ���¸�
        {
            BattleManager.Instance.AddTarget(partnum); //������ ī�带 Ÿ������ ����

            PlayerManager.Instance.pm.MovePos();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UiManager.Instance.MonsNamePannelSet(true, partname);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UiManager.Instance.MonsNamePannelSet(false, partname);
    }


}
