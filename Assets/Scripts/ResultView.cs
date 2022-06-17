using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Text))]
public class ResultView : MonoBehaviour
{

    Text _text;
    private void Start()
    {
        _text = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        int m = Mathf.FloorToInt(GameManager.Instance.PlayTime / 60);
        int s = Mathf.FloorToInt(GameManager.Instance.PlayTime - m * 60);
        _text.text = $"{m}:{s}";
    }
}
