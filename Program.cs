Console.WriteLine("Добро пожаловать в игру \"Крестики-нолики\"!\nНаше поле: ");
char[,] matrix = new char[,]
{
    { '#', '#', '#' },
    { '#', '#', '#' },
    { '#', '#', '#' }
};
for (int i = 0; i < 3; i++)
{
    Console.WriteLine("  ---    ---    ---");
    
    for (int j = 0; j < 3; j++)
    {
        Console.Write(" | " + matrix[i, j] + " | ");
    } 
    Console.WriteLine();
}
Console.WriteLine("  ---    ---    ---");
Console.WriteLine("Выберите сторону: X или O (вводите с английской раскладкой): ");
char team = char.Parse(Console.ReadLine());

if (team == 'X')
{
    while (true)
    {
        Console.WriteLine("Выберите координаты позиции вашего хода: ");
        int positionX = int.Parse(Console.ReadLine());
        int positionY = int.Parse(Console.ReadLine());
        
        if ((positionX < 1 || positionX > 3) || (positionY < 1 || positionY > 3)) 
        {   
            Console.WriteLine("Некорректная координата! Выберите в диапазоне от 1 до 3 включительно!");
            break;
        }
        else
        {
            matrix[positionX - 1, positionY - 1] = 'X';
        }
        
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("  ---    ---    ---");
    
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" | " + matrix[i, j] + " | ");
            } 
            Console.WriteLine();
        }
        Console.WriteLine("  ---    ---    ---");
        
        Random random = new Random();
        int randPosX =  random.Next(0, 3);
        int randPosY =  random.Next(0, 3);
        
        if (matrix[randPosX, randPosY] == '#')
        {
            matrix[randPosX, randPosY] = 'O';
        }
        else
        {
            while (matrix[randPosX, randPosY] != '#') 
            {
                randPosX =  random.Next(0, 3);
                randPosY =  random.Next(0, 3);
                if (matrix[randPosX, randPosY] == '#')
                {
                    matrix[randPosX, randPosY] = 'O';
                    break;
                }
            }
        }
        
        Console.WriteLine("Ход компьютера. Поле: ");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("  ---    ---    ---");
    
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" | " + matrix[i, j] + " | ");
            } 
            Console.WriteLine();
        }
        
        Console.WriteLine("  ---    ---    ---");
        if ((matrix[0, 0] == 'X' && matrix[0, 1] == 'X' && matrix[0, 2] == 'X') ||
            (matrix[1, 0] == 'X' && matrix[1, 1] == 'X' && matrix[1, 2] == 'X') ||
            (matrix[2, 0] == 'X' && matrix[2, 1] == 'X' && matrix[2, 2] == 'X'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        else if ((matrix[0, 0] == 'X' && matrix[1, 0] == 'X' && matrix[2, 0] == 'X') ||
                 (matrix[0, 1] == 'X' && matrix[1, 1] == 'X' && matrix[2, 1] == 'X') ||
                 (matrix[0, 2] == 'X' && matrix[1, 2] == 'X' && matrix[2, 2] == 'X'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        else if ((matrix[0, 0] == 'X' && matrix[1, 1] == 'X' && matrix[2, 2] == 'X') ||
                 (matrix[0, 2] == 'X' && matrix[1, 1] == 'X' && matrix[2, 0] == 'X'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        if ((matrix[0, 0] == 'O' && matrix[0, 1] == 'O' && matrix[0, 2] == 'O') ||
            (matrix[1, 0] == 'O' && matrix[1, 1] == 'O' && matrix[1, 2] == 'O') ||
            (matrix[2, 0] == 'O' && matrix[2, 1] == 'O' && matrix[2, 2] == 'O'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
        else if ((matrix[0, 0] == 'O' && matrix[1, 0] == 'O' && matrix[2, 0] == 'O') ||
                 (matrix[0, 1] == 'O' && matrix[1, 1] == 'O' && matrix[2, 1] == 'O') ||
                 (matrix[0, 2] == 'O' && matrix[1, 2] == 'O' && matrix[2, 2] == 'O'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
        else if ((matrix[0, 0] == 'O' && matrix[1, 1] == 'O' && matrix[2, 2] == 'O') ||
                 (matrix[0, 2] == 'O' && matrix[1, 1] == 'O' && matrix[2, 0] == 'O'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
    }
}
else if (team == 'O')
{
    while (true)
    {
        Console.WriteLine("Выберите координаты позиции вашего хода: ");
        int positionX = int.Parse(Console.ReadLine());
        int positionY = int.Parse(Console.ReadLine());
        
        if ((positionX < 1 || positionX > 3) || (positionY < 1 || positionY > 3)) 
        {   
            Console.WriteLine("Некорректная координата! Выберите в диапазоне от 1 до 3 включительно!");
            break;
        }
        else
        {
            matrix[positionX - 1, positionY - 1] = 'O';
        }
        
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("  ---    ---    ---");
    
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" | " + matrix[i, j] + " | ");
            } 
            Console.WriteLine();
        }
        Console.WriteLine("  ---    ---    ---");
        
        Random random = new Random();
        int randPosX =  random.Next(0, 3);
        int randPosY =  random.Next(0, 3);

        if (matrix[randPosX, randPosY] == '#')
        {
            matrix[randPosX, randPosY] = 'X';
        }
        else
        {
            while (matrix[randPosX, randPosY] != '#') 
            {
                randPosX =  random.Next(0, 3);
                randPosY =  random.Next(0, 3);
                if (matrix[randPosX, randPosY] == '#')
                {
                    matrix[randPosX, randPosY] = 'X';
                    break;
                }
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("  ---    ---    ---");
    
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" | " + matrix[i, j] + " | ");
            } 
            Console.WriteLine();
        }
        Console.WriteLine("  ---    ---    ---");
        
        if ((matrix[0, 0] == 'O' && matrix[0, 1] == 'O' && matrix[0, 2] == 'O') ||
            (matrix[1, 0] == 'O' && matrix[1, 1] == 'O' && matrix[1, 2] == 'O') ||
            (matrix[2, 0] == 'O' && matrix[2, 1] == 'O' && matrix[2, 2] == 'O'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        else if ((matrix[0, 0] == 'O' && matrix[1, 0] == 'O' && matrix[2, 0] == 'O') ||
                 (matrix[0, 1] == 'O' && matrix[1, 1] == 'O' && matrix[2, 1] == 'O') ||
                 (matrix[0, 2] == 'O' && matrix[1, 2] == 'O' && matrix[2, 2] == 'O'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        else if ((matrix[0, 0] == 'O' && matrix[1, 1] == 'O' && matrix[2, 2] == 'O') ||
                 (matrix[0, 2] == 'O' && matrix[1, 1] == 'O' && matrix[2, 0] == 'O'))
        {
            Console.WriteLine("Победа!");
            break;
        }
        if ((matrix[0, 0] == 'X' && matrix[0, 1] == 'X' && matrix[0, 2] == 'X') ||
            (matrix[1, 0] == 'X' && matrix[1, 1] == 'X' && matrix[1, 2] == 'X') ||
            (matrix[2, 0] == 'X' && matrix[2, 1] == 'X' && matrix[2, 2] == 'X'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
        else if ((matrix[0, 0] == 'X' && matrix[1, 0] == 'X' && matrix[2, 0] == 'X') ||
                 (matrix[0, 1] == 'X' && matrix[1, 1] == 'X' && matrix[2, 1] == 'X') ||
                 (matrix[0, 2] == 'X' && matrix[1, 2] == 'X' && matrix[2, 2] == 'X'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
        else if ((matrix[0, 0] == 'X' && matrix[1, 1] == 'X' && matrix[2, 2] == 'X') ||
                 (matrix[0, 2] == 'X' && matrix[1, 1] == 'X' && matrix[2, 0] == 'X'))
        {
            Console.WriteLine("Поражение!");
            break;
        }
    }
}

else
{
    Console.WriteLine("Некорректный выбор, попробуйте снова!");
}

//ЕЩЁ НАДО СДЕЛАТЬ ВЫВОД ПОЛЯ ПОСЛЕ КАЖДОГО ХОДА ИИИ... ПО ИДЕЕ ВСЁ