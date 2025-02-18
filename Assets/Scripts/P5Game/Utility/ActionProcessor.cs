using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class ActionProcessor : MonoSingleton<ActionProcessor>, IDisposable
{
    private class Callback
    {
        public Action action { get; set; }

        public Func<IEnumerator> routine { get; set; }

        public float delay { get; set; }
    }

    public static ActionProcessor construct(string name = "")
    {
        ActionProcessor processor = new GameObject().AddComponent<ActionProcessor>();
        processor.gameObject.name = name + "Processor";
        return processor;
    }

    private Queue<Callback> _actions = new Queue<Callback>();

    private ActionProcessor() { }

    public void Clear()
    {
        lock (this._actions)
        {
            this._actions.Clear();
            this.StopAllCoroutines();
        }
    }

    public void Dispose()
    {
        // Framework.D.Assert(this != ActionProcessor.One());
        GameObject.Destroy(this.gameObject);
    }

    public void dispatchAsync(Action action, float delay = 0f)
    {
        if (action == null)
        {
            return;
        }

        lock (this._actions)
        {
            this._actions.Enqueue(new Callback() { action = action, delay = delay });
        }
    }

    public void dispatchRoutine(Func<IEnumerator> routine, float delay = 0f)
    {
        if (routine == null)
        {
            return;
        }

        lock (this._actions)
        {
            this._actions.Enqueue(new Callback() { routine = routine, delay = delay });
        }
    }

    protected void Awake()
    {
        this.useGUILayout = false;
        GameObject.DontDestroyOnLoad(this);
    }

    // protected override void  OnApplicationQuit()
    // {
    //     GameObject.Destroy(this.gameObject);
    // }

    void LateUpdate()
    {
        lock (this._actions)
        {
            if (this._actions.Count > 0)
            {
                //if (this._actions.Count > 50)
                //{
                while (this._actions.Count > 0)
                {
                    this._actions.Dequeue().action();
                }
                //}
                //else
                //{
                //    //Delay.One().DelayDo()
                //    Callback callback = this._actions.Dequeue();
                //    StartCoroutine(doActionAsyncFunc(callback));
                //    //callback.action();
                //}
            }
        }
    }

    IEnumerator doActionAsyncFunc(Callback callback)
    {
        //if (callback.delay < 0f)
        //{
        //    yield return new WaitForFixedUpdate();
        //}
        //else if (callback.delay == 0f)
        //{
        //    yield return new WaitForSeconds(0.1f);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(callback.delay);
        //}
        yield return null;

        if (callback.routine != null)
        {
            yield return StartCoroutine(callback.routine());
        }
        else
        {
            callback.action();
        }
    }
}