using DialogueSystem.Core;
using System.Collections.Generic;

namespace DialogueSystem.Infra
{
    public class InMemoryPlayerState : IPlayerState
    {
        private readonly Dictionary<string, object> _variables = new();

        public T Get<T>(string key)
        {
            return _variables.TryGetValue(key, out var value) ? (T)value : default;
        }

        public void Set<T>(string key, T value)
        {
            _variables[key] = value;
        }

        public bool Check(string key, string op, object value)
        {
            if (!_variables.ContainsKey(key)) return false;

            var stored = _variables[key];

            if (stored is int a && value is int b)
            {
                return op switch
                {
                    "==" => a == b,
                    "!=" => a != b,
                    ">"  => a > b,
                    "<"  => a < b,
                    ">=" => a >= b,
                    "<=" => a <= b,
                    _ => false
                };
            }

            if (stored is bool ba && value is bool bb)
            {
                return ba == bb;
            }

            if (stored is string sa && value is string sb)
            {
                return sa == sb;
            }

            return false;
        }
    }
}