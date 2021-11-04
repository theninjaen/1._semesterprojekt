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

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, CheckBoxes);

        boxesStored = colliders.Length;

        if (boxesStored >= boxesFull)
        {
            full = true;
        }
        else
        {
            full = false;
        }
    }
}
