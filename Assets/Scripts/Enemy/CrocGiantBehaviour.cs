using System.Collections;
using UnityEngine;

public class CrocGiantBehaviour : Enemy, IDamageable
{
    [SerializeField] private Transform _playerLocation;
    public GameObject _hitbox;
    private Vector3 _destination;
    public GameObject _spriteCroc;
    private Vector2 _directionPlayer;
    private SpriteRenderer _spriteCrocRenderer;
    private Animator _crocAnimator;
    private bool _IsDestinationReached;
    private bool _Ishit;
    public float Health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }
    // Update is called once per frame
    public override void Update()
    {
        CheckDestinationReached();
        CheckPlayerLocation();
        CheckDistancePlayer();
        MovementCroc();
    }

    public void InitializeVariables()
    {
        _Ishit = false;
        Health = base.health;
        _IsDestinationReached = false;
        _spriteCrocRenderer = _spriteCroc.GetComponent<SpriteRenderer>();
        _crocAnimator = _spriteCroc.GetComponent<Animator>();
        _destination = pointA.transform.position;
    }

    public void MovementCroc()
    {
        if (!_Ishit && CheckDistancePlayer() > 2)
        {
            if (_IsDestinationReached == false)
            {
                if (_destination == pointA.transform.position)
                {
                    _spriteCrocRenderer.flipX = false;
                    _hitbox.transform.localRotation = new Quaternion(0, -180, -28, 0);
                }
                else if (_destination == pointB.transform.position)
                {
                    _spriteCrocRenderer.flipX = true;
                    _hitbox.transform.localRotation = new Quaternion(0, 0, 0, 0);
                }
                _crocAnimator.ResetTrigger("Idle");
                _crocAnimator.SetBool("Walk", true);
            }
            // To check if in combat mode
            transform.position = Vector3.MoveTowards(transform.position, _destination, 1f * Time.deltaTime);
        }
    }

    public float CheckDistancePlayer()
    {
        _Ishit = false;
        return Vector3.Distance(_playerLocation.position, transform.position);
    }

    public void CheckDestinationReached()
    {
        if (transform.position == pointA.transform.position)
        {
            _IsDestinationReached = true;
            _crocAnimator.SetBool("Walk", false);
            _crocAnimator.SetTrigger("Idle");
            _spriteCrocRenderer.flipX = true;
            _hitbox.transform.localRotation = new Quaternion(0, 0, 0, 0);
            StartCoroutine(WaitForSeconds());
        }
        else if (transform.position == pointB.transform.position)
        {
            _IsDestinationReached = true;
            _crocAnimator.SetBool("Walk", false);
            _crocAnimator.SetTrigger("Idle");
            _spriteCrocRenderer.flipX = false;
            _hitbox.transform.localRotation = new Quaternion(0, -180, 28, 0);
            StartCoroutine(WaitForSeconds());
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2f);
        if (transform.position == pointA.transform.position)
        {
            _destination = pointB.transform.position;
        }
        else if (transform.position == pointB.transform.position)
        {
            _destination = pointA.transform.position;
        }
        _IsDestinationReached = false;
    }

    public void Damage()
    {
        if (_directionPlayer.x > 0)
        {
            _spriteCrocRenderer.flipX = false;
            _hitbox.transform.localRotation = new Quaternion(0, -180f, 28f, 0);
        }
        else if (_directionPlayer.x < 0)
        {
            _spriteCrocRenderer.flipX = true;
            _hitbox.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        _crocAnimator.SetTrigger("Hit");
        Health -= 15f;
        if (Health < 1)
        {
            Destroy(gameObject);
        }
        _Ishit = true;
        _crocAnimator.SetBool("Walk", false);
        _crocAnimator.SetBool("InCombat", true);
        StartCoroutine(HitCoolDown());
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        _crocAnimator.SetBool("InCombat", false);
    }

    public void CheckPlayerLocation()
    {
        _directionPlayer = transform.localPosition - _playerLocation.localPosition;
    }
}
