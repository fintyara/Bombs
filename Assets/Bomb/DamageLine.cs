using UnityEngine;


namespace NucGames.Bombs
{
    public class DamageLine : MonoBehaviour
    {
        [SerializeField] private float _speedGrouth;
        [SerializeField] private float _maxScale = 1;
        private bool grouth;
        private Vector3 tempScale;


        private void OnEnable()
        {
            StartGrouth();
        }
        private void Update()
        {
            if (grouth)
                GrouthControl();
        }

        
        public void StartGrouth()
        {
            grouth = true;
        }
        public void StopGrouth()
        {
            grouth = false;
        }
        private void GrouthControl()
        {
            tempScale = transform.localScale;

            tempScale.x += _speedGrouth * Time.deltaTime;
            if (tempScale.x > _maxScale)
            {
                tempScale.x = _maxScale;
                StopGrouth();
            }
            
            transform.localScale = tempScale;
        }
    }
}
