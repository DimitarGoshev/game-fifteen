using System;

namespace GameFifteen
{
    /// <summary>
    /// Represents a static class
    /// performing game movement.
    /// </summary>
    static class Movement
    {
        /// <summary>
        /// Represents a method that moves the number in the available <seealso cref="MoveDirection.cs"/>.
        /// </summary>
        /// <param name="oldPosition">The position that the number was originally set to.</param>
        /// <param name="direction">The direction of the available movement.</param>
        private static void MoveInDirection(Position oldPosition, MoveDirection direction, GameField movingField)
        {
            Position newPosition = CalculatePositionWithDirection(oldPosition,
                direction);

            if (movingField[newPosition.Row, newPosition.Column] == GameField.EMPTY_CELL)
            {
                string itemToMove = movingField[oldPosition.Row, oldPosition.Column];
                movingField[newPosition.Row, newPosition.Column] = itemToMove;
                movingField[oldPosition.Row, oldPosition.Column] = GameField.EMPTY_CELL;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(movingField.ToString());
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Represents a method that attempts to move the number in proper
        /// <seealso cref="MoveDirecction.cs"/>
        /// </summary>
        /// <param name="isMoveValid">If the movement is valid.</param>
        /// <param name="currentPosition">The current position of the number..</param>
        /// <param name="direction">The direction to attempt to move to.</param>
        public static void TryMovingInDirection(ref int moveCount, ref bool isMoveValid, Position currentPosition, MoveDirection direction, GameField movingField)
        {
            if (CanMoveInDirection(currentPosition, direction, movingField))
            {
                MoveInDirection(currentPosition, direction, movingField);
                isMoveValid = true;
                moveCount++;
            }
        }

        /// <summary>
        /// Represents a method that validates the state of the number movement.
        /// </summary>
        /// <param name="currentPosition">The current position of the number..</param>
        /// <param name="direction">The direction to attempt to move to.</param>
        /// <returns>True or False depending if the number can be moved
        /// in the given <seealso cref="MoveDirection.cs"/></returns>
        private static bool CanMoveInDirection(Position position, MoveDirection direction, GameField movingField)
        {
            if (direction == MoveDirection.Up &&
                position.Row <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Down &&
                position.Row >= GameEngine.BOARD_SIZE - 1)
            {
                return false;
            }

            if (direction == MoveDirection.Left &&
                position.Column <= 0)
            {
                return false;
            }

            if (direction == MoveDirection.Right &&
                position.Column >= GameEngine.BOARD_SIZE - 1)
            {
                return false;
            }

            Position newPosition = CalculatePositionWithDirection(position, direction);

            if (movingField[newPosition.Row, newPosition.Column] != GameField.EMPTY_CELL)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Represents a method that gives the moved number new
        /// coordinates in the GameField
        /// </summary>
        /// <param name="position">The Old Position of the number.</param>
        /// <param name="direction">The direction to move to.</param>
        /// <returns>The new position of the number.</returns>
        private static  Position CalculatePositionWithDirection(Position position,
            MoveDirection direction)
        {
            Position newPosition = (Position)position.Clone();

            switch (direction)
            {
                case MoveDirection.Down:
                    newPosition.Row++;
                    break;
                case MoveDirection.Left:
                    newPosition.Column--;
                    break;
                case MoveDirection.Right:
                    newPosition.Column++;
                    break;
                case MoveDirection.Up:
                    newPosition.Row--;
                    break;
            }

            return newPosition;
        }
    }
}
