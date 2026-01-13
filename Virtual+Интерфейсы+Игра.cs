//задание 1
/*class Car
{
    public virtual void Action()
    {
        Console.WriteLine("Car Action");
    }
}

class DrivingState : Car
{
    public override void Action()
    {
        Console.WriteLine("Машина едет, неполадок нет.");
    }
}

class StoppedState : Car
{
    public override void Action()
    {
        Console.WriteLine("Машина заведена, но стоит на месте. Неполадок нет.");
    }
}

class OffState : Car
{
    public override void Action()
    {
        Console.WriteLine("Машина заглушена. Неполадок нет.");
    }
}
 
class BrokenState : Car
{
    public override void Action()
    {
        Console.WriteLine("Машина сломана, движение невозможно!");
    }
}

class SlippingState : Car
{
    public override void Action()
    {
        Console.WriteLine("Вы на бездорожье, машина буксует. Движение затруднено.");
    }
}*/

//задание 2
/*interface IActionOfSmartphone 
{
    void Action();
}

class SmartphoneAcceptCall : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Звонок принят.");
        
    }
}

class SmartphoneDeadPhone : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Телефон разряжен.");
        
    }
}

class SmartphonePhoneLocked : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Телефон заблокирован.");
        
    }
}

class SmartphoneOutOfNetwork : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Отсутствует интернет-соединение.");
        
    }
}

class SmartphoneDisplayDamaged : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Дисплей повреждён, но смартфон всё ещё работает.");

    }
}

class SmartphoneDisplayCriticallyDamaged : IActionOfSmartphone
{
    public void Action()
    {
        Console.WriteLine("Критическое повреждение дисплея, дальнейшая работа со смартфоном невозможна.");
        
    }
}*/
//задание 3
class Game
{
    public void FillPole(string[,] pole, int sizeI, int sizeJ, string symbolEmpty)
    {
        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                pole[i, j] = symbolEmpty;
            }
        }
    }

    /*public void FillItems(string[,] pole, int sizeI, int sizeJ,  string symbolEmpty, string symbol, int countItems)
    {
        Random rand = new Random();
        for (int i = 0; i < countItems; i++)
        {
            while (true)
            {
                int randPosI = rand.Next(0, sizeI);
                int randPosJ = rand.Next(0, sizeJ);

                if (pole[randPosI, randPosJ] == symbolEmpty)
                {
                    pole[randPosI, randPosJ] = symbol;
                    break;
                }
            }
        }
    }*/

    public void ShowPole(string[,] pole, int sizeI, int sizeJ)
    {
        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                Console.Write(pole[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    public void UserHod(string[,] pole, int sizeI, int sizeJ, string userPositionSymbol, string symbolEmpty)
    {
        Random rndI = new Random();
        Random rndJ = new Random();
        int userIndexI = rndI.Next(0, sizeI);
        int userIndexJ = rndJ.Next(0, sizeJ);
        pole[userIndexI, userIndexJ] = userPositionSymbol;
        ShowPole(pole, sizeI, sizeJ);

        while (true)
        {
            char chooseMove;
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            chooseMove = keyInfo.KeyChar;
            if (chooseMove == 'w')
            {
                if (userIndexI == 0)
                {
                    Console.Clear();
                    userIndexI = sizeI - 1;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[0, userIndexJ] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
                else
                {
                    Console.Clear();
                    userIndexI--;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI + 1, userIndexJ] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
                
            } 
            else if (chooseMove == 'a')
            {
                if (userIndexJ == 0)
                {
                    Console.Clear();
                    userIndexJ = sizeJ - 1;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI, 0] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
                else
                {
                    Console.Clear();
                    userIndexJ--;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI, userIndexJ + 1] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
            }
            else if (chooseMove == 's')
            {
                if (userIndexI == sizeI - 1)
                {
                    Console.Clear();
                    userIndexI = 0;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[sizeI - 1, userIndexJ] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
                else
                {
                    Console.Clear();
                    userIndexI++;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI - 1, userIndexJ] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
            }
            else if (chooseMove == 'd')
            {
                if (userIndexJ == sizeJ - 1)
                {
                    Console.Clear();
                    userIndexJ = 0;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI, sizeJ - 1] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
                else
                {
                    Console.Clear();
                    userIndexJ++;
                    pole[userIndexI, userIndexJ] = userPositionSymbol;
                    pole[userIndexI, userIndexJ - 1] = symbolEmpty;
                    ShowPole(pole, sizeI, sizeJ);
                }
            }
        }
    }
}

class Program
{
    public static void Main()
    {
         //задание 1
         /*List<Car> states = new List<Car>
         {
             new DrivingState(),
             new StoppedState(),
             new OffState(),
             new BrokenState(),
             new SlippingState()
         };
         
         Random rnd = new Random();
         int action = rnd.Next(0, states.Count);
         states[action].Action();*/
         
        //задание 2
        /*List<IActionOfSmartphone> states = new List<IActionOfSmartphone>
        {
            new SmartphoneAcceptCall(),
            new SmartphoneDeadPhone(),
            new SmartphonePhoneLocked(),
            new SmartphoneOutOfNetwork(),
            new SmartphoneDisplayDamaged(),
            new SmartphoneDisplayCriticallyDamaged()
        };

        Random random = new Random();
        int index = random.Next(0, states.Count);
        states[index].Action();*/
        
        //3 задание
        int sizeI = 10; 
        int sizeJ = 10;

        string symbolEmpty = "[_]";
        string userPositionSymbol = "[*]";
        //string symbolCoin = "[$]";
        //string symbolHP = "[H]";

        string[,] pole = new string[sizeI,sizeJ];
        
        Game game = new Game(); 
        game.FillPole(pole, sizeI, sizeJ, symbolEmpty);
        game.UserHod(pole, sizeI, sizeJ, userPositionSymbol, symbolEmpty);
    }
}