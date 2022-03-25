using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
    public class FindPath : MonoBehaviour
    {
        [SerializeField] private Raycast2DFix _rayLeft;
        [SerializeField] private Raycast2DFix _rayRight;
        [SerializeField] private Raycast2DFix _rayUp;
        [SerializeField] private Raycast2DFix _rayDown;
        private List<Vector3> _directions = new List<Vector3>();
        private bool isRandom;
        
        
        public Vector3 GetRandomHorizontal()
        {
            _directions.Clear();

            float lengthLeft = _rayLeft.GetLengthRay();
            if(lengthLeft > 1)
                _directions.Add(Vector3.left);
            float lengthRight = _rayRight.GetLengthRay();
            if(lengthRight > 1)
                _directions.Add(Vector3.right);

            if (_directions.Count == 0)
                return Vector3.zero;
            
            int randomIndex = Random.Range(0, _directions.Count);

            return _directions[randomIndex];
        }
        public Vector3 GetRandomVertical()
        {
            _directions.Clear();
            
            float lengthUp = _rayUp.GetLengthRay();
            if(lengthUp > 1)
                _directions.Add(Vector3.up);
            float lengthDown = _rayDown.GetLengthRay();
            if(lengthDown > 1)
                _directions.Add(Vector3.down);

            if (_directions.Count == 0)
                return Vector3.zero;
            
            int randomIndex = Random.Range(0, _directions.Count);

            return _directions[randomIndex];
        }
        public Vector3 GetRandomDirection()
        {
            _directions.Clear();

            float lengthLeft = _rayLeft.GetLengthRay();
            if(lengthLeft > 1)
                _directions.Add(Vector3.left);
            float lengthRight = _rayRight.GetLengthRay();
            if(lengthRight > 1)
                _directions.Add(Vector3.right);
            float lengthUp = _rayUp.GetLengthRay();
            if(lengthUp > 1)
                _directions.Add(Vector3.up);
            float lengthDown = _rayDown.GetLengthRay();
            if(lengthDown > 1)
                _directions.Add(Vector3.down);

            if (_directions.Count == 0)
                return Vector3.zero;
            
            int randomIndex = Random.Range(0, _directions.Count);

            return _directions[randomIndex];
        }
    }
}
