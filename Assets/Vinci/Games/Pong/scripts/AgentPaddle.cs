using UnityEngine;

public class AgentPaddle : Paddle 
{
    public Vector2 direction { get; private set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direction.sqrMagnitude != 0)
        {
            rigidbody.AddForce(direction * speed);
        }
    }
}