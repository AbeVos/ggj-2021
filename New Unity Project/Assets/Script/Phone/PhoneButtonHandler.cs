using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PhoneButtonHandler : MonoBehaviour
    {
        private SocialMedia socialMedia;
        private TextMeshProUGUI headerText;
        private ScrollRect scrollRect;

        private Image backButton;

        protected void Awake()
        {
            // todo: solve this less expensively
            socialMedia = GameObject.Find("Content").GetComponent<SocialMedia>();
            headerText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
            scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();

            backButton = GameObject.Find("BackButton").GetComponent<Image>();
        }

        public void OnClickBackButton()
        {
            ResetScreen();
        }

        public void OnClickHomeButton()
        {
            ResetScreen();
        }

        private void ResetScreen()
        {
            scrollRect.verticalNormalizedPosition = 1;
            socialMedia.Filter = string.Empty;
            headerText.text = "Kwettr";
            backButton.enabled = false;
        }
    }
}
