using UnityEngine;

public class BarrelBase : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool debugShoot;

    private void OnValidate()
    {
        if (debugShoot)
        {
            debugShoot = false;
            ShootOnce();
        }
    }

    public virtual void ShootOnce()
    {
        throw new System.Exception("this barrel dosent support shootOnce");
    }
    public virtual void StartShooting()
    {
        throw new System.Exception("this barrel dosent support StartShooting");
    }    
    public virtual void StopShooting()
    {
        throw new System.Exception("this barrel dosent support StopShooting");
    }


}
