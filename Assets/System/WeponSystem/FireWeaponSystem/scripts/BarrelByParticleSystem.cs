using UnityEngine;

public class BarrelByParticleSystem : BarrelBase, IHitter
{
    [SerializeField] float damagePerParticle = 0.03f;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule em;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        em = particleSystem.emission;
    }
    public override void ShootOnce()
    {
        particleSystem.Emit(1);
    }
    public override void StartShooting()
    {
        em.enabled = true;
    }
    public override void StopShooting()
    {
        em.enabled = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        other.GetComponent<HurtCollider>()?.NotifyHit(this);
    }

    float IHitter.GetDamage()
    {
        return damagePerParticle;
    }

    Transform IHitter.GetTransform()
    {
        return transform;
    }
}
