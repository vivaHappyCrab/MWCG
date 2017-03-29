using System.Collections.Generic;
using MWCGClasses.InGame;

namespace MWCGClasses.ClientInterface
{
    public enum ActionType{
        /// <summary>
        /// Select permanents
        /// </summary>
        FieldObjects,

        /// <summary>
        /// Select graveyard permanents
        /// </summary>
        GraveYardObjects,

        /// <summary>
        /// Select cards from hand
        /// </summary>
        HandCard,

        /// <summary>
        /// Select cards from library
        /// </summary>
        LibCard,

        /// <summary>
        /// Opponent cards with vision
        /// </summary>
        OppOpenCard,

        /// <summary>
        /// Opponent cards witout vision
        /// </summary>
        OppCloseCard,

        /// <summary>
        /// Select attacker/defenders
        /// </summary>
        Attack,

        /// <summary>
        /// Cancel action
        /// </summary>
        Cancel,

        /// <summary>
        /// Skip action
        /// </summary>
        Skip,

        /// <summary>
        /// System actions
        /// </summary>
        /// <remarks>target={0-Surrender,</remarks>
        System,

        /// <summary>
        /// Sort target's List
        /// </summary>
        Sort
    }

    /// <summary>
    /// Интерфейс клиента.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Создаёт клиенту запрос на действие.
        /// </summary>
        /// <param name="type">Тип действия.</param>
        /// <param name="targets">Доступные цели.</param>
        /// <param name="message">Описание действия.</param>
        /// <returns>Ответ с реакцией на действие.</returns>
        Answer CreateAction(ActionType type, List<int> targets, string message=null);

        /// <summary>
        /// Создаёт клиенту запрос на действие.
        /// </summary>
        /// <param name="type">Тип действия.</param>
        /// <param name="actors">Доступные инициаторы.</param>
        /// <param name="targets">Доступные цели.</param>
        /// <param name="message">Описание действия.</param>
        /// <returns>Ответ с реакцией на действие.</returns>
        Answer CreateAction(ActionType type, List<int> actors, List<int> targets, string message = null);

        /// <summary>
        /// Отдаёт клиенту состояние с игрой.
        /// </summary>
        /// <param name="g">Игра, отданная клиенту.</param>
        /// <returns>True в случае успешной записи игры.</returns>
        bool GetGameState(Game g);
    }
}
