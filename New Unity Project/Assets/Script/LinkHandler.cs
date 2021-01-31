using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkHandler : MonoBehaviour, IPointerClickHandler
{
    private Camera pCamera;

    protected void Awake()
    {
        pCamera = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TMP_Text pTextMeshPro = GetComponent<TMP_Text>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLine(pTextMeshPro, Input.mousePosition, pCamera); ;  // If you are not in a Canvas using Screen Overlay, put your camera instead of null
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
            Debug.Log("clickyclick");
        }
    }
}
