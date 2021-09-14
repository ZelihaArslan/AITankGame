using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string moveAxisName = "Vertical";
    string turnAxisName = "Horizontal";
    float moveSpeed = 10f;
    float turnSpeed = 240f;
    float moveAxis;
    float turnAxis;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAxis = Input.GetAxis(moveAxisName); //w ve s ye 1 -1 degeri atar verecegim yone göre deger atar
        turnAxis = Input.GetAxis(turnAxisName); // d ve a ya 1 yünde kullanıcaz -1 atar
        if(Input.GetKeyDown(KeyCode.Mouse0 ))
        {
            GetComponent<ShootBehaviour>().Shoot();
        }


    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * moveAxis * moveSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.Euler(0, turnAxis * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(transform.rotation * rotation);
    }
}
