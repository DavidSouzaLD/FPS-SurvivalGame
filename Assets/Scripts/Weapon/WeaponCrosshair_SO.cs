using UnityEngine;

[CreateAssetMenuAttribute(fileName = "CrossData", menuName = "ProjectSouza/CrossData")]
public class WeaponCrosshair_SO : ScriptableObject
{
    public Color color;
    public Color outlineColor;
    public bool useCross;
    public float startSize = 80, maxSize = 300, height = 2, width = 6;
    [Space]
    public bool usePoint;
    public float pointScale = 1;
}
