using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds
{
    Slow = 0,
    Normal,
    Fast,
    Faster,
    Fastest,
}

public class MapMove : MonoBehaviour
{
    public Speeds currentSpeed;
    static public bool Isright = true;
    public float moveDirection = Isright ? 1 : -1;

    //                      slow, normal, fast, faster, fastest
    public float[] SpeedValues = { 9.6f, 10.9f, 12.96f, 15.6f, 19.27f };

    private void Update()
    {
        if (GameManager.Instance.isGameover)
        {
            transform.Translate(Vector2.zero*Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * SpeedValues[(int)currentSpeed] * moveDirection * Time.deltaTime);
            //transform.position -= Vector3.right * SpeedValues[(int)currentSpeed] * moveDirection * Time.deltaTime;
        }


    }

}
