using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class PotionDexUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button prevButton;
    public Button nextButton;

    [Header("Other")]
    public PotionInfoDisplay potionInfoDisplay;
    public GameObject wholeBookPanel;

    [Header("Audio")]
    public AudioClip pageFlipSound;
    public AudioSource audioSource;


    private List<PotionRecipes> discoveredPotions = new List<PotionRecipes>();
    private int currentPage = 0;
    private void Start()
    {
        wholeBookPanel.SetActive(false);
        nextButton.onClick.RemoveAllListeners();
        prevButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(flipNext);
        prevButton.onClick.AddListener(flipPrev);
        //audioSource = GetComponent<AudioSource>();
    }
        public void LoadPotions()
        {
            if (PotionDex.existence == null)
            {
                Debug.LogWarning("[PotionDexUI] PotionDex.Instance is null!");
                discoveredPotions.Clear();
                return;
            }
            discoveredPotions = new List<PotionRecipes>(PotionDex.existence.potionDatabase.recipes);
            currentPage = 0;
            Debug.Log($"[PotionDexUI] Loaded {discoveredPotions.Count} potions");
            for (int i = 0; i < discoveredPotions.Count; i++)
            {
                Debug.Log($"  Potion {i}: {discoveredPotions[i].potionName}");
            }
        }
    public void flipNext()
    {
        Debug.Log($"FlipNext called — currentPage BEFORE decrement: {currentPage}");
        if (currentPage < discoveredPotions.Count - 1)
        {
            currentPage++;
            UpdatePage();
            if (audioSource != null && pageFlipSound != null)
            {
                audioSource.PlayOneShot(pageFlipSound);
            }
            else
            {
                Debug.LogWarning("Either your Audio Source or your sound effect are not assigned in the inspector!");
            }
        }
        else
        {
            Debug.Log("At last page, cannot advance");
        }
    }

    public void flipPrev()
    {
        Debug.Log($"FlipPrev called — currentPage BEFORE decrement: {currentPage}");
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
            if (audioSource != null && pageFlipSound != null)
            {
                audioSource.PlayOneShot(pageFlipSound);
            }
            else
            {
                Debug.LogWarning("Either your Audio Source or your sound effect are not assigned in the inspector!");
            }
        }
        else
        {
            Debug.Log("At first page, cannot advance");
        }
    }
    private void UpdatePage()
    {
        if (discoveredPotions.Count == 0)
        {
            potionInfoDisplay.hideInfo();
            return;
        }
        PotionRecipes currentPotion = discoveredPotions[currentPage];
        bool isDiscovered = PotionDex.existence.IsPotionDiscovered(currentPotion);

        potionInfoDisplay.ShowInfo(currentPotion, currentPage, isDiscovered);
    }

    public void RefreshBook()
    {
        discoveredPotions = new List<PotionRecipes>(PotionDex.existence.potionDatabase.recipes);
        currentPage = 0;
        Debug.Log("[RefreshBook] Reloaded potion list:");
        for (int i = 0; i < discoveredPotions.Count; i++)
        {
            Debug.Log($" - [{i}] {discoveredPotions[i].potionName}");
        }
        UpdatePage();
    }

    public void ForceRefresh()
    {
        Debug.Log("[PotionDexUI] FORCE REFRESH CALLED");
        currentPage = 0;
        RefreshBook();
    }
    public void TogglePanel()
    {
        if (audioSource != null && pageFlipSound != null)
        {
            audioSource.PlayOneShot(pageFlipSound);
        }
        if (wholeBookPanel != null)
        {
            bool isActive = wholeBookPanel.activeSelf;
            wholeBookPanel.SetActive(!isActive);
            Debug.Log($"TogglePanel called, panel active? {!isActive}");
        }
        else
        {
            Debug.LogWarning("TogglePanel called but wholeBookPanel is null!");
        }
    }
}

    // private void OnEnable()
    // {
    //     Debug.Log("PotionDexUI OnEnable");
    //     if (PotionDex.Instance == null)
    //     {
    //         Debug.LogWarning("PotionDex.Instance is null - skipping update");
    //         return;
    //     }
    //     discoveredPotions = new List<PotionRecipes>(PotionDex.Instance.GetDiscoveredPotions());
    //     currentPage = 0;
    //     UpdatePage();
    // }