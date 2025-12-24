static bool isFreeCell(string[,] pole, int sizeI, int sizeJ,string emptySymbol)
{
    for (int i = 0; i < sizeI; i++)
    {
        for (int j = 0; j < sizeJ; j++)
        {
            if (pole[i,j] == emptySymbol)
            {
                return true;
            }
        }
    }

    return false;
}

static void FillPole(string[,] pole, int sizeI, int sizeJ,string emptySymbol)
{
    for (int i = 0; i < sizeI; i++)
    {
        for (int j = 0; j < sizeJ; j++)
        {
            pole[i, j] = emptySymbol;
        }
    }
}

static void ShowPole(string[,] pole, int sizeI, int sizeJ)
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

static void User(string[,] pole, int sizeI, int sizeJ,string emptySymbol)
{
    while (true)
    {
        Console.WriteLine("X: ");
        string userI = Console.ReadLine();
        int.TryParse(userI, out int intUserI);
        
        Console.WriteLine("Y: ");
        string userJ = Console.ReadLine();
        int.TryParse(userJ, out int intUserJ);
        
        if ((intUserI > 0 && intUserI < sizeI+1  && intUserJ > 0 && intUserJ < sizeJ+1) && pole[intUserI-1, intUserJ-1] == emptySymbol)
        {
            Console.WriteLine("Ход сделан.");
            pole[intUserI-1, intUserJ-1] = "[X]";
            break;
        }
    }
}

static void Bot(string[,] pole, int sizeI, int sizeJ,string emptySymbol)
{
    Random random = new Random();

    while (true)
    {
        int randPosI = random.Next(0, sizeI);
        int randPosJ = random.Next(0, sizeJ);

        if (pole[randPosI, randPosJ] == emptySymbol)
        {
            pole[randPosI, randPosJ] = "[0]";
            break;
        }
    }
}

static bool Win(string[,] pole, int sizeI, int sizeJ)
{
    //ПОБЕДА ПО СТРОКАМ
    for (int i = 0; i < sizeI; i++)
    {
        int n = 0;
        for (int j = 0; j < sizeJ; j++)
        {
            if (pole[i, j] == "[X]")
            {
                n++;
            }
            else
            {
                break;
            }
        }
        if (n == sizeJ)
        {
            return true;
        }
    }
    //ПОБЕДА ПО СТОЛБЦАМ
    for (int j = 0; j < sizeJ; j++)
    {
        int m = 0;
        for (int i = 0; i < sizeI; i++)
        {
            if (pole[i, j] == "[X]")
            {
                m++;
            }
            else
            {
                break;
            }
        }
        if (m == sizeI)
        {
            return true;
        }
    }
    //ПОБЕДА ПО ДИАГОНАЛЯМ
    //ГЛАВНАЯ
    int score_for_main = 0;
    for (int i = 0; i < sizeI; i++)
    {
        if (pole[i, i] != "[X]")
        {
            break;
        }
        else
        {
            score_for_main++;
            if (score_for_main == sizeI)
            {
                return true;
            }
        }
    }
    //БОКОВАЯ
    int score_for_side = 0;
    for (int i = 0; i < sizeI; i++)
    {
        if (pole[i, sizeI - 1 - i] == "[X]")
        {
            score_for_side++;
            
        }
        else
        {
            break;
        }
    }
    if (score_for_side == sizeI)
    {
        return true;
    }
    return false;
}

static void Game()
{
    int sizeI = 3;
    int sizeJ = 3;

    string emptySymbol = "[#]";
    
    string[,] pole = new string[sizeI,sizeJ];
    
    FillPole(pole, sizeI, sizeJ,emptySymbol);

    while (true)
    {
        if (Win(pole, sizeI, sizeJ))
        {
            ShowPole(pole, sizeI, sizeJ);
            Console.WriteLine("Победа!");
            break;
        } 
        ShowPole(pole, sizeI, sizeJ);
        User(pole, sizeI, sizeJ,emptySymbol);
        if (!isFreeCell(pole, sizeI, sizeJ,emptySymbol))
        {
            Console.WriteLine("Все ячейки заняты!");
            break;
        }
        Bot(pole, sizeI, sizeJ,emptySymbol);
        isFreeCell(pole, sizeI, sizeJ,emptySymbol);
        if (!isFreeCell(pole, sizeI, sizeJ,emptySymbol))
        {
            Console.WriteLine("Все ячейки заняты!");
            break;
        }
    }
}

Game();