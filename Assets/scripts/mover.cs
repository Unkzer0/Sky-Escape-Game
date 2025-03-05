using UnityEngine;

public class mover : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField] Vector3 MoverVector;
    [SerializeField][Range(0, 1)] float MoverFactor;
    [SerializeField] float Period = 2f;
    void Start()
    {
        StartingPosition = transform.position;
    }
    void Update()
    {
        if (Period <= Mathf.Epsilon) { return; }
        float Cycle = Time.time / Period;

        const float Tau = Mathf.PI * 2;
        float RawsInWave = Mathf.Sin(Cycle * Tau);

        MoverFactor = (RawsInWave + 1f) / 2f;
        Vector3 Offset = MoverVector * MoverFactor;
        transform.position = StartingPosition + Offset;
    }
}


