using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerManager instance;

    public static PlayerManager Instance { get; private set; }

    public GameObject playerOb;
    public PlayerMove pm;
    public Player player; 
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;

            pm = playerOb.GetComponent<PlayerMove>();
            player = playerOb.GetComponent<Player>();
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
