using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Door : MonoBehaviour
{

    Scripts_Clee clee;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        clee = key.GetComponent<Scripts_Clee>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
