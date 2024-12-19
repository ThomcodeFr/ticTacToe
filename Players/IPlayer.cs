using CSharpFunctionalExtensions;
using ticTacToe.Board;
using ticTacToe.Players;

namespace ticTacToe
{
    /**
     * Sert à définir un contrat commun que les différentes implémentations (joueurs humains ou IA) doivent respecter. 
     * Cela permet au reste du code de manipuler ces objets de manière uniforme,
     * sans avoir à connaître leur type spécifique.
     * Ici, un player doit avoir une icon et une méthode GetNextMove qui retourne un Result<PlayerMoves>
     */

    interface IPlayer
    {
        // Result permet de retourner une valeur ou une erreur
        public Result<PlayerMoves> GetNextMove(List<Cell> grid);

        public char Icon { get; }
    }
}
