using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform startpos;
    public Transform monsterpos;
    public Transform pannelpos;
    public Transform monspannelpos;

    bool targetting;

    public XROrigin xrorgin;
    public GameObject pannel;

    Vector3 panelvec;
    // Start is called before the first frame update
    void Start()
    {
        xrorgin = FindObjectOfType<XROrigin>();
        pannelpos = pannel.GetComponent<Transform>();
        panelvec = pannelpos.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePos()
    {
        if(!targetting)
        {
            transform.DOMove(monsterpos.position, 1f);
            pannel.transform.DOMove(monspannelpos.position, 1f);
            pannel.transform.DOScale(new Vector3(0.03f, 0.03f, 0.03f), 1f);
            xrorgin.gameObject.transform.DORotate(monsterpos.rotation.eulerAngles, 1f);
            print(monsterpos.rotation.eulerAngles);
            targetting = true;
        }
        else
        {
            transform.DOMove(startpos.position, 1f);
            pannel.transform.DOMove(startpos.position + panelvec, 1f);
            pannel.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f);
            xrorgin.gameObject.transform.DORotate(startpos.rotation.eulerAngles, 1f);

            targetting = false;
        }
      
    }


}
