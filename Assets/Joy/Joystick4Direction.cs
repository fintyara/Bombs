using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NucGames.Bombs
{
    public class Joystick4Direction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        public Vector2Event onDirectionUpdate;
        [SerializeField] private bool _active;
        [SerializeField] private float _minDistance = 10;
        private bool _pressed;
        private Vector2 _direction;
        private Vector2 beginPos;
        private Vector2 curPos;


        private void OnEnable()
        {
            _active = true;
        }
        private void OnDisable()
        {
            _active = false;
            _direction = Vector3.zero;
        }
        private void Update()
        {
            if (_active)
            {
                DirectionControl();
            }
        }


        private void DirectionControl()
        {
            if (_pressed && Input.GetMouseButton(0))
            {
                curPos = Input.mousePosition;

                _direction = curPos - beginPos;

                float distX = Mathf.Abs(beginPos.x - curPos.x);
                float distY = Mathf.Abs(beginPos.y - curPos.y);


                if (distX < _minDistance || distX < distY)
                    _direction.x = 0;
                if (distY < _minDistance || distY < distX)
                    _direction.y = 0;

                _direction = _direction.normalized;

                onDirectionUpdate?.Invoke(_direction);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            beginPos = Input.mousePosition;
            _pressed = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _direction = Vector3.zero;
            _pressed = false;
            onDirectionUpdate?.Invoke(_direction);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _direction = Vector3.zero;
            _pressed = false;
            onDirectionUpdate?.Invoke(_direction);
        }
        // Afaik needed so Pointer exit works .. doing nothing further
        public void OnPointerEnter(PointerEventData eventData)
        {
        }
        
        public enum DirectionMove
        {
            left,right,up,down
        }
    }
}
