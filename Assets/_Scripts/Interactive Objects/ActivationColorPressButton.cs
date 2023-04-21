using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interactive_Objects;
using UnityEngine;

public class ActivationColorPressButton : PressButton<ActivationColor>
{
    [SerializeField] private ActivationColor _buttonActivationColor;
    [SerializeField] private List<ActivationBar> _activationBars;
    void Start()
    {
        _activationBars = FindObjectsOfType<ActivationBar>().Where((bar) => bar.BarActivationColor == _buttonActivationColor).ToList();
    }
    
    protected override void Active()
    {
        foreach (var bar in _activationBars)
        {
            bar.ActiveMove();
        }
    }

    protected override void Inactive()
    {
        foreach (var bar in _activationBars)
        {
            bar.InactiveMove();
        }
    }
}
