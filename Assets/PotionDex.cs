using System.Collections.Generic;
using UnityEngine;

public class PotionDex : MonoBehaviour
{
    public static PotionDex existence;
    public PotionDatabase potionDatabase;
    private List<PotionRecipes> discoveredPotions = new List<PotionRecipes>();
    private void Awake()
    {
        Debug.Log("Potion Awake called!");
        DontDestroyOnLoad(gameObject);
        if (existence == null)
        {
            existence = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPotion(PotionRecipes newPotion)
    {
        //Debug.Log("Registering: " + newPotion.potionName + " | Total: " + discoveredPotions.Count);
        Debug.Log($"[REGISTER] Adding potion: {newPotion.potionName} to instance ID: {GetInstanceID()}");
        if (!discoveredPotions.Contains(newPotion))
        {
            discoveredPotions.Add(newPotion);
            //add an exclamation: "NEW!" in the UI panel when a new potion is discovered
            Debug.Log("New potion discovered: " + newPotion.potionName + ". Total now: " + discoveredPotions.Count);
        }
    }
    public bool IsPotionDiscovered(PotionRecipes potion)
    {
        return discoveredPotions.Contains(potion);
    }
    public IEnumerable<PotionRecipes> GetDiscoveredPotions()
    {
        return discoveredPotions;
    }
}