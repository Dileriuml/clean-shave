using UnityEngine;

public static class Constants
{
    public static Vector3 ApplyIsometricScale(this Vector3 vector, float scale) 
        => new Vector3(vector.x, vector.y, vector.z * scale);
}
