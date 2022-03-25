using UnityEngine;


namespace NucGames.Bombs
{
    public class CameraSizeScaler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _factor;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private bool _test;
        private float _startSize;
        private float _scale;
        

        private void Start()
        {
            _startSize = _camera.orthographicSize;

            _scale = (float) Screen.width / Screen.height;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize / _scale * _factor, _minSize, _maxSize);
        }
        private void Update()
        {
            if (_test)
                Test();
        }


        private void Test()
        {
            _scale = (float) Screen.width / Screen.height;
            _camera.orthographicSize = Mathf.Clamp(_startSize / _scale * _factor, _minSize, _maxSize);
        }
    }
}
