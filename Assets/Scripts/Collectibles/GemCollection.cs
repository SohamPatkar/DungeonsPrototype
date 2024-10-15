using UnityEngine;

public class GemCollection : MonoBehaviour
{
    private PlayerMovement _GemCollection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _GemCollection = other.GetComponent<PlayerMovement>();
            _GemCollection.GemCollectCount();
            Destroy(gameObject);
        }
    }
}
