using UnityEngine;

public class Rock : MonoBehaviour
{
    LayerMask destroyers;

    [SerializeField]
    float shrinkRate = 5;

    private void Awake()
    {
        destroyers = LayerMask.NameToLayer("StrongWater");
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == destroyers)
        {
            transform.localScale -= new Vector3(1, 1, 0) * shrinkRate * Time.deltaTime;

            if (transform.localScale.x < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }
}
