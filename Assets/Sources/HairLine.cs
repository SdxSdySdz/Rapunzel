using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HairLine : MonoBehaviour
{
    [SerializeField] private Segment _segmentPrefab;
    [SerializeField] private Transform _head;
    [SerializeField] private float _width;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private int _segmentsCount;
    [SerializeField] private float _distanceBetween;

    private List<Segment> _segments;

    

    private List<Vector3> _positions;

    private void Awake()
    {
        _segments = new List<Segment>();
        for (int i = 0; i < _segmentsCount; i++)
        {
            _segments.Add(Instantiate(_segmentPrefab));
        }

        _positions = new List<Vector3>();
        _positions.Add(transform.position);
        
        for (int i = 1; i < _segmentsCount + 1; i++)
        {
           _positions.Add(_positions.Last() - _head.forward * _distanceBetween);
        }
        
        PlaceSegments();
    }

    private void OnEnable()
    {
        foreach (var segment in _segments)
        {
            segment.SawCollided += OnSawCollided;
        }
    }

    private void OnDisable()
    {
        foreach (var segment in _segments)
        {
            segment.SawCollided -= OnSawCollided;
        }
    }

    private void Update()
    {
        _positions[0] = transform.position;

        for (int i = 1; i < _positions.Count; i++)
        {
            Vector3 _ = Vector3.zero;
            _positions[i] = Vector3.SmoothDamp(
                _positions[i],
                _positions[i - 1] - _head.forward * _distanceBetween,
                ref _,
                _smoothSpeed
                );
            
        }

        PlaceSegments();
    
    }

    public void ChangeColor(Color color)
    {
        foreach (var segment in _segments)
        {
            segment.ChangeColor(color);
        }
    }

    public void Lengthen()
    {
        Segment firstNonactiveSegment = _segments.FirstOrDefault(segment => segment.gameObject.activeSelf == false);
        // int index = _segments.IndexOf(firstNonactiveSegment);
        firstNonactiveSegment?.gameObject.SetActive(true);
    }

    private void PlaceSegments()
    {
        for (int i = 0; i < _segments.Count; i++)
        {
            _segments[i].Place(_positions[i], _positions[i + 1], 2 * _width);
        }
    }
    
    private void OnSawCollided(Segment segment)
    {
        int index = _segments.IndexOf(segment);
        for (int i = index; i < _segments.Count; i++)
        {
            _segments[i].gameObject.SetActive(false);
        }
    }
}
