using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{
    public static FuzzyLogic instance;

    public float time;
    public int tries;

    private float max = 0f;
    private string diff = "";
    private float fuzzyValue = 0f;

    private Item.Difficulty difficulty;

    private List<string> trueRules;

    [HeaderAttribute("Time Curve")]
    public AnimationCurve shortTimeCurve;
    public AnimationCurve mediumTimeCurve;
    public AnimationCurve longTimeCurve;

    [HeaderAttribute("Try Curve")]
    public AnimationCurve lowTryCurve;
    public AnimationCurve mediumTryCurve;
    public AnimationCurve highTryCurve;

    [HeaderAttribute("Difficulty Curve")]
    public AnimationCurve easyCurve;
    public AnimationCurve mediumCurve;
    public AnimationCurve hardCurve;

    private void Awake()
    {
        instance = this;
        trueRules = new List<string>();
    }

    [ContextMenu("Test")]
    public void Test()
    {
        float shortTMF = shortTimeCurve.Evaluate(time);
        float mediumTMF = mediumTimeCurve.Evaluate(time);
        float longTMF = longTimeCurve.Evaluate(time);

        float lowMF = lowTryCurve.Evaluate(tries);
        float mediumMF = mediumTryCurve.Evaluate(tries);
        float highMF = highTryCurve.Evaluate(tries);

        Debug.Log($"Time  : {shortTMF} | {mediumTMF} | {longTMF}");
        Debug.Log($"Tries : {lowMF} | {mediumMF} | {highMF}");

        FuzzyRules(time, tries);
    }

    public void Defuzzification()
    {

    }

    public float FuzzyRules(float time, float tries)
    {
        List<float> acceptedRules = new List<float>();
        List<Item.Difficulty> dif = new List<Item.Difficulty>();
        trueRules.Clear();

        float shortTMF = shortTimeCurve.Evaluate(time);
        float mediumTMF = mediumTimeCurve.Evaluate(time);
        float longTMF = longTimeCurve.Evaluate(time);

        float lowMF = lowTryCurve.Evaluate(tries);
        float mediumMF = mediumTryCurve.Evaluate(tries);
        float highMF = highTryCurve.Evaluate(tries);

        //Get the mix => max

        //IF time is short and tries is low     => hard
        if (shortTMF > 0 && lowMF > 0)
        {
            trueRules.Add($"Time is short : {time} and tries is low : {tries}");
            acceptedRules.Add(Mathf.Min(shortTMF, lowMF));
            dif.Add(Item.Difficulty.VeryHard);
        }
        //IF time is short and tries is medium     => medium hard
        if (shortTMF > 0 && mediumMF > 0)
        {
            trueRules.Add($"Time is short : {time} and tries is medium : {tries}");
            acceptedRules.Add(Mathf.Min(shortTMF, mediumMF));
            dif.Add(Item.Difficulty.Hard);
        }
        //IF time is short and tries is high     => medium
        if (shortTMF > 0 && highMF > 0)
        {
            trueRules.Add($"Time is short : {time} and tries is high : {tries}");
            acceptedRules.Add(Mathf.Min(shortTMF, highMF));
            dif.Add(Item.Difficulty.Medium);
        }

        //IF time is medium and tries is low     => hard
        if (mediumTMF > 0 && lowMF > 0)
        {
            trueRules.Add($"Time is medium : {time} and tries is low : {tries}");
            acceptedRules.Add(Mathf.Min(mediumTMF, lowMF));
            dif.Add(Item.Difficulty.VeryHard);
        }
        //IF time is medium and tries is medium     => medium
        if (mediumTMF > 0 && mediumMF > 0)
        {
            trueRules.Add($"Time is medium : {time} and tries is medium : {tries}");
            acceptedRules.Add(Mathf.Min(mediumTMF, mediumMF));
            dif.Add(Item.Difficulty.Medium);
        }
        //IF time is medium and tries is high     => easy medium
        if (mediumTMF > 0 && highMF > 0)
        {
            trueRules.Add($"Time is medium : {time} and tries is high : {tries}");
            acceptedRules.Add(Mathf.Min(mediumTMF, highMF));
            dif.Add(Item.Difficulty.Easy);
        }

        //IF time is long and tries is low     => medium
        if (longTMF > 0 && lowMF > 0)
        {
            trueRules.Add($"Time is long : {time} and tries is low : {tries}");
            acceptedRules.Add(Mathf.Min(longTMF, lowMF));
            dif.Add(Item.Difficulty.Medium);
        }
        //IF time is long and tries is medium     => easy meadium
        if (longTMF > 0 && mediumMF > 0)
        {
            trueRules.Add($"Time is long : {time} and tries is medium : {tries}");
            acceptedRules.Add(Mathf.Min(longTMF, mediumMF));
            dif.Add(Item.Difficulty.Easy);
        }
        //IF time is long and tries is high     => easy
        if (longTMF > 0 && highMF > 0)
        {
            trueRules.Add($"Time is long : {time} and tries is high : {tries}");
            acceptedRules.Add(Mathf.Min(longTMF, highMF));
            dif.Add(Item.Difficulty.VeryEasy);
        }

        //Defuzzification
        float max = 0f;
        int counter = 0;
        foreach (float value in acceptedRules)
        {
            //max = Mathf.Max(value, max);
            if (value > max)
            {
                max = value;
                difficulty = dif[counter];
            }
            counter++;
        }

        diff = difficulty.ToString();
        this.max = max;
        fuzzyValue = CalculateTime(this.max, difficulty);

        return fuzzyValue;
    }

    public float CalculateTime(float value, Item.Difficulty difficulty)
    {
        float zStar = 0f;
        switch (difficulty)
        {
            case Item.Difficulty.VeryEasy:
                //zStar = 25f - (value * (25f - 0f));
                zStar = 0f;
                break;
            case Item.Difficulty.Easy:
                //zStar = 50f - (value * (50f - 25f));
                zStar = 25f;
                break;
            case Item.Difficulty.Medium:
                //zStar = (value * (50f - 25f) + 25f);
                zStar = 50f;
                break;
            case Item.Difficulty.Hard:
                //zStar = 75f - (value * (75f - 50f));
                zStar = 75f;
                break;
            case Item.Difficulty.VeryHard:
                //zStar = (value * (75f - 50f)) + 50f;
                //zStar = 100f - (value * (100f - 75f));
                zStar = 100f;
                break;
            default:
                break;
        }

        //zStar = (z + z2) / 2;
        return zStar;
    }

    public string GetDifficulty()
    {
        return diff;
    }

    public float GetMax()
    {
        return max;
    }

    public float GetFuzzyValue()
    {
        return fuzzyValue;
    }

    public string[] GetAcceptedRules()
    {
        return trueRules.ToArray();
    }
}
