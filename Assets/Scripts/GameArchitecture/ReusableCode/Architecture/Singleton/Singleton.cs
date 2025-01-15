using System;
using System.Reflection;
using UnityEngine;

public class Singleton<T> where T:class, ISingleton
{
    private static T mInstance;
    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic); // 获取所有非公共构造函数
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0); // 筛选出无参构造函数
                if (ctor == null)
                {
                    throw new Exception("没有找到非公共的无参构造函数");
                }
                mInstance = ctor.Invoke(null) as T;
            }
            return mInstance;
        }
    }
    public virtual void Init()
    {
        throw new System.NotImplementedException();
    }
}
