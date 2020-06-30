using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine;
using Assets.Scripts.model;
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
        Movement validatedMovement = engine.ValidateMovement(piece.Movement);
        if (validatedMovement != null)
        {
            //executa
            Debug.LogWarning("MOVIMENTO VALIDO ! ");
            piece.ExecuteMovement(validatedMovement);
            if (engine.FinishGame())
            {
                WinnerGame();
            }
            
        }
        else
        {
            Debug.LogWarning("MOVIMENTO INVALIDO ! ");
        }
    }

    private void WinnerGame()
    {
        pieces.ForEach(p => { p.Disable(); });
        winnerLabel.gameObject.SetActive(true);
    }
}
