using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{

    //[SerializeField] private InputActionAsset ActionAsset;
    [SerializeField] private InputActionReference JoyStitckR;

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
        bool boolValue = (JoyStitckR.action.ReadValue<float>() !=0 );
        //bool a = JoyStitckR.action.ReadValue<float>();
        print(boolValue);
       if(boolValue)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 현재 씬을 다시 로드합니다.
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
