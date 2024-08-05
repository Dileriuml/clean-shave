using UnityEngine;

public static class Utils
{
    public const float Epsilon = 1e-5f;

    public static bool IsAlmostZero(this float value, float epsilon = Epsilon) => Mathf.Abs(value) < epsilon;
}