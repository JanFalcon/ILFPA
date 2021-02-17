using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public TextMeshProUGUI easyValue;
    public TextMeshProUGUI mediumValue;
    public TextMeshProUGUI hardValue;

    public Slider easySlider;
    public Slider mediumSlider;
    public Slider hardSlider;

    public float eValue = 0f, mValue = 0f, hValue = 0f;

    public float totalValue = 0f;
    public float remainingValue = 100f;


    public void SetEasySlider()
    {
        easyValue.text = $"{CalculateRemainingValue(easySlider, ref eValue)}%";
    }

    public void SetMediumSlider()
    {
        mediumValue.text = $"{CalculateRemainingValue(mediumSlider, ref mValue)}%";
    }

    public void SetHardSlider()
    {
        hardValue.text = $"{CalculateRemainingValue(hardSlider, ref hValue)}%";
    }

    public float CalculateRemainingValue(Slider slider, ref float sValue)
    {
        //totalValue = Mathf.Max(Mathf.Min(easySlider.value + mediumSlider.value + hardSlider.value, 100f), 0f);
        totalValue = easySlider.value + mediumSlider.value + hardSlider.value;
        CheckSlider();

        //If Overflow
        if (totalValue > 100f)
        {
            slider.value = sValue + remainingValue;
            totalValue = 100f;
        }

        remainingValue = 100f - totalValue;
        sValue = slider.value;
   
        return sValue;
    }

    public void CheckSlider()
    {
        if(easySlider.value > 0 && mediumSlider.value > 0)
        {
            hardSlider.interactable = false;
        }
        else if(easySlider.value > 0 && hardSlider.value > 0)
        {
            mediumSlider.interactable = false;
        }
        else if(mediumSlider.value > 0 && hardSlider.value > 0)
        {
            easySlider.interactable = false;
        }
        else
        {
            easySlider.interactable = true;
            mediumSlider.interactable = true;
            hardSlider.interactable = true;
        }
    }

    public Item.Difficulty CheckDifficulty()
    {
        Item.Difficulty difficulty = Item.Difficulty.VeryEasy;

        if(easySlider.value > 0 && mediumSlider.value > 0)
        {
            return Item.Difficulty.Easy;
        }
        else if (mediumSlider.value > 0 && hardSlider.value > 0)
        {
            return mediumSlider.value >= hardSlider.value ? Item.Difficulty.Hard : Item.Difficulty.VeryHard;
        }
        else if(easySlider.value > 0 && hardSlider.value > 0)
        {
            return easySlider.value >= hardSlider.value ? Item.Difficulty.Medium : Item.Difficulty.Hard;
        }

        if (easySlider.value > 0)
        {
            difficulty = Item.Difficulty.VeryEasy;
        }
        if (mediumSlider.value > 0)
        {
            difficulty = Item.Difficulty.Medium;
        }
        if (hardSlider.value > 0)
        {
            difficulty = Item.Difficulty.VeryHard;
        }

        return difficulty;
    }

    public void Reset()
    {
        easySlider.value = 0f;
        mediumSlider.value = 0f;
        hardSlider.value = 0f;
    }
}
