using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class MapObjectDataManager : AbstractModel
{
    protected override void OnInit()
    {

    }
}

public enum MapObjectType
{
    None,
    NPC,
    Player,
}

public class MapObjectData
{
    public string MapObjectID { get; set; }     // 地图对象唯一标识
    public MapObjectType ObjType { get; set; }  // 地图对象类型
    public string Name { get; set; }            // 地图对象名称
    public GameObject gameObject { get; set; }  // 地图对象位置
}
