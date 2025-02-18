// public abstract class BaseState
// {
//     protected CharacterStateMachine machine;
//     protected CharacterConfig config;

//     public int Priority = 0; // 状态优先级

//     public BaseState(CharacterStateMachine machine)
//     {
//         this.machine = machine;
//         this.config = machine.Config;
//     }

//     public virtual void Enter() { }
//     public virtual void Update() { }
//     public virtual void Exit() { }
// }
namespace P5Game.State
{
    public abstract class BaseState
    {
        protected CharacterStateMachine Machine;
        protected IInputService InputService;
        protected CharacterConfig Config => Machine.Config; // 通过状态机访问配置

        // 状态计时器（逻辑时间）
        protected float StateTimer { get; private set; }
        // 动画时间（归一化）
        protected float AnimationNormalizedTime => Machine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;


        public BaseState(CharacterStateMachine machine, IInputService inputService)
        {
            Machine = machine;
            InputService = inputService;
        }

        public virtual void Enter() { }
        public virtual void HandleInput(InputState input) { }
        public virtual void Update() { }
        public virtual void Exit() { }

        public virtual void OnAnimationEndEvent() { }
    }
}