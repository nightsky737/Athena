using UnityEngine;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI;   // <-- Required for Image

public class LogicScript : MonoBehaviour
{
    Dictionary<string, bool> collected = new Dictionary<string, bool>();
    private Dictionary<string, Sprite> gatheredMap;
    private int gathered;
    public TextMeshProUGUI  collectedText; 
    public GameObject PlatformCanvas;
    public GameObject MixerCanvas;
    public GameObject switchButton;
 
    // public Image Fear;           // the UI Image
    // public Image Joy;           // the UI Image
    // public Image Sadness;           // the UI Image
    // public Image Anger;           // the UI Image
    // public Image Disgust;           // the UI Image

    // [SerializeField] public List<Sprite> UngatheredSkins;
    public Sprite UngatheredSkin;
    [SerializeField] public List<Sprite> GatheredSkins;

    [SerializeField] public List<Image> EmotionImages;


    // public void SetCollected()
    // {
    //     itemIcon.sprite = collectedSprite;
    // }

    // public void ResetIcon()
    // {
    //     itemIcon.sprite = defaultSprite;
    // }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gathered = 0;
        PlatformCanvas.SetActive(true);
        MixerCanvas.SetActive(false);
        switchButton.SetActive(false);
        collected.Add("joy", false);
        collected.Add("sadness", false);
        collected.Add("anger", false);
        collected.Add("fear", false);
        collected.Add("disgust", false);
        // Map emotion names to their corresponding sprites
        gatheredMap = new Dictionary<string, Sprite>();
        gatheredMap.Add("fear", GatheredSkins[0]);
        gatheredMap.Add("joy", GatheredSkins[1]);
        gatheredMap.Add("sadness", GatheredSkins[2]);
        gatheredMap.Add("anger", GatheredSkins[3]);
        gatheredMap.Add("disgust", GatheredSkins[4]);

        for (int i = 0; i < EmotionImages.Count; i++)
        {
            EmotionImages[i].sprite = UngatheredSkin;
        }

    }   

public void collect(string name)
{
    collected[name] = true;
    gathered+=1;
    foreach (var img in EmotionImages)
    {
        if (img.name.ToLower() == name)  
        {
            img.sprite = gatheredMap[name];
            break;
        }
    }
}


    // Update is called once per frame
    void Update()
    {
    //     collectedText.text = $@"Status:
    // Joy : {collected["joy"]}
    // Sadness : {collected["sadness"]}
    // Anger : {collected["anger"]}
    // Fear : {collected["fear"]}
    // Disgust : {collected["disgust"]}";
    if(gathered == 5)
        {
            switchButton.SetActive(true);
        }
    }

 public void pressSwitch()
{
    Debug.Log("SWITCH FIRED");

    PlatformCanvas.SetActive(false);

    MixerCanvas.SetActive(true);
    Canvas.ForceUpdateCanvases();

    Debug.Log("Platform active: " + PlatformCanvas.activeSelf);
    Debug.Log("Mixer active: " + MixerCanvas.activeSelf);
}
}
