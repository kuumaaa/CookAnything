using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int changeDirInterval = 200;
    //Orientierungswerte:
    //kakerlake 1.5, 50
    //Ratte 2, 200
    //Fliege 1, 200

    private int dirChangeCounter = 0;

    //neue Ratten und Kakerlaken spawnen regularly?

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
        if (dirChangeCounter >= changeDirInterval)
        {
            transform.Rotate(0, 0, Random.Range(0, 360));
            dirChangeCounter = 0;
        }
        dirChangeCounter++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Not working yet
        if(collision.gameObject.CompareTag("RatDead"))
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }
}
