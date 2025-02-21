using System;
using Unity.VisualScripting;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    public enum State
    {
        Wandering,
        Chasing
    }

    Vector3 wanderPosition;
    Vector2 wanderTEST;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //wanderPosition =
        //wanderTEST = Random.insideUnitCircle * 5;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (State)
        //{
        //    case State.Wandering:
        //        UpdateWandering();
        //        break;
        //    case State.Chasing:
        //        UpdateChasing();
        //        break;
        //}
    }


    private void UpdateWandering()
    {
    }
    //private void UpdateChasing()
    //{
    //    throw new NotImplementedException();
    //}
}
