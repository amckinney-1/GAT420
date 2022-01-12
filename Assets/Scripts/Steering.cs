using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] [Range(0, 45)] float wanderDisplacement = 5;
    [SerializeField] [Range(0, 5)] float wanderRadius = 3;
    [SerializeField] [Range(0, 5)] float wanderDistance = 1;

    float wanderAngle = 0;

    public Vector3 Seek(AutonomousAgent agent, GameObject target)
    {
        return CalculateSteering(agent, target.transform.position - agent.transform.position);
    }

    public Vector3 Flee(AutonomousAgent agent, GameObject target)
    {
        return CalculateSteering(agent, agent.transform.position - target.transform.position);
    }

    public Vector3 Wander(AutonomousAgent agent)
    {
        wanderAngle += Random.Range(-wanderDisplacement, wanderDisplacement);
        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);

        Vector3 point = rotation * (Vector3.forward * wanderRadius);
        Vector3 forward = agent.transform.forward * wanderDistance;

        return CalculateSteering(agent, forward + point);
    }

    Vector3 CalculateSteering(AutonomousAgent agent, Vector3 vector)
    {
        Vector3 direction = vector.normalized;
        Vector3 desired = direction * agent.maxSpeed;
        Vector3 steer = desired - agent.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

        return force;
    }
}
