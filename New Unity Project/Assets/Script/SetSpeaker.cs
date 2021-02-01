using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Script
{
    public class SetSpeaker : MonoBehaviour
    {
        // Drag and drop your Dialogue Runner into this variable.
        public DialogueRunner dialogueRunner;
        
        public TextMeshProUGUI text;

        public void Awake() 
        {
            dialogueRunner.AddCommandHandler("speaker_name", ShowSpeakerName);
        }
        
        private void ShowSpeakerName(string[] parameters)
        {
            text.text = parameters[0];
        }
    }
}
