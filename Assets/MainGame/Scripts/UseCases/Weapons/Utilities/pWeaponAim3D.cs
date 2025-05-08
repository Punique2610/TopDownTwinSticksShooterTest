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

    protected override void Initialization()
    {
        base.Initialization();

        if (_weapon != null)
        {
            //This is only a quick fix for prototyping. 

            if (_weapon.Owner)
                refTransform = _weapon.Owner.CharacterModel.transform.Find("root");
        }
    }

    protected override void Update()
    {
        base.Update();

        //if (refTransform != null)
            //weaponDirectionRelativeToCharTransform = CalculateRelativeDirection((CurrentAim - refTransform.position).normalized, refTransform.forward);
    }

    protected Vector3 CalculateRelativeDirection(Vector3 forwardCal, Vector3 forwardRef)
    {
        //forwardCal.Normalize();
        //forwardRef.Normalize();
        Quaternion rotation = Quaternion.LookRotation(forwardRef);

        return Quaternion.Inverse(rotation) * forwardCal;
    }

}
