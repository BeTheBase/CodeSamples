using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private Dictionary<string, object> playerVariables = new Dictionary<string, object>();

    private void Awake()
    {
        DIContainer.Register<PlayerStateManager>(this);
        DontDestroyOnLoad(gameObject);
    }

    // Sets a variable (for booleans, integers, and strings)
    public void SetVariable(string key, object value)
    {
        if (playerVariables.ContainsKey(key))
        {
            if (value is int && playerVariables[key] is int)
            {
                playerVariables[key] = (int)playerVariables[key] + (int)value;
            }
            else
            {
                playerVariables[key] = value;
            }
        }
        else
        {
            
            playerVariables.Add(key, value);
        }

        Debug.Log("VARIABLED CHANGED: " + (key, value));
        DialogueEvents.InvokeVariableChanged(key, playerVariables[key]);
    }

    // Gets a variable
    public object GetVariable(string key)
    {
        if (playerVariables.ContainsKey(key))
        {
            return playerVariables[key];
        }
        return null;
    }

    // Checks if a condition is met
    public bool CheckCondition(string key, string operatorType, object value)
    {
        if (!playerVariables.ContainsKey(key)) return false;

        object storedValue = playerVariables[key];

        if (storedValue is int && value is int)
        {
            int intStored = (int)storedValue;
            int intValue = (int)value;

            switch (operatorType)
            {
                case "==": return intStored == intValue;
                case "!=": return intStored != intValue;
                case ">": return intStored > intValue;
                case "<": return intStored < intValue;
                case ">=": return intStored >= intValue;
                case "<=": return intStored <= intValue;
            }
        }
        else if (storedValue is bool && value is bool)
        {
            return (bool)storedValue == (bool)value;
        }
        else if (storedValue is string && value is string)
        {
            return (string)storedValue == (string)value;
        }

        return false;
    }
}
