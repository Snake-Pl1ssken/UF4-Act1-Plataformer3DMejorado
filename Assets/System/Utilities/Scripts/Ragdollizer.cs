using UnityEngine;

public class Ragdollizer : MonoBehaviour
{

    [SerializeField] bool ragdolizeOnAwake = false;

    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animator;

    [Header("Debug")]
    [SerializeField] bool debugRagdollize;
    [SerializeField] bool debugDeRagdollize;

    private void OnValidate()
    {
        if (debugRagdollize)
        { 
            debugRagdollize = false;
            RagDollizer();
        }
        if (debugDeRagdollize)
        { 
            debugDeRagdollize = false;
            DeRagdollizer();
        }
    }

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();

        if (ragdolizeOnAwake)
        {
            animator.enabled = false;
        }
        else 
        {
            foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = true; }
            foreach (Collider c in colliders) { c.enabled = false; }
        }
    }

    public void RagDollizer()
    {
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = false; }
        foreach (Collider c in colliders) { c.enabled = true; }
        animator.enabled = false;
    }

    public void DeRagdollizer()
    {
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = true; }
        foreach (Collider c in colliders) { c.enabled = false; }
        animator.enabled = true;
    }


}
