using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class pWeaponAim3D : WeaponAim3D
{
    [HideInInspector]
    public Transform refTransform;

    [field:SerializeField]
    public Vector3 weaponDirectionRelativeToCharTransform { get; protected set; }

    public Vector3 weaponForward;
    public Vector3 refForward;

    protected override void Initialization()
    {
        base.Initialization();
    
        if (_weapon != null)
        {
            //This is only a quick fix for prototyping. 

            if (_weapon.Owner)
                refTransform = _weapon.Owner.CharacterModel.transform;
        }
    }

    protected override void Update()
    {
        base.Update();

        weaponForward = transform.forward;

        //if (refTransform != null)
        //{
        //    refForward = refTransform.forward;
        //    weaponDirectionRelativeToCharTransform = CalculateRelativeDirection(transform.forward, refTransform.forward);
        //}
    }

    protected Vector3 CalculateRelativeDirection(Vector3 forwardCal, Vector3 forwardRef)
    {
        //forwardCal.Normalize();
        //forwardRef.Normalize();
        forwardCal.y = 0;
        Quaternion rotation = Quaternion.LookRotation(forwardRef);
        return Quaternion.Inverse(rotation) * forwardCal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, _weaponAimCurrentAim * 100);
    }

}
