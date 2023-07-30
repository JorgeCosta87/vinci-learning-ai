using UnityEngine;

public class BasicAiPaddle : Paddle
{
    [SerializeField]
    private Rigidbody ball;
    private Vector3 startPos;

    private float centerTolerance = 0.1f;

    void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        // Check if the ball is moving towards the paddle (positive x velocity)
        // or away from the paddle (negative x velocity)
        if (ball.velocity.z > 0f)
        {
           // Debug.Log(ball.velocity.z);
           // Debug.Log(ball.position.x);
           // rigidbody.MovePosition(new Vector3(ball.position.x, 0, 0));

            if (ball.position.x > rigidbody.position.x) 
            {
                rigidbody.AddForce(Vector2.right * speed);
            } 
            else if (ball.position.x < rigidbody.position.x) 
            {
                rigidbody.AddForce(Vector2.left * speed);
            }
        }
        else
        {
            if(rigidbody.position.x - startPos.x < centerTolerance)
            {
                return;
            }

            if (rigidbody.position.x > 0f) 
            {
                rigidbody.AddForce(Vector2.left * speed);
            } 
            else if (rigidbody.position.x < 0f)
            {
                rigidbody.AddForce(Vector2.right * speed);
            }

        }
    }
}