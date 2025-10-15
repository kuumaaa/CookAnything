using System.Collections;
using UnityEngine;

public class Kakerlake : MonoBehaviour
{

    private float livingTime = 60f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DieOfOldAge());
    }

    IEnumerator DieOfOldAge()
    {
        float elapsedTime = 0f;


        while (elapsedTime < livingTime)
        {

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
