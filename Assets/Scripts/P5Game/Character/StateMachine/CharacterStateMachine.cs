using System.Collections.Generic;
using P5Game.State;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace P5Game
{
    // [RequireComponent(typeof(IInputService))]
    public class CharacterStateMachine : MonoBehaviour, IController
    {
        // 公开配置字段（在Inspector中赋值）
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private IInputService _inputService;
        [SerializeField] private Animator _animator;

        private InputState _currentFrameInputState;

        private Dictionary<System.Type, BaseState> _states = new();
        public CharacterConfig Config => _config;
        [ShowInInspector, DisplayAsString, BoxGroup("运行时信息")]
        public BaseState CurrentState { get; private set; }
        public Animator Animator { get => _animator; }

        void Start()
        {
            _inputService = GetComponent<IInputService>();
            _config = Resources.Load<CharacterConfig>("Configs/CharacterConfig");

            // 初始化状态
            RegisterState(new IdleState(this, _inputService));
            RegisterState(new MoveState(this, _inputService));
            RegisterState(new CombatAttackState(this, _inputService));
            RegisterState(new CombatIdleState(this, _inputService));
            RegisterState(new CombatMoveState(this, _inputService));
            // ...其他状态

            ChangeState<IdleState>();
        }

        void Update()
        {
            _currentFrameInputState = _inputService.GetCurrentState();
            CurrentState?.HandleInput(_currentFrameInputState);
            CurrentState?.Update();
        }

        public void ChangeState<T>() where T : BaseState
        {
            if (_states.TryGetValue(typeof(T), out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();

                // 立刻同步输入, 避免状态切换时的输入丢失
                if (!_currentFrameInputState.Equals(default(InputState)))
                {
                    CurrentState?.HandleInput(_currentFrameInputState);
                }
            }
        }

        private void RegisterState(BaseState state)
        {
            _states[state.GetType()] = state;
        }

        public void OnAnimationEndEvent()
        {
            CurrentState?.OnAnimationEndEvent();
        }

        public bool CheckAnimationComplete()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                return true;
            }
            return false;
        }
        public bool CheckAnimationName(string name)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(name))
            {
                return true;
            }
            return false;
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }
    }
}
