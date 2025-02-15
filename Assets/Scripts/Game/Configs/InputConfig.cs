using UnityEngine;
using Rewired;

[CreateAssetMenu(menuName = "Configs/Input Config", fileName = "InputConfig.asset")]
public class InputConfig : ScriptableObject
{
    [Header("Rewired 动作映射")]
    [Tooltip("Rewired中定义的移动水平轴名称")]
    public string MoveHorizontalAction = "MoveHorizontal";
    
    [Tooltip("Rewired中定义的移动垂直轴名称")] 
    public string MoveVerticalAction = "MoveVertical";
    
    [Tooltip("Rewired中定义的攻击按钮名称")]
    public string AttackAction = "Attack";
    
    [Tooltip("Rewired中定义的战斗模式切换按钮名称")]
    public string CancelAttackAction = "CancelAttack";

    [Header("输入预处理")]
    [Range(0f, 0.9f)]
    [Tooltip("移动输入死区阈值")]
    public float MoveDeadzone = 0.1f;
    
    [Tooltip("连击有效窗口时间（秒）")]
    public float ComboWindowDuration = 0.3f;
    
    [Tooltip("蓄力攻击最小保持时间")]
    public float MinChargeDuration = 1.5f;

    [Header("输入缓冲")]
    [Tooltip("输入缓冲帧数（60FPS下0.1秒=6帧）")]
    public int InputBufferFrames = 6;

    [Header("高级设置")]
    [Tooltip("是否启用输入预测")]
    public bool EnableInputPrediction = true;
    
    [Tooltip("网络补偿最大延迟（秒）")]
    public float MaxNetworkLatency = 0.2f;

    // 获取所有Rewired动作名称的只读属性
    public string[] RewiredActionNames => new[] {
        MoveHorizontalAction,
        MoveVerticalAction,
        AttackAction,
        CancelAttackAction
    };
}