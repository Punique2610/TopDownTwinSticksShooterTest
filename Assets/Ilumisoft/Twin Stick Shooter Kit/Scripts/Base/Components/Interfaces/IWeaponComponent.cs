using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IWeaponComponent : IComponent
    {
        GameObject Owner { get; set; }

        bool IsTriggerDown { get; }

        UnityAction OnPullTrigger { get; set; }

        UnityAction OnReleaseTrigger { get; set; }

        void PullTrigger();
        void ReleaseTrigger();
    }
}