using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public bool full;
    public LayerMask BlueBoxes;
    public int boxesStored;
    public int boxesFull;
    public Vector2 scale = new Vector2(3, 3);
    public Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, BlueBoxes);

        boxesStored = colliders.Length;

        if (boxesStored >= boxesFull)
        {
            full = true;
        }
        else
        {
            full = false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(i);
        }
    }
}
