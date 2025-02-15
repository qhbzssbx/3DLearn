using System.Collections.Generic;
using System.Linq;
using QFramework;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

public class RewiredInputService : AbstractSystem, IInputService
{
    [SerializeField] private InputConfig _inputConfig;
    private Player _rewiredPlayer;
    private InputState _currentState;
    // private Queue<InputState> _inputBuffer = new Queue<InputState>(5);

    protected override void OnInit()
    {
        _rewiredPlayer = ReInput.players.GetPlayer(0);
        ReInput.InputSourceUpdateEvent += OnInputUpdate;
    }

    private void OnInputUpdate()
    {
        // LogUtility.Log("OnInputUpdate", Color.yellow);
        // UpdateInputBuffer();
        UpdateCurrentState();
    }

    public InputState GetCurrentState() => _currentState;

    // 更新输入缓冲区
    public void UpdateInputBuffer()
    {
        // _inputBuffer.Enqueue(_currentState);
        // if (_inputBuffer.Count > 3) _inputBuffer.Dequeue();
        // _currentState.BufferedInputs = _inputBuffer.ToArray();
    }

    private void UpdateCurrentState()
    {

        // 采集原始输入
        _currentState = new InputState
        {
            OriginalMoveAxis = new Vector2(
                _rewiredPlayer.GetAxis(_inputConfig.MoveHorizontalAction),
                _rewiredPlayer.GetAxis(_inputConfig.MoveVerticalAction)
            ),
            IsAttackPressed = _rewiredPlayer.GetButtonDown(_inputConfig.AttackAction),
            IsCancelAttackPressed = _rewiredPlayer.GetButtonDown(_inputConfig.CancelAttackAction),
        };

        // 预处理逻辑
        ApplyDeadzone();
        // DetectComboWindow();
        // CalculateHoldDuration();
    }

    private void ApplyDeadzone()
    {
        // 设置死区, 避免微小抖动也被当成移动
        // 如果绝对值大于阈值，就根据正负得到离散的方向(-1, 0, 1)
        if (Mathf.Abs(_currentState.OriginalMoveAxis.x) > _inputConfig.MoveDeadzone) {
            _currentState.MoveAxis.x = (_currentState.OriginalMoveAxis.x > 0) ? 1 : -1;
        }
        else
        {
            _currentState.MoveAxis.x = 0;
        }
        if (Mathf.Abs(_currentState.OriginalMoveAxis.y) > _inputConfig.MoveDeadzone) {
            _currentState.MoveAxis.y = (_currentState.OriginalMoveAxis.y > 0) ? 1 : -1;
        }
        else
        {
            _currentState.MoveAxis.y = 0;
        }
    }



    // 是否处于连击窗口
    // private void DetectComboWindow()
    // {
    //     _currentState.InComboWindow = _currentState.IsAttackPressed && _inputBuffer.Any(i => i.IsAttackHeld);
    // }
    // 计算并记录玩家持续按住攻击键（Attack）的时间长度
    // private void CalculateHoldDuration()
    // {
    //     _currentState.AttackHoldDuration = (float)_rewiredPlayer.GetButtonTimePressed("Attack");
    // }
}