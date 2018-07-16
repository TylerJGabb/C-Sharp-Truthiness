using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Truthiness
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public static class ObjectExtensions
    {

        public static bool IsTruthy(this object o)
        {
            if (o == null) return false;
            var t = o.GetType();
            if (t.IsPrimitive)
            {
                if( t.IsValueType) return DoCascadingTry(new List<Func<bool>>()
                {
                    () => Math.Abs(Convert.ToDouble(o)) >= double.Epsilon,
                    () => Convert.ToChar(o) > 0,
                });
            }
            else
            {
                if (t == typeof(string)) return t.ToString().Any();

            }
                
            return false;
        }

        private static T DoCascadingTry<T>(IEnumerable<Func<T>> funcs)
        {
            foreach (var f in funcs)
            {
                try
                {
                    return f.Invoke();
                }
                catch
                {
                    // ignored
                }
            }
            return default(T);
        }
    }
}
