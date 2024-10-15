using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject _swordslashanim, _hitBoxSword;
    public Animator animator, _swordAnimation;
    private SpriteRenderer _playerRenderer, _swordslashrenderer;
    private void Start()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _swordslashrenderer = _swordslashanim.GetComponent<SpriteRenderer>();
        _swordslashanim.SetActive(false);
    }
    public void Move(float move)
    {
        animator.SetFloat("Move", Mathf.Abs(move));
        //Flip direction of the sprite
        if (move > 0)
        {
            _playerRenderer.flipX = false;
            _hitBoxSword.transform.localRotation = new Quaternion(0, 0, 0, 0);
            if (_swordslashrenderer.flipX == true)
            {
                _swordslashrenderer.flipX = false;
            }
        }
        else if (move < 0)
        {
            _playerRenderer.flipX = true;
            _swordslashrenderer.flipX = true;
            _hitBoxSword.transform.localRotation = new Quaternion(0, -180, 0, 0);
        }
    }
    public void IsJumping(bool jump)
    {
        if (!jump)
        {
            animator.SetBool("Jump", true);
        }
        else if (jump)
        {
            animator.SetBool("Jump", false);
        }
    }

    public void Attack()
    {
        _swordslashanim.SetActive(true);
        animator.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordSlash");
    }
}
