using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shootClip;

    public float damage = 25f;
    public float timeBetFire = 0.12f;
    public float fireDistance = 50f;
}
