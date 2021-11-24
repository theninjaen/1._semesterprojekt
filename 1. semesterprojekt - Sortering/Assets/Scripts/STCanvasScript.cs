using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STCanvasScript : MonoBehaviour
{
    public GameObject[] checkBoxes;


    public bool[] fullBoxesP1 = new bool[5];
    public bool[] fullBoxesP2 = new bool[4];

    public GameObject checkBoxCat;

    public Text winText1;
    public Text winText2;

    private CheckBoxCat m_EmptyCat;

    public bool player1Win;
    public bool player2Win;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fullBoxesP1.Length; i++)
        {
            fullBoxesP1[i] = checkBoxes[i + 4].GetComponent<CheckBox>().full;
        }

        for (int i = 0; i < fullBoxesP2.Length; i++)
        {
            fullBoxesP2[i] = checkBoxes[i].GetComponent<CheckBox>().full;
        }

        m_EmptyCat = checkBoxCat.GetComponent<CheckBoxCat>();

        if (player1Win != true)
        {
            if (m_EmptyCat.empty == true)
            {
                player1Win = true;
            }

            for (int i = 0; i < fullBoxesP1.Length; ++i)
            {
                if (fullBoxesP1[i] == false)
                {
                    player1Win = false;
                    break;
                }
                else
                    player1Win = true;
            }
        }

        for (int i = 0; i < fullBoxesP2.Length; ++i)
        {
            if (fullBoxesP2[i] == false)
            {
                player2Win = false;
                break;
            }
            else
                player2Win = true;
        }

        if (player1Win == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
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
