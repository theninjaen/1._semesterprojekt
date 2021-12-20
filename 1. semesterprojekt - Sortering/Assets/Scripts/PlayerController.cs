using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerController : MonoBehaviour
{
    private bool carryObject = false;
    private Vector3 previousGood = Vector3.zero;
    private BoxMovement moveBox;
    private GameObject hitBox;
    private float lastUse;
    private AudioSource walking;
    private float soundTimer;

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
    public float useLimit;
    public Vector2 highlightScale;
    public AudioSource pickUp;
    public AudioSource drop;
    public AudioClip[] walk;
    public AudioClip[] noises;
    public Sprite[] spriteList;
    public AudioSource ownNoise;

    // Start is called before the first frame update
    void Start()
    {
        lastUse = -useLimit;
        hitBoxHighlight.transform.localScale = highlightScale;
        walking = gameObject.GetComponent<AudioSource>();
        PlayNoise();
        GetComponent<SpriteRenderer>().sprite = spriteList[0];
        soundTimer = Random.Range(5f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + m_PlayerNumber);
        movement.y = Input.GetAxisRaw("Vertical" + m_PlayerNumber);

        if (movement.x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[2];
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement.x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[2];
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(movement.y < 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[0];
        }
        else if (movement.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[1];
        }

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

        if (hit && Input.GetButtonDown("PickUp" + m_PlayerNumber) && carryObject == false && hit.collider.tag == "Pick Up")
        {
            hit.collider.transform.SetParent(player);
            carryObject = true;

            moveBox = GetComponentInChildren<BoxMovement>();
            moveBox.OnPickup();

            pickUp.Play();

        } else if (Input.GetButtonDown("PickUp" + m_PlayerNumber) && carryObject == true)
        {
            moveBox.StartCoroutine("OnDrop");

            player.transform.DetachChildren();
            carryObject = false;

            drop.Play();
        }

        if (m_PlayerNumber == 2)
        {
            if (Input.GetButtonDown("Throw" + m_PlayerNumber) && carryObject == true && Time.fixedTime - lastUse > useLimit)
            {
                lastUse = Time.fixedTime;
                moveBox.parentTransform = transform;
                moveBox.StartCoroutine("OnThrow");

                transform.DetachChildren();
                carryObject = false;
            }
        }

        soundTimer -= Time.deltaTime;

        if (soundTimer <= 0)
        {
            PlayNoise();
            soundTimer = Random.Range(5f, 20f);
        }
    }

    void PlayRandom()
    {
        walking.clip = walk[Random.Range(0, walk.Length-1)];
        walking.pitch = Random.Range(0.95f, 1.05f);
        walking.Play();
        walking.volume = 0.3f;
    }

    void PlayNoise()
    {
        ownNoise.clip = noises[Random.Range(0, noises.Length-1)];
        ownNoise.pitch = Random.Range(0.95f, 1.05f);
        ownNoise.Play();
        ownNoise.volume = 0.75f;
    }
    void FixedUpdate()
    {
        rb.AddForce(movement * speed * Time.fixedDeltaTime);
    }
}
