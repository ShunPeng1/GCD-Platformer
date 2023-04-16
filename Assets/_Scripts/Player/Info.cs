using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {
    [SerializeField] private int _point;

    public void SetPoint(int val) {
        _point = val;
        if (_point < 0) _point = 0;
    }

    public void AddPoint(int val) {
        _point += val;
        if (_point < 0) _point = 0;
    }

    public int GetPoint() {
        return _point;
    }
}
