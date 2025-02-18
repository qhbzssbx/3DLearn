namespace P5Game.State
{
    public class CombatIdleState : IdleState
    {
        private bool _isCombatMode;

        public CombatIdleState(CharacterStateMachine machine, IInputService inputService)
            : base(machine, inputService) { }

        public override void Enter()
        {
            Machine.Animator.SetBool("combat_idle", true);
        }

        public override void Exit()
        {
            Machine.Animator.SetBool("combat_idle", false);
        }

        public override void HandleInput(InputState input)
        {
            if (input.IsAttackPressed)
            {
                Machine.ChangeState<CombatAttackState>();
                return;
            }
            if (input.IsCancelAttackPressed)
            {
                Machine.ChangeState<IdleState>();
                return;
            }
            // LogUtility.Log("CombatIdleState HandleInput " + input.HasMoveInput);
            if (input.HasMoveInput)
            {
                Machine.ChangeState<CombatMoveState>();
            }
        }
    }
}