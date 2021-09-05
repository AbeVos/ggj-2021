using UnityEngine;
using YarnSpinner.Runtime;

namespace Script
{
    public class VariableStorage : VariableStorageBehaviour
    {
        private Person person;

        public void Awake()
        {
            person = GetComponent<Person>();
        }

        public override void SetValue(string variableName, Yarn.Value value)
        {
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

            return variableName switch
            {
                "$mood" => new Yarn.Value(person.Mood),
                "$player_name" => new Yarn.Value("Quinn"),
                _ => new Yarn.Value("<b>" + variableName + "</b>")
            };
        }

        public override void ResetToDefaults()
        {
        }
    }
}
