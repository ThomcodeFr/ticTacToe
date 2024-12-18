using CSharpFunctionalExtensions;
using TicTacToe;

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
        public Result<PlayerMoves> GetNextMove(List<Cell> grid);

        public char icon { get; }
    }

    // Créer un stupidIA (bonus une vraie IA) qui prend la prochaine cell disponible pour jouer
    // Par defaut, jouer contre cette IA (bonus configurer la partie 2P 1Pvs1TA IAvsIA)


}
