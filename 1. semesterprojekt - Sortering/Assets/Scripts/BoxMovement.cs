using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private PlayerController placement;

    //Movement Variables
    private bool flying;
    private Vector2 newPos;
    private Vector2 startPos;
    private Vector2 flyPos;
    private float multiplier;
    private float direction;
    private float trajectoryY;
    private float trajectoryX;
    private float trajectoryHeight;
    private float trajectoryLenght;
    private int count = 0;

    [Range(0, 1)]
    public float lerpSetting;
    public float distance;
    public float trajectoryHeightAdjustment;
    public float trajectoryHeightSpeed;
    public float trajectoryLenghtAdjustment;
    public bool moving;

    //Stacking/Drop Variables
    private Vector2 scale;
    private float currentOverlap;
    private float bestOverlap;
    private GameObject bestGameObject;
    private GameObject bestCanvas;
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
    public int testX;
    public int testY;
    public LayerMask pickUpLayer;
    public LayerMask pickedUpLayer;

    void Awake()
    {
        scale = new Vector2(testX, testY);

        bestOverlap = 100;

        boxesStacked = 1;
        colorBoxesStacked = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scale = new Vector2(testX, testY);

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

            newPos = new Vector3(placement.movement.x * multiplier, placement.movement.y * multiplier, -0.3f);
        }

        if (flying)
        {
            trajectoryHeight += trajectoryHeightSpeed;
            trajectoryLenght += trajectoryLenghtAdjustment;

            trajectoryY = Mathf.Sin(trajectoryHeight) * trajectoryHeightAdjustment;
            if (transform.position.x > -8.6 && transform.position.x < 8.6)
            {
                trajectoryX = trajectoryLenght * direction;
            }

            flyPos = new Vector2(startPos.x + trajectoryX, startPos.y + trajectoryY);

            transform.position = flyPos;

            if (flyPos.y <= startPos.y)
            {
                count++;
                if (count == 2)
                {
                    count = 0;
                    transform.position = new Vector2(flyPos.x, startPos.y);

                    flying = false;
                    moving = true;
                    OnDrop();

                    trajectoryHeight = 0;
                    trajectoryLenght = 0;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (moving && Input.GetButton("Horizontal" + placement.m_PlayerNumber) || moving && Input.GetButton("Vertical" + placement.m_PlayerNumber))
        {
            transform.localPosition = Vector3.Lerp(newPos, transform.localPosition, lerpSetting);
        }
    }

    public void OnPickup()
    {
        gameObject.layer = 16;

        moving = true;
        transform.position = transform.position + new Vector3(0, 0, -0.3f);
        placement = GetComponentInParent<PlayerController>();
        boxesStacked = 1;
        colorBoxesStacked = 1;

        bestBox.enabled = true;
        bestSprite.enabled = true;
        bestCanvas.SetActive(true);
    }

    public void OnDrop()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, scale, 0, pickUpLayer);

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
                    bestCanvas = bestGameObject.transform.GetChild(1).gameObject;
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

                bestSprite.enabled = false;
                bestBox.enabled = false;
                bestCanvas.SetActive(false);
            }

            gameObject.layer = 3;
            moving = false;
            bestOverlap = 100;

        }
        else
        {
            randomX = Random.Range(-1, 2);
            randomY = Random.Range(-1, 2);
            transform.position = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            boxesStacked = 1;
            colorBoxesStacked = 1;

            gameObject.layer = 3;

            moving = false;
        }
    }

    public void OnThrow()
    {
        startPos = transform.position;
        moving = false;
        flying = true;
        direction = Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber);
        trajectoryHeight = 0;

        if (Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber) == 0)
        {
            float dif = transform.position.x - GetComponentInParent<Transform>().position.x;

            if (dif <= 0)
            {
                direction = -1;
            } else if (dif > 0)
            {
                direction = 1;
            }
        }

        placement.player.transform.DetachChildren();
        placement.currentCarry = 0;
    }
}
