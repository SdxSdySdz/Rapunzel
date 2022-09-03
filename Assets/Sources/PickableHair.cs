using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SphereCollider))]
public class PickableHair : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _hairPartsRendrers;

    public Color Color => _hairPartsRendrers[0].sharedMaterial.color;

/*    private void OnValidate()
    {
        ApplyColor();
    }*/

    /*    private void Awake()
        {
            ApplyColor();
        }*/

/*    private void ApplyColor()
    {
        foreach (var partRenderer in _hairPartsRendrers)
        {
            partRenderer.material.color = _color;
        }
    }*/
}
