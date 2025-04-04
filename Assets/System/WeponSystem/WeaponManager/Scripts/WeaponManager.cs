using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] int initialWeaponToselect;

    [Header("Debug")]
    [SerializeField] bool debugSelectNextWeapon;
    [SerializeField] bool debugSelectPrevWeapon;
    [SerializeField] bool debugShot;
    [SerializeField] bool debugStartShooting;
    [SerializeField] bool debugStopShooting;


    private void OnValidate()
    {
        if (debugSelectNextWeapon)
        {
            debugSelectNextWeapon = false;
            SelectNextWeapon();
        }
        if (debugSelectPrevWeapon)
        {
            debugSelectPrevWeapon = false;
            SelectPreviousWeapon();
        }
        if (debugShot)
        {
            debugShot = false;
            shot();
        }
        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartContinuosShooting();
        }
        if (debugStopShooting)
        {
            debugStopShooting = false;
            StopContinuosShooting();
        }
    }

    weapon[] weapons;
    int currentWeaponIndex = -1;
    private void Awake()
    {
        weapons = GetComponentsInChildren<weapon>();
    }

    private void Start()
    {
        foreach (weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        SelectWeapon(initialWeaponToselect);
    }

    public void SelectWeapon(int WeaponIndex)
    {
        if (WeaponIndex < -1)
        { WeaponIndex = weapons.Length - 1; }
        if (WeaponIndex >= weapons.Length)
        { WeaponIndex = - 1; }

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].NotifyDeselected();
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        currentWeaponIndex = WeaponIndex;

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].NotifySelected();
        }
    }

    public bool HasSelectedWeapon()
    { return currentWeaponIndex != -1; }

    public bool SelectedWeaponIsShotByShot()
    { 
        return currentWeaponIndex != -1 ?
            weapons[currentWeaponIndex].weapontype == weapon.WeaponType.ShotByShot :
            false;
    }

    public void shot()
    {
        if (currentWeaponIndex != -1)
        { weapons[currentWeaponIndex].Shoot(); }
    }

    public void StartContinuosShooting()
    {
        if (currentWeaponIndex != -1)
        { weapons[currentWeaponIndex].StartShooting(); }
    }

    public void StopContinuosShooting()
    {
        if (currentWeaponIndex != -1)
        { weapons[currentWeaponIndex].StopShooting(); }
    }

    public void SelectNextWeapon() { SelectWeapon(currentWeaponIndex + 1); }
    public void SelectPreviousWeapon() { SelectWeapon(currentWeaponIndex - 1); }

    public weapon GetCurrentweapon()
    {
        return currentWeaponIndex == -1 ? null :
            weapons[currentWeaponIndex];
    }
}
