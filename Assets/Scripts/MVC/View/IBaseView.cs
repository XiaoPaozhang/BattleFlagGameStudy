namespace BattleFlagGameStudy
{
  public interface IBaseView
  {
    bool IsInit();
    bool IsShow();
    void InitUI();
    void InitData();
    void Open(params object[] args);
    void Close(params object[] args);
    void DestroyView();
    //触发本模块事件
    void ApplyFunc(string eventName, params object[] args);
    //触发控制器事件
    void ApplyControllerFunc(int controllerKey, string eventName, params object[] args);
    void SetVisible(bool visible);
    int ViewId { get; set; }
    BaseController Controller { get; set; }
  }
}
