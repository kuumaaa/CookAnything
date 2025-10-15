using System.Collections;
using TMPro;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    private Vector3 startPos = Vector3.zero;
    private Vector3 endPos = Vector3.zero;
    private float duration = 0f;
    
    [SerializeField] private TMP_Text name;
    [SerializeField] private StarsUI starsOne;
    [SerializeField] private StarsUI starsTwo;
    [SerializeField] private StarsUI starsThree;
    [SerializeField] private TMP_Text scoreUI;

    [SerializeField] private GameObject essen;
    
    
    
    

    public void Spawn(Vector3 startPos, Vector3 endPos, float duration, TabletData data)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.duration = duration;
        
        name.text = data.mealName;
        starsOne.InitializeStars(data.Geschmack, "Mild","Funky",false);
        starsTwo.InitializeStars(data.Konsistenz, "Weich","Hart",false);
        starsThree.InitializeStars(data.Temperatur, "Kalt","Heiß",false);
        
        scoreUI.text = "Score: " + CalculateScore();
        
        StartCoroutine(MoveObject());
    }
    
    IEnumerator MoveObject()
    {
        float elapsedTime = 0f;
        Vector3 startingPos = startPos;
        Vector3 targetPos = endPos;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPos;
        MealFinished();
    }



    private void MealFinished()
    {
        GameManager.Instance.UpdateScore(CalculateScore());
        Destroy(gameObject);
    }

    public void AddObject(ObjectData data)
    {
        starsOne.AddObject(data.Geschmack);
        starsTwo.AddObject(data.Konsistenz);
        starsThree.AddObject(data.Temperatur);
        
        scoreUI.text = "Score: " + CalculateScore();
        essen.SetActive(true);
    }

    private int CalculateScore()
    {
        int score = 0;
        score += starsOne.CalculateScore();
        score += starsTwo.CalculateScore();
        score += starsThree.CalculateScore();

        score *= 10;
        
        return score;
    }
    
}
