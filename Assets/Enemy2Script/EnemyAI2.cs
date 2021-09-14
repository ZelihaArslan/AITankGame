using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI2 : MonoBehaviour
{

    public Transform player;
    NavMeshAgent agent;
    public Transform[] wayPoints2;
    public Transform rayOrigin2;
    int currentWaypointIndex = 0;
    Animator fsm2;
    float shootFreq = 5f;
    Vector3[] wayPointsPoss = new Vector3[3];




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wayPoints2.Length; i++)
             wayPointsPoss[i] = wayPoints2[i].position;

        fsm2 = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPointsPoss[currentWaypointIndex]);

        StartCoroutine("CheckPlayer");
    }


    IEnumerator CheckPlayer()
    {
        CheckVisibility();
        CheckDistance();
        CheckDistanceFromCurrentWaypoint(); 
        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }

    private void CheckDistanceFromCurrentWaypoint()
    {
        float distanceFromWP = Vector3.Distance(wayPointsPoss[currentWaypointIndex], transform.position);
        //(player.position - transform.position).magnitude;

        fsm2.SetFloat("distanceFromCurrentWaypoint", distanceFromWP);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        //(player.position - transform.position).magnitude;
         
        fsm2.SetFloat("dis", distance);
    }
    private void CheckVisibility()
    {
        float maxDistance = 20;
        Vector3 direction = (player.position - transform.position) / (player.position - transform.position).magnitude;
        //Vector3 direction = (player.position -rayOrigin.position).normalized;
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);


        if (Physics.Raycast(transform.position, direction, out RaycastHit info, maxDistance))
        {
            if (info.transform.CompareTag("Player")) //ışının çartıgı yer playersa
                fsm2.SetBool("isVisiblee", true);

            else
                fsm2.SetBool("isVisiblee", false);
        }
        else
            fsm2.SetBool("isVisiblee", false);
    }


    public void SetLookrotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(dir); //verdiğin yöne göre rotasyon belirtiyor
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f); //0.2 sn de bir dönme yapıyor . birden dönmesini engellemek için
    }

    public void Shoot()
    {
        GetComponent<ShootBehaviour>().Shoot(shootFreq); //yeni obje uzerinden shoot metodunu calıştır.
    }

    public void Patrol()
    {


    }

    public void Chase()
    {

        agent.SetDestination(player.position);
    }


    public void SetNewWaypoint()// üç nokta arasında devriye gezer
    {
        switch (currentWaypointIndex)
        {
            case 0:
                currentWaypointIndex = 1;
                break;
            case 1:
                currentWaypointIndex = 2;
                break;
            case 2:
                currentWaypointIndex = 0;
                break;
        }
        agent.SetDestination(wayPointsPoss[currentWaypointIndex]);
    }
}
