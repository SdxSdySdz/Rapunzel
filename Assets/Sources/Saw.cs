using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Saw : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }
}
