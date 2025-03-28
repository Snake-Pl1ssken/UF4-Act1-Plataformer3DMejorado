using UnityEngine;

public class State_Shooting : BaseState
{
    float angularSpeed;

    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        angularSpeed = enemy.GetAgent().angularSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        enemy.GetWeaponManager().StartContinuosShooting();
        enemy.GetAgent().destination = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 desiredDirection = enemy.GetTarget().position - transform.position;
        desiredDirection.y = 0f;

        enemy.GetOrientator().OrientateTo(desiredDirection);
        //Todo orientarse todo el rato hacia el target
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        enemy.GetWeaponManager().StartContinuosShooting();
    }
}
