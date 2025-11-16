using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler
{
    public CauldronManager cauldronManager;
    public AudioSource auidoSOurce;
    public AudioClip bubbling;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem != null)
        {
            Debug.Log("Dropped " + droppedItem.name + " on " + gameObject.name);
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedItem.transform.SetAsFirstSibling();
            IngredientCarrier carrier = droppedItem.GetComponent<IngredientCarrier>();
            if (carrier != null && carrier.ingredient != null)
            {
                Debug.Log("Adding ingredient: " + carrier.ingredient.IngredientName);
                cauldronManager.AddIngredient(carrier.ingredient);
            }
            if (auidoSOurce != null && bubbling != null)
            {
                auidoSOurce.PlayOneShot(bubbling);
                }
            else
            {
                Debug.LogWarning("sound is missing!");

            }
        }
    }
}