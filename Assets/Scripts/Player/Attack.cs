using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if it has IDamageable Script
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.Damage();
        }
    }
}
