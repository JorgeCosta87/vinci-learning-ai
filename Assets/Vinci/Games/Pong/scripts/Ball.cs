using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public new Rigidbody rb { get; private set; }

    public float baseSpeed = 5f;
    public float maxSpeed = Mathf.Infinity;
    public float currentSpeed { get; set; }

    public Vector3 direction;

    public bool hitWat = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void Start()
    {
        AddStartingForce(true);
    }

    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
    }

    public void AddStartingForce(bool startBottom)
    {
        // Flip a coin to determine if the ball starts left or right
        float z = startBottom ? -1f : 1f;

        // Flip a coin to determine if the ball goes up or down. Set the range
        // between 0.5 -> 1.0 to ensure it does not move completely horizontal.
        float x = Random.value < 0.5f ? Random.Range(-1f, -0.5f)
                                      : Random.Range(0.5f, 1f);

        // Apply the initial force and set the current speed
        direction = new Vector3(x, 0, z).normalized;
        rb.AddForce(direction * baseSpeed * 2f, ForceMode.Impulse);
        currentSpeed = baseSpeed;
    }

    private void FixedUpdate()
    {
        // Clamp the velocity of the ball to the max speed
       // Vector3 direction = rb.velocity.normalized;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        rb.velocity = direction * currentSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            direction.x = -direction.x;
        }
    }
}