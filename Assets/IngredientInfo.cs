using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Potion/Ingredient")]
public class IngredientInfo : ScriptableObject
{
    public string IngredientName;
    public Sprite IngredientIcon;
    [TextArea]
    public string Ingredientdescription;

}