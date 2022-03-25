using UnityEngine;


namespace NucGames.Bombs
{
    public class JoystickStaticUI4Direction : MonoBehaviour
    {
        [SerializeField] private bool _active;
        [SerializeField] private RectTransform _joyCenterRT;
        [SerializeField] private float _scale = 10;
        private Vector3 _direction;
        private Vector3 beginPos;
        private Vector3 curPos;

        
        private void OnEnable()
        {
            _active = true;
        }
        private void OnDisable()
        {
            _active = false;
        }
       

        public void SetDirection(Vector2 direction)
        {
            if (!_active)
                return;

            UpdatePosition(direction);
        }
        private void UpdatePosition(Vector2 direction)
        {
            _joyCenterRT.localPosition = direction * _scale;
        }
    }
}
