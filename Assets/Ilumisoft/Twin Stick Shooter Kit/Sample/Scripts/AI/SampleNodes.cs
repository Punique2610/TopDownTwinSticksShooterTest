// This file contains a set of custom sample behavior tree nodes, that have been created for the sample agent.
// This should give you a good example of how you can create custom behavior tree nodes for your agents.

using Ilumisoft.AI.BehaviorTreeToolkit;
using Ilumisoft.TwinStickShooterKit.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    /// <summary>
    /// Custom Action Node
    /// Makes the agent orient towards the player, using the orientation speed from the blackboard
    /// </summary>
    public class OrientTowardsPlayerNode : ActionNode
    {
        IPlayerManager playerManager = null;

        protected override void OnInitialize()
        {
            playerManager = Managers.Get<IPlayerManager>();
        }

        protected override StatusCode OnUpdate()
        {
            if (playerManager.TryGetPlayer(out var player))
            {
                OrientTowards(player.transform.position);

                return StatusCode.Success;
            }

            return StatusCode.Failure;
        }

        public void OrientTowards(Vector3 lookPosition)
        {
            Vector3 lookDirection = Vector3.ProjectOnPlane(lookPosition - Owner.transform.position, Vector3.up).normalized;

            if (lookDirection.sqrMagnitude != 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Owner.transform.rotation = Quaternion.Slerp(Owner.transform.rotation, targetRotation, Time.deltaTime * GetOrientationSpeed());
            }
        }

        float GetOrientationSpeed() => Blackboard.GetStats().OrientationSpeed;

        public override string ToString() => "Orient towards player";
    }

    /// <summary>
    /// Custom Condition Node
    /// Returns 'Success' if the player is in detection range, 'Failure' otherwise
    /// </summary>
    public class IsPlayerInDetectionRangeNode : ConditionNode
    {
        IPlayerManager playerManager = null;

        protected override void OnInitialize()
        {
            playerManager = Managers.Get<IPlayerManager>();
        }

        protected override StatusCode OnUpdate()
        {
            if (playerManager.TryGetPlayer(out var player))
            {
                if (Vector3.Distance(player.transform.position, Owner.transform.position) < GetDetectionRange())
                {
                    return StatusCode.Success;
                }
            }

            return StatusCode.Failure;
        }

        float GetDetectionRange() => Blackboard.GetStats().DetectionRange;

        public override string ToString() => "Is player in detection range?";
    }

    /// <summary>
    /// Custom Condition Node
    /// Returns 'Success' if the player is in attack range, 'Failure' otherwise
    /// </summary>
    public class IsPlayerInAttackRangeNode : ConditionNode
    {
        IPlayerManager playerManager = null;

        protected override void OnInitialize()
        {
            playerManager = Managers.Get<IPlayerManager>();
        }

        protected override StatusCode OnUpdate()
        {
            if (playerManager.TryGetPlayer(out var player))
            {
                if (Vector3.Distance(player.transform.position, Owner.transform.position) < GetAttackRange())
                {
                    return StatusCode.Success;
                }
            }

            return StatusCode.Failure;
        }

        float GetAttackRange() => Blackboard.GetStats().AttackRange;

        public override string ToString() => "Is player in attack range?";
    }

    /// <summary>
    /// Custom Condition Node
    /// Returns 'Success' if the agent is alerted, 'Failure' otherwise
    /// </summary>
    public class IsAlertedNode : ConditionNode
    {
        protected override StatusCode OnUpdate()
        {
            return GetIsAlerted() ? StatusCode.Success : StatusCode.Failure;
        }

        bool GetIsAlerted() => Blackboard.IsAlerted();

        public override string ToString() => "Is alerted?";
    }

    /// <summary>
    /// Custom Condition Node
    /// Returns 'Success' if the agent is alerted, 'Failure' otherwise
    /// </summary>
    public class SetAlertedNode : ConditionNode
    {
        readonly bool value = false;

        public SetAlertedNode(bool value)
        {
            this.value = value;
        }

        protected override StatusCode OnUpdate()
        {
            Blackboard.SetAlerted(value);

            return StatusCode.Success;
        }

        public override string ToString() => $"Set alerted: {value}";
    }

    /// <summary>
    /// Custom Action Node
    /// Commands the agent to walk to the position of the player
    /// </summary>
    public class MoveToPlayerNode : ActionNode
    {
        NavMeshAgent navMeshAgent = null;
        IPlayerManager playerManager = null;

        protected override void OnInitialize()
        {
            navMeshAgent = Owner.GetComponent<NavMeshAgent>();
            playerManager = Managers.Get<IPlayerManager>();
        }

        protected override StatusCode OnUpdate()
        {
            if (playerManager.TryGetPlayer(out var player))
            {
                navMeshAgent.SetDestination(player.transform.position);

                return StatusCode.Success;
            }

            return StatusCode.Failure;
        }

        public override string ToString() => "Move to player";
    }

    /// <summary>
    /// Custom Action Node
    /// Commands the agent to wader around randomly
    /// </summary>
    public class WanderNode : ActionNode
    {
        NavMeshAgent navMeshAgent = null;

        protected override void OnInitialize()
        {
            navMeshAgent = Owner.GetComponent<NavMeshAgent>();
        }

        protected override StatusCode OnUpdate()
        {
            if (navMeshAgent.remainingDistance < 1.0f)
            {
                var nextPosition = GetRandomPositionInRadius();
                navMeshAgent.SetDestination(nextPosition);
            }

            return StatusCode.Success;
        }

        Vector3 GetRandomPositionInRadius()
        {
            return NavMeshUtility.GetRandomPoint(navMeshAgent.transform.position, 100);
        }

        public override string ToString() => "Wander";
    }

    /// <summary>
    /// Custom Action Node
    /// Commands the agent to stop navigation
    /// </summary>
    public class StopNavigationNode : ActionNode
    {
        NavMeshAgent navMeshAgent = null;

        protected override void OnInitialize()
        {
            navMeshAgent = Owner.GetComponent<NavMeshAgent>();
        }

        protected override StatusCode OnUpdate()
        {
            navMeshAgent.ResetPath();

            return StatusCode.Success;
        }

        public override string ToString() => "Stop navigation";
    }

    /// <summary>
    /// Custom Condition Node
    /// Checks whether the player is alive.
    /// Return Success when alive, Failure otherwise
    /// </summary>
    public class IsPlayerAlive : ConditionNode
    {
        IPlayerManager playerManager = null;

        protected override void OnInitialize()
        {
            playerManager = Managers.Get<IPlayerManager>();
        }

        protected override StatusCode OnUpdate()
        {
            if (playerManager.TryGetPlayer(out var player) && player.TryGetComponent<IHealthComponent>(out var health))
            {
                if (health.IsAlive)
                {
                    return StatusCode.Success;
                }
            }

            return StatusCode.Failure;
        }

        public override string ToString() => "Is Player Alive?";
    }

    /// <summary>
    /// Custom Condition Node
    /// Checks whether the owner is dead.
    /// Return Failure when alive, Success otherwise
    /// </summary>
    public class IsDead : ConditionNode
    {
        IHealthComponent health;

        protected override void OnInitialize()
        {
            health = Owner.GetComponent<IHealthComponent>();
        }

        protected override StatusCode OnUpdate()
        {
            return health.IsAlive ? StatusCode.Failure : StatusCode.Success;
        }

        public override string ToString() => "Is Dead?";
    }

    /// <summary>
    /// Custom Action Node
    /// Commands the agent to fire it's weapon
    /// </summary>
    public class FireWeaponNode : ActionNode
    {
        readonly float duration = 2.0f;

        IWeaponControllerComponent weaponController = null;

        float timer = 0.0f;

        public FireWeaponNode(float duration)
        {
            this.duration = duration;
        }

        protected override void OnInitialize()
        {
            weaponController = Owner.GetComponent<IWeaponControllerComponent>();
        }

        protected override void OnStart()
        {
            timer = 0.0f;

            weaponController.StartFiring();

            base.OnStart();
        }

        protected override StatusCode OnUpdate()
        {
            timer += Time.deltaTime;

            if (timer > duration)
            {
                return StatusCode.Success;
            }

            return StatusCode.Running;
        }

        protected override void OnStop()
        {
            weaponController.StopFiring();
        }

        protected override void OnAbort()
        {
            weaponController.StopFiring();
        }

        public override string ToString() => "Fire Weapon";
    }
}