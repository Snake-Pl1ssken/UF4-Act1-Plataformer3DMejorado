using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class Enemy : Entity, ITargeteable
{
    [SerializeField] float shootingDistance = 10f;
    [SerializeField] DecisionTreeNode decissionTreeRoot;


    NavMeshAgent agent;
    WeaponManager weaponManager;
    Orientator orientator;
    Sight sight;

    BaseState[] allState;

    BaseState currentState;

    DecisionTreeNode[] allDecisionTreeNode;


    //enum State
    //{ 
    //    Wandering,
    //    Chasing,
    //}

    //State state;

    HurtCollider hurtCollider;

    [Header("estados")]
    [SerializeField] BaseState chasingState;
    [SerializeField] BaseState notChasingState;
    [SerializeField] BaseState shootingState;



    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        hurtCollider = GetComponent<HurtCollider>();
        allState = GetComponents<BaseState>();
        foreach (BaseState s in allState)
        {
            s.Init(this);
        }

        weaponManager = GetComponentInChildren<WeaponManager>();

        orientator = GetComponent<Orientator>();
        sight = GetComponentInChildren<Sight>();

        allDecisionTreeNode = GetComponentsInChildren<DecisionTreeNode>();
        foreach (DecisionTreeNode node in allDecisionTreeNode)
        {
            node.SetEnemy(this);
        }
    }

    private void OnEnable()
    {
        hurtCollider.onHitRecived.AddListener(OnHitRecived);
    }

    private void Start()
    {
        //ChangeState(wanderingState);
        decissionTreeRoot.Execute();
    }


    Transform target;
    private void Update()
    {
        //Vector3 playerPosition = PlayerController.instance.transform.position;
        target = CheckSenses();
        decissionTreeRoot.Execute();
        UpdateAnimation();
    }



    private Transform CheckSenses()
    {
        ITargeteable targeteable = sight.GetClosestTarget();
        if(targeteable != null)
        {
            hasAlreadyVisitedTheLastTargetPosition = false;
            lastTargetPosition = targeteable.GetTransform().position;
        }
        return (targeteable != null) ? targeteable.GetTransform() : null;
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

    internal void ChangeStateTo(BaseState state)
    {
        if (currentState != state)
        {
            if (currentState != null) { currentState.enabled = false; }
            currentState = state;
            if (currentState != null) { currentState.enabled = true; }

        }
    }

    internal bool HasTarget()
    {
        return target != null;
    }
    public bool TargetIsInRange()
    {
        return (target != null) &&
            (Vector3.Distance(target.position, transform.position) < shootingDistance);
    }

    Vector3 lastTargetPosition;
    bool hasAlreadyVisitedTheLastTargetPosition = true;
    internal bool HasAlreadyVisitedTheLastTargetPosition()
    {
        return hasAlreadyVisitedTheLastTargetPosition;
    }

    internal Vector3 GetLastTargetPosition()
    {
        return lastTargetPosition;
    }

    internal void NotifyLastTargetPositionReached()
    {
        hasAlreadyVisitedTheLastTargetPosition = false;
    }


    #endregion

    [SerializeField] ITargeteable.Faction faction = ITargeteable.Faction.Enemy;

    public ITargeteable.Faction GetFaction()
    {
        return faction;
    }
    public Transform GetTransform() { return transform; }
}
