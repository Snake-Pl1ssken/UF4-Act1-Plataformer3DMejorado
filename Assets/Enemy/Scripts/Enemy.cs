using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] float wanderinfRadius = 5f;
    [SerializeField] float reachingDistance = 1.5f;

    [SerializeField] float detectionDistance = 3f;

    NavMeshAgent agent;
    
    Vector3 homeOrigin;
    Vector3 wanderPosition;

    HurtCollider hurtCollider;

    protected override void Awake()
    {
        base.Awake();
        hurtCollider = GetComponent<HurtCollider>();
        
        agent = GetComponent<NavMeshAgent>();
        
        homeOrigin = transform.position;
        SelectWanderPosition();
    }

    private void OnEnable()
    {
        hurtCollider.onHitRecived.AddListener(OnHitRecived);
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;

        if (PlayerController.instance.gameObject.activeSelf && 
            (Vector3.Distance(playerPosition, transform.position) < detectionDistance))
        {
            agent.SetDestination(playerPosition);
        }
        else 
        {
            agent.SetDestination(wanderPosition);
            if (Vector3.Distance(transform.position, wanderPosition) < reachingDistance)
            { SelectWanderPosition(); }
        }

        UpdateAnimation();
    }

    private void OnDisable()
    {
        hurtCollider.onHitRecived.RemoveListener(OnHitRecived);
    }

    private void OnHitRecived(HitCollider agressor, HurtCollider victim)
    {
        if(agressor.CompareTag("Player"))
        {
            Debug.Log("Muerte Enemigo");
            this.gameObject.SetActive(false);
        }
    }

    private void SelectWanderPosition()
    {
        Vector2 positionXY = Random.insideUnitCircle * wanderinfRadius;
        Vector3 positionXZ = new Vector3(positionXY.x, 0f, positionXY.y);
        wanderPosition = homeOrigin + positionXZ;
    }

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
}
