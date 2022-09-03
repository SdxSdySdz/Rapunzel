using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class VerticalPlacer : MonoBehaviour
{
    [SerializeField] private float _distanceBetween;
    [SerializeField] private List<Transform> _transforms;

    private void OnValidate()
    {
        if (_transforms.Count == 0)
            return;
        
        Filter();
        Adopt();
        Place();
    }

    private void Filter()
    {
        var filteredTransforms = new List<Transform>();
        
        foreach (var child in _transforms)
        {
            if (filteredTransforms.Contains(child) == false)
                filteredTransforms.Add(child);
        }

        _transforms = filteredTransforms;
    }

    private void Adopt()
    {
        foreach (var child in _transforms)
        {
            child.SetParent(transform);
        }
    }

    private void Place()
    {
        for (int i = 0; i < _transforms.Count; i++)
        {
            _transforms[i].position = transform.position + (i * _distanceBetween) * Vector3.down;
        }
    }
}
