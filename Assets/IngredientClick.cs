using UnityEngine;
using UnityEngine.EventSystems;
public class IngredientClick : MonoBehaviour, IPointerClickHandler
{
    public IngredientInfo ingredientInfo;
    public IngredientInfoDisplay infoDisplay;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (infoDisplay.IsVisible() && infoDisplay.GetCurrentIngredient() == ingredientInfo)
        {
            infoDisplay.hideInfo();
        }
        else
        {
            infoDisplay.showInfo(ingredientInfo);
        }
    }
}