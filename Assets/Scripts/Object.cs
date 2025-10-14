using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private starValue Geschmack;
    [SerializeField] private starValue Konsistenz;
    [SerializeField] private starValue Temperatur;
    [SerializeField] private string beschreibung;

    public string GetName()
    {
        return name;
    }

    public ObjectData GetObjectData()
    {
        ObjectData data = ScriptableObject.CreateInstance<ObjectData>();
        
        data.objectName = name;
        data.Geschmack = Geschmack;
        data.Konsistenz = Konsistenz;
        data.Temperatur = Temperatur;
        data.beschreibung = beschreibung;
        
        return data;
    } 
    
    private void OnCollisionEnter(Collision collision)
    {
        // If hit by a thrown object, enable physics
        if (collision.gameObject.GetComponent<Thrown>() != null)
        {
            if (GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = true;
            }
        }
    }
}
