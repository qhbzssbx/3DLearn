using System;
using Sirenix.OdinInspector;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class MoveState : BaseState
{
    private bool moving = false;
    private Vector3 nextTargetRotate;
    private Vector3 nextTargetPosition; // 提前缓存下一个目标点, 避免状态中断
    private Vector3 curPosition;

    private float startTime = 0;
    private float endTime = 0;

    public MoveState(CharacterStateMachine machine, IInputService inputService) : base(machine, inputService) { 
        nextTargetPosition = Vector3.zero;
    }

    public override void Enter()
    {
        Machine.Animator.SetBool("walk", true);
        Machine.Animator.speed = 1.5f;
        // Machine.Animator.SetTrigger("walk");
    }
    public override void Exit()
    {
        Machine.Animator.SetBool("walk", false);
        Machine.Animator.speed = 1;
    }

    int checkYFrameCD = 2;
    int checkYFrameIndex = 0;
    public override void Update()
    {
        if (moving)
        {
            var curTrsPos = Machine.transform.position;
            curTrsPos.y = 0;
            var curFrameMovePoint = Vector3.MoveTowards(curTrsPos, curPosition, Time.deltaTime * 1.5f);
            curFrameMovePoint.y = MapUtility.DetectSlope(curFrameMovePoint);
            Machine.transform.position = curFrameMovePoint;

            curFrameMovePoint.y = 0;
            if (Vector3.Distance(curFrameMovePoint, curPosition) < 0.01 )
            {
                // endTime = Time.time;
                curPosition.y = MapUtility.DetectSlope(curPosition);
                Machine.transform.position = curPosition;
                if (nextTargetPosition != Vector3.zero)
                {
                    // LogUtility.Log("移动消耗时间: " + (endTime - startTime), Color.yellow);
                    curPosition = nextTargetPosition;
                    // am.SetAnimSpeed(Vector3.Distance(Machine.transform.position, nextPos) / (Vector3.Distance(Machine.transform.position, nextPos)/maxSpeed) / maxSpeed);
                    Machine.transform.localRotation = Quaternion.Euler(nextTargetRotate);
                    // startTime = Time.time;
                    ClearCacheNextPos();
                }
                else
                {
                    ExitMove();
                    moving = false;
                }
            }
        }
    }

    public void UpdateMove(Vector2Int input)
    {
        ClearCacheNextPos();
        if (!moving)
        {
            // var dis = CellUtility.CellPosToWorldPosition(transAmt, rotAmt);
            var curPos = CellUtility.WorldToGrid(Machine.transform.position); // 获取当前世界格子坐标
            curPosition = CellUtility.GridToWorld(curPos.x + input.x, curPos.y + input.y); // 加上传入方向坐标后转换为目标世界坐标
            // curPosition.y = MapUtility.DetectSlope(curPosition); // 获取目标点的Y轴坐标
            moving = true;

            var rotate = CellUtility.GetDir(input.x, input.y); // 获取目标位置相对当前位置的旋转角度
            Machine.transform.localRotation = Quaternion.Euler(rotate.x, rotate.y, rotate.z);
            // startTime = Time.time;
            // am.SetAnimSpeed(1 * (Vector3.Distance(Machine.transform.position, nextPos)/Machine.Config.WalkSpeed));
        }
        else
        {
            // LogUtility.Log("!!!!!!");
            // LogUtility.Log("input: " + input);

            var nextGridPos = CellUtility.WorldToGrid(curPosition);
            nextTargetPosition = CellUtility.GridToWorld(nextGridPos.x + input.x, nextGridPos.y + input.y);
            // nextTargetPosition.y = MapUtility.DetectSlope(curPosition); // 获取目标点的Y轴坐标

            nextTargetRotate = CellUtility.GetDir(input.x, input.y);
        }
    }

    public override void HandleInput(InputState input)
    {
        if (input.IsAttackPressed)
        {
            ForceDragToTarget();
            Machine.ChangeState<CombatAttackState>();
            return;
        }

        // 战斗模式专属输入处理
        if (input.HasMoveInput)
        {
            UpdateMove(input.MoveAxis);
        }
        else
        {
            ClearCacheNextPos();
        }
    }

    protected virtual void ExitMove()
    {
        Machine.ChangeState<IdleState>();
    }
    private void ClearCacheNextPos()
    {
        nextTargetPosition = Vector3.zero;
        nextTargetRotate = Vector3.zero;
    }

    // 强制拖拽玩家到目标点, 并清楚缓存指令
    private void ForceDragToTarget()
    {   
        curPosition.y = MapUtility.DetectSlope(curPosition);
        Machine.transform.position = curPosition;
        curPosition = Vector3.zero;
        ClearCacheNextPos();
        moving = false;
    }

}

