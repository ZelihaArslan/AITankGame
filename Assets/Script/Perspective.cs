using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public Transform target;
    public float fieldofView;
    LineRenderer lr;

    public override void Initialize()
    {
        fieldofView = 60;
        
    }

    public override void UpdateSense()
    {
            float angle = GetAngle();
            Vector3 dir = (target.position - transform.position).normalized;
            if (angle < fieldofView / 2f)
            {
               
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit))
            {
                aspect = hit.transform.GetComponent<Aspect>();
                if (aspect)
                    Debug.Log(aspect.aspectType + " has been detected!");
            }
        } 
        
    }
       

    private float GetAngle()
    {
        //gorus alanani girdiyse isin gonder.
        //isin ile HitInfo kullanarak hedefin tipine ogren.

        Vector3 dir = target.position - transform.position;
        float cosAngle = Vector3.Dot(transform.forward, dir) / (transform.forward.magnitude * dir.magnitude);

        float angle = Mathf.Acos(cosAngle);
        return angle * Mathf.Rad2Deg;
    }
}
