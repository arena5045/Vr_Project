using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBtn : MonoBehaviour
{
    public bool isClick = false;
    public bool isSelected = false;

    public Image outline;
    public Image selectedOutline;

    PlayerMove pm;
    Card card;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindAnyObjectByType<PlayerMove>();
        card = GetComponent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick() 
    {

        if (BattleManager.Instance.SelectedCard !=null && BattleManager.Instance.SelectedCard != card)
            return;

       if(!isClick) //클릭하면
        {
            Vector3 cp = gameObject.GetComponent<RectTransform>().anchoredPosition;
            cp.y += 20;
            gameObject.GetComponent<RectTransform>().anchoredPosition = cp;
            isClick = true;

            outline.enabled = true;
            selectedOutline.enabled = false;
            pm.MovePos();
            Debug.Log(isClick);

            BattleManager.Instance.AddCard(gameObject.GetComponent<Card>());
        }
       else //취소하면
        {
            if(isSelected) //선택된 상태서 취소시
            {
                Vector3 cp = gameObject.GetComponent<RectTransform>().anchoredPosition;
                cp.y -= 20;
                gameObject.GetComponent<RectTransform>().anchoredPosition = cp;
                isClick = false;

                outline.enabled = false;
                selectedOutline.enabled = false;

                BattleManager.Instance.RemoveTarget(card);
            }
            else //그냥취소시
            {
                Vector3 cp = gameObject.GetComponent<RectTransform>().anchoredPosition;
                cp.y -= 20;
                gameObject.GetComponent<RectTransform>().anchoredPosition = cp;
                isClick = false;

                outline.enabled = false;
                selectedOutline.enabled = false;
                pm.MovePos();
                Debug.Log(isClick);
                BattleManager.Instance.RemoveCard();
            }
        }
    }


    public void Selected()
    {
        outline.enabled = false;
        selectedOutline.enabled = true;
        isSelected = true;
    }


}
