using UnityEngine;


namespace NucGames.Bombs
{
    public class Input4Direction : MonoBehaviour
    {
        public Vector2Event onDirectionUpdate;
        [SerializeField] private bool _active;
        private Vector2 _direction;
        
        private void OnEnable()
        {
            _active = true;
        }
        private void OnDisable()
        {
            _active = false;
        }
        
        
        public void MoveX(int axis)
        { 
            if (!_active)
                return;
            
            _direction.y = 0;
            _direction.x = axis;
            
            onDirectionUpdate?.Invoke(_direction);
        }
        public void MoveY(int axis)
        {
            if (!_active)
                return;

            _direction.y = axis;
            _direction.x = 0;
            
            onDirectionUpdate?.Invoke(_direction);
        }
        public void MoveStop()
        {
            if (!_active)
                return;

            _direction.y = 0;
            _direction.x = 0;
            
            onDirectionUpdate?.Invoke(_direction);
        }
    }
}
