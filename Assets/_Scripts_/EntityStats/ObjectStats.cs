using UnityEngine;

[CreateAssetMenu(fileName = "ObjectStats", menuName = "ScriptableObjects/ObjectsStats")]
public class ObjectStats : ScriptableObject
{
    #region Variables
    public int durability;
    public int hit_damage;
    public float obj_Mass;
    #endregion
}
