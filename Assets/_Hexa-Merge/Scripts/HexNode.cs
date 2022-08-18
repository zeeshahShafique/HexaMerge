using System;
using _Hexa_Merge.Scripts.Enums;
using UnityEngine;

public class HexNode : MonoBehaviour
{
    private Vector2 _movement;
    
    [SerializeField] private Vector2 _Position;
    [SerializeField] private int _Index;
    [SerializeField] private HexState _State;

    public HexNode(Vector2 position, int index)
    {
        this._Position = position;
        this._Index = index;
        this._State = HexState.Vacant;
    }
    
    private void Update()
    {
        if (this._State == HexState.Selected)
        {
            _movement = InputSystem.TouchToWorldPos;
            float distance = Vector2.Distance(transform.localPosition, _movement);
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                _movement + Vector2.up,
                Time.deltaTime * 2 + distance);
        }
    }
}
