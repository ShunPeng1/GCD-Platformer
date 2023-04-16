using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour {
    [Header("Object's info")]
    [SerializeField] private GameObject obj;

    private Text _text;
    private Info info;

    private void Awake() {
        _text = this.GetComponent<Text>();
        info = obj.GetComponent<Info>();
    }

    private void Update() {
        _text.text = info.GetPoint().ToString();
    }
}
