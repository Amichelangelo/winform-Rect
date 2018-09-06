using System;
using System.Reflection;

namespace ConsoleThread
{
    class WeakDelegate
    {
        public WeakDelegate(WeakReference weakTarget, MethodInfo method)
        {
            WeakTarget = weakTarget;
            Method = method;
        }

        public object Invoke(params object[] parameters)
        {
            try
            {
                return Method.Invoke(WeakTarget.Target, parameters);
            }
            catch
            {
                return null;
            }
        }

        public WeakReference WeakTarget { get; }
        public MethodInfo Method { get; }
    }
}
