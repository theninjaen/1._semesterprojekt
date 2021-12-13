using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText;
    public GameObject witch;
    public GameObject cat;
    public GameObject[] pickUps;
    public GameObject[] checkBoxes;
    public GameObject pickUpColor;
    public GameObject txt;
    public GameObject playButton;

    private SpriteRenderer boxColor;
    private CheckBox checkBox;
    private CheckBoxCat EmptyStock;
    public GameObject checkBoxStock;

    public int stage;
    public bool pickUpComplete = false;
    public bool stealComplete = false;
    public bool throwReady = false;
    public bool throwComplete = false;

    public int fullCheckP1;
    public int fullCheckP2;

    public bool[] fullP1 = new bool[5];
    public bool[] fullP2 = new bool[4];
    private int[] BoxesP1 = new int[5];
    private int[] BoxesP2 = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        boxColor = checkBoxes[6].GetComponent<SpriteRenderer>();
        checkBox = checkBoxes[6].GetComponent<CheckBox>();
        boxColor.color = new Color(255f / 255f, 51f / 255f, 51f / 255f, 123f / 255f);
        checkBox.checkColor = "Red";
        checkBox.checkBoxColor = boxColor.color;
        boxColor = checkBoxes[3].GetComponent<SpriteRenderer>();
        checkBox = checkBoxes[3].GetComponent<CheckBox>();
        boxColor.color = new Color(255f / 255f, 51f / 255f, 51f / 255f, 123f / 255f);
        checkBox.checkColor = "Red";
        checkBox.checkBoxColor = boxColor.color;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                tutorialText.text = "The witch must move and pick up a jar to the left";
                stage = 1;
                break;
            case 1:
                if (pickUpComplete == true)
                {
                    tutorialText.text = "Place the jar on the work table with the appropriate color";
                    stage = 2;
                }
                break;
            case 2:
                if (checkBoxes[6].GetComponent<CheckBox>().boxesStored > 0)
                {
                    tutorialText.text = "The cat must move and steal a jar from the witch's worktable";
                    stage = 3;
                }
                break;
            case 3:
                if (stealComplete == true && checkBoxes[6].GetComponent<CheckBox>().boxesStored == 0)
                {
                    tutorialText.text = "The cat must place the jar in its own spots";
                    stage = 4;
                }
                break;
            case 4:
                if (checkBoxes[3].GetComponent<CheckBox>().boxesStored > 0)
                {
                    tutorialText.text = "The witch can win by filling the tables with " + fullCheckP1 + " Jars, you can stack jars onto each other";
                    stage = 5;
                }
                break;
            case 5:
                if (fullP1[2] == true)
                {
                    tutorialText.text = "The cat can prevent the witch from winning by stealing from a full table";
                    stage = 6;
                }
                break;
            case 6:
                if (fullP1[2] == false)
                {
                    tutorialText.text = "The cat can throw the Jars back towards the storage area or towarsds its own spots";
                    stage = 7;
                }
                break;
            case 7:
                if (throwComplete == true)
                {
                    tutorialText.text = "The cat can win by filling up its own spots with " + fullCheckP2 + " Jars";
                    stage = 8;
                }
                break;
            case 8:
                if (fullP2[3] == true)
                {
                    tutorialText.text = "Additionally the Witch can win by emptying the stock";
                    stage = 9;
                }
                break;
            case 9:
                if (EmptyStock.empty == true)
                {
                    tutorialText.text = "You've completed the Tutorial";
                    playButton.SetActive(true);
                    stage = 10;
                }
                break;
        }

        if (stage == 1 && witch.transform.childCount > 0)
        {
            pickUpComplete = true;
        }

        if (stage == 3 && cat.transform.childCount > 0)
        {
            stealComplete = true;
        }

        if (stage == 7 && cat.transform.childCount > 0)
        {
            throwReady = true;
        }

        if (throwReady == true && Input.GetButtonDown("Throw2"))
        {
            throwComplete = true;
        }

        CheckIfBoxesFull();
        
    }

    void CheckIfBoxesFull()
    {
        for (int i = 0; i < BoxesP1.Length; i++)
        {
            BoxesP1[i] = checkBoxes[i + 4].GetComponent<CheckBox>().boxesStored;
        }

        for (int i = 0; i < BoxesP2.Length; i++)
        {
            BoxesP2[i] = checkBoxes[i].GetComponent<CheckBox>().boxesStored;
        }

        for (int i = 0; i < fullP1.Length; i++)
        {
            if (BoxesP1[i] >= fullCheckP1)
            {
                fullP1[i] = true;
                checkBoxes[i + 4].GetComponent<SpriteRenderer>().color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 123f / 255f);
            }
            else
            {
                fullP1[i] = false;
                checkBoxes[i + 4].GetComponent<SpriteRenderer>().color = checkBoxes[i + 4].GetComponent<CheckBox>().checkBoxColor;
            }
        }

        for (int i = 0; i < fullP2.Length; i++)
        {
            if (BoxesP2[i] >= fullCheckP2)
            {
                fullP2[i] = true;
                checkBoxes[i].GetComponent<SpriteRenderer>().color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 123f / 255f);
            }
            else
            {
                fullP2[i] = false;
                checkBoxes[i].GetComponent<SpriteRenderer>().color = checkBoxes[i].GetComponent<CheckBox>().checkBoxColor;
            }
        }

        EmptyStock = checkBoxStock.GetComponent<CheckBoxCat>();
    }
}
