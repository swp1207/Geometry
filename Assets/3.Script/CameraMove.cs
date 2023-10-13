using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float camSpeed = 0.5f;
    private GameObject Player;
    Rigidbody2D rb;
    Transform camera;
    Movement movement;
    Vector3 startpos = new Vector3(0, 1, -10);

    private void Start()
    {
        Player = GameObject.Find("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        camera = GetComponent<Transform>();
        movement = new Movement();
    }

    private void Update()
    {
        CamMove();
    }

    private void CamMove()
    {
        if (Mathf.Floor(Player.transform.position.y) > 2 && movement.currentGameMode == GameMode.Cube)
        {


            camera.position = Vector3.MoveTowards(transform.position, new Vector3(0, Mathf.Round(Player.transform.position.y), camera.position.z), camSpeed*Time.deltaTime);
        }
        else
        {
            camera.position = Vector3.MoveTowards(transform.position, startpos, 0.1f);
        }
        if (movement.currentGameMode != GameMode.Cube)
        {
            camera.position = Vector3.MoveTowards(transform.position, new Vector3(0, 2.5f, camera.position.z), 3f);
        }
        
    }

    /*
     * Å×½ºÆ®
     * ±êÇé
     * Á¶¾Æ¿ä
     * 
     * 
     * 
     * 1 2 3 4*/

}
