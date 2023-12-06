using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSizeColor : MonoBehaviour {

    public Gradient color;
    public Color m_changeColor;
    //[HideInInspector]
    public GameObject m_obj;
    Renderer[] m_rnds;
    float color_Value;
    public Image m_ColorHandler;
    public Text m_intensityfactor;
    public float intensity = 2.0f;

	private void Update()
	{
        //m_changeColor = color.Evaluate(color_Value);
        //m_ColorHandler.color = m_changeColor;

        if( m_obj != null)
        {
            m_rnds = m_obj.GetComponentsInChildren<Renderer>(true);

            foreach(Renderer rend in m_rnds)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetColor("_TintColor", m_changeColor * intensity);
                    rend.materials[i].SetColor("_Color", m_changeColor * intensity);
                    rend.materials[i].SetColor("_RimColor", m_changeColor * intensity);
                }
            }
        }
	}

}
