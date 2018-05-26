using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour {

    private Transform target;
    private float nextTimeToSearch = -1f;
    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;
    

    public float updateRate = 2f;
    public Path path;
    public float speed = 300f;
    public ForceMode2D fMode;
    public float nextWaypointDistance = 3f;

    [HideInInspector]
    public bool pathIsEnded = false;

    private void Start()
    {
        FindPlayer();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            Debug.LogError("No player found");
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath ()
    {
        if (target == null)
        {
            yield return false;
        }
        
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete (Path p)
    {
        Debug.Log("We got a path. Dit it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            //TODO: blep
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached.");

            pathIsEnded = true;
            return;
        }

        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                target = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.3f;
        }
    }
}
