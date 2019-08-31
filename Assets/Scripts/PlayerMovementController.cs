using UnityEngine;

[RequireComponent(typeof(CharacterGrounding))] //covermyowna
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4;
    [SerializeField]
    private float jumpForce = 400;

    private new Rigidbody2D rigidbody;
    private CharacterGrounding characterGrounding;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        characterGrounding = GetComponent<CharacterGrounding>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0);

        transform.position += movement * Time.deltaTime * moveSpeed;

        if (Input.GetButtonDown("Fire1") && characterGrounding.IsGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
        }
    }
}