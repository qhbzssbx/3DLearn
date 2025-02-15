using QFramework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : AbstractModel
{
    private PlayerData players;

    protected override void OnInit()
    {
        
    }

    public void SetPlayerID(string playerID)
    {
        players.PlayerID = playerID;
    }

    public string GetPlayerID()
    {
        return players.PlayerID;
    }

    public void SetName(string name)
    {
        players.Name = name;
    }

    public string GetName()
    {
        return players.Name;
    }

    public void SetLevel(int level)
    {
        players.Level = level;
    }

    public int GetLevel()
    {
        return players.Level;
    }

    public void SetPosition(Vector3 position)
    {
        players.Position = position;
    }

    public Vector3 GetPosition()
    {
        return players.Position;
    }
}

public class PlayerData
{
    public string PlayerID { get; set; }  // 玩家唯一标识
    public string Name { get; set; }      // 玩家名称
    public int Level { get; set; }        // 玩家等级
    public Vector3 Position { get; set; } // 玩家位置
    public PlayerEquip[] EquipList { get; set; } // 装备列表

    public PlayerData()
    {
        EquipList = new PlayerEquip[12];
    }
}

public class PlayerEquip
{
    public string EquipID { get; set; } // 装备唯一标识
    public string Name { get; set; }    // 装备名称
    public int Level { get; set; }      // 装备等级
    public int CellIndex { get; set; }  // 装备位置
}