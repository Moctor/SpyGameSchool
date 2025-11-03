using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Scripts_Guard : MonoBehaviour
{
	public static event System.Action OnPlayerSpotted;

	public float speed = 5;
	public float waitTime = .3f;
	public float turnSpeed = 90;
	public float timeToSpotPlayer = .5f;

	public Light spotlight;
	public float viewDistance;
	public LayerMask viewMask;

	float playerVisibleTimer;
	float viewAngle;

    public Transform Player;
    public float triggerDistance = 1.2f;

    public Transform pathHolder;
	Transform player;
	Color originialSpotlightColor;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		viewAngle = spotlight.spotAngle;
		originialSpotlightColor = spotlight.color;

		Vector3[] waypoints = new Vector3[pathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints[i] = pathHolder.GetChild (i).position;
			waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
		}
		StartCoroutine(FollowPath(waypoints));
	}

    private void Update()
    {
        Vector3 endPosition = transform.position;
        Vector3 PlayerPosition = Player.position;

        float distance = Vector3.Distance(endPosition, PlayerPosition);

        if (distance < triggerDistance)
			{
				OnPlayerSpotted();
			}

        if (CanSeePlayer())
		{
			playerVisibleTimer += Time.deltaTime;
		}
		else
		{
			playerVisibleTimer -= Time.deltaTime;
		}
		playerVisibleTimer = Mathf.Clamp(playerVisibleTimer,0, timeToSpotPlayer);
		spotlight.color = Color.Lerp(originialSpotlightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

		if(playerVisibleTimer >= timeToSpotPlayer)
		{
			if( OnPlayerSpotted != null )
			{
				OnPlayerSpotted();
			}
		}
    }

    bool CanSeePlayer()
	{
		if(Vector3.Distance(transform.position, player.position) < viewDistance)
		{
			Vector3 dirToPlayer = (player.position - transform.position).normalized;
			float angleBetweenGuardAndPlayer = Vector3.Angle (transform.forward, dirToPlayer);
			if(angleBetweenGuardAndPlayer < viewAngle / 2f)
			{
				if(!Physics.Linecast(transform.position,player.position, viewMask))
				{
					return true;
				}
			}
		}
		return false;
	}

    IEnumerator FollowPath(Vector3[] waypoints)
	{
		transform.position = waypoints[0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints[targetWaypointIndex];
		transform.LookAt (targetWaypoint);

		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, targetWaypoint, speed * Time.deltaTime);

			if (transform.position == targetWaypoint) {
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints[targetWaypointIndex];
				yield return new WaitForSeconds (waitTime);
				yield return StartCoroutine(TurnToFace(targetWaypoint));
			}
			yield return null;
		}
	}

	IEnumerator TurnToFace(Vector3 lookTarget)
	{
		Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
		float targetAngle = 90-Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

		while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) >0.05)
		{
			float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
			transform.eulerAngles = Vector3.up * angle;
			yield return null;
		}
	}

	void OnDrawGizmos() 
	{
		Vector3 startPosition = pathHolder.GetChild(0).position;
		Vector3 previousPosition = startPosition;

		foreach	(Transform waypoint in pathHolder){
			Gizmos.DrawSphere(waypoint.position, .3f);
			Gizmos.DrawLine(previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);

        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Player.position, transform.position);
    }

}
