using DG.Tweening;
using System.Collections;
using System.Drawing;
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
        StartCoroutine(HpBarColorChange(UnityEngine.Color.red));
        Hpbar.gameObject.GetComponent<DOTweenAnimation>().DORestart();
        curHp -= dmg;

        if (curHp <= 0)
        {
            curHp = 0;
            print("惟績神獄っっっっっっっっっっっ");
            PlayerManager.Instance.Gameover();
        }
    }


    public void Heal(int value)
    {
        StartCoroutine(HpBarColorChange(UnityEngine.Color.green));

        if (curHp +value >= MaxHp)
        {
            DOTween.To(()=>curHp, x => curHp = x, MaxHp,1f);
        }
        else
        {
            DOTween.To(() => curHp, x => curHp = x, curHp + value, 1f);
        }
    }

    IEnumerator HpBarColorChange(UnityEngine.Color color)
    {
        UnityEngine.Color savecolor = Hpbar.fillRect.gameObject.GetComponent<Image>().color;
        Hpbar.fillRect.gameObject.GetComponent<Image>().color = color;
        yield return new WaitForSeconds(1f);
        Hpbar.fillRect.gameObject.GetComponent<Image>().color = savecolor;
    }

}
