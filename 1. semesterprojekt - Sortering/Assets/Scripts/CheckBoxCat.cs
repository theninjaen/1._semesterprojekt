using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxCat : MonoBehaviour
{
    public bool empty;
    public int boxesStored;
    private Vector2 scale;
    public Collider2D[] colliders;
    public LayerMask checkLayer;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, checkLayer);

        boxesStored = colliders.Length;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                boxesStored -= 1;
            }
        }

        if (boxesStored <= 0)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }
    }
}
