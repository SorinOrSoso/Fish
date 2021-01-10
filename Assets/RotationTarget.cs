using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    private float parentSpeed;
    float speedWobble = 10.0f;

    Controller parentController;
    void Start()
    {
        if (transform.parent.gameObject.GetComponent<Controller>()) { }
        parentController = transform.parent.gameObject.GetComponent<Controller>();

    }
    // Update is called once per frame
    void Update()
    {
        parentSpeed = parentController.speed;
        speedWobble = Mathf.Lerp(0.01f, parentSpeed, .5f);
        //print("maxSpeed " + parentSpeed);
        print("speedWobble " + speedWobble);
        transform.localPosition = new Vector3(transform.localPosition.z + Mathf.PingPong(Time.time * speedWobble, 1f), transform.localPosition.y, transform.localPosition.z);
    }
}
