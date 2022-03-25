using UnityEngine;


namespace NucGames.Bombs
{
    public class JoystickUI4Direction : MonoBehaviour
    {
        [SerializeField] private bool _active;
        [SerializeField] private RectTransform _joyRT;
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
        private void Update()
        {
            if (_active)
            {
                JoyControl();
            }
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
        private void JoyControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _joyRT.gameObject.SetActive(true);
                _joyRT.position = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _joyRT.gameObject.SetActive(false);
                _joyCenterRT.position = Vector3.zero;
            }
        }
    }
}
