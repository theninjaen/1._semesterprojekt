using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject[] checkBoxes;

    public bool[] fullBoxes = new bool[4];

    public GameObject checkBoxCat;

    public Text winText1;
    public Text winText2;

    private CheckBoxCat m_EmptyCat;

    public bool player2Win = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fullBoxes[0] = checkBoxes[0].GetComponent<CheckBox>().full;
        fullBoxes[1] = checkBoxes[1].GetComponent<CheckBox>().full;
        fullBoxes[2] = checkBoxes[2].GetComponent<CheckBox>().full;
        fullBoxes[3] = checkBoxes[3].GetComponent<CheckBox>().full;

        m_EmptyCat = checkBoxCat.GetComponent<CheckBoxCat>();

        if (m_EmptyCat.empty == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        for (int i = 0; i < fullBoxes.Length; ++i)
        {
            if (fullBoxes[i] == false)
            {
                player2Win = false;
                break;
            }
            else
                player2Win = true;
        }

        if (player2Win == true)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {

    }
}
