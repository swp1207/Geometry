using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    public GameMode gameMode;
    public Speeds speed;
    public bool gravity;
    public int State;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    try
    //    {
    //        Movement movement = collision.gameObject.GetComponent<Movement>();

    //        movement.ChangeThroughtPortal(gameMode, speed, gravity ? 1 : -1, State);

    //    }
    //    catch (System.Exception)
    //    {

    //        throw;
    //    }
    //}

    public void InitiatePortal(Movement movement)
    {
        movement.ChangeThroughtPortal(gameMode, speed, gravity ? 1 : -1, State);
    }

}
