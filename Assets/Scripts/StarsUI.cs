using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsUI : MonoBehaviour
{
    
    [SerializeField] private RawImage starNegativeThree;
    [SerializeField] private RawImage starNegativeTwo;
    [SerializeField] private RawImage starNegativeOne;
    [SerializeField] private RawImage starZero;
    [SerializeField] private RawImage starPositiveOne;
    [SerializeField] private RawImage starPositiveTwo;
    [SerializeField] private RawImage starPositiveThree;
    
    [SerializeField] private TMP_Text leftAxisText;
    [SerializeField] private TMP_Text rightAxisText;
    private starValue expectedValue = 0;
    private starValue actualValue = 0;
    private string leftAxis = "Left";
    private string rightAxis = "Right";
    
    private Color emptyStarColor = new Color(0f, 0f, 0f, 0f);
    private Color goalStarColor = Color.white;
    private Color incorrectStarColor = Color.red;
    private Color correctStarColor = Color.yellow;

    private void Awake()
    {
        ResetStars();
    }

    private void ResetStars()
    {
        foreach (starValue value in Enum.GetValues(typeof(starValue)))
        {
            UpdateStars(value,emptyStarColor);
        }
    }


    public void InitializeStars(starValue value, string leftAxis, string rightAxis, bool isObjectStars)
    {
        ResetStars();
        
        Color color = goalStarColor;
        if (isObjectStars)
        {
            color = correctStarColor;
        }
        
        leftAxisText.text = leftAxis;
        rightAxisText.text = rightAxis;
        expectedValue = value;
        
        IterateStars(starValue.zero,value,color);
    }
    
    private void IterateStars(starValue start, starValue end, Color color)
    {
        int step = start < end ? 1 : -1;

        for (int i = (int)start; step > 0 ? i <= (int)end : i >= (int)end; i += step)
        {
            starValue value = (starValue)i;
            UpdateStars(value, color);
        }
    }

    public void UpdateStars(starValue value,Color color) 
    {
        switch (value)
        {
            case starValue.negativeThree:
                starNegativeThree.color = color;
                break;
            case starValue.negativeTwo:
                starNegativeTwo.color = color;
                break;
            case starValue.negativeOne:
                starNegativeOne.color = color;
                break;
            case starValue.zero:
                starZero.color = color;
                break;
            case starValue.positiveOne:
                starPositiveOne.color = color;
                break;
            case starValue.positiveTwo:
                starPositiveTwo.color = color;
                break;
            case starValue.positiveThree:
                starPositiveThree.color = color;
                break;
        }
    }

    public void AddObject(starValue valueStar)
    {
        actualValue = (starValue)Mathf.Clamp((int)actualValue  + (int)valueStar, -3, 3);
        
        foreach (starValue value in Enum.GetValues(typeof(starValue)))
        {
            UpdateStars(value,emptyStarColor);
        }
        
        IterateStars(starValue.zero,expectedValue,goalStarColor);
        
        //Todo fix this:
        
        if (Math.Sign((int)expectedValue) != Math.Sign((int)actualValue))
        {
            IterateStars(starValue.zero,actualValue,incorrectStarColor);
        }
        else
        {
            if ((int)expectedValue < (int)actualValue)
            {
                IterateStars(starValue.zero,expectedValue,correctStarColor);
                IterateStars(expectedValue+Math.Sign((int)actualValue),actualValue,incorrectStarColor);
            }
            else
            {
                IterateStars(starValue.zero,actualValue,correctStarColor);
            }
        }
    }

    public int CalculateScore()
    {
        int score = 0;

        if (Math.Sign((int)expectedValue) == Math.Sign((int)actualValue))
        {
            score += 5 * Math.Min((int)expectedValue, (int)actualValue) - Math.Abs((int)expectedValue - (int)actualValue);
        }
        else
        {
            score -= Math.Abs((int)expectedValue - (int)actualValue);
        }
        
        return score;
    }
    
}
