using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IGotHitResponder
{
    private readonly Character parentChar;
    private readonly CharacterSettings charSettings;

    private int _airJumpsRemaining = 1;
    private bool _isFacingRight;
    private bool _isGrounded = true;
    private bool _frontIsOnWall = false;
    private bool _backIsOnWall = false;
    private float _damagePercentage = 0;
    private float _meter = 0;
    private float _hitstunDuration = 0;
    private float _lagDuration = 0;
    private float _curlandingLag = 0;//depends on the last aerial move you used
    private bool _canMoveHorizontal = true;
    private float _superArmorDuration = 0;
    private float _armor = 0;
    private float _velocityX = 0;
    private float _velocityY = 0;



    public int airJumpsRemaining { get { return _airJumpsRemaining; } private set { _airJumpsRemaining = value; parentChar.charAnimations.SetAirJumpsRemaining(value); } }
    public bool isFacingRight { get { return _isFacingRight; } private set { _isFacingRight = value; parentChar.charAnimations.SetIsFacingRight(value); } }
    public bool isGrounded { get { return _isGrounded; } private set { _isGrounded = value; parentChar.charAnimations.SetIsGrounded(value); } }
    public bool frontIsOnWall { get { return _frontIsOnWall; } private set { _frontIsOnWall = value; parentChar.charAnimations.SetIsOnWall(value,true); } }
    public bool backIsOnWall { get { return _backIsOnWall; } private set { _backIsOnWall = value; parentChar.charAnimations.SetIsOnWall(value,false); } }
    public float damagePercentage
    {
        get
        {
            return _damagePercentage;
        }
        private set
        {
            _damagePercentage = value;
            if (UIManager.Instance != null)
            {
                CharacterPanel cp = UIManager.Instance.GetCharPanel(parentChar.playerNumber);
                if (cp != null) 
                {
                    cp.UpdateDamageText();
                }
            }

        }
    }
    public float meter
    {
        get
        {
            return _meter;
        }
        private set
        {
            if (value <= 0)
            {
                _meter = 0;
            }
            if (value >= charSettings.maxMeterSize)
            {
                _meter = charSettings.maxMeterSize;
            }
            else
            {
                _meter = value;
            }

            if (UIManager.Instance != null)
            {
                CharacterPanel cp = UIManager.Instance.GetCharPanel(parentChar.playerNumber);
                if (cp != null)
                {
                    cp.meterBar.SetMeterBar(_meter, charSettings.maxMeterSize);
                }
            }
        } }
    public float hitstunDuration { get { return _hitstunDuration; } set { _hitstunDuration = value; } }
    public float lagDuration { get { return _lagDuration; } set { _lagDuration = value; parentChar.charAnimations.animator.SetBool("isInLag", value > 0); } }
    public float curLandingLag { get { return _curlandingLag; } set { _curlandingLag = value; } }
    public bool canMoveHorizontal { get { return _canMoveHorizontal; } set { _canMoveHorizontal = value; } }
    public float superArmorDuration { get { return _superArmorDuration; } set { _superArmorDuration = value; } }
    public float armor { get { return _armor; } set { _armor = value; } }
    public float velocityX { get { return _velocityX; } private set { _velocityX = value; parentChar.charAnimations.animator.SetFloat("velocityX", value); } }
    public float velocityY { get { return _velocityY; } private set { _velocityY = value; parentChar.charAnimations.animator.SetFloat("velocityY", value); } }


    public CharacterState(Character _parentChar,CharacterSettings _charSettings)
    {
        parentChar = _parentChar;
        charSettings = _charSettings;
        airJumpsRemaining = charSettings.numOfAirJumps;

        parentChar.OnLandEvent += OnLand;
        parentChar.OnLeaveGroundEvent += OnLeaveGround;
        parentChar.OnTouchWallEvent += OnTouchWall;
        parentChar.OnLeaveWallEvent += OnLeaveWall;
        parentChar.OnKnockedOutEvent += OnRespawn;
    }



    public void Tick()
    {
        if (hitstunDuration > 0)
        {
            hitstunDuration -= Time.deltaTime;
        }

        if (lagDuration > 0)
        {
            lagDuration -= Time.deltaTime;
        }

        if (superArmorDuration > 0)
        {
            superArmorDuration -= Time.deltaTime;
        }

        velocityX = parentChar.rb.velocity.x;
        velocityY = parentChar.rb.velocity.y;

    }


    public void Flip()
    {
        //parentChar.transform.localScale = new Vector3(-parentChar.transform.localScale.x, parentChar.transform.localScale.y, parentChar.transform.localScale.z);
        if (isFacingRight)
        {
            parentChar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            parentChar.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        isFacingRight = !isFacingRight;
    }

    private void OnLand()
    {
        isGrounded = true;
        airJumpsRemaining = charSettings.numOfAirJumps;
    }

    private void OnLeaveGround()
    {
        isGrounded = false;
    }

    private void OnTouchWall(bool isFront)
    {
        if (isFront)
        {
            frontIsOnWall = true;
        }
        else
        {
            backIsOnWall = true;
        }

    }

    private void OnLeaveWall(bool isFront)
    {
        if (isFront)
        {
            frontIsOnWall = false;
        }
        else
        {
            backIsOnWall = false;
        }
    }



    public void OnGotHit(Hitbox hitbox)
    {

        if (superArmorDuration <= 0)
        {
            parentChar.rb.velocity = Vector2.zero;
            parentChar.rb.AddForce(FormulaHelper.GetKnockBack(hitbox, parentChar, new Vector2(parentChar.charInput.InputDirectionX, 0)));
            hitstunDuration = FormulaHelper.GetHitstunDuration(hitbox, parentChar);
            lagDuration = 0;
            curLandingLag = 0;
        }


        damagePercentage += hitbox.hitboxData.damage;
        meter += hitbox.hitboxData.damage;

        //////////  
    }

    public void OnAirJump()
    {
        airJumpsRemaining--;
    }

    public void OnDealDamage(float amount)
    {
        meter += amount / 3;
    }

    public void OnRespawn()
    {
        damagePercentage = 0;
        airJumpsRemaining = charSettings.numOfAirJumps;
        hitstunDuration = 0;
    }

    public void UseMeter(int numOfBars)
    {
        meter -= numOfBars * charSettings.sizeOfEachMeterBar;
    }

    public void EnterLandingLag()
    {
        lagDuration = curLandingLag;
    }


    

}


