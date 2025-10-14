using System.Collections;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    private Vector3 startPos = Vector3.zero;
    private Vector3 endPos = Vector3.zero;
    private float duration = 0f;
    

    public void Spawn(Vector3 startPos, Vector3 endPos, float duration)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.duration = duration;
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
        //todo calculate score
        
        
        GameManager.Instance.UpdateScore(100);
        Destroy(gameObject);
    }
}
