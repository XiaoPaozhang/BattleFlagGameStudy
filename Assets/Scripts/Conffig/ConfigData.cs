using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleFlagGameStudy
{
  /// <summary>
  /// 读取csv格式数据表
  /// </summary>
  public class ConfigData
  {
    /// <summary>
    /// 每个数据表存储的数据到字典中,key是字典id,value是每一行数据
    /// </summary>
    private Dictionary<int, Dictionary<string, string>> datas;
    public string fileName;//配置表文件名
    public ConfigData(string fileName)
    {
      this.fileName = fileName;
      this.datas = new Dictionary<int, Dictionary<string, string>>();
    }

    public TextAsset LoadFile()
    {
      return Resources.Load<TextAsset>($"Data/{fileName}");
    }

    //根据csv文件内容解析数据
    public void Load(string txt)
    {
      //完整文本数组拆分每一行
      string[] dataArr = txt.Split('\n');//换行

      //获取标题行内容,数组拆分单元格
      string[] titleArr = dataArr[0].Trim().Split(',');//标题行

      //内容从第三行开始读取
      for (int i = 2; i < dataArr.Length; i++)
      {
        //每一行数据
        string[] rowArr = dataArr[i].Trim().Split(',');
        Dictionary<string, string> tempArr = new Dictionary<string, string>();
        for (int j = 0; j < titleArr.Length; j++)
        {
          tempArr.Add(titleArr[j], rowArr[j]);
        }
        datas.Add(int.Parse(tempArr["Id"]), tempArr);
      }
    }

    public Dictionary<string, string> GetDataById(int id)
    {
      //根据id获取数据
      datas.TryGetValue(id, out Dictionary<string, string> data);
      return data;
    }

    public Dictionary<int, Dictionary<string, string>> GetLines()
    {
      return datas;
    }
  }
}
