using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables for deciding resource colors

    public LayerMask pickUpLayer;

    private string[] color = new string[6];
    private GameObject pickUpColor;
    private GameObject txt;
    private Color objectColor;
    private int colorIndex;
    private Collider2D[] pickUps;
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

    // Start is called before the first frame update
    void Start()
    {
        //Deciding resource colors

        //Finds all pickups in game
        pickUps = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(18,12), 0, pickUpLayer);

        color[0] = "Red";
        color[1] = "Orange";
        color[2] = "Yellow";
        color[3] = "Green";
        color[4] = "Blue";
        color[5] = "Purple";

        //Assigns values for all pickups in game
        for (int i = 0; i < pickUps.Length; i++)
        {
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
            txt.GetComponent<UnityEngine.UI.Text>().text = color[colorIndex];

        }

        //=======================================================================================================
        //Checking for suitable colors for checkboxes

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

    // Update is called once per frame
    void Update()
    {

    }
}