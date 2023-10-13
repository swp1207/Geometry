using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColliderCorner
{
    public Vector2 Topleft;
    public Vector2 Bottomleft;
    public Vector2 BottomRight;
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

public class PlayerControl : MonoBehaviour
{
    [Header("Raycast Collision")]
    [SerializeField] private LayerMask CollisionLayer;

    [Header("Raycast Count")]
    [SerializeField] private int HorizontalCount = 3;
    [SerializeField] private int VerticalCount = 3;

    //raycast 간격
    private float HorizontalSpacing;
    private float VerticalSpacing;


    [Header("Movement")]
    Rigidbody2D rb;
    MapMove mapMove;

    private readonly float SkinWidth = 0.015f;

    private Collider2D collider2D;
    private ColliderCorner colliderCorner;
    private ColliderChecker colliderChecker;


    public ColliderChecker IscollisionChecker => colliderChecker;
    public Transform Hittransform { get; private set; }



    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        rb = new Rigidbody2D();
        mapMove = new MapMove();

    }

    private void Update()
    {
        CalculateRayCastSpacing();
        UpdateColliderCorner();
        colliderChecker.Reset();
        UpdateMovement();
        if (colliderChecker.Up || colliderChecker.Down)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    //spacing 간격계산
    private void CalculateRayCastSpacing()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(SkinWidth * -2);

        HorizontalSpacing = bounds.size.y / (HorizontalCount - 1);
        VerticalSpacing = bounds.size.x / (VerticalCount - 1);

    }
    //collidercorner 위치 갱신 메소드
    private void UpdateColliderCorner()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(SkinWidth * -2);

        colliderCorner.Topleft = new Vector2(bounds.min.x, bounds.max.y);
        colliderCorner.Bottomleft = new Vector2(bounds.min.x, bounds.min.y);
        colliderCorner.BottomRight = new Vector2(bounds.max.x, bounds.min.y);

    }

    private void UpdateMovement()
    {
        Vector3 currentVelocity = (new Vector3(mapMove.SpeedValues[(int)mapMove.currentSpeed], rb.velocity.y, 0)) * Time.deltaTime;
        //좌우로 움직일때        
        if (mapMove.currentSpeed != 0)
        {
            //raycast쏘는 메소드
            RayCastHorizontal(ref currentVelocity);
        }
        if (rb.velocity.y != 0 )
        {
            //raycast쏘는 메소드
            RayCastVertical(ref currentVelocity);
        }
        transform.position += currentVelocity;
    }

    private void RayCastHorizontal(ref Vector3 velocity)
    {
        //ref란 내부 메소드에서 적용된 값을 변경
        float distance = Mathf.Abs(velocity.x) + SkinWidth; //광선길이
        Vector2 rayPosition = Vector2.zero;
        RaycastHit2D hit;
        for (int i = 0; i < HorizontalCount; ++i)
        {
            rayPosition = (mapMove.moveDirection == 1) ? colliderCorner.BottomRight : colliderCorner.Bottomleft;
            rayPosition += Vector2.up * (HorizontalSpacing * i);

            hit = Physics2D.Raycast(rayPosition,
                                    Vector2.right * mapMove.moveDirection,
                                    distance,
                                    CollisionLayer);
            if (hit)//null인지 아닌지
            {
                //x축 속력을 관선과 오브젝트 사이의 거리로 설정(거리가 0이면 속력도 0)
                velocity.x = (hit.distance * SkinWidth) * mapMove.moveDirection;
                //다음에 발사되는 광선의 거리설정
                distance = hit.distance;
                //현재 진행방향, 부딪힌 방향 정보를 true로 변경
                colliderChecker.Left = mapMove.moveDirection == -1;
                colliderChecker.Right = mapMove.moveDirection == 1;
            }
            Debug.DrawRay(rayPosition, rayPosition + Vector2.right * mapMove.moveDirection * distance, Color.blue);
        }
    }

    private void RayCastVertical(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.y);
        float distance = Mathf.Abs(velocity.y) + SkinWidth;
        Vector2 rayPosition = Vector2.zero;
        RaycastHit2D hit;
        for (int i = 0; i < VerticalCount; ++i)     //++i맞나
        {
            rayPosition = (direction == 1) ? colliderCorner.Topleft : colliderCorner.Bottomleft;
            rayPosition += Vector2.right * (VerticalSpacing * i + velocity.x); //x가 맞나
            hit = Physics2D.Raycast(rayPosition, Vector2.up * direction, distance, CollisionLayer);
            if (hit)
            {
                velocity.y = (hit.distance - SkinWidth) * direction;
                distance = hit.distance;
                colliderChecker.Down = (direction == -1);
                colliderChecker.Up = (direction == 1);

                Hittransform = hit.transform;
            }
            Debug.DrawRay(rayPosition, rayPosition + Vector2.up * direction * distance, Color.yellow);
        }
    }





}
