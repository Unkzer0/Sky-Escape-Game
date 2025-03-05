using UnityEngine;

public class Box : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    [SerializeField] private GameObject boxParticles;
    [SerializeField] private Transform parent;

    private void Update()
    {
        RotateBox();
    }

    private void RotateBox()
    {
        transform.Rotate(x * Time.fixedDeltaTime, y * Time.fixedDeltaTime, z * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Validate collider type
        {
            InstantiateParticles();
            Destroy(gameObject);
        }
    }

    private void InstantiateParticles()
    {
        if (boxParticles != null)
        {
            GameObject vfx = Instantiate(boxParticles, transform.position, Quaternion.identity);
            if (parent != null)
            {
                vfx.transform.parent = parent;
            }
        }
    }
}
