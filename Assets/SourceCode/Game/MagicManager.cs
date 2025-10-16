using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MagicManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider magicSlider;
    public TMP_Text magicText;

    [Header("Inventory")]
    public Inventory playerInventory;


    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMagic;
        magicSlider.value = playerInventory.maxMagic;
        playerInventory.currentMagic = playerInventory.maxMagic;
        UpdateMagicText();
    }

    public void AddMagic()
    {
        //currentMagic++;
        //playerInventory.currentMagic++;
        magicSlider.value = playerInventory.currentMagic;
        if (magicSlider.value > playerInventory.maxMagic)
        {
            magicSlider.value = playerInventory.maxMagic;
            playerInventory.currentMagic = playerInventory.maxMagic;
        }
        UpdateMagicText();
    }

    public void DecreaseMagic()
    {
        //currentMagic--;
        //playerInventory.currentMagic--;
        magicSlider.value = playerInventory.currentMagic;
        if (magicSlider.value < 0)
        {
            magicSlider.value = 0;
            playerInventory.currentMagic = 0;
        }
        UpdateMagicText();
    }

    void UpdateMagicText()
    {
        magicText.text = playerInventory.currentMagic.ToString();
    }
}
