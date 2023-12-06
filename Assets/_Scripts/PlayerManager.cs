using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    private PlayerManager instance;

    public static PlayerManager Instance { get; private set; }

    public GameObject playerOb;
    public PlayerMove pm;
    public Player player;

    public bool gameover = false;
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;

            pm = playerOb.GetComponent<PlayerMove>();
            player = playerOb.GetComponent<Player>();
        }

    }

    public void Gameover()
    {
        gameover = true;
        UiManager.Instance.SetGameover();
        ActionBasedController[] cont = playerOb.GetComponentsInChildren<ActionBasedController>();
        foreach (ActionBasedController controller in cont)
        {
            controller.enabled = false;
        }
    }

    public void GameClear()
    {
        UiManager.Instance.SetGameClear();
        ActionBasedController[] cont = playerOb.GetComponentsInChildren<ActionBasedController>();
        foreach (ActionBasedController controller in cont)
        {
            controller.enabled = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
