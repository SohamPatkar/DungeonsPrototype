using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        PlayerMovement _playerScript = other.gameObject.GetComponent<PlayerMovement>();
        UIScript _gameOvercall = GameObject.Find("UIManager").GetComponent<UIScript>();
        _playerScript.Health = 0;
        _gameOvercall.GameOver();
    }
}
