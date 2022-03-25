using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
    public class ChangeSpriteByDirection : MonoBehaviour
    {
        [SerializeField] private List<Skin> _skins;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Space(10)]
        [SerializeField] private Sprite _left;
        [SerializeField] private Sprite _right;
        [SerializeField] private Sprite _up;
        [SerializeField] private Sprite _down;
        [Space(10)]
        private Vector3 _curDirection;
        private int prevIndex = -1;

        
        private void OnEnable()
        {
            _curDirection.x = 1;
            
            UpdateSprite();
        }
        
        
        public void SetMoveDirection(Vector2 targetDirection)
        {
            if (targetDirection == Vector2.zero ||
                _curDirection.x == targetDirection.x && _curDirection.y == targetDirection.y)
                return;
            
            _curDirection = targetDirection;
            UpdateSprite();
        }
        public void UpdateSkin(int index)
        {
            if (index >= _skins.Count || _skins.Count == 0 )
            {
                Debug.LogError("Need more skins!!!");
                return;
            }
            if (prevIndex == index)
                return;
            
            _left = _skins[index].Left;
            _right = _skins[index].Right;
            _up = _skins[index].Up;
            _down = _skins[index].Down;
            
            UpdateSprite();
        }
        private void UpdateSprite()
        {
            Sprite sprite = _down;

            if (_curDirection.x < 0)
                sprite = _left;
            else if (_curDirection.x > 0)
                sprite = _right;
            else if (_curDirection.y > 0)
                sprite = _up;
            else if (_curDirection.y < 0)
                sprite = _down;

            _spriteRenderer.sprite = sprite;
        }
    }
}
