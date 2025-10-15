using System.Collections;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    [SerializeField] private GameObject shootPosition;

    private bool isFreezing = false;
    private float freezingTime = 5f;
    
    private GameObject freezingObject = null;
    
    
    public bool IsFreezing()
    {
        return isFreezing;
    }

    public void StartFreezing(GameObject objectToCook)
    {
        if (!isFreezing)
        {
            freezingObject = objectToCook;
            
            freezingObject.transform.SetParent(shootPosition.transform);
            freezingObject.transform.localPosition = Vector3.zero;
            Destroy(freezingObject.GetComponent<Rigidbody>());
            freezingObject.GetComponent<Collider>().enabled = false;
            freezingObject.SetActive(false);
            isFreezing = true;

            StartCoroutine(FreezeObject());
        }
    }


    private void FinishFreezing()
    {
        isFreezing = false;
        freezingObject.transform.SetParent(null);
        freezingObject.SetActive(true);
        
        freezingObject.AddComponent<Rigidbody>();
        freezingObject.GetComponent<Rigidbody>().useGravity = true;
        freezingObject.GetComponent<Rigidbody>().linearDamping = 0.1f;
        freezingObject.GetComponent<Rigidbody>().AddForce(shootPosition.transform.up * 10f, ForceMode.Impulse);
        
        freezingObject.GetComponent<Object>().Freeze();

        Collider col = freezingObject.GetComponent<Collider>();
        if (col == null) col = freezingObject.AddComponent<BoxCollider>();
        else col.enabled = true;
        
        freezingObject = null;
    }
    
    
    
    IEnumerator FreezeObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < freezingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        FinishFreezing();
    }
}