// public class MoveState : BaseState
// {
//     private bool moving = false;
//     private Vector3 nextTargetRotate;
//     private Vector3 nextTargetPosition; // 提前缓存下一个目标点, 避免状态中断
//     private Vector3 curPosition;
//     private Vector3 curMovement;
//     private Vector3 nextMovement;
//     private Vector3 curDir;
//     private Vector3 nextDir;
//     private CharacterController controller;

//     private float startTime = 0;
//     private float endTime = 0;

//     public MoveState(CharacterStateMachine machine, IInputService inputService) : base(machine, inputService)
//     {
//         nextTargetPosition = Vector3.zero;
//         controller = machine.GetComponent<CharacterController>();
//     }

//     public override void Enter()
//     {
//         Machine.Animator.SetBool("walk", true);
//         Machine.Animator.speed = 1.5f;
//         // Machine.Animator.SetTrigger("walk");
//     }
//     public override void Exit()
//     {
//         Machine.Animator.SetBool("walk", false);
//         Machine.Animator.speed = 1;
//     }

//     public override void Update()
//     {
//         if (moving)
//         {
//             float step = Config.WalkSpeed * Time.deltaTime;
//             Vector3 movement = Vector3.ClampMagnitude(curPosition, step);
//             controller.Move(movement);
//             curPosition -= movement;
//             if (curPosition.magnitude <= 0.01f)
//             {
//                 curPosition = Vector3.zero;
//                 if (nextTargetPosition != Vector3.zero)
//                 {
//                     curPosition = nextTargetPosition;
//                     Machine.transform.localRotation = Quaternion.Euler(nextTargetRotate);
//                 }
//                 else
//                 {
//                     ExitMove();
//                     moving = false;
//                 }
//             }
//         }
//     }

//     public void UpdateMove(Vector2Int input)
//     {
//         ClearCacheNextPos();
//         if (!moving)
//         {
//             LogUtility.Log(">>>>>>>>>");
//             var dir = new Vector3(input.x, 0, input.y);
//             curDir = dir.normalized;



//             // var curPos = CellUtility.WorldToGrid(Machine.transform.position); // 获取当前世界格子坐标
//             // curPosition = CellUtility.GridToWorld(curPos.x + input.x, curPos.y + input.y); // 加上传入方向坐标后转换为目标世界坐标
//             LogUtility.Log("curDir: " + curDir);
//             curPosition =curDir;
//             // curPosition = Machine.transform.TransformDirection(curDir);
//             LogUtility.Log("curPosition: " + curPosition);
//             curPosition *= CalculateMoveDistance(input);
//             moving = true;
//             LogUtility.Log("input: " + input);
            
//             var rotate = CellUtility.GetDir(input.x, input.y); // 获取目标位置相对当前位置的旋转角度
//             Machine.transform.localRotation = Quaternion.Euler(rotate.x, rotate.y, rotate.z);
//             // startTime = Time.time;
//             // am.SetAnimSpeed(1 * (Vector3.Distance(Machine.transform.position, nextPos)/Machine.Config.WalkSpeed));
//         }
//         else
//         {
//             var dir = new Vector3(input.x, 0, input.y);
//             nextDir = dir.normalized;
//             nextTargetPosition = nextDir;
//             nextTargetPosition *= CalculateMoveDistance(input);

//             nextTargetRotate = CellUtility.GetDir(input.x, input.y);
//         }
//     }

//     float CalculateMoveDistance(Vector2Int direction)
//     {
//         // 确保方向是八向之一
//         // direction.x = Mathf.Clamp(direction.x, -1, 1);
//         // direction.y = Mathf.Clamp(direction.y, -1, 1);
//         // 计算距离
//         float magnitude = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
//         return magnitude * CellUtility.CELLSIZE;
//     }

//     public override void HandleInput(InputState input)
//     {
//         if (input.IsAttackPressed)
//         {
//             ForceDragToTarget();
//             Machine.ChangeState<CombatAttackState>();
//             return;
//         }

//         // 战斗模式专属输入处理
//         if (input.HasMoveInput)
//         {
//             UpdateMove(input.MoveAxis);
//         }
//         else
//         {
//             ClearCacheNextPos();
//         }
//     }

//     protected virtual void ExitMove()
//     {
//         Machine.ChangeState<IdleState>();
//     }
//     private void ClearCacheNextPos()
//     {
//         nextTargetPosition = Vector3.zero;
//         nextTargetRotate = Vector3.zero;
//     }

//     // 强制拖拽玩家到目标点, 并清楚缓存指令
//     private void ForceDragToTarget()
//     {
//         curPosition.y = MapUtility.DetectSlope(curPosition);
//         Machine.transform.position = curPosition;
//         curPosition = Vector3.zero;
//         ClearCacheNextPos();
//         moving = false;
//     }

// }