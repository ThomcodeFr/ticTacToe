using CSharpFunctionalExtensions;
using ticTacToe;
using ticTacToe.Board;
using ticTacToe.Players;

namespace TicTacToe;

internal class Game
{
    // Attributs
    public static char PlayerOneIcon = 'O';
    public static char PlayerTwoIcon = 'X';

    private readonly Board board;
    private readonly IPlayer humanPlayer;
    private readonly IPlayer aiPlayer;

    // Importe l'inteface IPlayer
    public IPlayer currentPlayer { get; private set; }

    // Constructeur
    public Game()
    {
        this.board = new Board();
        this.humanPlayer = new Player(Game.PlayerTwoIcon);
        this.aiPlayer = new StupidIA();
    }

    // Méthodes
    public void Init()
    {
        this.board.Init();
        this.currentPlayer = this.humanPlayer; // Permet au joueur humain de commencer la partie
    }

    public void Play()
    {
        this.board.DisplayGameBoardAndHeader();

        while (true)
        {
            Result<PlayerMoves> playerMoves = this.currentPlayer.GetNextMove(this.board.grid);

            if (playerMoves.IsFailure)
            {
                Console.WriteLine(playerMoves.Error);
                continue;
            }

            bool movePlayedSuccessfully = this.board.PlayMoveOnBoard(playerMoves.Value, this.currentPlayer.Icon);
            if (!movePlayedSuccessfully)
            {
                Console.WriteLine("Invalid move");
                continue;
            }

            this.board.DisplayGameBoardAndHeader();

            Maybe<string> gameResult = this.board.IsGameOver(currentPlayer);
            if (gameResult.HasValue)
            {
                Console.WriteLine(gameResult.Value);
                break;
            }

            this.SwitchPlayer();
        }
    }

    private void SwitchPlayer()
    {
        this.currentPlayer = (this.currentPlayer == humanPlayer) ? aiPlayer : humanPlayer;
    }
}