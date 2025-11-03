using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scirpts_RadialTrigger : MonoBehaviour
{

    public static event System.Action OnPlayerOverlapp;

    public Transform Player;
    public float triggerDistance = 1.2f;

    void Start()
    {
    }


    void Update()
    {

        Vector3 endPosition = transform.position;
        Vector3 PlayerPosition = Player.position;

        float distance = Vector3.Distance(endPosition, PlayerPosition);

        if (distance < triggerDistance)
        {
            OnPlayerOverlapp();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Player.position, transform.position);
    }

}
