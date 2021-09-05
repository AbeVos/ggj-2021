using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PhoneBackButton : MonoBehaviour
    {
        private SocialMedia socialMedia;
        private TextMeshProUGUI headerText;

        protected void Awake()
        {
            socialMedia = GameObject.Find("Content").GetComponent<SocialMedia>();
            headerText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
        }

        //todo: netjes met events afhandelen
        public void OnClickHandle()
        {
            socialMedia.Filter = string.Empty;
            headerText.text = "Kwettr";
            GetComponent<Image>().enabled = false;
        }
    }
}
