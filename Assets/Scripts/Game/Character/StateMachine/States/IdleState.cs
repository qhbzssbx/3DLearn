public class IdleState : BaseState
{
    public IdleState(CharacterStateMachine machine, IInputService inputService) : base(machine, inputService) { }

    public override void Enter()
    {
        Machine.Animator.SetBool("idle", true);
    }

    public override void HandleInput(InputState input) 
    {
        if (input.IsAttackPressed)
        {
            Machine.ChangeState<CombatAttackState>();
            return;
        }

        if (input.HasMoveInput)
        {
            Machine.ChangeState<MoveState>();
        }
    }

    public override void Exit()
    {
        Machine.Animator.SetBool("idle", false);
    }
}