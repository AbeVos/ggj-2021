using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStorage : Yarn.Unity.VariableStorageBehaviour
{
    public override void SetValue(string variableName, Yarn.Value value)
    {
        Debug.Log(variableName);
        if (variableName == "emotion")
        {
            Debug.Log("Change emotion to " + value);
        }
    }

    public override Yarn.Value GetValue(string variableName)
    {
        return new Yarn.Value(null);
    }

    public override void ResetToDefaults()
    {
    }
}
