using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right, Time.fixedDeltaTime * _speed, Space.Self);
    }
}
