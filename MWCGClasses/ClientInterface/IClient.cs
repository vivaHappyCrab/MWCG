using System.Collections.Generic;
using MWCGClasses.InGame;

namespace MWCGClasses.ClientInterface
{
    public enum ActionType{
        FieldObjects,
        GraveYardObjects,
        HandCard,
        LibCard,
        OppOpenCard,
        OppCloseCard,
        Attack,
        Cancel,
        Skip,
        System 
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
        /// <param name="messsage">Описание действия.</param>
        /// <returns>Ответ с реакцией на действие.</returns>
        Answer CreateAction(ActionType type, List<int> targets, string message=null);

        /// <summary>
        /// Создаёт клиенту запрос на действие.
        /// </summary>
        /// <param name="type">Тип действия.</param>
        /// <param name="actors">Доступные инициаторы.</param>
        /// <param name="targets">Доступные цели.</param>
        /// <param name="messsage">Описание действия.</param>
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
