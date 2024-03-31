using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 命令管理器
  /// </summary>
  public class CommandManager
  {
    private Queue<BaseCommand> willDoCommands;//将要执行的命令队列
    private Stack<BaseCommand> unDoCommands;//撤销命令 栈
    private BaseCommand currentCommand;//当前执行的命令
    public CommandManager()
    {
      willDoCommands = new Queue<BaseCommand>();
      unDoCommands = new Stack<BaseCommand>();
    }

    //是否在执行命令
    public bool IsRunningCommand => currentCommand != null;

    public void AddCommand(BaseCommand cmd)
    {
      willDoCommands.Enqueue(cmd);//添加到队列
      unDoCommands.Push(cmd);//添加到撤销栈
    }

    public void Update(float dt)
    {
      if (currentCommand == null)
      {
        if (willDoCommands.Count > 0)
        {
          currentCommand = willDoCommands.Dequeue();
          currentCommand.Do();
        }
      }
      else
      {
        if (currentCommand.Update(dt))
        {
          currentCommand = null;
        }
      }
    }

    public void Clear()
    {
      willDoCommands.Clear();
      unDoCommands.Clear();
      currentCommand = null;
    }

    //撤销上一个命令
    public void UnDo()
    {
      if (unDoCommands.Count > 0)
      {
        unDoCommands.Pop().UnDo();
      }
    }
  }
}
