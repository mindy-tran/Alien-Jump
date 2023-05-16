using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // https://www.google.com/search?q=moving+platforms+unity+3d&rlz=1C1CHBF_enUS919US919&oq=moving+platforms&aqs=chrome.6.69i57j0i512l6j69i61.11181j0j7&sourceid=chrome&ie=UTF-8#kpvalbx=_f_ljZJv8D6az0PEPgJaPoA0_34
    public Transform GetWaypoint(int waypointIndex){
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex){
        int nextWaypointIndex = currentWaypointIndex + 1;
        if (nextWaypointIndex == transform.childCount){
            // start the loop over
            nextWaypointIndex = 0;
        }
        return nextWaypointIndex;
    }

}
