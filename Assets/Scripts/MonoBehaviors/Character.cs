using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class Character : MonoBehaviour,IGotHitResponder,ICanCreateHitbox
{

    public Rigidbody2D rb { get; private set; }
    public ICharacterInput charInput { get; private set; }
    public CharacterActions charActions { get; private set; }
    public CharacterState charState { get; private set; }
    public CharacterAnimations charAnimations { get; private set; }
    public int playerNumber { get; private set; }
    public TeamColor teamColor{ get; private set; }
    public Sprite charIcon { get; private set; }

    public delegate void CharDelegate();
    public delegate void CharBoolDelegate(bool b);
    public event CharDelegate OnLandEvent = delegate { };
    public event CharDelegate OnLeaveGroundEvent = delegate { };
    public event CharDelegate OnKnockedOutEvent = delegate { };
    public event CharBoolDelegate OnTouchWallEvent = delegate { };
    public event CharBoolDelegate OnLeaveWallEvent = delegate { };

    [SerializeField]
    private CharacterSettings _charSettings;
    public CharacterSettings charSettings { get { return _charSettings; } set { } }

    public Character hitboxParentChar
    {
        get
        {
            return this;
        }
    }

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = charSettings.animatorController;

        charAnimations = new CharacterAnimations(this, charState, animator);
        charState = new CharacterState(this, charSettings);
        charIcon = charSettings.charIcon;
        rb.gravityScale = charSettings.gravityScale;
        rb.drag = charSettings.linearDrag;
    }

    private void Start()
    {
        if (charSettings.isDummy)
        {
            charInput = new DummyInput();
        }
        else
        {
            charInput = new PlayerInput(playerNumber);
        }
        charActions = new CharacterActions(charInput, rb, charSettings, charState, charAnimations);
    }

    private void Update()
    {

        charInput.ReadInput();
        charActions.Tick();
        charState.Tick();


        if (rb.velocity.y < charSettings.maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, charSettings.maxFallSpeed);
        }

    }

    public void OnLand()
    {
        OnLandEvent();
    }

    public void OnLeaveGround()
    {
        OnLeaveGroundEvent();
    }

    public void OnSpawn(TeamColor _teamColor, int _playerNumber)
    {
        teamColor = _teamColor;
        playerNumber = _playerNumber;
    }

    public void OnGotHit(Hitbox hitbox)
    {
        charAnimations.OnGotHit(hitbox);
        charState.OnGotHit(hitbox);
    }

    public void OnEnterHitstun()
    {
        charAnimations.OnEnterHitstun();
    }

    public void OnExitHitstun()
    {
        charAnimations.OnExitHitstun();
    }

    public void OnKnockedOut()
    {
        OnKnockedOutEvent();
    }

    public void OnTouchWall(bool isFront)
    {
        OnTouchWallEvent(isFront);
    }

    public void OnLeaveWall(bool isFront)
    {
        OnLeaveWallEvent(isFront);
    }

}

public enum TeamColor
{
    Red,
    Blue,
    Yellow,
    Green
}


