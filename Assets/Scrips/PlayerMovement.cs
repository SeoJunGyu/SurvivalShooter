using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    private PlayerInput input;

    private Rigidbody rigid;
    private Animator animator;

    private Vector3 moveDir;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit))
        {
            Vector3 pos = hit.point;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
        
        moveDir = new Vector3(input.Hor, 0f, input.Ver);
        rigid.MovePosition(transform.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

        float move = input.Ver != 0 || input.Hor != 0 ? 1 : 0;
        animator.SetFloat("Movement", move);
    }
}
