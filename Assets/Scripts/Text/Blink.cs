using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    TextMeshProUGUI _lbl;
    public float BlinkFadeInTime = 1f;
    public float BlinkStayTime = 1f;
    public float BlinkFadeOutTime = 1f;
    private float _timeChecker = 0;
    private Color _color;
    private bool _fadeIn = true;

    void Start()
    {
        _lbl = GetComponent<TextMeshProUGUI>();
        _color = _lbl.color;
    }

    void Update()
    {
        if (_fadeIn)
        {
            _timeChecker += Time.deltaTime;
            if (_timeChecker < BlinkFadeInTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, _timeChecker / BlinkFadeInTime);
            }
            else if (_timeChecker < BlinkFadeInTime + BlinkStayTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, 1);
            }
            else if (_timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, 1 - (_timeChecker - (BlinkFadeInTime + BlinkStayTime)) / BlinkFadeOutTime);
            }
            else
            {
                _timeChecker = 0;
                _fadeIn = false;
            }
        }
        else
        {
            _timeChecker += Time.deltaTime;
            if (_timeChecker < BlinkFadeInTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, 1 - (_timeChecker / BlinkFadeInTime));
            }
            else if (_timeChecker < BlinkFadeInTime + BlinkStayTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, 0);
            }
            else if (_timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
            {
                _lbl.color = new Color(_color.r, _color.g, _color.b, (_timeChecker - (BlinkFadeInTime + BlinkStayTime)) / BlinkFadeOutTime);
            }
            else
            {
                _timeChecker = 0;
                _fadeIn = true;
            }
        }
    }
}
