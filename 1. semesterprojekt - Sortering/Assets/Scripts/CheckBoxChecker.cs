using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxChecker : MonoBehaviour
{
    private bool m_Full;
    public int fullChecker;
    private GameObject m_CheckBoxColor;
    private GameObject m_CheckBox;
    // Start is called before the first frame update
    void Start()
    {
        m_CheckBox = GameObject.Find("CheckBox");
        //m_CheckBoxColor = LayerMask.GetMask(Color);
    }

    // Update is called once per frame
    void Update()
    {
        //m_Full = GameObject.Find("CheckBox" + LayerMask);
    }

    void FixedUpdate()
    {
        /*if (m_Full.full == true)
            fullCheckers += 1;
        else if (full == false && fullCheckers > 0)
            fullCheckers -= 1;*/
    }
}
