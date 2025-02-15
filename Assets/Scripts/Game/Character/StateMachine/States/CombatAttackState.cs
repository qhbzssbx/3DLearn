public class CombatAttackState : BaseState
{
    private bool _isCombatMode;

    public CombatAttackState(CharacterStateMachine machine, IInputService inputService)
        : base(machine, inputService) { }

    public override void Enter()
    {
        Machine.Animator.SetTrigger("attack");
    }

    public override void Update()
    {
        if (Machine.CheckAnimationName("attack") && Machine.CheckAnimationComplete())
        {
            OnAnimationEndEvent();
        }
    }

    public override void HandleInput(InputState input)
    {

    }

    public override void OnAnimationEndEvent()
    {
        Machine.ChangeState<CombatIdleState>();
    }
}