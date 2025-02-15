using UnityEngine;

[System.Serializable]
public struct InputState
{
    // 基础输入
    public Vector2Int MoveAxis;
    public Vector2 OriginalMoveAxis;
    public bool IsAttackPressed;
    public bool IsCancelAttackPressed;
    // public bool IsAttackHeld; // 检测长按
    // public bool IsJumpPressed;
    // public bool IsCombatToggled;

    // 预处理标记
    public bool InComboWindow;
    public bool IsChargingAttack;

    // 时间相关数据
    public float AttackHoldDuration;

    // 输入缓冲（最近3帧）
    public InputState[] BufferedInputs;

    // 快速访问方法
    public bool HasMoveInput => MoveAxis.magnitude > 0.1f;
    public bool IsBufferedAttack => CheckBufferedInput("Attack", 0.3f);

    private bool CheckBufferedInput(string action, float withinSeconds)
    {
        foreach (var input in BufferedInputs)
        {
            if (input.IsAttackPressed && Time.time - input.AttackHoldDuration <= withinSeconds)
                return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"MoveAxis: {MoveAxis}, IsAttackPressed: {IsAttackPressed}, IsCancelAttackPressed: {IsCancelAttackPressed}";
    }
}