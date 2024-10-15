using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [SerializeField]
    public float _playerhealth;
    [SerializeField]
    public float Gems;
    public float Health { get; set; }
    private Rigidbody2D movement_rigidbody;
    public bool IsGrounded;
    public GameObject _playerAnimation;
    private PlayerAnimation _anim;
    private UIScript _uiScriptVariable;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _uiScriptVariable = GameObject.Find("UIManager").GetComponent<UIScript>();
        Health = _playerhealth;
        _speed = 1.5f;
        _anim = _playerAnimation.GetComponent<PlayerAnimation>();
        movement_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        MovementScript();
    }

    void MovementScript()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        movement_rigidbody.velocity = new Vector2(horizontalInput * _speed, movement_rigidbody.velocity.y);
        _anim.Move(horizontalInput);
        //Regenerate
        Regenerate();
        //Attack
        PressAttack();
        //check whether grounded
        ISGroundedCheck();
        _anim.IsJumping(IsGrounded);
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            _anim.IsJumping(!IsGrounded);
            Debug.Log("space pressed");
            movement_rigidbody.velocity = new Vector2(0, 5);
        }
    }

    public void Regenerate()
    {
        if (Input.GetKeyDown(KeyCode.E) && Gems > 0)
        {
            Health += 10;
            Gems--;
        }
        else if (Input.GetKeyDown(KeyCode.E) && Gems < 1)
        {
            _uiScriptVariable._gameMessages.text = "Not Enough Gems";
        }
    }

    public void GemCollectCount()
    {
        Gems++;
    }

    void PressAttack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded == true)
        {
            _anim.Attack();
        }
    }

    void ISGroundedCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, 1 << 6);
        if (hit.collider == null)
        {
            IsGrounded = false;
        }
        else
        {
            IsGrounded = true;
        }
    }
    public void Damage()
    {
        Health -= 20;
        if (Health < 1)
        {
            Destroy(gameObject);
            _uiScriptVariable.GameOver();
        }
    }
}
