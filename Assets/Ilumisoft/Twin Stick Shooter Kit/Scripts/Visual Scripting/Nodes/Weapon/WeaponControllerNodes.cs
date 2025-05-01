using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Start Firing")]
    [UnitSurtitle("Weapon Controller")]
    [UnitCategory("Twin Stick Shooter Kit\\Weapon Controller")]
    [TypeIcon(typeof(WeaponComponent))]
    public class StartFiringNode : ComponentNode<IWeaponControllerComponent>
    {
        protected override void OnExecuteFlow(Flow flow, IWeaponControllerComponent weaponController)
        {
            weaponController.StartFiring();
        }
    }

    [UnitTitle("Stop Firing")]
    [UnitSurtitle("Weapon Controller")]
    [UnitCategory("Twin Stick Shooter Kit\\Weapon Controller")]
    [TypeIcon(typeof(WeaponComponent))]
    public class StopFiringNode : ComponentNode<IWeaponControllerComponent>
    {
        protected override void OnExecuteFlow(Flow flow, IWeaponControllerComponent weaponController)
        {
            weaponController.StopFiring();
        }
    }

    [UnitTitle("Equip Weapon")]
    [UnitSurtitle("Weapon Controller")]
    [UnitCategory("Twin Stick Shooter Kit\\Weapon Controller")]
    [TypeIcon(typeof(WeaponComponent))]
    public class EquipWeaponNode : ComponentNode<IWeaponControllerComponent>
    {
        [DoNotSerialize]
        public ValueInput PrefabInputValue;

        protected override void Definition()
        {
            base.Definition();

            PrefabInputValue = ValueInput<GameObject>("Prefab", null);
        }

        protected override void OnExecuteFlow(Flow flow, IWeaponControllerComponent weaponController)
        {
            var prefab = flow.GetValue<GameObject>(PrefabInputValue);

            if (prefab != null)
            {
                if (prefab.TryGetComponent<IWeaponComponent>(out var weapon))
                {
                    weaponController.Equip(weapon);
                }
                else
                {
                    Debug.Log("Could not equip Weapon, because the weapon prefab does not have a weapon component");
                }
            }
            else
            {
                Debug.Log("Could not equip Weapon, because no weapon prefab has been provided");
            }
        }
    }
}