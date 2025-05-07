using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using System;

public class pCharacterOrientation3D : CharacterOrientation3D
{
    protected const string _forwardAimAnimationParameterName = "ForwardAim";
    protected const string _sideAimAnimationParameterName = "SideAim";

    protected int _forwardAimAnimationParameter;
    protected int _sideAimAnimationParameter;

    protected pWeaponAim3D _pWeaponAim3D;
    protected pCharacterOrientation3D _characterOrientation3D;

    protected override void Initialization()
    {
        base.Initialization();

        if(_characterHandleWeapon != null)
        {
            _characterHandleWeapon.OnWeaponChange -= OnWeaponChange;
            _characterHandleWeapon.OnWeaponChange += OnWeaponChange;

            _pWeaponAim3D = _characterHandleWeapon.WeaponAimComponent as pWeaponAim3D;
        }
    }

    private void OnWeaponChange()
    {
        _pWeaponAim3D = _characterHandleWeapon.WeaponAimComponent as pWeaponAim3D;
    }

    protected override void InitializeAnimatorParameters()
    {
        base.InitializeAnimatorParameters();

        RegisterAnimatorParameter(_forwardAimAnimationParameterName, AnimatorControllerParameterType.Float, out _forwardAimAnimationParameter);
        RegisterAnimatorParameter(_sideAimAnimationParameterName, AnimatorControllerParameterType.Float, out _sideAimAnimationParameter);
    }

    public override void UpdateAnimator()
    {
        base.UpdateAnimator();

        if (_pWeaponAim3D) 
        {
            MMAnimatorExtensions.UpdateAnimatorFloat(_animator, _forwardAimAnimationParameter, _pWeaponAim3D.weaponDirectionRelativeToCharTransform.z, _character._animatorParameters);
            MMAnimatorExtensions.UpdateAnimatorFloat(_animator, _sideAimAnimationParameter, _pWeaponAim3D.weaponDirectionRelativeToCharTransform.x, _character._animatorParameters);
        }
    }
}
