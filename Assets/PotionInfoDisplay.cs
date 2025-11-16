using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
public class PotionInfoDisplay : MonoBehaviour
{
    public GameObject potionBookPanel;

    public TextMeshProUGUI potionNameText;
    public TextMeshProUGUI potionDescriptionText;
    public Image potionIconImage;
    public Image expressionImage;
    public Image shadowOverlay;
    public Image shadowOverlay1;
    private PotionRecipes currentPotion = null;
    private void Start()
    {
        if (potionBookPanel != null)
        {
            potionBookPanel.SetActive(false);
        }
    }
    public void ShowInfo(PotionRecipes potion, int index, bool IsDiscovered)
    {
        if (potion == null)
        {
            Debug.LogWarning("[ShowInfo] Recieved null potion.");
            return;
        }
        currentPotion = potion;
        if (IsDiscovered)
        {
            potionNameText.text = $"#{index + 1}: {potion.potionName}";
            potionDescriptionText.text = potion.potionDescription;
            potionIconImage.sprite = potion.potionIcon;
            expressionImage.sprite = potion.emotionIcon;
            shadowOverlay.gameObject.SetActive(false);
            shadowOverlay1.gameObject.SetActive(false);
        }
        else
        {
            potionNameText.text = $"#{index + 1}: ???";
            potionDescriptionText.text = "This potion is, as of yet, undiscovered.";
            potionIconImage.sprite = potion.potionIcon;
            expressionImage.sprite = potion.emotionIcon;
            shadowOverlay.gameObject.SetActive(true);
            shadowOverlay1.gameObject.SetActive(true);
        }
        if (potionBookPanel != null && !potionBookPanel.activeSelf)
        {
            potionBookPanel.SetActive(true);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(potionNameText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(potionDescriptionText.rectTransform);
    }


    public void hideInfo()
    {
        if (potionBookPanel != null)
        {
            potionBookPanel.SetActive(false);
            currentPotion = null;
        }
    }

    public bool IsVisible()
    {
        return potionBookPanel != null && potionBookPanel.activeSelf;
    }

    public PotionRecipes GetCurrentPotion()
    {
        return currentPotion;
    }
}
