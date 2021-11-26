using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{

    private Vector2 scale;
    private SpriteRenderer boxColor;

    public LayerMask CheckBoxes;
    public int boxesStored;
    public Collider2D[] colliders;
    public string checkColor;
    public Color checkBoxColor;


    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector2(transform.localScale.x, transform.localScale.y);
        boxColor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, CheckBoxes);
        boxesStored = 0;
    
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.GetChild(0).gameObject.layer == LayerMask.NameToLayer(checkColor))
            {
                boxesStored += colliders[i].GetComponent<BoxMovement>().colorBoxesStacked;
            }
        }
    }
}
