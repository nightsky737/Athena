using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class CauldronManager : MonoBehaviour
{
    public PotionResultsScreen resultScreen;
    public PotionDatabase potionDatabase;
    public Image potionIcon;
    public Image emotionIcon;
    public TextMeshProUGUI potionName;
    public TextMeshProUGUI potionDescription;
    private List<IngredientInfo> currentIngredients = new List<IngredientInfo>();
    public GameObject cauldronObject;
    public GameObject restartButton;
    public IngredientInfoDisplay infoDisplay;
    public PotionDexUI potionDexUI;
    [SerializeField] private RectTransform backgroundImage;

    private void LateUpdate()
    {
        if (backgroundImage != null)
            backgroundImage.SetAsFirstSibling();
    }


    private void Start()
    {
        potionDexUI.ForceRefresh();
    }
    // Fir future reference, add null checks to code - will save me a lot of trouble
    private void Update()
    {
        if (resultScreen != null && resultScreen.resultPanel != null)
        {
            if (!resultScreen.resultPanel.activeSelf && Time.timeSinceLevelLoad < 5f)
            {
                Debug.LogWarning("{LIVE WATCH} resultPanel is OFF during first 5 seconds");
            }
        }
    }
    public void AddIngredient(IngredientInfo ingredient)
    {
        Debug.Log("Trying to add ingredient");
        currentIngredients.Add(ingredient);
        if (currentIngredients.Count == 2)
        {
            TryBrew();
        }
    }

    void TryBrew()
    {
        if (potionDatabase == null || potionDatabase.recipes == null)
        {
            Debug.LogWarning("Potion database or recipes list is null!");
            return;
        }

        Debug.Log($"Trying to brew with {currentIngredients.Count} ingredients and {potionDatabase.recipes.Count} recipes.");

        foreach (var recipe in potionDatabase.recipes)
        {
            if (Matches(recipe.ingredientA, recipe.ingredientB))
            {
                ShowResult(recipe);
                currentIngredients.Clear();
                return;
            }
        }
        Debug.Log("No matching recipe found.");
        currentIngredients.Clear();
    }

    bool Matches(IngredientInfo a, IngredientInfo b)
    {
        if (currentIngredients.Count < 2 || currentIngredients[0] == null || currentIngredients[1] == null || a == null || b == null)
        {
            Debug.LogWarning("Null or insufficient ingredients detected in Matches");
            return false;
        }

        //Debug.Log($"Comparing: {currentIngredients[0].IngredientName} + {currentIngredients[1].IngredientName}  with  {a.IngredientName} + {b.IngredientName}");
        return (currentIngredients[0] == a && currentIngredients[1] == b) || (currentIngredients[0] == b && currentIngredients[1] == a);
    }
    void ShowResult(PotionRecipes recipe)
    {
        // Register to the PotionDex
        if (PotionDex.existence != null)
        {
            PotionDex.existence.RegisterPotion(recipe);
        }
        //resultScreen.ShowPotionResult(recipe.potionName, recipe.potionDescription, recipe.potionIcon);
        StartCoroutine(DelayedResult(recipe));
        restartButton.SetActive(true);
        Debug.Log("Brewed: " + recipe.potionName);
        if (infoDisplay != null && infoDisplay.IsVisible())
        {
            infoDisplay.hideInfo();
        }
        if (potionDexUI != null)
        {
            Debug.Log("[ShowResult] Forcing PotionDexUI refresh...");
            potionDexUI.ForceRefresh();
        }
    }
    private IEnumerator DelayedResult(PotionRecipes recipe)
    {
        yield return null;
        Debug.Log("[CauldronManager] Showing result panel now");
        resultScreen.ShowPotionResult(recipe.potionName, recipe.potionDescription, recipe.potionIcon, recipe.emotionIcon);
    }
    private IEnumerator DelayedRefresh()
    {
        yield return null;
        potionDexUI.RefreshBook();
    }
    public void RestartGame()
    {
        currentIngredients.Clear();
        resultScreen.HidePanel();
        restartButton.SetActive(false);
        cauldronObject.SetActive(true);
        Debug.Log("Game Restarted!");
    }
}