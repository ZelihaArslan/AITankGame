using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{ 
    public Transform player;
    NavMeshAgent agent;
    public Transform[] wayPoints;
    public Transform rayOrigin;
    int currentWayPointIndex = 0;
    Animator fsm;
    float shootFreq = 5f; 
    Vector3[] wayPointsPos = new Vector3[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wayPoints.Length; i++) 
            wayPointsPos[i] = wayPoints[i].position;

        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPointsPos[currentWayPointIndex]);

        StartCoroutine("CheckPlayer");
    }


    IEnumerator CheckPlayer()
    {
        CheckVisibility();
        CheckDistance();
        CheckDistanceFromCurrentWayPoint();
        yield return new WaitForSeconds(0.1f);
        yield return CheckPlayer();
    }

    private void CheckDistanceFromCurrentWayPoint()
    {
        float distanceFromWP = Vector3.Distance(wayPointsPos[currentWayPointIndex]  , rayOrigin.position);
        //(player.position - transform.position).magnitude;

        fsm.SetFloat("distanceFromCurrentWayPoint", distanceFromWP);
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, rayOrigin.position);
        //(player.position - transform.position).magnitude;

        fsm.SetFloat("distance", distance);
    }
    private void CheckVisibility()
    {
        float maxDistance = 20;
        //Vector3 direction = (player.position - transform.position) / (player.position - transform.position).magnitude;
        Vector3 direction = (player.position - transform.position).normalized;
        Debug.DrawRay(rayOrigin.position, direction * maxDistance, Color.red);
       

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit info, maxDistance))
        {
            if (info.transform.tag == "Player") //ışının çartıgı yer playersa
                fsm.SetBool("isVisible", true);

            else
                fsm.SetBool("isVisible", false);
        }
        else
            fsm.SetBool("isVisible", false);
    }


    public void SetLookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(dir); //verdiğin yöne göre rotasyon belirtiyor
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,0.2f); //0.2 sn de bir dönme yapıyor . birden dönmesini engellemek için
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


    public void SetNewWayPoint()// üç nokta arasında devriye gezer
    {
        switch (currentWayPointIndex)
        {
            case 0:
                currentWayPointIndex = 1;
                break;
            case 1:
                currentWayPointIndex = 2;
                break;
            case 2:
                currentWayPointIndex = 0;
                break;
        }
        agent.SetDestination(wayPointsPos[currentWayPointIndex]);
    }
}