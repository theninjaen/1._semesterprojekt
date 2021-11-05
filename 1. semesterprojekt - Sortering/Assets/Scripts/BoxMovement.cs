using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private PlayerController placement;
    private bool moving;
    private Vector2 boxPos;
    private float multiplier;

    public float distance;

    [Range(0, 1)]
    public float lerpSetting;

    // Start is called before the first frame update
    void Start()
    {
        
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

        
    }

    public void OnDrop()
    {
        moving = false;
    }
}
