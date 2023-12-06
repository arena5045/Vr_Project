using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{

    [SerializeField] private InputActionAsset ActionAsset;
    [SerializeField] private InputActionReference JoyStitckR;
    [SerializeField] private InputActionReference JoyStitckSecondary;
    // Start is called before the first frame update
    void Start()
    {
        //if (ActionAsset != null)
        {
  
           // ActionAsset.Enable();
           // print(ActionAsset.enabled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool privalue = (JoyStitckR.action.ReadValue<float>() !=0 );
        bool secondaryvalue = (JoyStitckSecondary.action.ReadValue<float>() != 0);
        //bool a = JoyStitckR.action.ReadValue<float>();
        print(privalue);
       if(privalue)
        {
            SceneTransitionManager.singleton.GoToSceneAsync(1);
            enabled = false;
        }
       else if(secondaryvalue)
        {
            SceneTransitionManager.singleton.GoToSceneAsync(0);
            enabled = false;
        }
    }
}
