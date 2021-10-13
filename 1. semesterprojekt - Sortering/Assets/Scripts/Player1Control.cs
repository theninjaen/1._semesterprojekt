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
            if (Mathf.Abs(bodyP1.velocity.x) >= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.x >= 0)
            {
                rayGunPosition = new Vector2(0.52f, 0);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 0);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) <= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.y >= 0)
            {
                rayGunPosition = new Vector2(0, 1);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 90);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) >= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.x <= 0)
            {
                rayGunPosition = new Vector2(-0.52f, 0);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 180);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
            else if (Mathf.Abs(bodyP1.velocity.x) <= Mathf.Abs(bodyP1.velocity.y) && bodyP1.velocity.x <= 0)
            {
                rayGunPosition = new Vector2(0, -1);
                rayGunP1.transform.localPosition = rayGunPosition;

                rayGunRotation = new Vector3(0, 0, 270);
                rayGunP1.transform.eulerAngles = rayGunRotation;
            }
        }

        RaycastHit hit;

        if (Input.GetButtonDown("PickUpP1") && Physics.Raycast(rayGunP1.transform.position, transform.forward, out hit, range) && hit.collider.gameObject.CompareTag("Stack"))
        {

        }
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 moveP1 = new Vector2(1f * horizontalP1 * speed * Time.fixedDeltaTime, 1f * verticalP1 * speed * Time.fixedDeltaTime);
        bodyP1.velocity = moveP1;
    }
}
