using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STGameManager : MonoBehaviour
{
    //Variables for deciding resource colors

    public LayerMask pickUpLayer;
    public GameObject[] pickUps;

    private string[] color = new string[6];
    private GameObject pickUpColor;
    private GameObject txt;
    private Color objectColor;
    private int colorIndex;
    private int countdown;
    private BoxMovement boxMovement;

    //Variables for Checking for suitable colors for checkboxes
    public Vector2 overlapScale;
    public Vector2 overlapPosition;

    public LayerMask checkRed;
    public LayerMask checkOrange;
    public LayerMask checkYellow;
    public LayerMask checkGreen;
    public LayerMask checkBlue;
    public LayerMask checkPurple;

    public int reqAllowed;

    private Collider2D[] collidersRed;
    private Collider2D[] collidersOrange;
    private Collider2D[] collidersYellow;
    private Collider2D[] collidersGreen;
    private Collider2D[] collidersBlue;
    private Collider2D[] collidersPurple;

    private bool allowedRed = false;
    private bool allowedOrange = false;
    private bool allowedYellow = false;
    private bool allowedGreen = false;
    private bool allowedBlue = false;
    private bool allowedPurple = false;

    //Variables for deciding checkbox colors
    public GameObject[] checkBoxes;

    private string[] checkBoxColors = new string[6];
    private SpriteRenderer boxColor;
    private CheckBox checkBox;
    private string colorBox;
    private int running;

    //Variables for checking who won
    public GameObject checkBoxCat;
    public GameObject winText1;
    public GameObject winText2;
    public GameObject reset;
    public GameObject menu;
    public bool player1Win;
    public bool player2Win;
    public int fullCheckP1;
    public int fullCheckP2;

    private bool[] fullP1 = new bool[5];
    private bool[] fullP2 = new bool[4];
    private int[] BoxesP1 = new int[5];
    private int[] BoxesP2 = new int[4];
    private CheckBoxCat m_EmptyCat;

    void Start()
    {
        //Deciding resource colors

        color[0] = "Red";
        color[1] = "Orange";
        color[2] = "Yellow";
        color[3] = "Green";
        color[4] = "Blue";
        color[5] = "Purple";

        countdown = 1;

        //Assigns values for all pickups in game
        for (int i = 0; i < pickUps.Length; i++)
        {
            pickUps[i].SetActive(true);

            pickUpColor = pickUps[i].transform.GetChild(0).gameObject;
            txt = pickUps[i].transform.GetChild(1).GetChild(0).gameObject;
            boxMovement = pickUps[i].GetComponent<BoxMovement>();

            colorIndex = Random.Range(0, color.Length);
            pickUpColor.layer = 6 + colorIndex;

            switch (pickUpColor.layer)
            {
                case 6: //Red
                    objectColor = new Color(255f / 255f, 0f / 255f, 0f / 255f);
                    boxMovement.boxColor = color[0];
                    break;

                case 7: //Orange
                    objectColor = new Color(255f / 255f, 144f / 255f, 0f / 255f);
                    boxMovement.boxColor = color[1];
                    break;

                case 8: //Yellow
                    objectColor = new Color(255f / 255f, 255f / 255f, 0f / 255f);
                    boxMovement.boxColor = color[2];
                    break;

                case 9: //Green
                    objectColor = new Color(124f / 255f, 250f / 255f, 131f / 255f);
                    boxMovement.boxColor = color[3];
                    break;

                case 10: //Blue
                    objectColor = new Color(0f / 255f, 164f / 255f, 255f / 255f);
                    boxMovement.boxColor = color[4];
                    break;

                case 11: //Purple
                    objectColor = new Color(167f / 255f, 90f / 255f, 231f / 255f);
                    boxMovement.boxColor = color[5];
                    break;
            }

            //Applys selected color to object material and defines text.
            pickUps[i].gameObject.GetComponent<Renderer>().material.color = objectColor;
            txt.GetComponent<Text>().text = color[colorIndex];

            countdown--;
            if (countdown != 0)
            {
                boxMovement.flying = false;
                pickUps[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                boxMovement.StartCoroutine("OnDrop");
                pickUps[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
            } else
            {
                countdown = 4;
            }
        }

        //=======================================================================================================
        //Checking for suitable colors for checkboxes

        //int count = GameObject.FindGameObjectsWithTag("Player").Length;

        collidersRed = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkRed);
        collidersOrange = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkOrange);
        collidersYellow = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkYellow);
        collidersGreen = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkGreen);
        collidersBlue = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkBlue);
        collidersPurple = Physics2D.OverlapBoxAll(overlapPosition, overlapScale, 0, checkPurple);

        if (collidersRed.Length >= reqAllowed)
            allowedRed = true;

        if (collidersOrange.Length >= reqAllowed)
            allowedOrange = true;

        if (collidersYellow.Length >= reqAllowed)
            allowedYellow = true;

        if (collidersGreen.Length >= reqAllowed)
            allowedGreen = true;

        if (collidersBlue.Length >= reqAllowed)
            allowedBlue = true;

        if (collidersPurple.Length >= reqAllowed)
            allowedPurple = true;

        //=======================================================================================================
        //Deciding checkbox colors



        if (allowedRed == true)
        {
            checkBoxColors[0] = "Red";
        }
        if (allowedOrange == true)
        {
            checkBoxColors[1] = "Orange";
        }
        if (allowedYellow == true)
        {
            checkBoxColors[2] = "Yellow";
        }
        if (allowedGreen == true)
        {
            checkBoxColors[3] = "Green";
        }
        if (allowedBlue == true)
        {
            checkBoxColors[4] = "Blue";
        }
        if (allowedPurple == true)
        {
            checkBoxColors[5] = "Purple";
        }

        for (int i = 0; i < checkBoxes.Length; i++)
        {
            running = 10;

            boxColor = checkBoxes[i].GetComponent<SpriteRenderer>();
            checkBox = checkBoxes[i].GetComponent<CheckBox>();

            while (running != 0)
            {
                colorIndex = Random.Range(0, color.Length);
                colorBox = checkBoxColors[colorIndex];

                if (i == 4)
                {
                    if (allowedRed == true)
                    {
                        checkBoxColors[0] = "Red";
                    }
                    if (allowedOrange == true)
                    {
                        checkBoxColors[1] = "Orange";
                    }
                    if (allowedYellow == true)
                    {
                        checkBoxColors[2] = "Yellow";
                    }
                    if (allowedGreen == true)
                    {
                        checkBoxColors[3] = "Green";
                    }
                    if (allowedBlue == true)
                    {
                        checkBoxColors[4] = "Blue";
                    }
                    if (allowedPurple == true)
                    {
                        checkBoxColors[5] = "Purple";
                    }
                }

                switch (colorBox)
                {
                    case "Red": //Red
                        checkBoxColors[0] = "";
                        boxColor.color = new Color(255f / 255f, 51f / 255f, 51f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "Orange": //Orange
                        checkBoxColors[1] = "";
                        boxColor.color = new Color(255f / 255f, 153f / 255f, 51f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "Yellow": //Yellow
                        checkBoxColors[2] = "";
                        boxColor.color = new Color(255f / 255f, 255f / 255f, 51f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "Green": //Green
                        checkBoxColors[3] = "";
                        boxColor.color = new Color(51f / 255f, 255f / 255f, 51f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "Blue": //Blue
                        checkBoxColors[4] = "";
                        boxColor.color = new Color(51f / 255f, 153f / 255f, 255f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "Purple": //Purple
                        checkBoxColors[5] = "";
                        boxColor.color = new Color(153f / 255f, 51f / 255f, 255f / 255f, 123f / 255f);
                        checkBox.checkColor = colorBox;
                        checkBox.checkBoxColor = boxColor.color;
                        running = 0;
                        break;

                    case "":
                        if (running == 1)
                        {
                            i = checkBoxes.Length;
                            Start();
                        }
                        running--;
                        break;
                }
            }
        }
    }

    void Update()
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

        m_EmptyCat = checkBoxCat.GetComponent<CheckBoxCat>();

        if (player1Win != true)
        {
            for (int i = 0; i < fullP1.Length; ++i)
            {
                if (fullP1[i] == false)
                {
                    player1Win = false;
                    break;
                }
                else
                    player1Win = true;
            }

            if (m_EmptyCat.empty == true)
            {
                player1Win = true;
            }
        }

        for (int i = 0; i < fullP2.Length; ++i)
        {
            if (fullP2[i] == false)
            {
                player2Win = false;
                break;
            }
            else
                player2Win = true;
        }

        if (player1Win == true)
        {
            winText1.SetActive(true);
            reset.SetActive(true);
        }

        if (player2Win == true)
        {
            winText2.SetActive(true);
            reset.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }
}
