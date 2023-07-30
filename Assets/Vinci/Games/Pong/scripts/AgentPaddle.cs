using System;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentPaddle : Paddle 
{

    public Transform ball;
    public Vector2 direction { get; private set; }

    public event Action episodeBegin;

    public override void OnEpisodeBegin()
    {
        Debug.Log("Begin Episode!");
        episodeBegin?.Invoke();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(ball.position);
    }

    public override void OnActionReceived(ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        rigidbody.velocity = 
            new Vector3(continuousActionsOut[0],rigidbody.position.y, rigidbody.position.z) * speed;


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {   
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    }
/*
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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
    */
}