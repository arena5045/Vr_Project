using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBtn : MonoBehaviour
{
    public bool isClick = false;

    public Image outline;

    PlayerMove pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindAnyObjectByType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick() 
    {
     if(!isClick) //클릭하면
        {
            Vector3 cp = gameObject.GetComponent<RectTransform>().anchoredPosition;
            cp.y += 20;
            gameObject.GetComponent<RectTransform>().anchoredPosition = cp;
            isClick = true;

            outline.enabled = true;
            pm.MovePos();
            Debug.Log(isClick);
        }
     else //취소하면
        {
            Vector3 cp = gameObject.GetComponent<RectTransform>().anchoredPosition;
            cp.y -= 20;
            gameObject.GetComponent<RectTransform>().anchoredPosition = cp;
            isClick = false;

            outline.enabled = false;
            pm.MovePos();
            Debug.Log(isClick);
        }
    
    
    }
}
