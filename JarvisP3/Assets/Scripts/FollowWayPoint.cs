using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10.0f;
    public float rotspeed = 10.0f;

    GameObject tracker;
    void Start()
    {
      tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
      DestroyImmediate(tracker.GetComponent<Collider>());
      tracker.transform.position = this.transform.position;
      tracker.transform.rotation = this.transform.rotation;
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTracker();
       
        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotspeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
