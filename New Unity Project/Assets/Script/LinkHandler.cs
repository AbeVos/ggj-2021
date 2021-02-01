using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkHandler : MonoBehaviour, IPointerClickHandler
{
    private Camera pCamera;
    private SocialMedia socialMedia;
    private TextMeshProUGUI headerText;
    private GameObject headerButton;

    protected void Awake()
    {
        pCamera = Camera.main;
        socialMedia = GameObject.Find("Content").GetComponent<SocialMedia>();
        headerText = GameObject.Find("HeaderText").GetComponent<TextMeshProUGUI>();
        headerButton = GameObject.Find("HeaderButton");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TMP_Text pTextMeshPro = GetComponent<TMP_Text>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, eventData.position, pCamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
            ApplyFilter(linkInfo.GetLinkID());
        }
    }

    public void ApplyFilter(string filter)
    {
        socialMedia.Filter = filter;
        headerText.text = filter;
        headerButton.GetComponent<Image>().enabled = true;
    }
}
