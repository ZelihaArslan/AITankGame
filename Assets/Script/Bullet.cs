using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10); //bir yere carpmıyorsa kendi kendine yok etsin
    }
    private void OnCollisionEnter(Collision collision)
    {
        HealthBehaviour healthBehaviour = collision.gameObject.GetComponent<HealthBehaviour>();
     

        if (healthBehaviour !=null)
        {
            healthBehaviour.TakeDamage(20);  //%20 sağlık degeri gider.
        }
        Destroy(gameObject, 1);

       
    }
}
 