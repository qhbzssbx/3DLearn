using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class CellUtility
{
    // public const float CELLSIZE = 1.25f;
    // public const float CELLSIZE = 1f;
    public static float CELLSIZE = 0.85f;

    public static Vector3 GridToWorld(int gridX, int gridY)
    {
        // float worldX = gridX * gridSize + gridSize / 2f + offset.x;
        // float worldZ = gridY * gridSize + gridSize / 2f + offset.z;
        float worldX = gridX * CELLSIZE + CELLSIZE / 2f;
        float worldZ = gridY * CELLSIZE + CELLSIZE / 2f;

        // 如果是在 2D（x-y 平面）:
        // return new Vector3(worldX, worldY, 0f);

        // 如果是在 3D（x-z 平面）:
        return new Vector3(worldX, 0f, worldZ);
    }
    public static Vector3 CellPosToWorldPosition(float gridX, float gridY)
    {
        // return new Vector3(x * CELLSIZE + CELLSIZE / 2, 0, y * CELLSIZE + CELLSIZE / 2);
        // float worldX = gridX * CELLSIZE + CELLSIZE / 2f + offset.x;
        float worldX = gridX * CELLSIZE + CELLSIZE / 2f;
        // float worldZ = gridY * CELLSIZE + CELLSIZE / 2f + offset.z;
        float worldZ = gridY * CELLSIZE + CELLSIZE / 2f;

        // 如果是在 2D（x-y 平面）:
        // return new Vector3(worldX, worldY, 0f);

        // 如果是在 3D（x-z 平面）:
        return new Vector3(worldX, 0f, worldZ);
    }

    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        if (worldPos == Vector3.zero)
        {
            return new Vector2Int(0, 0);
        }
        // 去掉偏移
        // float x = worldPos.x - offset.x;
        // float z = worldPos.z - offset.z;
        float x = worldPos.x;
        float z = worldPos.z;

        // 计算网格索引（考虑中心点对齐，先减去 halfSize 再除以 gridSize）
        float halfSize = CELLSIZE / 2f;

        int gridX = Mathf.RoundToInt((x - halfSize) / CELLSIZE);
        int gridY = Mathf.RoundToInt((z - halfSize) / CELLSIZE);

        return new Vector2Int(gridX, gridY);
    }

    public static Vector3 WorldPosToCellPos(Vector3 worldPos)
    {

        float x = worldPos.x;
        float z = worldPos.z;

        // 计算网格索引（考虑中心点对齐，先减去 halfSize 再除以 gridSize）
        float halfSize = CELLSIZE / 2f;

        int gridX = Mathf.FloorToInt((x - halfSize) / CELLSIZE);
        int gridY = Mathf.FloorToInt((z - halfSize) / CELLSIZE);

        return new Vector3(gridX, 0, gridY);
    }

    public static float ClosestMultipleOfTwo(float n)
    {
        if (n <= 0)
        {
            throw new ArgumentException("Input must be a positive number.");
        }

        // 找到最接近的 CELLSIZE 的倍数
        float lower = (n / CELLSIZE) * CELLSIZE;  // 向下取整的 CELLSIZE 的倍数
        float upper = (n % CELLSIZE == 0) ? lower : lower + CELLSIZE;  // 向上取整的 2 的倍数（如果 n 本身不是偶数）

        // 比较哪个更接近
        return (n - lower) <= (upper - n) ? lower : upper;
    }

    public static Vector3 GetDir(float x, float y)
    {
        if (x == 1 && y == 1)
        {
            return new Vector3(0, 45, 0);
        }
        else if (x == -1 && y == 1)
        {
            return new Vector3(0, -45, 0);
        }
        else if (x == 0 && y == 1)
        {
            return new Vector3(0, 0, 0);
        }
        else if (x == 0 && y == -1)
        {
            return new Vector3(0, 180, 0);
        }
        else if (x == 1 && y == -1)
        {
            return new Vector3(0, 135, 0);
        }
        else if (x == 1 && y == 0)
        {
            return new Vector3(0, 90, 0);
        }
        else if (x == -1 && y == 0)
        {
            return new Vector3(0, -90, 0);
        }
        else if (x == -1 && y == -1)
        {
            return new Vector3(0, 225, 0);
        }

        return new Vector3(0, 0, 0);
    }


    /// <summary>
    /// 将指定transform的位置吸附到最近的格子中心点。
    /// </summary>
    /// <param name="transform">要吸附位置的transform</param>
    /// <param name="gridSize">单个格子的边长</param>
    /// <param name="offset">网格相对世界原点的偏移量</param>
    public static void SnapToGridCenter(Transform transform)
    {
        // 原始世界坐标
        Vector3 worldPos = transform.position;

        // 去掉网格的偏移，得到“局部网格”坐标
        // (假设地面在 x-z 平面：pos.y 可保持原始高度或固定为0)
        // float localX = worldPos.x - offset.x;
        // float localZ = worldPos.z - offset.z;
        float localX = worldPos.x;
        float localZ = worldPos.z;

        // 计算所在格子索引(以中心对齐，所以先除以 gridSize 再四舍五入)
        int gridIndexX = Mathf.RoundToInt(localX / CELLSIZE);
        int gridIndexZ = Mathf.RoundToInt(localZ / CELLSIZE);

        // 计算出格子中心点的世界坐标 (再加回 offset)
        // float snappedX = gridIndexX * CELLSIZE + CELLSIZE / 2f + offset.x;
        // float snappedZ = gridIndexZ * CELLSIZE + CELLSIZE / 2f + offset.z;
        float snappedX = gridIndexX * CELLSIZE + CELLSIZE / 2f;
        float snappedZ = gridIndexZ * CELLSIZE + CELLSIZE / 2f;

        // 更新 transform.position
        // 如果想保持物体当前的 y 值，可以保留 worldPos.y；如果始终贴地，则可设为 0
        // transform.position = new Vector3(snappedX, worldPos.y, snappedZ);
        transform.position = new Vector3(snappedX, 0, snappedZ);
    }
}
