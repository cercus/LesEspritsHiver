
using UnityEngine;

public static class XPTable
{

    public static int GetXPForLevel(int level)
    {
        const float XP_MAX = 660_000f;
        const float k = 5f; // intensité exponentielle

        float t = level / 100f;
        float xp = XP_MAX * (Mathf.Exp(k * t) - 1f) / (Mathf.Exp(k) - 1f);

        return Mathf.FloorToInt(xp);
    }

    public static int CalculateHealth(int level)
    {
        float t = level / 100f;
        float health = Mathf.Lerp(20, 9999,  Mathf.Pow(level / 100f, 2.2f));
        return Mathf.FloorToInt(health);
    }
}