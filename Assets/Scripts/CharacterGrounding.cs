using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{

    [SerializeField]
    private Transform leftFoot;

    [SerializeField]
    private Transform rightFoot;

    [SerializeField]
    private float maxDistance = 0.5f;

    [SerializeField]
    private LayerMask layerMask;

    public bool IsGrounded { get; private set; }

    void Update()
    {
        CheckGrounding(leftFoot);
        if (IsGrounded == false)
        {
            CheckGrounding(rightFoot);
        }
    }

    private void CheckGrounding(Transform foot)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(foot.position, Vector2.down, maxDistance, layerMask);

        Debug.DrawRay(foot.position, Vector3.down * maxDistance, Color.red);
        if (raycastHit.collider != null)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
}
