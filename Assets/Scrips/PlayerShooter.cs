using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private Rigidbody rigid;
    private PlayerInput input;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(input.Fire)
        {
            gun.Fire();
        }
    }
}
