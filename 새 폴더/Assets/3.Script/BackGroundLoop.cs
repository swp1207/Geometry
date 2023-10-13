using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    [SerializeField] private float speed;
    private float width;

    void Start()
    {
        width = transform.GetComponent<BoxCollider2D>().size.x;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameover)
        {
            transform.Translate(Vector2.zero * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= -width * 1.25f)
            {
                Vector2 offset = new Vector2(width * 1.25f * 2f, 0);
                transform.position = (Vector2)transform.position + offset;
            }
        }
        
    }

}
