using Unity.MLAgents;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Paddle : Agent
{
    protected Rigidbody rigidbody { get; private set; }

    public float minDirection = 0.05f;

    public float speed = 8f;

    [Tooltip("Changes how the ball bounces off the paddle depending on where it hits the paddle. The further from the center of the paddle, the steeper the bounce angle.")]
    public bool useDynamicBounce = false;

    private void Awake()
    {
        base.Awake();

        rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.position = new Vector2(rigidbody.position.x, 0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("trigger ball");
            Vector3 newDir = (other.transform.position - transform.position).normalized;
            newDir.x = Mathf.Sign(newDir.x) * Mathf.Max(Mathf.Abs(newDir.x), this.minDirection);
            newDir.z = Mathf.Sign(newDir.z) * Mathf.Max(Mathf.Abs(newDir.z), this.minDirection);

            other.gameObject.GetComponent<Ball>().direction = newDir;
        }
    }


    /*
        private void OnCollisionEnter(Collision collision)
        {

            Debug.Log("Colision:" + collision.gameObject.tag);
            Debug.Log("colision contact:" + collision.GetContact(0).thisCollider.gameObject.tag);
            if (useDynamicBounce && collision.gameObject.CompareTag("Ball"))
            {
                Rigidbody ball = collision.rigidbody;
                Collider paddle = collision.GetContact(0).thisCollider;

                // Gather information about the collision
                Vector3 ballDirection = ball.velocity.normalized;
                Vector3 contactDistance = ball.transform.position - paddle.bounds.center;
                Vector3 surfaceNormal = collision.GetContact(0).normal;
                Debug.Log("surface normal: " + surfaceNormal);
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, surfaceNormal);
                Debug.Log("rotationAxis: " + rotationAxis);

                // Rotate the direction of the ball based on the contact distance
                // to make the gameplay more dynamic and interesting
                float maxBounceAngle = 75f;
                float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
                ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;

                Debug.Log("dir: " + ballDirection);

                // Re-apply the new direction to the ball
                ball.velocity = ballDirection * ball.velocity.magnitude * speed;
            }
        }
    */
}