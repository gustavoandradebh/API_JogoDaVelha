using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JogoDaVelhaAPI.Models
{
    public class BoardItem
    {
        public string id { get; set; }

        public char nextPlayer { get; set; }

        public string internalBoardData { get; set; }

        [NotMapped]
        public string[] boardData
        {
            get
            {
                return internalBoardData.Split(';');
            }
        }
    }

    
}
