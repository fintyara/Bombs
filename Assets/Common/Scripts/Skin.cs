using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Common/Skin")]
public class Skin : ScriptableObject
{
    public Sprite Left => _left;
    public Sprite Right => _right;
    public Sprite Up => _up;
    public Sprite Down => _down;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _down;
}
