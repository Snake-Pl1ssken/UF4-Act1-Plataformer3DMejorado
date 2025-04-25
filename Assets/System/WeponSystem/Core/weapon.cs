using UnityEngine;

public class weapon : MonoBehaviour
{
    public enum WeaponType
    { 
        ShotByShot,
        ContinuoShot,
    }

    [SerializeField] public WeaponType weapontype;

    [SerializeField] public Transform grabPointsParent;

    [Header("Debug")]
    public bool debugShot;
    public bool debugStartShooting;
    public bool debugStopShooting;

    public BarrelBase[] allBarrels;


    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            Shoot();
        }

        if (debugStartShooting)
        { 
            debugStartShooting = false;
            StartShooting();
        }

        if (debugStopShooting)
        { 
            debugStopShooting = false;
            StopShooting();
        }

    }

    private void Awake()
    {
        allBarrels = GetComponentsInChildren<BarrelBase>();
    }

    public void Shoot()
    {
        foreach (BarrelBase barrel in allBarrels)
        {
            barrel.ShootOnce();
        }
    }

    public void StartShooting()
    {
        foreach (BarrelBase barrel in allBarrels)
        {
            barrel.StartShooting();
        }
    }

    public void StopShooting()
    {
        foreach (BarrelBase barrel in allBarrels)
        {
            barrel.StopShooting();
        }
    }

    public void NotifySelected()
    { 
        //nada de momento
    }
    public void NotifyDeselected()
    {
        if (weapontype == WeaponType.ContinuoShot)
        { 
            StopShooting();
        }
    }
}
