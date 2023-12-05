using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int MaxHp=1000;
    public int curHp=1000;

    public Slider Hpbar;
    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 1000;
        curHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //print(curHp);
       Hpbar.value = (float)curHp / MaxHp;
        //Damaged(1); 
    }



    public void Damaged(int dmg)
    {
        curHp -= dmg;

        if (curHp <= 0)
        {
            curHp = 0;
            print("惟績神獄っっっっっっっっっっっ");
            PlayerManager.Instance.Gameover();
        }
    }



}
