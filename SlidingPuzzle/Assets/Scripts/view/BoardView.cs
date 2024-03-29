﻿using System;
using System.Collections.Generic;
using Assets.Scripts.engine;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour
{
    [SerializeField] private List<PieceView> pieces;
    [SerializeField] private Text winnerLabel;

    private BoardEngine engine;
    void Start()
    {
        winnerLabel.gameObject.SetActive(false);
        engine = new BoardEngine();
        int index = 0;

        engine.Movements.ForEach(m =>
        {
            PieceView p = pieces[index];
            if (m.Number != 0)
            {
                p.Initiate(m);
                p.RequestMovement += ReceiveMovement;
                index++;
            }
          
        });
    }

    public void ReceiveMovement(PieceView piece)
    {
        Tuple<int,int> validated = engine.ValidateMovement(piece.Movement);
        if (validated != null)
        {
            piece.ExecuteMovement(validated);
            if (engine.FinishGame())
            {
                WinnerGame();
            }
            
        }
    }

    private void WinnerGame()
    {
        pieces.ForEach(p => { p.Disable(); });
        winnerLabel.gameObject.SetActive(true);
    }
}
