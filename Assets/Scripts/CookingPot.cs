using System.Collections;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    [SerializeField] private GameObject shootPosition;
    [SerializeField] private GameObject fullPot;
    [SerializeField] private GameObject emptyPot;
    private bool isCooking = false;
    private float cookingTime = 5f;
    
    private GameObject cookingObject = null;

    private void Start()
    {
        fullPot.SetActive(false);
        emptyPot.SetActive(true);
    }


    public bool IsPotCooking()
    {
        return isCooking;
    }

    public void StartCooking(GameObject objectToCook)
    {
        if (!isCooking)
        {
            cookingObject = objectToCook;
            
            cookingObject.transform.SetParent(shootPosition.transform);
            cookingObject.transform.localPosition = Vector3.zero;
            Destroy(cookingObject.GetComponent<Rigidbody>());
            cookingObject.GetComponent<Collider>().enabled = false;
            cookingObject.SetActive(false);
            isCooking = true;
            fullPot.SetActive(true);
            emptyPot.SetActive(false);
            StartCoroutine(CookObject());
        }
    }


    private void FinishCooking()
    {
        isCooking = false;
        cookingObject.transform.SetParent(null);
        cookingObject.SetActive(true);
        
        cookingObject.AddComponent<Rigidbody>();
        cookingObject.GetComponent<Rigidbody>().useGravity = true;
        cookingObject.GetComponent<Rigidbody>().linearDamping = 0.1f;
        cookingObject.GetComponent<Rigidbody>().AddForce(shootPosition.transform.up * 10f, ForceMode.Impulse);
        
        cookingObject.GetComponent<Object>().Cook();

        Collider col = cookingObject.GetComponent<Collider>();
        if (col == null) col = cookingObject.AddComponent<BoxCollider>();
        else col.enabled = true;
        
        fullPot.SetActive(false);
        emptyPot.SetActive(true);

        cookingObject = null;
    }
    
    
    
    IEnumerator CookObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < cookingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        FinishCooking();
    }
}
