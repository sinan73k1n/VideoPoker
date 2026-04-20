using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Video Poker/Int Variable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] int _value;

    public event Action<int> OnValueChanged;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public void Add(int amount) => Value += amount;
}
