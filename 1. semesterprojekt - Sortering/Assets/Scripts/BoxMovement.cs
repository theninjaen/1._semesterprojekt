using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private PlayerController placement;

    //Movement Variables
    private bool moving;
    private Vector2 boxPos;
    private float multiplier;

    public float distance;

    [Range(0, 1)]
    public float lerpSetting;

    //Stacking/Drop Variables
    private BoxCollider2D myCollider;
    public Collider2D[] colliders;
    private Vector2 scale;
    private float currentOverlap;
    private float bestOverlap;
    private GameObject bestGameObject;
    private SpriteRenderer bestSprite;
    private BoxCollider2D bestBox;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();

        scale = new Vector2(transform.localScale.x, transform.localScale.y);

        bestOverlap = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && Input.GetButton("Horizontal" + placement.m_PlayerNumber) || moving && Input.GetButton("Vertical" + placement.m_PlayerNumber))
        {
            if (placement.movement.x > 0 && placement.movement.y > 0)
            {
                multiplier = distance / (Mathf.Sqrt(Mathf.Pow(placement.movement.x, 2) * Mathf.Pow(placement.movement.y, 2)));

            } else if (placement.movement.x > 0 && placement.movement.y == 0)
            {
                multiplier = distance / placement.movement.x;

            } else if (placement.movement.x == 0 && placement.movement.y >0)
            {
                multiplier = distance / placement.movement.y;
            }
            
            boxPos = new Vector2(placement.movement.x * multiplier, placement.movement.y * multiplier);
        }
    }

    private void FixedUpdate()
    {
        if (moving && Input.GetButton("Horizontal" + placement.m_PlayerNumber) || moving && Input.GetButton("Vertical" + placement.m_PlayerNumber))
        {
            transform.localPosition = Vector2.Lerp(boxPos, transform.localPosition, lerpSetting);
        }
    }

    public void OnPickup()
    {
        moving = true;
        placement = GetComponentInParent<PlayerController>();

        myCollider.enabled = false;

        if (bestGameObject && bestBox.enabled == false && bestSprite.enabled == false)
        {
            bestBox.enabled = true;
            bestSprite.enabled = true;
        }
    }

    public void OnDrop()
    {
        moving = false;

        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0);

        myCollider.enabled = true;

        placement.player.transform.DetachChildren();
        placement.currentCarry = 0;

        if (colliders.Length != 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetComponent<BoxCollider2D>().enabled && colliders[i].gameObject.layer == 3)
                {
                    currentOverlap = Vector2.Distance(transform.position, colliders[i].gameObject.transform.position);

                    if (currentOverlap < bestOverlap)
                    {
                        bestOverlap = currentOverlap;

                        bestGameObject = colliders[i].gameObject;
                    }
                }
            }

            transform.position = bestGameObject.transform.position;

            bestBox = bestGameObject.GetComponent<BoxCollider2D>();
            bestSprite = bestGameObject.GetComponent<SpriteRenderer>();

            bestBox.enabled = false;
            bestSprite.enabled = false;

            bestOverlap = 100;
        }
    }
}
