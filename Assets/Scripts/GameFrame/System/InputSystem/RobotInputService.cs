using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInputService : MonoBehaviour, IInputService
{
    private InputState _currentState;
    public InputState GetCurrentState()
    {
        var a = _currentState;
        _currentState.MoveAxis = Vector2Int.zero;
        return a;
    }

    public float _moveIntervalMax = 6f;
    public float _moveIntervalMin = 2f;
    public float _moveCd = 1f;

    public void UpdateInputBuffer()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveCd -= Time.deltaTime;
        if (_moveCd <= 0)
        {
            _currentState = new InputState
            {
                OriginalMoveAxis = new Vector2(
                    GetRondamDir(),
                    GetRondamDir()
                ),
                // IsAttackPressed = false,
                // IsCancelAttackPressed = false,
                // IsAttackHeld = _rewiredPlayer.GetButton(_inputConfig.AttackAction), // 长按攻击检测, 用不到注释掉
                // IsJumpPressed = _rewiredPlayer.GetButtonDown("Jump"), // 跳跃按键检测, 用不到注释掉
                // IsCombatToggled = _rewiredPlayer.GetButtonDown("CombatToggle") // 战斗状态切换按键检测, 用不到注释掉
            };
            _moveCd = Random.Range(_moveIntervalMin, _moveIntervalMax);
            _currentState.MoveAxis = new Vector2Int((int)_currentState.OriginalMoveAxis.x, (int)_currentState.OriginalMoveAxis.y);
        }
    }

    int GetRondamDir()
    {
        if (_moveCd <= 0)
        {
            int[] numbers = { -1, 0, 1 };
            int randomIndex = Random.Range(0, numbers.Length);
            return numbers[randomIndex];
        }
        return 0;
    }
}
