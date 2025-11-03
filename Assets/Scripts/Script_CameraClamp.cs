using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CameraClamp : MonoBehaviour
{
    public Transform targetToFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -10.5f, 10.5f),
            transform.position.y,
            Mathf.Clamp(targetToFollow.position.z, -15.5f, 15f));  
    }
}
