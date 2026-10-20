using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlatformController player = collision.GetComponent<PlatformController>();
            player.coinsCollected++;
            Debug.Log("coin collected!");
            Debug.Log("player score:" + player.coinsCollected);
            Destroy(gameObject);
        }
    }
}
