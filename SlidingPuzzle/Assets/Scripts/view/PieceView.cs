﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.model;
using DG.Tweening;
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
        ExecuteMovement(movement,true);
    }

    private void TryMovement()
    {
        RequestMovement?.Invoke(this);
    }

    public void ExecuteMovement(Tuple<int, int> tuple)
    {
        piecePosition.DOAnchorPos(
            new Vector2(InitialPosition.x + (tuple.Item2 * 100),
                -(InitialPosition.y + (tuple.Item1 * 100))), 0.2f);

        Movement.SwapTuple(tuple);
    }

    public void ExecuteMovement(Movement movement, bool forceMovement)
    {
        piecePosition.anchoredPosition =
            new Vector2(InitialPosition.x + (movement.Tuple.Item2 * 100), -(InitialPosition.y + (movement.Tuple.Item1 * 100)));

        Movement.SwapTuple(movement.Tuple);
    }

    public void Disable()
    {
        button.enabled = false;
    }
}
