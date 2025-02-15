using Rewired;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance ;

    // The Rewired player id of this character
    public int playerId = 0;
    private Player rewiredPlayerIpt; // The Rewired Player

    private float moveJoystickThreshold = 0.2f;//避免轻微摇杆偏移也被当成移动
    private Vector3 rVec;//摄像机的右向向量
    private Vector3 fVec;//摄像机的前向向量

    // 输入事件
    public event System.Action<Vector2Int, Vector2> OnMove;
    public event System.Action OnMoveStop;
    public event System.Action OnSit;
    public event System.Action OnAttack;
    public event System.Action OnInteract;

    // private InputSystem() {
    //     rewiredPlayerIpt = ReInput.players.GetPlayer(playerId);
    // }

    void Awake() {
        rewiredPlayerIpt = ReInput.players.GetPlayer(playerId);
        Instance = this;
    }

    private void Start() {
        rVec = Camera.main.transform.right; //获取摄像机的右向向量
        Vector3 tempV = Camera.main.transform.forward; //获取摄像机的前向向量
        tempV.y = 0; //将高度设为0, 使其只在水平面上运动
        tempV.Normalize();//归一化, 使其长度为1, 代表方向
        fVec = tempV;
    }
    void FixedUpdate()
    {
        int h = (int)rewiredPlayerIpt.GetAxis("MoveH");
        int v = (int)rewiredPlayerIpt.GetAxis("MoveV");
        // if (h != 0 || v != 0)
        // {
        //     // 移动输入
        //     Vector2Int moveInput = new Vector2Int(h, v);
        //     Debug.Log("Move Input: " + moveInput);
        //     OnMove?.Invoke(moveInput);
        // }
        // transAmt = player.GetAxis("MoveH");
        // rotAmt = player.GetAxis("MoveV");

        int xDir = 0;
        int yDir = 0;

        // 如果绝对值大于阈值，就根据正负得到离散的方向(-1, 0, 1)
        if (Mathf.Abs(h) > moveJoystickThreshold) {
            xDir = (h > 0) ? 1 : -1;
        }
        if (Mathf.Abs(v) > moveJoystickThreshold) {
            yDir = (v > 0) ? 1 : -1;
        }

        var direction = GetInputDirection(h, v);
        if (xDir != 0 || yDir != 0)
        {
            OnMove?.Invoke(new Vector2Int(xDir, yDir), direction);
        }
        else
        {
            OnMoveStop?.Invoke();
        }

        // 其他操作
        if (Input.GetKeyDown(KeyCode.C)) OnSit?.Invoke();
        if (Input.GetKeyDown(KeyCode.Space)) OnAttack?.Invoke();
        if (Input.GetKeyDown(KeyCode.E)) OnInteract?.Invoke();
    }

    protected Vector3 GetInputDirection(float h, float v)
    {
        //前方
        Vector3 forward = Vector3.ProjectOnPlane(fVec, Vector3.up).normalized;
        //右方
        Vector3 right = Vector3.ProjectOnPlane(rVec, Vector3.up).normalized;
        //输入值
        var input = new Vector2(h, v);
        //返回值
        return input.x * right + input.y * forward;
    }
}