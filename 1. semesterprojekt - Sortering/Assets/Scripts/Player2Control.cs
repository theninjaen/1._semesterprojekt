using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Control : MonoBehaviour
{
    private Rigidbody2D bodyP2;

    // Movement containers
    private float verticalP2;
    private float horizontalP2;
    public float speed;

    public Transform rayGunP2;

    void Start()
    {
        bodyP2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // For movement
        verticalP2 = Input.GetAxis("VerticalP2");
        horizontalP2 = Input.GetAxis("HorizontalP2");

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Mathf.Abs(bodyP2.velocity.x) >= Mathf.Abs(bodyP2.velocity.y) && bodyP2.velocity.x >= 0)
            {
                rayGunP2.transform.localPosition = new Vector3(0.52f, 0, 0);
            }
            else if (Mathf.Abs(bodyP2.velocity.x) <= Mathf.Abs(bodyP2.velocity.y) && bodyP2.velocity.y >= 0)
            {
                rayGunP2.transform.localPosition = new Vector3(0, 1, 0);
            }
            else if (Mathf.Abs(bodyP2.velocity.x) >= Mathf.Abs(bodyP2.velocity.y) && bodyP2.velocity.x <= 0)
            {
                rayGunP2.transform.localPosition = new Vector3(-0.52f, 0, 0);
            }
            else if (Mathf.Abs(bodyP2.velocity.x) <= Mathf.Abs(bodyP2.velocity.y) && bodyP2.velocity.x <= 0)
            {
                rayGunP2.transform.localPosition = new Vector3(0, -1, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 moveP2 = new Vector2(1f * horizontalP2 * speed * Time.fixedDeltaTime, 1f * verticalP2 * speed * Time.fixedDeltaTime);
        bodyP2.velocity = moveP2;
    }
}
