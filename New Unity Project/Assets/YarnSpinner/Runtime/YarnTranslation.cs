using System;
using UnityEngine;

namespace YarnSpinner.Runtime
{
    [Serializable]
    public class YarnTranslation
    {
        public YarnTranslation(string LanguageName, TextAsset Text = null) {
            languageName = LanguageName;
            text = Text;
        }
        public string languageName;
        public TextAsset text;
    }
}
