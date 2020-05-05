using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Settings", fileName = "CharacterData")]
public class CharacterSettings : ScriptableObject
{
    [SerializeField]
    private float _dashSpeed = 500;

    [SerializeField]
    private float _dashDuration = 0.2f;

    [SerializeField]
    private float _groundSpeed = 5;

    [SerializeField]
    private float _airSpeed = 5;

    [SerializeField]
    private float _jumpPower = 700;

    [SerializeField]
    private float _gravityScale = 3;

    [SerializeField]
    private float _linearDrag = 0;

    [SerializeField]
    private float _maxFallSpeed = -9.8f;

    [SerializeField]
    private int _numOfAirJumps = 1;

    [SerializeField]
    private int _numOfMeterBars = 3;

    [SerializeField]
    private int _sizeOfEachMeterBar = 40;

    [SerializeField]
    private bool _isDummy = false;

    [SerializeField]
    private Sprite _charIcon;

    [SerializeField]
    private RuntimeAnimatorController _animatorController;


    public float dashSpeed { get { return _dashSpeed; } }
    public float dashDuration { get { return _dashDuration; } }
    public float groundSpeed { get { return _groundSpeed; } }
    public float airSpeed { get { return _airSpeed; } }
    public float jumpPower { get { return _jumpPower; } }
    public float gravityScale { get { return _gravityScale; } }
    public float linearDrag { get { return _linearDrag; } }
    public float maxFallSpeed { get { return _maxFallSpeed; } }
    public int numOfAirJumps { get { return _numOfAirJumps; } }
    public bool isDummy { get { return _isDummy; } }
    public int numOfMeterBars { get { return _numOfMeterBars; } }
    public float sizeOfEachMeterBar { get { return _sizeOfEachMeterBar; } }
    public float maxMeterSize { get { return _numOfMeterBars * _sizeOfEachMeterBar; } }
    public Sprite charIcon { get { return _charIcon; } }
    public RuntimeAnimatorController animatorController { get { return _animatorController; } }
}
