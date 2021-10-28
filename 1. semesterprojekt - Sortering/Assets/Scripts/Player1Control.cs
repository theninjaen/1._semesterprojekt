using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Control : MonoBehaviour
{
    private Rigidbody2D bodyP1;

    // Movement containers
    private float verticalP1;
    private float horizontalP1;
    public float speed;

    // Pick up objects
    public GameObject box;
    public Transform player;
    private bool carryObject = false;
    public int maxCarry = 3;
    private int currentCarry = 0;

    public Transform rayGunP1;
    private Vector2 rayGunPosition;
    private Vector3 rayGunRotation;
    public int range = 10;

    void Start()
    {
        bodyP1 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // For movement
        verticalP1 = Input.GetAxis("VerticalP1");
        horizontalP1 = Input.GetAxis("HorizontalP1");

        // Raygun Position Set
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Mathf.Abs(bodyP1.velocity.x) >= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.x > 0)
            {
                rayGunPosition = new Vector2(0.52f, 0);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 0);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) <= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.y > 0)
            {
                rayGunPosition = new Vector2(0, 1);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 90);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) >= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.x < 0)
            {
                rayGunPosition = new Vector2(-0.52f, 0);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 180);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) <= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.y < 0)
            {
                rayGunPosition = new Vector2(0, -1);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 270);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(rayGunP1.transform.position, transform.position, range);
        if (hit.collider != null)
        {
            Debug.Log("Hit " + hit.collider.tag);
            Debug.DrawRay(transform.position, transform.position, Color.black);
        }

        if (Input.GetButtonDown("PickUpP1") && Physics2D.Raycast(rayGunP1.transform.position, transform.position, range) && hit.collider.gameObject.CompareTag("Stack"))
        {
            if (carryObject == false)
            {
                box.transform.SetParent(player);
                carryObject = true;
                currentCarry = currentCarry + 1;
            }

            else if (carryObject == true && currentCarry <= maxCarry)
            {
                box.transform.SetParent(player);
                currentCarry = currentCarry + 1;
            }

            /*else if (carryObject == true && currentCarry == maxCarry)
            {
                Debug.Log("Can't carry anymore!");
            }*/
            Debug.Log("Target hit");
        }
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 moveP1 = new Vector2(1f * horizontalP1 * speed * Time.fixedDeltaTime, 1f * verticalP1 * speed * Time.fixedDeltaTime);
        bodyP1.velocity = moveP1;
    }
}
