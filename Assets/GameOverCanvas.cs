using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverCanvas : MonoBehaviour
{

    [SerializeField] private InputActionAsset ActionAsset;
    [SerializeField] private InputActionReference JoyStitckR;

    // Start is called before the first frame update
    void Start()
    {
        if (ActionAsset != null)
        {
            print("작동");
            ActionAsset.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool boolValue = (JoyStitckR.action.ReadValue<float>() !=0 );
        //bool a = JoyStitckR.action.ReadValue<float>();
        print(boolValue);
        //if(JoyStitckR.action.ReadValue<float>())
        {
            //print("눌렀다고");
        }
    }
}
