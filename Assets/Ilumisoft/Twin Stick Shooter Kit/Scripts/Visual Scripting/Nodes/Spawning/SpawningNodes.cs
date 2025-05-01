using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Spawn")]
    [UnitCategory("Twin Stick Shooter Kit\\Spawning")]
    public class SpawnNode : ManagerNode<ISpawningManager>
    {
        [DoNotSerialize]
        private ValueInput prefabInput;

        protected override void Definition()
        {
            base.Definition();

            prefabInput = ValueInput<GameObject>("Prefab", null);
        }

        protected override void OnExecuteFlow(Flow flow, ISpawningManager manager)
        {
            var prefab = flow.GetValue<GameObject>(prefabInput);

            if (prefab != null)
            {
                manager.Spawn(prefab);
            }
        }
    }

    [UnitTitle("Despawn")]
    [UnitCategory("Twin Stick Shooter Kit\\Spawning")]
    public class DespawnNode : ManagerNode<ISpawningManager>
    {
        [DoNotSerialize, NullMeansSelf, PortLabelHidden]
        private ValueInput targetInput;

        protected override void Definition()
        {
            base.Definition();

            targetInput = ValueInput<GameObject>("Game Object", null).NullMeansSelf();
        }

        protected override void OnExecuteFlow(Flow flow, ISpawningManager manager)
        {
            var target = flow.GetValue<GameObject>(targetInput);

            if (target != null)
            {
                manager.Despawn(target);
            }
        }
    }
}