using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCircle : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    Rigidbody2D rb;
    Movement movement;
   
    

    private void Start()
    {
        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        movement = new Movement();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector2.up * 21f * movement.Gravity;
        }
    }
}
