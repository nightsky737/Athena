using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientInfoDisplay : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image ingredientImage;
    private IngredientInfo currentIngredient = null;
   private void Start()
    {
        infoPanel.SetActive(false);
    }

    public void showInfo(IngredientInfo data)
    {
        currentIngredient = data;
        nameText.text = data.IngredientName;
        descriptionText.text = data.Ingredientdescription;
        ingredientImage.sprite = data.IngredientIcon;

        infoPanel.SetActive(true);
    }

    public void hideInfo()
    {
        infoPanel.SetActive(false);
        currentIngredient = null;
    }

    public bool IsVisible()
    {
        return infoPanel.activeSelf;
    }
    public IngredientInfo GetCurrentIngredient()
    {
        return currentIngredient;
    }
}