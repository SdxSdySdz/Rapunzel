using System;
using System.Collections.Generic;
using UnityEngine;

public class Hair : MonoBehaviour
{
    [SerializeField] private List<HairLine> _hairLines;

    public void ChangeColor(Color color)
    {
        foreach (var hairLine in _hairLines)
        {
            hairLine.ChangeColor(color);
        }
    }

    public void Lengthen()
    {
        foreach (var hairLine in _hairLines)
        {
            hairLine.Lengthen();
        }
    }
}
