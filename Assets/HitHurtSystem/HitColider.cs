using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HitCollider : MonoBehaviour, IHitter
{
    public UnityEvent<HitCollider, HurtCollider> onHitDelivered;
    [SerializeField] List<string> hittableTags;

    [SerializeField] float damage = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        CheckCollider(other);
    }

    private void CheckCollider(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            HurtCollider hurtCollider = other.GetComponent<HurtCollider>();
            if (hurtCollider)
            {
                hurtCollider.NotifyHit(this);
                onHitDelivered.Invoke(this, hurtCollider);
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollider(collision.collider);
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
