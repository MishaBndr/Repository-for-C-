using System.ComponentModel;

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
    
    public void FillItems(string[,] pole, int sizeI, int sizeJ,  string symbolEmpty, string symbolCoin, string symbolHP, int countCoins, int countHP)
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
            while(true)
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
    
    public void ShowPole(string[,] pole, int sizeI, int sizeJ, string symbolCoin, string symbolEmpty, string symbolHP, string userPositionSymbol)
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
    
    public void UserHod(string[,] pole, int sizeI, int sizeJ, string userPositionSymbol, string symbolEmpty, string symbolCoin, string symbolHP, string symbolEnemy)
    {
        int coinsQuantity = 0;
        int HPQuantity = 1;
        int damageQuantity = 3;
        int armoreQuantity = 2;
        int killedEnemys = 0;

        Random rnd = new Random();

        int userIndexI = rnd.Next(0, sizeI);
        int userIndexJ = rnd.Next(0, sizeJ);

        pole[userIndexI, userIndexJ] = userPositionSymbol;
        ShowPole(pole, sizeI, sizeJ, symbolCoin, symbolEmpty, symbolHP, userPositionSymbol);
        showBalance(coinsQuantity, HPQuantity, killedEnemys);

        while (HPQuantity > 0)
        {
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
                Enemy enemy = new Enemy(rnd.Next(0,5), rnd.Next(0,5), rnd.Next(0,5));
                int meanEnemy = (enemy.getHP() + enemy.getDamage() + enemy.getArmor()) / 3;
                int meanPlayer = (HPQuantity + damageQuantity + armoreQuantity) / 3;

                if (meanPlayer >= meanEnemy)
                    killedEnemys++;
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
    public static void Main(String[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        
        int sizeI = 10; 
        int sizeJ = 10;

        string symbolEmpty = "[_]";
        string userPositionSymbol = "[*]";
        string symbolCoin = "[●]";
        int countOfCoins = 10;
        int countOfHP = 5;
        int countOfEnemies = 5;
        string symbolHP = "[♥]";
        Enemy enemy1 = new Enemy(0, 0, 0);
        string symbolEnemy = enemy1.GetSymbol();
        
        string[,] pole = new string[sizeI,sizeJ];
        
        Game game = new Game(); 
        game.FillPole(pole, sizeI, sizeJ, symbolEmpty);
        game.FillItems(pole, sizeI, sizeJ, symbolEmpty, symbolCoin, symbolHP, countOfCoins, countOfHP);
        game.FillEnemies(pole, sizeI, sizeJ, symbolEmpty, countOfEnemies);
        game.UserHod(pole, sizeI, sizeJ, userPositionSymbol, symbolEmpty, symbolCoin, symbolHP, symbolEnemy);
    }
}