using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Player : MonoBehaviour
{
    float PlayerSpeed = 7;
    bool stopRight;
    bool stopLeft;
    bool stopUp;
    bool stopDown;
    bool disabled;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Scripts_Guard.OnPlayerSpotted += OnDisable;
        Scirpts_RadialTrigger.OnPlayerOverlapp += OnDisable;

    }

    // Update is called once per frame
    void Update()
    {

        if (!disabled)
        {



            if (!stopRight)
            {
                  if (Input.GetKey("right"))
                  {
                    transform.position += Vector3.right * PlayerSpeed * Time.deltaTime;
                  }
            }
            
            if (!stopLeft)
            {
                if (Input.GetKey("left"))
                {
                   transform.position -= Vector3.right * PlayerSpeed * Time.deltaTime;
                }
            }

            if (!stopUp)
            {
                if (Input.GetKey("up"))
                {
                   transform.position += Vector3.forward * PlayerSpeed * Time.deltaTime;
                }
            }

            if (!stopDown)
            {
               if (Input.GetKey("down"))
               {
                  transform.position -= Vector3.forward * PlayerSpeed * Time.deltaTime;
               }
            }
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -18f, 18f), transform.position.y,
            Mathf.Clamp(transform.position.z, -18.7f, 18f)
            );
    }

    private void OnDisable()
    {
        disabled = true;

    }

    private void FixedUpdate()
    {
        stopRight = Physics.Raycast(transform.position, Vector3.right, .67f);
        stopLeft = Physics.Raycast(transform.position, Vector3.left, .67f);
        stopUp = Physics.Raycast(transform.position, Vector3.forward, .67f); ;
        stopDown = Physics.Raycast(transform.position, Vector3.back, .67f); ;
    }

    private void OnDestroy()
    {
        Scripts_Guard.OnPlayerSpotted -= OnDisable;
    }

}
