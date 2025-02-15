using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMoveState : MoveState
{
    public CombatMoveState(CharacterStateMachine machine, IInputService inputService) : base(machine, inputService)
    {
    }

    public override void Enter()
    {
        Machine.Animator.SetBool("combat_walk", true);
        Machine.Animator.speed = 1.5f;
    }

    public override void Exit()
    {
        Machine.Animator.SetBool("combat_walk", false);
        Machine.Animator.speed = 1f;
    }

    protected override void ExitMove()
    {
        Machine.ChangeState<CombatIdleState>();
    }
}
