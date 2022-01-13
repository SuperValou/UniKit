using System;
using UnityEngine;

namespace Packages.UniKit.Runtime.Extensions
{
    public static class DelegateExtensions
    {
        /// <summary>
        /// Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to Debug.LogError instead.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
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

        /// <summary>
        /// Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to Debug.LogError instead.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// /// <param name="arg">Argument of the action.</param>
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

        /// <summary>
        /// Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to Debug.LogError instead.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="arg1">First argument of the action.</param>
        /// <param name="arg2">Second argument of the action.</param>
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

        /// <summary>
        /// Invoke all registered callbacks, even if some of them throw an exception. Exceptions get logged to Debug.LogError instead.
        /// </summary>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// /// <param name="arg1">First argument of the action.</param>
        /// <param name="arg2">Second argument of the action.</param>
        /// /// <param name="arg3">Third argument of the action.</param>
        public static void SafeInvoke<TEventArg1, TEventArg2, TEventArg3>(this Action<TEventArg1, TEventArg2, TEventArg3> actionToInvoke,
            TEventArg1 arg1, TEventArg2 arg2, TEventArg3 arg3)
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
                    ((Action<TEventArg1, TEventArg2, TEventArg3>) delegateToInvoke).Invoke(arg1, arg2, arg3);
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