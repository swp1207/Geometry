using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Cube = 0,
    Ship,
    Ball,
    UFO,
    Dart,
}

//public enum Gravity
//{
//    Upright = 1,
//    Upsidedown = -1,
//}

public struct ColliderCorner
{
    public Vector2 Topleft;
    public Vector2 Bottomleft;
    public Vector2 Bottomright;
}
public struct ColliderChecker
{
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;

    public void Reset()
    {
        Up = false;
        Down = false;
        Left = false;
        Right = false;
    }
}



public class Movement : MonoBehaviour
{
    public GameMode currentGameMode = 0;

    //public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;        //LayerMask?
    public Transform Sprite;
    private GameObject Player;

    MapMove mapMove;
    Rigidbody2D rb;

    public int Gravity = 1;
    public bool clickProcessed = false;

    float abc = 0;
    public bool isDead = false;
    private AudioSource audio;


    //[Header("Raycast count")]
    //[SerializeField] private int HorizontalCount = 3;
    //[SerializeField] private int VerticalCount = 3;
    //private float HorizontalMeter;
    //private float VerticalMeter;
    //private readonly float SkinWidth = 0.015f;
    //private Collider2D collider2D;
    //private ColliderCorner colliderCorner;
    //private ColliderChecker colliderChecker;
    //public Transform Hittransfrom { get; private set; }

    //public GameObject Player = null;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapMove = new MapMove();
        audio = transform.GetComponent<AudioSource>();
        //Player = transform.GetChild(0).gameObject;
        //collider2D = Player.GetComponent<Collider2D>();
        Player = GameObject.Find("Player");

        OnGameModeChange += ChangeMode;
    }

    private event Action OnGameModeChange;

    private void ChangeMode()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(GameMode)).Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild((int)currentGameMode).gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        Invoke(currentGameMode.ToString(), 0);
    }

    public bool IsGround()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.down * Gravity * 0.3f, Vector2.right * 0.8f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    public bool IsTouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.35f), Vector2.up * 0.5f + Vector2.right * GroundCheckRadius, 0, GroundMask);
    }

    void Cube()
    {
        Genetic.CreateGameMode(rb, this, true, 18.1269f, 8.857f, true, false, 709.1f); //19.5269, 9.057, 409.1
    }

    void Ship()
    {
        rb.gravityScale = 2.93f * ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) ? -1 : 1) * Gravity;
        Genetic.LimitYVelocity(9.95f, rb);
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);
    }

    void Ball()
    {
        Genetic.CreateGameMode(rb, this, true, 0, 6.2f, false, true);
        abc += 200*Time.deltaTime;
        Sprite.Rotate(new Vector3(0, 0, abc));
    }

    void UFO()
    {
        Genetic.CreateGameMode(rb, this, false, 10.841f, 4.1483f, false, false, 0, 10.841f);
    }

    void Dart()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, mapMove.SpeedValues[(int)mapMove.currentSpeed] * ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) ? 1 : -1) * Gravity);
        transform.rotation = Quaternion.Euler(0, 0, (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) ? 90 : 0));
    }


    public void ChangeThroughtPortal(GameMode gameMode, Speeds speed, int gravity, int State)
    {
        switch (State)
        {
            case 0:
                currentGameMode = gameMode;
                OnGameModeChange();
                break;
            case 1:
                mapMove.currentSpeed = speed;
                break;
            case 2:
                Gravity = gravity;
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                break;                
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direc = collision.contacts[0].normal;
        Debug.Log(direc);
        if (direc.x <= -0.9 || direc.x >= 0.9)
        {
          // Die();
        }

        Portal portal = collision.gameObject.GetComponent<Portal>();
        if (portal)
        {
            portal.InitiatePortal(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead") && !isDead)
        {
            Die();
        }
        if (collision.CompareTag("Clear") && !isDead)
        {
            GameManager.Instance.GameClear();
            if (Input.anyKeyDown)
            {
                Application.Quit();
            }
        }        
    }

    private void Die()
    {
        audio.Play();
        for (int i = 0; i < System.Enum.GetValues(typeof(GameMode)).Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isDead = true;
        GameManager.Instance.PlayerDead();
    }

    //private void FirstMove()
    //{
        
    //}


}
