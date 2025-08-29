using UnityEngine;

[CreateAssetMenu(fileName = "MonDatas", menuName = "Scriptable Objects/MonDatas")]
public class MonDatas : ScriptableObject
{
    public float maxHp = 100f;
    public float damage = 20f;
    public float speed = 2f;

    public GameObject prefab;
}
