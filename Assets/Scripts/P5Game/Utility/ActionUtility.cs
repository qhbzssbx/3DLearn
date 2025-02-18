using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionUtility
{
    private static ActionProcessor processor;
    public static ActionProcessor Processor
    {
        get
        {
            if (processor)
            {
                return processor;
            }
            else
            {
                processor = ActionProcessor.construct("Action");
                return processor;
            }
        }
    }
}
