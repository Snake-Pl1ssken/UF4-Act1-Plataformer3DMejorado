using UnityEngine;

public class Torreta : MonoBehaviour, ITargeteable
{
    [SerializeField] private Sight sight;                   
    [SerializeField] private WeaponManager weaponManager;   
    [SerializeField] private float rotationSpeed = 5f;      

    private ITargeteable currentTarget;
    private void Update()
    {
        ITargeteable newTarget = sight.GetClosestTarget();
        Debug.Log("Que estoy viendo? = " + newTarget);
        if (newTarget != currentTarget)
        {
            Debug.Log("Entra primer if");
            if (newTarget != null)
            {
                Debug.Log("player detectado");
                weaponManager.StartContinuosShooting();
            }
            else if (currentTarget != null)
            {
                Debug.Log("player no detectado");
                weaponManager.StopContinuosShooting();
            }

            currentTarget = newTarget; 
        }

        if (currentTarget != null)
        {
            Vector3 targetDirection = currentTarget.GetTransform().position - transform.position;
            targetDirection.y = 0; 
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            Debug.DrawLine(transform.position, currentTarget.GetTransform().position, Color.green);
        }
    }


    ITargeteable.Faction ITargeteable.GetFaction()
    {
        return ITargeteable.Faction.Enemy;
    }

    Transform ITargeteable.GetTransform()
    {
        return transform;
    }

}
