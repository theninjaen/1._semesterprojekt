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
    
    [Range(0, 1)]
    public float lerpSetting;
    public float distance;

    //Stacking/Drop Variables
    private BoxCollider2D myCollider;
    private Vector2 scale;
    private float currentOverlap;
    private float bestOverlap;
    private GameObject bestGameObject;
    private SpriteRenderer bestSprite;
    private BoxCollider2D bestBox;
    private BoxMovement bestBoxMovement;
    private Collider2D[] colliders;
    private int randomX;
    private int randomY;

    [HideInInspector]
    public int boxesStacked;
    [HideInInspector]
    public int colorBoxesStacked;
    [HideInInspector]
    public string boxColor;
    public int maxStack;
    public LayerMask PickUpLayer;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();

        scale = new Vector2(transform.localScale.x, transform.localScale.y);

        bestOverlap = 100;

        boxesStacked = 1;
        colorBoxesStacked = 1;
        maxStack = 4;
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
        boxesStacked = 1;
        colorBoxesStacked = 1;

        if (bestGameObject && bestBox.enabled == false && bestSprite.enabled == false)
        {
            bestBox.enabled = true;
            bestSprite.enabled = true;
        }
    }

    public void OnDrop()
    {
        placement.player.transform.DetachChildren();
        placement.currentCarry = 0;

        while (moving == true)
        {
            colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, PickUpLayer);

            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    currentOverlap = Vector2.Distance(transform.position, colliders[i].gameObject.transform.position);

                    if (currentOverlap < bestOverlap)
                    {
                        bestOverlap = currentOverlap;
                        bestGameObject = colliders[i].gameObject;
                        bestBox = bestGameObject.GetComponent<BoxCollider2D>();
                        bestSprite = bestGameObject.GetComponent<SpriteRenderer>();
                        bestBoxMovement = bestGameObject.GetComponent<BoxMovement>();
                    }
                }
            }

            if (bestOverlap != 100)
            {
                boxesStacked += bestBoxMovement.boxesStacked;

                if (boxColor == bestBoxMovement.boxColor)
                {
                    colorBoxesStacked += bestBoxMovement.colorBoxesStacked;
                }
            }

            if (boxesStacked <= maxStack)
            {
                if (bestOverlap != 100)
                {
                    transform.position = bestGameObject.transform.position;

                    bestBox.enabled = false;
                    bestSprite.enabled = false;
                }

                myCollider.enabled = true;
                moving = false;
                bestOverlap = 100;

            } else
            {
                randomX = Random.Range(-1, 2);
                randomY = Random.Range(-1, 2);
                transform.position = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
                boxesStacked = 1;
                colorBoxesStacked = 1;

                myCollider.enabled = true;
                moving = false;
            }
        }
    }
}
