using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Move")]
    [UnitSurtitle("Player Controller")]
    [UnitCategory("Twin Stick Shooter Kit\\Player")]
    [TypeIcon(typeof(PlayerControllerComponent))]
    public class MovePlayerNode : ComponentNode<IPlayerControllerComponent>
    {
        [DoNotSerialize]
        public ValueInput MoveInputValue;

        [DoNotSerialize]
        public ValueInput SprintInputValue;

        protected override void Definition()
        {
            base.Definition();

            MoveInputValue = ValueInput("Move Input", Vector2.zero);
            SprintInputValue = ValueInput("Sprint", false);
        }

        protected override void OnExecuteFlow(Flow flow, IPlayerControllerComponent playerController)
        {
            var moveInput = flow.GetValue<Vector2>(MoveInputValue);
            var sprint = flow.GetValue<bool>(SprintInputValue);

            playerController.Move(moveInput, sprint);
        }
    }

    [UnitTitle("Rotate")]
    [UnitSurtitle("Player Controller")]
    [UnitCategory("Twin Stick Shooter Kit\\Player")]
    [TypeIcon(typeof(PlayerControllerComponent))]
    public class RotatePlayerNode : ComponentNode<IPlayerControllerComponent>
    {
        [DoNotSerialize]
        public ValueInput LookInputValue;

        protected override void Definition()
        {
            base.Definition();

            LookInputValue = ValueInput("Look Input", Vector2.zero);
        }

        protected override void OnExecuteFlow(Flow flow, IPlayerControllerComponent playerController)
        {
            var lookInput = flow.GetValue<Vector2>(LookInputValue);

            playerController.Rotate(lookInput);
        }
    }
}