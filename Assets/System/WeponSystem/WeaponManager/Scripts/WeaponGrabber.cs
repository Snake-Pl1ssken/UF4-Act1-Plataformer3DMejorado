using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponGrabber : MonoBehaviour
{
    [SerializeField] Rig leftArmRig;
    [SerializeField] Rig rightArmRig;

    [SerializeField] RigTransform leftTarget;
    [SerializeField] RigTransform rightTarget;
    [SerializeField] RigTransform leftHint;
    [SerializeField] RigTransform rightHint;
    
    WeaponManager weaponManager;
    Transform currentGrabPointsParent;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void OnEnable()
    {
        weaponManager.onWeaponChange.AddListener(onWeaponChange);
    }

    private void Update()
    {
        if (currentGrabPointsParent != null)
        {
            MoveRigTransformtoGrabPointChildren(leftTarget, currentGrabPointsParent.GetChild(0).transform);
            MoveRigTransformtoGrabPointChildren(rightTarget, currentGrabPointsParent.GetChild(1).transform);
            MoveRigTransformtoGrabPointChildren(leftHint, currentGrabPointsParent.GetChild(2).transform);
            MoveRigTransformtoGrabPointChildren(rightHint, currentGrabPointsParent.GetChild(3).transform);

        }
    }

    private void MoveRigTransformtoGrabPointChildren(RigTransform rt, Transform t)
    {
        rt.transform.position = t.transform.position;
        rt.transform.rotation = t.transform.rotation;
    }

    private void OnDisable()
    {
        weaponManager.onWeaponChange.RemoveListener(onWeaponChange);
    }

    private void onWeaponChange(weapon weapon)
    {
        currentGrabPointsParent = weapon.grabPointsParent;
    }
}
