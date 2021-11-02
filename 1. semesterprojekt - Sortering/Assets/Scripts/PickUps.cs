using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    public string[] color = new string[6];
    public string[] type = new string[6];
    public GameObject[] info = new GameObject[2];
    public GameObject txt;
    public Color objectColor;

    // Start is called before the first frame update
    void Start()
    {
        color[0] = "Red";
        color[1] = "Orange";
        color[2] = "Yellow";
        color[3] = "Green";
        color[4] = "Blue";
        color[5] = "Purple";

        type[0] = "Hat";
        type[1] = "Wand";
        type[2] = "Broom";
        type[3] = "Goblet";
        type[4] = "Book";
        type[5] = "Bomb";

        int colorIndex = Random.Range(0, color.Length);
        info[0].transform.gameObject.tag = color[colorIndex];

        int typeIndex = Random.Range(0, type.Length);
        info[1].transform.gameObject.tag = type[typeIndex];

        switch (info[0].gameObject.tag)
        {
            case "Red":
                objectColor = new Color(255f / 255f, 51f / 255f, 51f / 255f);
                info[0].layer = 6;
                info[1].layer = 12;
                break;
            case "Orange":
                objectColor = new Color(255f / 255f, 153f / 255f, 51f / 255f);
                info[0].layer = 7;
                info[1].layer = 12;
                break;
            case "Yellow":
                objectColor = new Color(255f / 255f, 255f / 255f, 51f / 255f);
                info[0].layer = 8;
                info[1].layer = 12;
                break;
            case "Green":
                objectColor = new Color(51f / 255f, 255f / 255f, 51f / 255f);
                info[0].layer = 9;
                info[1].layer = 12;
                break;
            case "Blue":
                objectColor = new Color(51f / 255f, 153f / 255f, 255f / 255f);
                info[0].layer = 10;
                info[1].layer = 12;
                break;
            case "Purple":
                objectColor = new Color(153f / 255f, 51f / 255f, 255f / 255f);
                info[0].layer = 11;
                info[1].layer = 12;
                break;
        }

        gameObject.GetComponent<Renderer>().material.color = objectColor;
        txt.GetComponent<UnityEngine.UI.Text>().text = info[1].tag;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
