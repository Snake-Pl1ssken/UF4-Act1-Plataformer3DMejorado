using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] InputActionReference shot;
    [SerializeField] InputActionReference weapon1;
    [SerializeField] InputActionReference weapon2;
    [SerializeField] InputActionReference weapon3;
    [SerializeField] InputActionReference weapon4;
    [SerializeField] InputActionReference weapon5;
    [SerializeField] InputActionReference cycleWeapon;

    [SerializeField] WeaponManager weaponManager;

    private void OnEnable()
    {
        shot.action.Enable();
        weapon1.action.Enable();
        weapon2.action.Enable();
        weapon3.action.Enable();
        weapon4.action.Enable();
        weapon5.action.Enable();
        cycleWeapon.action.Enable();
    }

    private void OnDisable()
    {
        shot.action.Disable();
        weapon1.action.Disable();
        weapon2.action.Disable();
        weapon3.action.Disable();
        weapon4.action.Disable();
        weapon5.action.Disable();
        cycleWeapon.action.Disable();
    }

    private void Update()
    {
        UpdateShooting();
        SelectWeaponByNumber();
        SelectWeaponCycle();
        //Debug.Log();

    }

    private void UpdateShooting()
    {
        if (weaponManager.HasSelectedWeapon())
        {
            weapon currentWeapon = 
                weaponManager.GetCurrentweapon();
            switch (currentWeapon.weapontype)
            { 
                case weapon.WeaponType.ShotByShot:
                    if (shot.action.WasPressedThisFrame())
                    { 
                        currentWeapon.Shoot();
                    }
                    break;
                case weapon.WeaponType.ContinuoShot:
                    if (shot.action.WasPressedThisFrame())
                    {
                        currentWeapon.StartShooting();
                    }
                    else if (shot.action.WasReleasedThisFrame())
                    { 
                        currentWeapon.StopShooting();
                    }
                    break;
            }
        }
    }

    private void SelectWeaponByNumber()
    {
        int weaponToSelect = -2;
        //Debug.Log(weaponToSelect);
        //Seleccion del weapon to select segun tecla
        if (weapon1.action.WasPressedThisFrame())
        {
            weaponToSelect = 0;
        }
        else if (weapon2.action.WasPressedThisFrame())
        {
            weaponToSelect = 1;
        }
        else if (weapon3.action.WasPressedThisFrame())
        {
            weaponToSelect = 2;
        }
        else if (weapon4.action.WasPressedThisFrame())
        {
            weaponToSelect = 3;
        }
        else if (weapon5.action.WasPressedThisFrame())
        {
            weaponToSelect = 4;
        }


        if (weaponToSelect != -2)
        {
            weaponManager.SelectWeapon(weaponToSelect);
        }
    }

    private void SelectWeaponCycle()
    {
        Vector2 scrollDelta =
            cycleWeapon.action.ReadValue<Vector2>();
        if (scrollDelta.y > 0f)
        {
            { weaponManager.SelectNextWeapon(); }
        }
        else if(scrollDelta.y < 0f)
        {
            { weaponManager.SelectPreviousWeapon(); }
        }
    }
}
