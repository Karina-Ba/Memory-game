using System;

namespace B20_Ex02_01
{
    public class Program
    {
        public static void Main()
        {
            InitProgram();
        }

        public static void InitProgram()
        {
            UserInterface UI;
            //Call method from UI to get players 
            //Call Method start game
            UI.InitGame(); //Ask player for name, create first player in GuessingGame(0 in array), returns to UI asks for second player, creates second player in GuessingGame(1 in arr)
            //returns to ui asks for size of board, goes to GuessingGame createsBoard and start the game 


        }
    }
}
