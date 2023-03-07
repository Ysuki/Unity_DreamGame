using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepManager : MonoBehaviour
{

    public Transform creepRoute;



    Statistics statistics;

    public enum State { Walk, Attack };

    public State state;


    public Transform creepEnemy;

    public const float idleWaitTime = 3f;
    public const float patrolWaitTime = 5f;

    public NavMeshAgent navMesh;


    Vector3 destination;

    private void Start()
    {
        //ChangeState(state);

        statistics = GetComponent<Statistics>();
        //navMesh = GetComponent<NavMeshAgent>();

    }


    private void FixedUpdate()
    {
        StateManager();
    }

    void StateManager()
    {


        switch (state)
        {
            case State.Attack:

                destination = creepEnemy.position;
                navMesh.destination = destination;
                break;
            case State.Walk:

                destination = creepRoute.position;
                navMesh.destination = destination;
                break;
        }


    }



    void ChangeState(State newState)
    {
        StopAllCoroutines();
        state = newState;

        switch (newState)
        {
            case State.Attack:

                destination = creepRoute.position;
                navMesh.destination = destination;


                StartCoroutine("Attack");
                break;
            case State.Walk:


                destination = creepEnemy.position;
                navMesh.destination = destination;

                StartCoroutine("Walk");

                break;
        }


    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(idleWaitTime);

    }

    IEnumerator Walk()
    {
        yield return new WaitForSeconds(idleWaitTime);

    }



    int Rand()
    {
        int rand = Random.Range(0, 100);
        return rand;
    }



}
