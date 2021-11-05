using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWall : MonoBehaviour
{
    public CapsuleCollider2D player;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
