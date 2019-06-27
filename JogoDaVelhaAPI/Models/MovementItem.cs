namespace JogoDaVelhaAPI.Models
{
    public class MovementItem
    {
        public string id { get; set; }
        public char player { get; set; }

        public PositionItem position { get; set; }
    }
}
