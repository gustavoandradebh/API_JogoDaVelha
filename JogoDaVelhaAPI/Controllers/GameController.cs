using JogoDaVelhaAPI.Helpers;
using JogoDaVelhaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JogoDaVelhaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameContext _context;

        public GameController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardItem>>> GetNotImplemented()
        {
            return await _context.BoardItems.ToListAsync();
        }


        // GET: /Game/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardItem>> GetGameItem(string id)
        {
            var todoItem = await _context.BoardItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }


        // POST: /Game
        [HttpPost]
        public async Task<ActionResult<StartGameItem>> PostGameItems()
        {
            string _id = System.Guid.NewGuid().ToString();
            char _firstPlayer = Util.PickFirstPlayer();

            _context.BoardItems.Add(new BoardItem { id = _id, nextPlayer = _firstPlayer, internalBoardData = Util.InitializeBoard() });

            _context.SaveChanges();

            StartGameItem initialData = new StartGameItem { id = _id, firstPlayer = _firstPlayer };

            return initialData;
        }

        // POST: /Game/{id}/movement
        [HttpPost("{id}/movement")]
        public async Task<ActionResult<EndGameItem>> PostGameItem(string id, MovementItem item)
        {
            var board = _context.BoardItems.Find(item.id);

            EndGameItem endGameItem = Util.CheckErrors(board, id, item);
            if (endGameItem != null)
                return endGameItem;

            string oldPositionIdentifier = item.position.X.ToString() + item.position.Y.ToString() + "_";
            var positionValue = board.boardData.FirstOrDefault(x => x == oldPositionIdentifier);
            if (positionValue != null)
            {
                int keyIndex = Array.IndexOf(board.boardData, oldPositionIdentifier);

                string newPositionIdentifier = item.position.X.ToString() + item.position.Y.ToString() + item.player;
                board.internalBoardData = board.internalBoardData.Replace(oldPositionIdentifier, newPositionIdentifier);

                if (item.player == 'X')
                    board.nextPlayer = 'O';
                else
                    board.nextPlayer = 'X';

                await _context.SaveChangesAsync();

                endGameItem = Util.CheckIfGameIsOver(board, item.player);
                if (endGameItem != null)
                    return endGameItem;
            }
            else
            {
                EndGameItem endGameReturn = new EndGameItem();
                endGameReturn.msg = "Posição já preenchida, jogue novamente.";
                return endGameReturn;
            }

            return Ok();
        }
    }
}
