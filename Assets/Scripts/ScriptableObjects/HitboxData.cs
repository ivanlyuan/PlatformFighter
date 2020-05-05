using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hitbox/Settings", fileName = "HitboxData")]
public class HitboxData : ScriptableObject
{
    [SerializeField] private Vector2 _positionOffset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private int _numOfActiveFrames = 1;
    [SerializeField] private float _damage;
    [SerializeField] private float _rotation;
    [SerializeField] private int _bkb;
    [SerializeField] private int _kbg;
    [SerializeField] private float _sdiFactor;
    [SerializeField] private Vector2 _kbAngle;

    public Vector2 positionOffset { get { return _positionOffset; } }
    public Vector2 size { get { return _size; } }
    public int numOfActiveFrames { get { return _numOfActiveFrames; } }
    public float damage { get { return _damage; } }
    public float rotation { get { return _rotation; } }
    public int bkb { get { return _bkb; } }
    public int kbg { get { return _kbg; } }
    public float sdiFactor { get { return _sdiFactor; } }
    public Vector2 kbAngle { get { return _kbAngle; } }





}
