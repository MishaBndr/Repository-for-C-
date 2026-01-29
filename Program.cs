class Enemy
{
    private int HP;
    private int damage;
    private int armor;
    private string enemySymbol = "[×]";

    public Enemy(int HP, int damage, int armor)
    {
        this.HP = HP;
        this.damage = damage;
        this.armor = armor;
    }

    public string GetSymbol()
    {
        return enemySymbol;
    }

    public int getHP()
    {
        return HP;
    }

    public int getDamage()
    {
        return damage;
    }

    public int getArmor()
    {
        return armor;
    }
}


class Game
{
    public enum BattleResult
    {
        Win,
        Lose,
        Exit
    }

    public int Coins => coinsQuantity;
    public int HP => HPQuantity;
    public int Damage => damageQuantity;
    public int Armor => armorQuantity;


    private int coinsQuantity;
    private int HPQuantity;
    private int damageQuantity;
    private int armorQuantity;
    private int killedEnemys = 0;

    public Game(int coinsQuantity, int HPQuantity, int damageQuantity, int armorQuantity)
    {
        this.coinsQuantity = coinsQuantity;
        this.HPQuantity = HPQuantity;
        this.damageQuantity = damageQuantity;
        this.armorQuantity = armorQuantity;
    }

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

    public void FillItems(string[,] pole, int sizeI, int sizeJ, string symbolEmpty, string symbolCoin, string symbolHP,
        int countCoins, int countHP)
    {
        Random rand = new Random();
        for (int i = 0; i < countCoins; i++)
        {
            while (true)
            {
                int randPosI = rand.Next(0, sizeI);
                int randPosJ = rand.Next(0, sizeJ);

                if (pole[randPosI, randPosJ] == symbolEmpty)
                {
                    pole[randPosI, randPosJ] = symbolCoin;
                    break;
                }
            }
        }

        for (int i = 0; i < countHP; i++)
        {
            while (true)
            {
                int randPosI = rand.Next(0, sizeI);
                int randPosJ = rand.Next(0, sizeJ);

                if (pole[randPosI, randPosJ] == symbolEmpty)
                {
                    pole[randPosI, randPosJ] = symbolHP;
                    break;
                }
            }
        }
    }

    public void FillEnemies(string[,] pole, int sizeI, int sizeJ, string symbolEmpty, int countEnemies)
    {
        Random randPos = new Random();


        for (int i = 0; i < countEnemies; i++)
        {
            Enemy enemy = new Enemy(0, 0, 0);
            while (true)
            {
                int randPosI = randPos.Next(0, sizeI);
                int randPosJ = randPos.Next(0, sizeJ);

                if (pole[randPosI, randPosJ] == symbolEmpty)
                {
                    pole[randPosI, randPosJ] = enemy.GetSymbol();
                    break;
                }
            }
        }
    }

    public void ShowPole(string[,] pole, int sizeI, int sizeJ, string symbolCoin, string symbolEmpty, string symbolHP,
        string userPositionSymbol)
    {
        Enemy enemy = new Enemy(0, 0, 0);
        for (int i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                if (pole[i, j] == symbolCoin)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (pole[i, j] == symbolHP)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else if (pole[i, j] == symbolEmpty)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (pole[i, j] == userPositionSymbol)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (pole[i, j] == enemy.GetSymbol())
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }

                Console.Write(pole[i, j]);
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    private void showBalance(int coins, int HPs, int kilEnemy)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine();
        }

