using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    [HideInInspector]
    public Vector3 desiredVelocity;
    public Transform target;
    [HideInInspector]
    public float speed = .2f;
    public float maxSpeed = 40f;

    private new Rigidbody rigidbody;


    float friendDistance = 3;
    GameObject Friend;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        desiredVelocity = Vector3.zero;
        speed = Random.Range(.2f, .4f);
    }

    void Update()
    {

        Friend = FindClosestFriend(friendDistance);

        desiredVelocity = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float acc = Mathf.Lerp(0, desiredVelocity.sqrMagnitude, .05f);

        //print("desiredVelocity.sqrMagnitude " + acc);

        speed += acc;
        //print("speed " + speed);

        float singleStep = speed * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, desiredVelocity, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, singleStep);

    }
    public GameObject FindClosestFriend(float dist)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Hunter");
        GameObject closest = null;
        float distance = dist;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}