using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    private int rotVal = -1;

    [SerializeField]
    private float speed = 30;

    private void Start()
    {
        Invoke(nameof(RotRandon), 3);
    }

    private void RotRandon()
    {
        speed = 20;
        Invoke(nameof(RotRandon2), 2);
    }

    private void RotRandon2()
    {
        speed = 30;
        rotVal = rotVal * -1;

        Invoke(nameof(RotRandon), 3);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotVal * speed * Time.deltaTime));
    }
}
