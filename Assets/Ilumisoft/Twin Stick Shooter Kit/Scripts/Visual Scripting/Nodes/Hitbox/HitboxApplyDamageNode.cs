using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitSurtitle("Hitbox")]
    [UnitTitle("Apply Damage")]
    [UnitCategory("Twin Stick Shooter Kit\\Hitbox")]
    [TypeIcon(typeof(HitboxComponent))]
    public class HitboxApplyDamageNode : ComponentNode<IHitboxComponent>
    {
        [DoNotSerialize, NullMeansSelf, PortLabelHidden]
        public ValueInput DamageInput;

        float damage;

        protected override void Definition()
        {
            base.Definition();

            DamageInput = ValueInput("Damage", 0.0f);
        }

        protected override void OnExecuteFlow(Flow flow, IHitboxComponent hitbox)
        {
            damage = flow.GetValue<float>(DamageInput);

            hitbox.ApplyDamage(damage);
        }
    }
}