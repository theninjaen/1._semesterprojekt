using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private PlayerController placement;

    //Movement Variables
    private Vector3 newPos;
    private Vector3 bobbing;
    private Vector2 shadowSize = new Vector2(2.5f, 2.5f);
    private float multiplier;
    private float bobFloat;
    private float bobFloatVar;

    [Range(0, 1)]
    public float lerpSetting;
    public float distance;
    public float trajectoryHeightAdjustment;
    public float trajectoryHeightSpeed;
    public float trajectoryLenghtAdjustment;
    public float bobFloatAdjust;
    public float bobFloatHeight;
    public float bobHeight;
    public float shadowScaleDistance;
    public GameObject shadow;
    public bool flying;
    public bool moving;

    //Stacking/Drop Variables
    private Vector2 scale;
    private float bestOverlap;
    private GameObject bestCanvas;
    private SpriteRenderer bestSprite;
    private BoxCollider2D bestBox;

    [HideInInspector] public int boxesStacked;
    [HideInInspector] public int colorBoxesStacked;
    [HideInInspector] public string boxColor;
    [HideInInspector] public Transform parentTransform;


    public float dropAdjust;
    public float dropHeight;
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

        if (moving)
        {
            bobFloatVar += bobFloatAdjust;
            bobFloat = Mathf.Sin(bobFloatVar) * bobFloatHeight;
            bobbing = new Vector3(0, bobHeight + bobFloat, 0);
        }

        if (shadow.transform.parent == transform)
        {
            shadow.transform.localScale = shadowSize * ((shadowScaleDistance - Vector3.Distance(shadow.transform.position, transform.position)) / shadowScaleDistance);
        } else
        {
            shadow.transform.SetParent(transform);
            shadow.transform.localScale = shadowSize * ((shadowScaleDistance - Vector3.Distance(shadow.transform.position, transform.position)) / shadowScaleDistance);
            shadow.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.localPosition = Vector3.Lerp(newPos + bobbing, transform.localPosition, lerpSetting);
            shadow.transform.localPosition = new Vector3(0, -(bobFloat + bobHeight), 0.1f);
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

        if (bestBox != null && bestSprite != null && bestCanvas != null)
        {
            bestBox.enabled = true;
            bestSprite.enabled = true;
            bestCanvas.SetActive(true);
            bestCanvas = null;
            bestSprite = null;
            bestBox = null;
        }
    }

    public IEnumerator OnDrop()
    {
        moving = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(shadow.transform.position, scale, 0, pickUpLayer);
        BoxMovement bestBoxMovement = this;
        GameObject bestGameObject = shadow;
        Vector3 bestPos;
        int randomX;
        int randomY;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                float currentOverlap = Vector2.Distance(shadow.transform.position, colliders[i].gameObject.transform.position);

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

            if (boxesStacked <= maxStack)
            {
                bestPos = bestGameObject.transform.position;

                if (boxColor == bestBoxMovement.boxColor)
                {
                    colorBoxesStacked += bestBoxMovement.colorBoxesStacked;
                }
            } else
            {
                randomX = Random.Range(-1, 2);
                randomY = Random.Range(-1, 2);
                bestPos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

                boxesStacked = 1;
            }
        } else
        {
            bestGameObject = shadow;
            bestPos = shadow.transform.position;
        }
        
        if (!flying)
        {

            Vector3 startPos = transform.position;
            Vector3 moveDirection = (bestPos - startPos) / 10;
            Vector3 shadowStartPos = shadow.transform.position;
            Vector3 shadowMoveDirection = (bestPos - shadowStartPos) / 10;
            float dropVar = 0;
            float startDistance = Vector3.Distance(startPos, bestPos);
            Vector3 drop = new Vector3(1, 1, 1);

            shadow.transform.parent = null;

            while (Vector3.Distance(transform.position, bestPos) > startDistance / 10 && drop.y > -0.45f)
            {
                dropVar += dropAdjust;
                drop = new Vector3(0, dropHeight * dropVar - Mathf.Pow(dropVar, 2), 0);

                transform.position += moveDirection + drop;

                shadow.transform.position += new Vector3(shadowMoveDirection.x, shadowMoveDirection.y, 0);

                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            shadow.transform.SetParent(transform);
        }

        if (bestOverlap != 100)
        {
            if (boxesStacked <= maxStack)
            {
                bestSprite.enabled = false;
                bestBox.enabled = false;
                bestCanvas.SetActive(false);
                bestOverlap = 100;
            }
        }

        transform.position = bestPos;
        shadow.transform.position = transform.position + new Vector3(0, 0, 0.1f);

        flying = false;
        gameObject.layer = 3;
    } 

    public IEnumerator OnThrow()
    {
        Vector3 startPos = shadow.transform.position;
        Vector3 realStartPos = transform.position;
        Vector3 flyPos = realStartPos + new Vector3(1, 1, 0);
        float direction;
        float trajectoryHeight = 0;
        float trajectoryLenght = 0;
        float trajectoryX = 0;

        moving = false;
        flying = true;

        if (transform.position.x > parentTransform.position.x)
        {
            direction = 1;
            if (Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber) < 0)
            {
                direction = -1;
            }
        } else if (transform.position.x < parentTransform.position.x)
        {
            direction = -1;
            if (Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber) > 0)
            {
                direction = 1;
            }
        } else
        {
            direction = 0;
        }
        /*
        if (transform.position.x <= parentPos.x)
        {
            direction = -1;
            if (Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber) > 0)
            {
                direction = 1;
            }
        } else if (transform.position.x > parentPos.x)
        {
            direction = 1;
            if (Input.GetAxisRaw("Horizontal" + placement.m_PlayerNumber) < 0)
            {
                direction = -1;
            }
        }
        */

        while (flyPos.y >= startPos.y)
        {
            trajectoryHeight += trajectoryHeightSpeed;
            trajectoryLenght += trajectoryLenghtAdjustment;

            float trajectoryY = trajectoryHeightAdjustment * trajectoryHeight - Mathf.Pow(trajectoryHeight, 2);

            if (transform.position.x > -8.6 && transform.position.x < 8.6)
            {
                trajectoryX = trajectoryLenght * direction;
            }

            flyPos = new Vector3(realStartPos.x + trajectoryX, realStartPos.y + trajectoryY, -0.2f);

            transform.position = flyPos;
            shadow.transform.position = new Vector3(transform.position.x, startPos.y, 0.3f);

            if (flyPos.y >= startPos.y)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }

        transform.position = new Vector3(flyPos.x, startPos.y, -2f);

        StartCoroutine("OnDrop");
    }
}
