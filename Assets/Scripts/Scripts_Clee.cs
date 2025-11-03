using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Clee : MonoBehaviour
{
    public static event Action OnPickedUp;

    public Transform Player;
    public float triggerDistance = 1.2f;
    public GameObject Door;

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
            OnPickedUp?.Invoke();
            Destroy(Door);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Player.position, transform.position);
    }
}
