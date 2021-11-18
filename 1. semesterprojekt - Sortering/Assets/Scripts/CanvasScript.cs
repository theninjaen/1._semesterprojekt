using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameObject checkBox1;
    public GameObject checkBox2;
    public GameObject checkBox3;
    public GameObject checkBox4;

    public bool[] fullBoxes = new bool[4];
=======
    public GameObject checkBoxRed;
    public GameObject checkBoxOrange;
    public GameObject checkBoxYellow;
    public GameObject checkBoxGreen;
    public GameObject checkBoxBlue;
    public GameObject checkBoxPurple;

    public bool[] fullBoxes = new bool[6];
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        fullBoxes[0] = checkBox1.GetComponent<CheckBox>().full;
        fullBoxes[1] = checkBox2.GetComponent<CheckBox>().full;
        fullBoxes[2] = checkBox3.GetComponent<CheckBox>().full;
        fullBoxes[3] = checkBox4.GetComponent<CheckBox>().full;
=======
        fullBoxes[0] = checkBoxRed.GetComponent<CheckBox>().full;
        fullBoxes[1] = checkBoxOrange.GetComponent<CheckBox>().full;
        fullBoxes[2] = checkBoxYellow.GetComponent<CheckBox>().full;
        fullBoxes[3] = checkBoxGreen.GetComponent<CheckBox>().full;
        fullBoxes[4] = checkBoxBlue.GetComponent<CheckBox>().full;
        fullBoxes[5] = checkBoxPurple.GetComponent<CheckBox>().full;
>>>>>>> Stashed changes

        m_EmptyCat = checkBoxCat.GetComponent<CheckBoxCat>();

        if (m_EmptyCat.empty == true)
<<<<<<< Updated upstream
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
=======
            transform.GetChild(0).gameObject.SetActive(true);
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
=======
            transform.GetChild(1).gameObject.SetActive(true);
>>>>>>> Stashed changes
    }

    void FixedUpdate()
    {

    }

}
