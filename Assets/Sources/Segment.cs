using System;
using UnityEngine;
using UnityEngine.Events;

public class Segment : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public event UnityAction<Segment> SawCollided;

    public void Place(Vector3 from, Vector3 to, float radius)
    {
        transform.position = (to + from) / 2f;
        transform.localScale = new Vector3(radius, (to - from).magnitude / 2f, radius);
        transform.up = to - from;
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Segment _))
        {

        }
        else if (other.gameObject.TryGetComponent(out Saw _))
        {
            SawCollided?.Invoke(this);
        }
    }
}
