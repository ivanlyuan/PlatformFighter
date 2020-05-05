using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Settings", fileName = "ProjectileData")]
public class ProjectileSettings : ScriptableObject
{
    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    [SerializeField]
    private Vector2 _launchAngle = new Vector2(10f,0f);

    [SerializeField]
    private Vector2 _spawnOffset;

    [SerializeField]
    private float _gravityScale = 0;

    [SerializeField]
    private float _maxExistTime = 2.5f;


    public RuntimeAnimatorController animatorController { get { return _animatorController; } }
    public Vector2 launchAngle { get { return _launchAngle; } }
    public Vector2 spawnOffset { get { return _spawnOffset; } }
    public float gravityScale { get { return _gravityScale; } }
    public float maxExistTime { get { return _maxExistTime; } }
}
