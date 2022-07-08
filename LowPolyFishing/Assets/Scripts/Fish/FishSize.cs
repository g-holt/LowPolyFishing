using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishSize : MonoBehaviour
{
    [SerializeField] float goalWeight = 15f;
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] TextMeshProUGUI inchesText;
    [SerializeField] TextMeshProUGUI totalWeightText;
    [HideInInspector] public bool goalReached;

    float minWeight;
    float maxWeight;
    float minInches;
    float maxInches;
    float finalWeight;
    float finalInches;
    float totalWeight;

    
    void Start() 
    {
        goalReached = false;
        totalWeight = 0f;   

        UpdateTotalWeight(totalWeight); 
    }


    public void SetFishSize()
    {
        float randomNumber = Random.Range(0, 1000);
        
        switch(randomNumber)
        {
            case <= 800:
                minWeight = .5f;
                maxWeight = 2.5f;
                minInches = 10f;
                maxInches = 16.5f;
                break;
            case <= 900:
                minWeight = 2.6f;
                maxWeight = 5f;
                minInches = 16.6f;
                maxInches = 20.5f;
                break;
            case <= 975:
                minWeight = 5.1f;
                maxWeight = 7.5f;
                minInches = 20.6f;
                maxInches = 23f;
                break;
            case <= 995:
                minWeight = 7.6f;
                maxWeight = 9.9f;
                minInches = 23.1f;
                maxInches = 25f;
                break;
            case <= 1000:
                minWeight = 10f;
                maxWeight = 15f;
                minInches = 25.1f;
                maxInches = 30f;
                break;
        }

        finalWeight = Random.Range(minWeight, maxWeight);
        finalInches = Random.Range(minInches, maxInches);
        finalWeight = Mathf.Round(finalWeight * 100f) * .01f;   //Setting weight to 2 decimal places
        finalInches = Mathf.Round(finalInches * 100f) * .01f;

        SetFishText(finalWeight, finalInches);
        UpdateTotalWeight(finalWeight);
    } 


    void SetFishText(float weight, float inches)
    {
        weightText.text = "Weight: " + weight + " lb";
        inchesText.text = "Length: " + inches + " in.";
    }


    void UpdateTotalWeight(float fishWeight)
    {
        totalWeight += fishWeight;
        totalWeight = Mathf.Round(totalWeight * 100f) * .01f;
        totalWeightText.text = "Total Weight: \n" + totalWeight + " lb" + " / " + goalWeight + " lb";

        if(totalWeight >= goalWeight)
        {
            goalReached = true;
        }
    }
    
}
