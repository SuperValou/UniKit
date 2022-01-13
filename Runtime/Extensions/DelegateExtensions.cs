using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class DelegateExtensions
    {
        public static void SafeInvoke(this Action actionToInvoke)
        {
            if (actionToInvoke == null)
            {
                return;
            }

            var invocationList = actionToInvoke.GetInvocationList();
            foreach (var delegateToInvoke in invocationList)
            {
                try
                {
                    ((Action) delegateToInvoke).Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception occured during invocation of method '{delegateToInvoke.Method.Name}' " +
                                   $"of object '{delegateToInvoke.Target}': {e}");
                }
            }
        }

        public static void SafeInvoke<TEventArg>(this Action<TEventArg> actionToInvoke, TEventArg arg)
        {
            if (actionToInvoke == null)
            {
                return;
            }

            var invocationList = actionToInvoke.GetInvocationList();
            foreach (var delegateToInvoke in invocationList)
            {
                try
                {
                    ((Action<TEventArg>) delegateToInvoke).Invoke(arg);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception occured during invocation of method '{delegateToInvoke.Method.Name}' " +
                                   $"of object '{delegateToInvoke.Target}': {e}");
                }
            }
        }

        public static void SafeInvoke<TEventArg1, TEventArg2>(this Action<TEventArg1, TEventArg2> actionToInvoke,
            TEventArg1 arg1, TEventArg2 arg2)
        {
            if (actionToInvoke == null)
            {
                return;
            }

            var invocationList = actionToInvoke.GetInvocationList();
            foreach (var delegateToInvoke in invocationList)
            {
                try
                {
                    ((Action<TEventArg1, TEventArg2>) delegateToInvoke).Invoke(arg1, arg2);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception occured during invocation of method '{delegateToInvoke.Method.Name}' " +
                                   $"of object '{delegateToInvoke.Target}': {e}");
                }
            }

        }

        public static void SafeInvoke<TEventArg1, TEventArg2, TEventArg3>(this Action<TEventArg1, TEventArg2, TEventArg3> actionToInvoke,
            TEventArg1 eventArg1, TEventArg2 eventArg2, TEventArg3 eventArg3)
        {
            if (actionToInvoke == null)
            {
                return;
            }

            var invocationList = actionToInvoke.GetInvocationList();
            foreach (var delegateToInvoke in invocationList)
            {
                try
                {
                    ((Action<TEventArg1, TEventArg2, TEventArg3>) delegateToInvoke).Invoke(eventArg1, eventArg2, eventArg3);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception occured during invocation of method '{delegateToInvoke.Method.Name}' " +
                                   $"of object '{delegateToInvoke.Target}': {e}");
                }
            }
        }
    }
}