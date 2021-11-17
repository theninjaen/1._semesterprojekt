using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public bool full;
    public LayerMask CheckBoxes;
    public int boxesStored;
    public int boxesFull;
    private Vector2 scale;
    public Collider2D[] colliders;
    public string checkColor;

    public string thisCheckBoxColor;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
         if (boxesStored >= boxesFull)
        {
            full = true;
        }
        else
        {
            full = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boxesStored = 0;
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, CheckBoxes);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.GetChild(0).gameObject.layer == LayerMask.NameToLayer(checkColor))
            {
                boxesStored += colliders[i].GetComponent<BoxMovement>().colorBoxesStacked;
            }
        }
    }
}
