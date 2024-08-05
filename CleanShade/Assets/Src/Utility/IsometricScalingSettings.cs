using System;
using UnityEngine;

[Serializable]
public class IsometricScalingSettings
{ 
    public float IsometricScalingFactor = 1 / Mathf.Cos(Mathf.PI / 6);
}