using UnityEngine;

static public class Genetic
{
    static public void LimitYVelocity(float limit, Rigidbody2D rb)
    {
        int gravityMultiplier = (int)(Mathf.Abs(rb.velocity.y) / rb.velocity.y);
        if (rb.velocity.y * -gravityMultiplier > limit )
        {
            rb.velocity = Vector2.up * -limit * gravityMultiplier;
        }        
    }

    static public void CreateGameMode(Rigidbody2D rb, Movement movement, bool IsGroundRequired, float initalVelocity, float gravityScale, bool canHold = false, bool flipOnClick = false, float rotationMode = 0, float yVelocityLimit = Mathf.Infinity)
    {
        if ((!Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) || canHold && movement.IsGround())
        {
            movement.clickProcessed = false;
        }


        rb.gravityScale = gravityScale * movement.Gravity;
        LimitYVelocity(yVelocityLimit, rb);

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            if (movement.IsGround() && !movement.clickProcessed || !IsGroundRequired && !movement.clickProcessed)
            {
                movement.clickProcessed = true;
                rb.velocity = Vector2.up * initalVelocity * movement.Gravity;
                movement.Gravity *= flipOnClick ? -1 : 1;
            }
            movement.Sprite.Rotate(Vector3.back, rotationMode * Time.deltaTime * movement.Gravity);
        }
        else if (movement.IsGround() || !IsGroundRequired)
        {
            movement.Sprite.rotation = Quaternion.Euler(0, 0, Mathf.Round(movement.Sprite.rotation.z % 90) * 90);
        }
        else
        {
            movement.Sprite.Rotate(Vector3.back, rotationMode * Time.deltaTime * movement.Gravity);
        }


    }


}
