using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private WaypointPath wPath;

    [SerializeField]
    private float speed;

    private int targetWaypointIndex;
    

    private Transform prevWaypoint;
    private Transform targetWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint(); // set firstway point to prev and second to target
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        //LERP to move nicely between the two waypoints
        float elapsedPecent = elapsedTime / timeToWaypoint; // get the percentage
        transform.position = Vector3.Lerp(prevWaypoint.position, targetWaypoint.position, elapsedPecent);

        //we have reached the targetWaypoint, time to go to the next
        if (elapsedPecent >= 1){
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint(){
        // we are updating to go to the next waypoint

        //update the previous and target waypoints
        prevWaypoint = wPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = wPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = wPath.GetWaypoint(targetWaypointIndex);

        //set time back to 0
        elapsedTime = 0;

        //get the distance to the next waypoint and calc how long to get there
        float distanceToWaypoint = Vector3.Distance(prevWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }

    private void OnTriggerEnter(Collider other){
        // do this so the character will stick to the platform
        //print(other.transform.lossyScale);

        other.transform.SetParent(transform);
        
        //print(other.transform.localRotation);
        //print(other.transform.lossyScale);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);
        other.transform.localScale = new Vector3(1, 1, 1);

    }
    
}
