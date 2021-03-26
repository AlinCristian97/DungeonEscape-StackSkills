using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private LayerMask _groundLayer;
    private bool _resetJumpNeeded;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _isGrounded = false;
            _resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
        }
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);

        if (hitInfo.collider != null)
        {
            if (_resetJumpNeeded == false)
            {
                _isGrounded = true;
            }
        }
        
        _rigidbody.velocity = new Vector2(horizontalInput, _rigidbody.velocity.y);
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }
}
