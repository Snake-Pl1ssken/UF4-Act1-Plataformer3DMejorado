using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float animationSmoothingSpeed = 10f;
    Animator animator;

    HurtCollider hurtCollider;
    Ragdollizer ragdollizer;
    virtual protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.keepAnimatorStateOnDisable = true;

        hurtCollider = GetComponent<HurtCollider>();
        ragdollizer = GetComponentInChildren<Ragdollizer>();
    }

    virtual protected void OnEnable()
    {
        hurtCollider.onHitRecived.AddListener(OnHitRecived);
    }

    virtual protected void OnDisable()
    {
        hurtCollider.onHitRecived.RemoveListener(OnHitRecived);
    }
    private void OnHitRecived(IHitter hitCollider, HurtCollider hurtCollider)
    {
        Debug.Log("Hit");
        ragdollizer.RagDollizer();
        Invoke(nameof(Deactivate), 2f);
        Invoke(nameof(Resurrect), 3f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        ragdollizer.DeRagdollizer();
    }

    void Resurrect()
    {
        gameObject.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame


    Vector3 currentAnimationSpeed = Vector3.zero;
    Vector3 desiredAnimationSpeed = Vector3.zero;

    protected void UpdateAnimation()
    {
        float speedMultiplier = IsRunning() ? 2f : 1f;
        desiredAnimationSpeed =
            speedMultiplier *
            transform.InverseTransformDirection(GetLastNormalizedVelocity());

        Vector3 direction = desiredAnimationSpeed - currentAnimationSpeed;
        float distance = direction.magnitude;
        float smoothingStep = (animationSmoothingSpeed * Time.deltaTime);
        float distanceToApply = Mathf.Min(distance, smoothingStep);
        currentAnimationSpeed += direction.normalized * distanceToApply;

        animator.SetFloat("SidewardVelocity", currentAnimationSpeed.x);
        animator.SetFloat("ForwardVelocity", currentAnimationSpeed.z);
        animator.SetBool("IsGrounded", IsGrounded());

        float clampedVerticalVelocity =
            Mathf.Clamp(GetCurrentVerticalSpeed(), -GetJumpSpeed(), GetJumpSpeed());
        float verticalProgress = Mathf.InverseLerp(
            GetJumpSpeed(), -GetJumpSpeed(), clampedVerticalVelocity
            );
        animator.SetFloat("VerticalProgress",
            IsGrounded() ? 1f : verticalProgress);
    }

    abstract protected float GetCurrentVerticalSpeed();
    abstract protected float GetJumpSpeed();
    abstract protected bool IsRunning();
    abstract protected bool IsGrounded();
    abstract protected Vector3 GetLastNormalizedVelocity();
    


}
