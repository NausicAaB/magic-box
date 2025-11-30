using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private float offsetY = 50f;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        HideTooltip();
    }

    public void ShowTooltip(string text, Vector3 worldPosition)
    {
        tooltipText.text = text;
        tooltipPanel.SetActive(true);
        
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        screenPos.y += offsetY;
        
        tooltipPanel.transform.position = screenPos;
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}