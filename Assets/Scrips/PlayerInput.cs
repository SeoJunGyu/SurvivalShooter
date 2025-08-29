using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string Vertical = "Vertical";
    public static readonly string Horizontal = "Horizontal";
    public static readonly string Fire1 = "Fire1";

    public float Ver { get; set; }
    public float Hor { get; set; }
    public bool Fire { get; set; }

    private void Update()
    {
        Ver = Input.GetAxis(Vertical);
        Hor = Input.GetAxis(Horizontal);
        Fire = Input.GetButton(Fire1);
    }
}
