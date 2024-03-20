
using System;

public class Singleton<T> where T : new()
{
  private static readonly T instance = Activator.CreateInstance<T>();
  public static T Instance
  {
    get
    {
      return instance;
    }
  }

  public virtual void OnInit() { }

  public virtual void OnUpdate(float deltaTime) { }

  public virtual void OnDestroy() { }
}