        Console.WriteLine("Ваш баланс (монет): " + coins);
        Console.WriteLine("Ваш баланс (здоровья): " + HPs);
        Console.WriteLine("Количество поверженных врагов: " + kilEnemy);
    }

    public BattleResult UserHod(string[,] pole, int sizeI, int sizeJ, string userPositionSymbol, string symbolEmpty,
        string symbolCoin, string symbolHP, string symbolEnemy, int countEnemies)
    {

        Random rnd = new Random();

        int userIndexI = rnd.Next(0, sizeI);
        int userIndexJ = rnd.Next(0, sizeJ);

        pole[userIndexI, userIndexJ] = userPositionSymbol;
        ShowPole(pole, sizeI, sizeJ, symbolCoin, symbolEmpty, symbolHP, userPositionSymbol);
        showBalance(coinsQuantity, HPQuantity, killedEnemys);

        while (true)
        {
            if (HPQuantity <= 0)
            {
                Console.WriteLine("Очков здоровья не осталось, вы проиграли!");
                Console.WriteLine("Нажмите *, чтобы выйти в меню.");
                while (Console.ReadKey(true).KeyChar != '*')
                {
                }

                return BattleResult.Lose;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char move = keyInfo.KeyChar;

            int newI = userIndexI;
            int newJ = userIndexJ;

            if (move == 'w')
            {
                newI = (userIndexI - 1 + sizeI) % sizeI;
            }
            else if (move == 's')
            {
                newI = (userIndexI + 1) % sizeI;
            }
            else if (move == 'a')
            {
                newJ = (userIndexJ - 1 + sizeJ) % sizeJ;
            }
            else if (move == 'd')
            {
                newJ = (userIndexJ + 1) % sizeJ;
            }
            else if (move == '*')
            {
                return BattleResult.Exit;
            }

            else
            {
                continue;
            }

            Console.Clear();

            string target = pole[newI, newJ];

            if (target == symbolCoin)
            {
                coinsQuantity++;
            }
            else if (target == symbolHP)
            {
                HPQuantity++;
            }
            else if (target == symbolEnemy)
            {
                Enemy enemy = new Enemy(rnd.Next(0, 5), rnd.Next(0, 5), rnd.Next(0, 5));
                int meanEnemy = (enemy.getHP() + enemy.getDamage() + enemy.getArmor()) / 3;
                int meanPlayer = (HPQuantity + damageQuantity + armorQuantity) / 3;

                if (meanPlayer >= meanEnemy)
                {
                    killedEnemys++;
                    if (killedEnemys == countEnemies)
                    {
                        Console.WriteLine("Все враги уничтожены, вы победили!");
                        Console.WriteLine("Нажмите *, чтобы выйти в меню.");
                        while (Console.ReadKey(true).KeyChar != '*')
                        {
                        }

                        return BattleResult.Win;
                    }

                }
                else
                {
                    HPQuantity--;
                    ShowPole(pole, sizeI, sizeJ, symbolCoin, symbolEmpty, symbolHP, userPositionSymbol);
                    showBalance(coinsQuantity, HPQuantity, killedEnemys);
                    continue;
                }
            }

            pole[userIndexI, userIndexJ] = symbolEmpty;
            userIndexI = newI;
            userIndexJ = newJ;
            pole[userIndexI, userIndexJ] = userPositionSymbol;

            ShowPole(pole, sizeI, sizeJ, symbolCoin, symbolEmpty, symbolHP, userPositionSymbol);
            showBalance(coinsQuantity, HPQuantity, killedEnemys);
        }
    }
}

class Program
{
    static void OpenShop(ref int coins, ref int hp, ref int damage, ref int armor)
    {
        Console.Clear();
        Console.WriteLine("=== МАГАЗИН ===");
        Console.WriteLine($"Монеты: {coins}");
        Console.WriteLine("1 - +1 урон (2 монеты)");
        Console.WriteLine("2 - +1 броня (2 монеты)");
        Console.WriteLine("* - выйти в меню");

        while (true)
        {
            char c = Console.ReadKey(true).KeyChar;

            if (c == '1' && coins >= 2)
            {
                coins -= 2;
                damage += 1;
                Console.WriteLine("\nУрон увеличен!");
            }
            else if (c == '2' && coins >= 2)
            {
                coins -= 2;
                armor += 1;
                Console.WriteLine("\nБроня увеличена!");
            }
            else if (c == '*')
            {
                break;
            }
        }
    }


    public static void Main(String[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        int sizeI = 10;
        int sizeJ = 10;

        string symbolEmpty = "[_]";
        string userPositionSymbol = "[*]";
        string symbolCoin = "[●]";
        string symbolHP = "[♥]";
        int countOfCoins = 10;
        int countOfHP = 5;
        int countOfEnemies = 10;

        Enemy enemy1 = new Enemy(0, 0, 0);
        string symbolEnemy = enemy1.GetSymbol();

        string[,] pole = new string[sizeI, sizeJ];

        int coinsQuantity = 0;
        int HPQuantity = 1;
        int damageQuantity = 3;
        int armorQuantity = 2;

        bool gameRunning = true;

        while (gameRunning)
        {
            Console.Clear();
            Console.WriteLine("=== МЕНЮ ===\n1 - Магазин\n2 - Играть\n0 - Выйти");

            string choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    OpenShop(ref coinsQuantity, ref HPQuantity, ref damageQuantity, ref armorQuantity);
                    break;

                case "2":
                    Game game = new Game(coinsQuantity, HPQuantity, damageQuantity, armorQuantity);

                    game.FillPole(pole, sizeI, sizeJ, symbolEmpty);
                    game.FillItems(pole, sizeI, sizeJ, symbolEmpty, symbolCoin, symbolHP, countOfCoins, countOfHP);
                    game.FillEnemies(pole, sizeI, sizeJ, symbolEmpty, countOfEnemies);

                    Game.BattleResult result = game.UserHod(
                        pole, sizeI, sizeJ,
                        userPositionSymbol, symbolEmpty,
                        symbolCoin, symbolHP,
                        symbolEnemy, countOfEnemies
                    );

                    // забираем обновлённые характеристики после боя
                    coinsQuantity = game.Coins;
                    HPQuantity = game.HP;
                    damageQuantity = game.Damage;
                    armorQuantity = game.Armor;

                    break;

                case "0":
                    gameRunning = false;
                    break;
            }
        }
    }
}
