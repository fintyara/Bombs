using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawJoystickUI : MonoBehaviour
{
    #region VAR
    [SerializeField] private bool _onlyY;
    [SerializeField] private bool _onlyX;
    [SerializeField] private bool _active;
    [SerializeField] private Image _joyImage;
    [SerializeField] private Image _joyCenterImage;
    [SerializeField] private float _scale;
    [SerializeField] private float _minDistance = 10;
    private Vector3 _direction;
    private Vector3 beginPos;
    private Vector3 curPos;
    #endregion

    #region MONO
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

    #endregion

    #region FUNC
    private void JoyControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _joyImage.gameObject.SetActive(true);
            _joyImage.rectTransform.position = Input.mousePosition;

            SetBeginPos();
        }

        if (Input.GetMouseButton(0))
        {
            SetCurPos();

            if (Vector3.Distance(beginPos, curPos) > _minDistance)
            {
                _direction = curPos - beginPos;
                _direction = _direction.normalized;
            }
            else
            {
                _direction = Vector3.zero;
            }

            _joyCenterImage.rectTransform.localPosition = _direction * _scale;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _joyImage.gameObject.SetActive(false);
            _joyCenterImage.rectTransform.position = Vector3.zero;
        }
    }
    private void SetBeginPos()
    {
        if (_onlyX)
        {
            beginPos = new Vector3(Input.mousePosition.x, 0, 0);
        }
        else if (_onlyY)
        {
            beginPos = new Vector3(0, Input.mousePosition.y, 0);
        }
        else
        {
            beginPos = Input.mousePosition;
        }

    }
    private void SetCurPos()
    {
        if (_onlyX)
        {
            curPos = new Vector3(Input.mousePosition.x, 0, 0);
        }
        else if (_onlyY)
        {
            curPos = new Vector3(0, Input.mousePosition.y, 0);
        }
        else
        {
            curPos = Input.mousePosition;
        }
    }

    public void Activate()
    {
        _active = true;
    }
    public void Deactivate()
    {
        _joyImage.gameObject.SetActive(false);
        _active = false;
    }
    public void Switch()
    {
        _active = !_active;
    }

    #endregion
}

