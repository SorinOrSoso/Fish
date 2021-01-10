using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public float maxSpeed = 5;
    private Vector3 newTargetPosition = Vector3.zero;
    GameObject hunterObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = maxSpeed * Time.deltaTime; // calculate distance to move
        hunterObject = FindClosestEnemy(6);
        if (hunterObject != null)
        {
            if (Vector3.Distance(transform.position, hunterObject.transform.position) < 6f || newTargetPosition == Vector3.zero)
        {
            print(hunterObject.name);
            newTargetPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
        }
        }
        
            transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, step * 16);

    }

    public GameObject FindClosestEnemy(float dist)
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
