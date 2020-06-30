using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine;
using Assets.Scripts.model;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private List<PieceView> pieces;

    private BoardEngine engine;
    void Start()
    {
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
        }
        else
        {
            Debug.LogWarning("MOVIMENTO INVALIDO ! ");
        }
    }
}
