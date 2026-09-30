using TMPro;
using UnityEngine;

public static class CustomUtilities
{
    public enum PlayerTeams
    {
        Player,
        Enemy
    }

    public static Vector2 Vec2ToVec3(Vector2 vect2, float zValue = 0)
    {
        return new Vector3(vect2.x, vect2.y, zValue);
    }
    public static Vector3 Vec3ToVec2(Vector3 vect3)
    {
        return new Vector2(vect3.x, vect3.y);
    }

    public static float GetAngleOf2DVect(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 directonVect = targetPos - startPos; //Ai
        return Mathf.Atan2(directonVect.y, directonVect.x) * Mathf.Rad2Deg; //Ai
    }
}
