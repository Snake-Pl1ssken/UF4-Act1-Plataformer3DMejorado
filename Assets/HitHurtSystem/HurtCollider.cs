using System;
using UnityEngine;
using UnityEngine.Events;


public class HurtCollider : MonoBehaviour, IHitter
{
    [SerializeField] float damage = 0.25f;
    public UnityEvent <IHitter, HurtCollider> onHitRecived;

    public void NotifyHit(IHitter hitter)
    {
        onHitRecived.Invoke(hitter, this);
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
