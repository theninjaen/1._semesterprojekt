using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    private int maxCarry;
    private bool carryObject = false;
    private Vector3 previousGood = Vector3.zero;
    private BoxMovement moveBox;
    private GameObject hitBox;
    
    [HideInInspector]
    public int currentCarry = 0;
    [HideInInspector]
    public Vector2 movement;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public int m_PlayerNumber;

    public Rigidbody2D rb;
    public GameObject hitBoxHighlight;
    public float radius;
    public float distance;
    public float speed;
    public Vector2 highlightScale;
    public AudioSource pickUp;
    public AudioSource drop;
    private AudioSource walking;
    public AudioClip[] walk;

    // Start is called before the first frame update
    void Start()
    {
        hitBoxHighlight.transform.localScale = highlightScale;
        walking = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + m_PlayerNumber);
        movement.y = Input.GetAxisRaw("Vertical" + m_PlayerNumber);

        if(rb.velocity.magnitude > 0.5)
        {
            if (!walking.isPlaying)
            {
                PlayRandom();
            }
        }
        else
        {
            walking.Stop();
        }

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
        hit = Physics2D.CircleCast(rb.position, radius, dir, distance);
        Debug.DrawRay(rb.position, dir, Color.blue);

        if (hit && carryObject != true && hit.collider.gameObject.tag == "Pick Up")
        {
            //Debug.Log("Hit " + hit.collider.gameObject.tag);
            hitBox = hit.collider.gameObject;

            hitBoxHighlight.transform.position = hitBox.transform.position + new Vector3(0, 0, 0.01f);
        } else
        {
            hitBoxHighlight.transform.position = new Vector2(20, 20);
        }

        if (Input.GetButtonDown("PickUp" + m_PlayerNumber))
        {
            if (carryObject == false && hit.collider.tag == "Pick Up")
            {
                hit.collider.transform.SetParent(player);
                carryObject = true;

                moveBox = GetComponentInChildren<BoxMovement>();
                moveBox.OnPickup();

                pickUp.Play();
            }

            else if (carryObject == true)
            {
                moveBox.OnDrop();

                player.transform.DetachChildren();
                carryObject = false;

                drop.Play();
            }
        }
        if (m_PlayerNumber == 2)
        {
            if (Input.GetButtonDown("Throw" + m_PlayerNumber) && carryObject == true)
            {
                moveBox.OnThrow();
                carryObject = false;
                print("yas");
            }
        }
    }

    void PlayRandom()
    {
        walking.clip = walk[Random.Range(0, walk.Length)];
        walking.Play();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed * Time.fixedDeltaTime);
    }
}
