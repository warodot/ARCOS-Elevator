using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class ValuesRelated
{
    public float yTimeToLoop = 2;
    public float xTimeToLoop = 1;

    [Range(0, 1)] public float xTargetValue = 0.5f;
    [Range(0, 1)] public float yTargetValue = 1;
}

public class WaterEffect : MonoBehaviour
{
    public Volume _volume;
    LensDistortion _lens;
    public ValuesRelated _values;
    

    Coroutine _xCor;
    Coroutine _yCor;

    // Update is called once per frame
    void Start()
    {
        _volume.profile.TryGet(out _lens);
        if (_lens)
        {
         //   _xCor = 
           // _yCor = 
            StartCoroutine(XWave()); 
            StartCoroutine(YWave());
        }
    }
    IEnumerator YWave()
    {
        float initial;
        float final;

        if (_lens.yMultiplier.value == _values.yTargetValue)
        {
            initial = _values.yTargetValue;
            final = 0;
        }
        else if (_lens.yMultiplier.value == 0)
        {
            initial = 0;
            final = _values.yTargetValue;
        }
        else
        {
            initial = 0;
            final = 0;
        }

        for (float i = 0; i < _values.yTimeToLoop; i += Time.deltaTime)
        {
            float t = i / _values.yTimeToLoop;
            _lens.yMultiplier.value = Mathf.Lerp(initial, final, t);
            yield return null;
        }

        _lens.yMultiplier.value = final;

        //StopCoroutine(_yCor);
        //_yCor =
        StartCoroutine(YWave());
    }

    IEnumerator XWave()
    {
        float initial;
        float final;

        if (_lens.xMultiplier.value == _values.xTargetValue)
        {
            initial = _values.xTargetValue;
            final = 0;
        }
        else if (_lens.xMultiplier.value == 0)
        {
            initial = 0;
            final = _values.xTargetValue;
        }
        else
        {
            initial = 0;
            final = 0;
        }

        for (float i = 0; i < _values.xTimeToLoop; i += Time.deltaTime)
        {
            float t = i / _values.xTimeToLoop;
            _lens.xMultiplier.value = Mathf.Lerp(initial, final, t);
            yield return null;
        }

        _lens.xMultiplier.value = final;

        //StopCoroutine(_xCor);
        //_xCor = 
        StartCoroutine(XWave());
    }

    
}
