using UnityEngine;

public class BarrelByRaycast : BarrelBase, IHitter
{
    [SerializeField] float range = 15f;
    LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] float damage = 0.25f;


    public override void ShootOnce()
    {
        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            range,
            layerMask))
        { 
            HurtCollider hurtCollider = hit.collider.GetComponent<HurtCollider>();
            hurtCollider.NotifyHit(this);
        }
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
