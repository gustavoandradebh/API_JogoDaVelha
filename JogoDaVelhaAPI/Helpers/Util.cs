using JogoDaVelhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelhaAPI.Helpers
{
    public static class Util
    {
        public static char PickFirstPlayer()
        {
            char[] starter = { 'X', 'O' };

            Random rand = new Random();
            int index = rand.Next(starter.Length);

            return starter[index];
        }

        public static string InitializeBoard()
        {
            return "00_;10_;20_;01_;11_;21_;02_;12_;22_";
        }

        public static EndGameItem CheckErrors(BoardItem board, string id, MovementItem item)
        {
            if (board == null)
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Partida não encontrada";
                return endGameReturn;
            }

            if (board.nextPlayer != item.player)
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Não é turno do jogador";
                return endGameReturn;
            }

            if (id != item.id)
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Os id's não são iguais. Confira o id da URL e o id do input e tente novamente.";
                return endGameReturn;
            }

            if (item.player != 'X' && item.player != 'O')
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Os jogadores aceitos são X e O.";
                return endGameReturn;
            }

            return null;
        }

        public static EndGameItem CheckIfGameIsOver(BoardItem board, char player)
        {
            if (IsDraw(board.internalBoardData))
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Partida finalizada";
                endGameReturn.winner = "Draw";

                return endGameReturn;
            }
            else
            if (HasWinner(board.boardData))
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Partida finalizada";
                endGameReturn.winner = player.ToString();

                return endGameReturn;
            }

            return null;
        }
        public static Boolean HasWinner(string[] board)
        {
            if (checkDiagonal(board) || checkHorizontal(board) || checkVertical(board))
            {
                return true;
            }
            return false;
        }

        public static Boolean IsDraw(string board)
        {
            bool isDraw = board.IndexOf("_") < 0;
            return isDraw;
        }

        private static Boolean checkDiagonal(string[] board)
        {
            if (board[4][2] == '_')
                return false;

            if (board[0][2] == board[4][2] && board[0][2] == board[8][2])
                return true;

            if (board[2][2] == board[4][2] && board[2][2] == board[6][2])
                return true;

            return false;
        }

        private static Boolean checkHorizontal(string[] board)
        {
            if (board[0][2] == '_' || board[3][2] == '_' || board[6][2] == '_')
                return false;

            if (board[0][2] == board[1][2] && board[0][2] == board[2][2])
                return true;

            if (board[3][2] == board[4][2] && board[3][2] == board[5][2])
                return true;

            if (board[6][2] == board[7][2] && board[6][2] == board[8][2])
                return true;

            return false;
        }

        private static Boolean checkVertical(string[] board)
        {
            if (board[0][2] == board[3][2] && board[0][2] == board[6][2])
            {
                if (board[0][2] != '_')
                    return true;
            }

            if (board[1][2] == board[4][2] && board[1][2] == board[7][2])
            {
                if (board[1][2] != '_')
                    return true;
            }

            if (board[2][2] == board[5][2] && board[2][2] == board[8][2])
            {
                if (board[2][2] != '_')
                    return true;
            }

            return false;
        }


    }
}
