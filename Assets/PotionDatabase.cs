using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "PotionDatabase", menuName = "Potion System/Potion Database")]
public class PotionDatabase : ScriptableObject
{
    public List<PotionRecipes> recipes;
}
