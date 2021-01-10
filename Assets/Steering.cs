using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public float Mass = 15;
    public float MaxVelocity = 3;
    public float MaxForce = 15;

    private Vector3 velocity;
    private Vector3 dummyTarget;
    private Transform target;
    private string targetName;
    private float distanceToObstacle;
    private float speed = 2;
    private readonly float radius = 2.0f;
    private readonly float damping = 10;

    private void Start()
    {
        velocity = Vector3.zero;
        dummyTarget = Vector3.zero;
    }

    private void Update()
    {
        RaycastHit hit;
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, 6))
        {
            distanceToObstacle = hit.distance;
            targetName = hit.collider.gameObject.tag;
            target = hit.collider.transform;

            dummyTarget = Vector3.zero;
            Debug.Log("DISTANCE Physics.SphereCast " + distanceToObstacle);
        }
        else
        {
            targetName = "";                    
        }

        // go to
        if (targetName.Equals("Target")) {
                Vector3 desiredVelocity = target.position - transform.position;
                desiredVelocity = desiredVelocity.normalized * MaxVelocity;

                Vector3 steering = desiredVelocity - velocity;
                steering = Vector3.ClampMagnitude(steering, MaxForce);
                steering /= Mass;

                velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
                transform.position += velocity * Time.deltaTime;
                transform.forward = velocity.normalized;

                Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
                Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);

            if (distanceToObstacle < 0.01f)
            {
                Debug.Log("DESTROY " + target.gameObject.name);
                Destroy(target.gameObject);
                //targetName = "";               
            }
        } 
        else
        {           
            if (distanceToObstacle < 0.01f )
            {
                int range = 10;
                Vector3 dir = Vector3.forward;

                if (dummyTarget.x > range  || dummyTarget.x == 0)
                {
                    dir *= Random.Range(-range, -1);
                }
                if ( dummyTarget.x < -range || dummyTarget.x == 0)
                {
                    dir *= Random.Range(1, range);
                }
                if (dummyTarget.y > range || dummyTarget.y < 0)
                {
                    dir *= Random.Range(-range, -1);
                }
                if (dummyTarget.z > range || dummyTarget.z == 0)
                {
                    dir *= Random.Range(-range, -1);
                }
                if (dummyTarget.z < -range || dummyTarget.z == 0)
                {
                    dir *= Random.Range(1, range);
                }


                //dummyTarget = new Vector3(x, y, z);
                dummyTarget = transform.TransformPoint(dir);
                Debug.Log("dummyTarget pos IN " + dummyTarget);
            }

            Debug.Log("dummyTarget pos OUT " + dummyTarget);
            distanceToObstacle = Vector3.Distance(dummyTarget, transform.position);

            Debug.Log("DISTANCE " + distanceToObstacle);
            Vector3 targetDirection = dummyTarget - transform.position;
            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.5f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, dummyTarget, step);

           //targetDirection = targetDirection.normalized * MaxVelocity;

            Vector3 steering = targetDirection - velocity;
            steering = Vector3.ClampMagnitude(steering, MaxForce);
            steering /= Mass;

            velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
            transform.position += velocity * Time.deltaTime;
            transform.forward = velocity.normalized;

            Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
            Debug.DrawRay(transform.position, targetDirection.normalized * 2, Color.magenta);
        }
       

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
