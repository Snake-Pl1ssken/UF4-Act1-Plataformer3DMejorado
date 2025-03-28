using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] float detectionDistance = 20f;
    [SerializeField] float shootingDistance = 10f;

    //[SerializeField] float detectionDistance = 3f;

    NavMeshAgent agent;
    WeaponManager weaponManager;
    Orientator orientator;
    Vector3 homeOrigin;
    Vector3 wanderPosition;

    //enum State
    //{ 
    //    Wandering,
    //    Chasing,
    //}

    //State state;

    HurtCollider hurtCollider;
    Sight sight;
    BaseState[] allState;

    [Header("estados")]
    [SerializeField] BaseState chasingState;
    [SerializeField] BaseState notChasingState;
    [SerializeField] BaseState shootingState;

    BaseState currentState;

    protected override void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();
        
        agent = GetComponent<NavMeshAgent>();

        allState = GetComponents<BaseState>();

        weaponManager = GetComponentInChildren<WeaponManager>();

        sight = GetComponentInChildren<Sight>();
        orientator = GetComponent<Orientator>();
        foreach (BaseState s in allState)
        {
            s.Init(this);
        }

    }

    private void OnEnable()
    {
        hurtCollider.onHitRecived.AddListener(OnHitRecived);
    }

    private void Start()
    {
        //ChangeState(wanderingState);
    }


    Transform target;
    private void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        target = CheckSenses();
        //ejecutar sentidos


        //Toma de decisiones
        if (target == null)
        {
            ChangeState(notChasingState);
        }
        else if (TargetIsInRange())
        {
            ChangeState(shootingState);
        }
        else
        {
            ChangeState(chasingState);
        }

        //Ejecucion de estados

        UpdateAnimation();
    }

    void ChangeState(BaseState newState)
    {
        if (currentState != newState)
        {
            if (currentState != null) { currentState.enabled = false; }
            currentState = newState;
            if (currentState != null) { currentState.enabled = true; } 
            
        }
    }

    private Transform CheckSenses()
    {
        ITargeteable targeteable = sight.GetClosestTarget();
        return (targeteable != null) ? targeteable.GetTransform() : null;
    }

    bool TargetIsInRange()
    {
        return (target != null) &&
            (Vector3.Distance(target.position, transform.position) < shootingDistance);
    }

    private void OnDisable()
    {
        hurtCollider.onHitRecived.RemoveListener(OnHitRecived);
    }

    private void OnHitRecived(IHitter agressor, HurtCollider victim)
    {
        if(agressor.GetTransform())  //nose si funciona
        {
            Debug.Log("Muerte Enemigo");
            this.gameObject.SetActive(false);
        }
    }

    #region Entity implementation


    protected override float GetCurrentVerticalSpeed()
    {
        return 0f;
    }

    protected override float GetJumpSpeed()
    {
        return 0f;
    }

    protected override bool IsRunning()
    {
        return false;
    }

    protected override bool IsGrounded()
    {
        return true;
    }

    protected override Vector3 GetLastNormalizedVelocity()
    {
        return agent.velocity.normalized;
    }

    #endregion


    #region AI Getters
    internal NavMeshAgent GetAgent()
    { 
        return agent;
    }

    internal Transform GetTarget()
    {
        return target;
    }

    internal WeaponManager GetWeaponManager() { return weaponManager; }

    internal Orientator GetOrientator()
    {
        return orientator;
    }
    #endregion
}
