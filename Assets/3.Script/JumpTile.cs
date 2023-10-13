using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTile : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
            rb.velocity = Vector2.up * 21f * movement.Gravity;
    }
}
