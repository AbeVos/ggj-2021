using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStorage : Yarn.Unity.VariableStorageBehaviour
{
    private Person person;

    public void Awake()
    {
        person = GetComponent<Person>();
    }

    public override void SetValue(string variableName, Yarn.Value value)
    {
        Debug.Log(variableName);
        if (variableName == "$emotion")
        {
            Debug.Log("Change emotion to " + value.AsString);
            person.CurrentEmotion = value.AsString;
        }
    }

    public override Yarn.Value GetValue(string variableName)
    {
        if (variableName.Contains("tag"))
        {
            return new Yarn.Value(
                "<b>"
                + variableName.Split(char.Parse("_"))[1]
                + "</b>"
            );
        }
        return new Yarn.Value("<b>" + variableName + "</b>");
    }

    public override void ResetToDefaults()
    {
    }
}
