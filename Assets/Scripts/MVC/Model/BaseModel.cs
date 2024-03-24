namespace BattleFlagGameStudy
{
  public class BaseModel
  {
    public BaseController controller;

    public BaseModel(BaseController ctrl)
    {
      this.controller = ctrl;
    }

    public BaseModel() { }
    public virtual void Init() { }
  }
}
