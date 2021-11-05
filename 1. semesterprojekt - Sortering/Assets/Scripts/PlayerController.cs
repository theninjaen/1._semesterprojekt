using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public GameObject pickUp;
    public Transform player;
    public int maxCarry = 1;
    public int m_PlayerNumber = 1;
    public Vector3 boxOffset;

    public bool carryObject = false;
    private Vector3 previousGood = Vector3.zero;
    public int currentCarry = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + m_PlayerNumber);
        movement.y = Input.GetAxisRaw("Vertical" + m_PlayerNumber);

        Vector3 dir = new Vector2(movement.x, movement.y);

        if (dir == Vector3.zero)
        {
            dir = previousGood;
        }
        else
        {
            previousGood = dir;
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(rb.position, dir, 1f);
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.gameObject.tag);
        }
        Debug.DrawRay(rb.position, dir, Color.blue);

        if (Input.GetButtonDown("PickUp" + m_PlayerNumber) && hit.collider.tag == "Pick Up" /*&& hit.collider.GetComponent<SpriteRenderer>().enabled == true*/)
        {
            if (carryObject == false)
            {
                hit.collider.transform.SetParent(player);
                carryObject = true;
                currentCarry += 1;
                //hit.collider.transform.position = player.transform.position;
                //hit.collider.gameObject.SetActive(false);

                BoxMovement moveBox = GetComponentInChildren<BoxMovement>();
                moveBox.OnPickup();
            }

            /*else if (carryObject == true && currentCarry < maxCarry)
            {
                hit.collider.transform.SetParent(player);
                currentCarry += 1;
                hit.collider.transform.position = player.transform.position;
                hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }*/

            else if (carryObject == true && currentCarry >= maxCarry)
            {
                Debug.Log("Can't carry anymore.");
            }
        }

        if (Input.GetButtonDown("Drop" + m_PlayerNumber))
        {
            if (carryObject == true)
            {
                //player.transform.GetChild(0).gameObject.SetActive(true);
                if (player.transform.GetChild(0).gameObject.activeSelf)
                {
                    BoxMovement moveBox = GetComponentInChildren<BoxMovement>();
                    moveBox.OnDrop();

                    player.transform.DetachChildren();
                    currentCarry = 0;
                    //player.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

        if (currentCarry <= 0)
        {
            carryObject = false;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        /*if (carryObject == true)
        {
            player.transform.GetChild(0).position = (rb.position, dir);
        }*/
    }


}
