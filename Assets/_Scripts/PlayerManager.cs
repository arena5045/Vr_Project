using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerManager instance;

    public static PlayerManager Instance { get; private set; }

    public GameObject player;
    public PlayerMove pm;
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;

            pm = player.GetComponent<PlayerMove>();
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
