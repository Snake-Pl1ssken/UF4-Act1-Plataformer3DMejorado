using UnityEngine;

public class Explosion : MonoBehaviour, IHitter
{
    [Header("Damage Settings")]
    [SerializeField] float range = 5f;
    [SerializeField] LayerMask layermask = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask occludingLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] float damage = 5f;


    [Header("Damage Settings")]
    [SerializeField] GameObject VisualExplosionPrefab;
    private void Start()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, range, layermask);

        foreach (Collider c in colliders)
        { 
            HurtCollider hurtCollider = c.GetComponent<HurtCollider>();
            if (hurtCollider != null)
            {
                bool dealDamage = false;
                if (Physics.Linecast(
                    transform.position,
                    c.transform.position,
                    out RaycastHit hit,
                    occludingLayerMask))
                {
                    dealDamage = hit.collider == c;
                }
                else
                { 
                    dealDamage = true;  
                }

                if (dealDamage)
                {
                    hurtCollider.NotifyHit(this);
                }
            }
        }
        
        Instantiate(
            VisualExplosionPrefab, 
            transform.position, 
            Quaternion.identity);

        Destroy(gameObject);
    }

    float IHitter.GetDamage()
    {
        return damage;
    }

    Transform IHitter.GetTransform()
    {
        return transform;
    }
}
