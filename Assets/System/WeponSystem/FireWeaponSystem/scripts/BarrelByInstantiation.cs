using UnityEngine;

public class BarrelByInstantiation : BarrelBase
{
    [SerializeField] GameObject Projectileprefab;



    public override void ShootOnce()
    {
        Instantiate(
            Projectileprefab,
            transform.position,
            transform.rotation);
    }
}
