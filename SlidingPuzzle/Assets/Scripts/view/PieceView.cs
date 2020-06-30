using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PieceView : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform piecePosition;

    private static Vector2 InitialPosition = new Vector2(50,50);

    public UnityAction<PieceView> RequestMovement;

    public Movement Movement { get; private set; }

    public void Initiate(Movement movement)
    {
        Movement = movement;
        label.text = movement.Number.ToString();
        button.onClick.AddListener(TryMovement);
        ExecuteMovement(movement);
    }

    private void TryMovement()
    {
        RequestMovement?.Invoke(this);
    }

    public void ExecuteMovement(Movement movement)
    {
        piecePosition.anchoredPosition =
            new Vector2(InitialPosition.x + (movement.Tuple.Item1*100), -(InitialPosition.y + (movement.Tuple.Item2*100)));

        Movement.SwapTuple(movement.Tuple);
    }
}
