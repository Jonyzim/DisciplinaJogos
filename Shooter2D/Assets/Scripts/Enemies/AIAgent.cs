using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AIAgent : Agent
{
    public Vector3 StartPosition;
    [SerializeField] private Transform target;
    [SerializeField] private float movespeed;
    [SerializeField] private float timer;
    private float _curTimer;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = StartPosition;
        _curTimer = timer;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * movespeed;
    }

    private void Update()
    {
        _curTimer -= Time.deltaTime;
        if (_curTimer <= 0)
        {
            float distance = Vector3.Distance(transform.localPosition, target.localPosition);
            if (distance < 5)
            {
                SetReward(3);
            }
            else if (distance < 10)
            {
                SetReward(2);
            }
            else if (distance < 15)
            {
                SetReward(1);
            }
            else
            {
                SetReward(0);
            }
            EndEpisode();
        }
    }
}
