using UnityEngine;

public class Fence : MonoBehaviour
{
    LayerMask destructibles;

    private void Awake()
    {
        destructibles = LayerMask.NameToLayer("Fruit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == destructibles)
        {
            Debug.Log("Check 2");
            Destroy(collision.collider.gameObject);
        }
    }
}