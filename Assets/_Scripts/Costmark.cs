using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costmark : MonoBehaviour
{
    public GameObject activedot;
    public GameObject deactivedot;

    private void Awake()
    {
        activedot.SetActive(false);
        deactivedot.SetActive(false);
    }


    public void AtciveCost()
    {
        deactivedot.SetActive(false);
        activedot.SetActive(true);
    }

    public void DeactiveCost()
    {
        deactivedot.SetActive(true);
        activedot.SetActive(false);
    }
}
