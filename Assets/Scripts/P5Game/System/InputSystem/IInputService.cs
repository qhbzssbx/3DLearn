
/// <summary>
/// 输入服务接口
/// </summary>
public interface IInputService
{
    InputState GetCurrentState();
    void UpdateInputBuffer();
}