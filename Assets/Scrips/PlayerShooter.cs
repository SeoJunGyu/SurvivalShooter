using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private Rigidbody rigid;
    private PlayerInput input;

    private UiManager uiManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        var findGo = GameObject.FindWithTag("UIManager");
        uiManager = findGo.GetComponent<UiManager>();
    }

    private void Update()
    {
        if(uiManager.IsPaused)
        {
            return;
        }

        if(input.Fire)
        {
            gun.Fire();
        }
    }
}
