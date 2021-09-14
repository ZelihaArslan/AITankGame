using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch :Sense
{
    public override void Initialize()
    {

    }

    public override void UpdateSense()
    {
        if (aspect)
        {
            Debug.Log(aspect.aspectType + " has touched");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        aspect = other.GetComponent<Aspect>();
    }

    private void OnTriggerExit(Collider other)
    {
        aspect = null;
    }
}
