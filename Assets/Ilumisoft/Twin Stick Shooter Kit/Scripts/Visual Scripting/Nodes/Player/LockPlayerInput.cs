using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Lock Player Input")]
    [UnitSurtitle("Player")]
    [UnitShortTitle("Lock Input")]
    [UnitCategory("Twin Stick Shooter Kit\\Player")]
    [TypeIcon(typeof(PlayerControllerComponent))]
    public class LockPlayerInputNode : Unit
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput OutputTrigger;

        GameObject playerGameObject = null;

        protected override void Definition()
        {
            InputTrigger = ControlInput("InputTrigger", ExecuteFlow);
            OutputTrigger = ControlOutput("OutputTrigger");

            Succession(InputTrigger, OutputTrigger);
        }

        private ControlOutput ExecuteFlow(Flow flow)
        {
            if (TryFindPlayer() && playerGameObject.TryGetComponent<IControllable>(out var controllable))
            {
                controllable.IsControllable = false;
            }

            return OutputTrigger;
        }

        private bool TryFindPlayer()
        {
            if (playerGameObject == null)
            {
                playerGameObject = GameObject.FindWithTag("Player");
            }

            return playerGameObject != null;
        }
    }
}