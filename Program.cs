using System;
using System.Collections.Generic;

class Game{
    public const int countPlayer = 3;
    private List<string> deck = new List<string>();
    private List<string>[] players = new List<string>[countPlayer];
    private string[] values = { "6","7","8","9","10","B","D","K","T" };
    
    char trumpSuit;
    Random rand = new Random();
    
    int GetCardValue(string value){
        for(int i = 0; i < values.Length; i++){
            if(values[i] == value)
                return i;
        }
        return -1;
    }
    
    public void CreateDeck(){
        string[] suits = {"♥","♦","♣","♠"};
        string[] meent = {"6","7","8","9","10","B","D","K","T"};

        for (int i = 0; i < suits.Length; i++)
        {
            for (int j = 0; j < meent.Length; j++)
            {
                deck.Add(suits[i] + meent[j]);
            }
        }

    }

    public void ShowDeck() {
        Console.WriteLine("Колода:");
        foreach (string card in deck){
            Console.Write(card + " ");     
        }     
        Console.WriteLine();
    }
    
    public void Cards(){
        for (int i = 0; i < countPlayer; i++){
            players[i] = new List<string>();
            for (int j = 0; j < 6; j++){
                if (deck.Count == 1) {
                    players[i].Add(deck[0]);
                    deck.RemoveAt(0);
                } else {
                    players[i].Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
        }
    }

    public void ShowCardPlayer(int indexPlayer){
        Console.Write($"Карты игрока {indexPlayer+1}: ");
        for(int i = 0; i < players[indexPlayer].Count; i++){
            Console.Write($"[{i}] - {players[indexPlayer][i]}  ");
        }
    }
    
    public void ShowTrump(){
        Console.Write($"Масть: {deck[deck.Count-1]}");
    }
    
    public bool CanBeat(string attacker, string defender){
        if(attacker[0] == defender[0]){
            if(GetCardValue(defender.Substring(1)) > GetCardValue(attacker.Substring(1))){
                return true;
            } else {
                return false;
            }
        } else if(defender[0] == trumpSuit && attacker[0] != trumpSuit){
            return true;
        } else {
            return false;
        }
    }
    

    public void RefillHands(){
        for(int i = 0; i < players.Length; i++){
            while(players[i].Count < 6 && deck.Count > 0){
                string card = deck[0];
                players[i].Add(card);
                deck.RemoveAt(0);
            }
            
        }
    }
    
    public List<string> GetPlayerCards(int index) {
        return players[index];
    }
    
    public List<string>[] Players {
        get { return players; }
    }
    
    public void ShuffleDeck(){
        if (deck.Count <= 1) return;
        for (int i = deck.Count - 1; i > 0; i--){
            int j = rand.Next(i + 1);
            string temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
        if (deck.Count > 0){
            trumpSuit = deck[deck.Count-1][0];
        }
    }
    

}

class Table{
    private List<string> attackCard = new List<string>();
    private List<string> defenseCard = new List<string>();
    
    public void ClearTable(){
        attackCard.Clear();
        defenseCard.Clear();
    }
    
    public bool IsEmpty(){
        if(attackCard.Count == 0 && defenseCard.Count == 0) return true;
        else return false;
    }
    
    public bool CanAddAttackCard(string card){
        if (attackCard.Count == 0) return false;
        string value = card.Substring(1);
        for(int i = 0; i < attackCard.Count; i++){
            if(value == attackCard[i].Substring(1)) return true;
        }
        for(int i = 0; i < defenseCard.Count; i++){
            if(defenseCard[i] != null && value == defenseCard[i].Substring(1)) return true;
        }
        return false;

    }
    
    public bool AllCardsDefended(){
        for(int i = 0; i < defenseCard.Count; i++){
            if(defenseCard[i] == null) return false;
        }
        return true;
    }
    
    public void AddAttackCard(string card){
        attackCard.Add(card);
        defenseCard.Add(null);
    }
    
    public void AddDefenseCard(string card, int index){ defenseCard[index] = card; }
    
    public void show(){
        Console.Write("Атака: ");
        for(int i = 0; i < attackCard.Count; i++){
            Console.Write($"{attackCard[i]}\t");
        }
        
        Console.Write("\nЗащита: ");
        for(int i = 0; i < defenseCard.Count; i++){
            if(defenseCard[i] == null) Console.Write("?\t");
            else Console.Write($"{defenseCard[i]}\t");
        }
    }
    
    public List<string> CollectCards(){
        List<string> result = new List<string>();
        for(int i = 0; i < attackCard.Count; i++){
            if(attackCard[i] != null && attackCard[i] != "") result.Add(attackCard[i]);
        }
        for(int i = 0; i < defenseCard.Count; i++){
            if(defenseCard[i] != null && defenseCard[i] != "") result.Add(defenseCard[i]);
        }
        ClearTable();
        return result;
    }
    
    public List<string> GetAttackCards(){ return attackCard; }
    
    public List<string> GetDefenseCards(){ return defenseCard; }
    
    public int AttackCount() { return attackCard.Count; }

    public string GetAttackCard(int index) { return attackCard[index]; }
    
}

class DurakGame
{
    private Game game;
    private Table table;
    private int attackerIndex;
    private int defenderIndex;

    public DurakGame()
    {
        game = new Game();
        table = new Table();
        attackerIndex = 0;
        defenderIndex = 1;

        game.CreateDeck();
        game.ShuffleDeck();
        game.Cards();
    }

    private void Line()
    {
        Console.WriteLine("\n========================================================================");
    }

    private void ShowTable()
    {
        Console.Clear();
        Line();
        game.ShowTrump();
        Line();
        Console.WriteLine("Сейчас карт на столе:");
        table.show();
        Line();
    }

    private int InputCardIndex(int playerIndex)
    {
        int index;
        while (int.TryParse(Console.ReadLine(), out index) == false || index < -1 || index >= game.Players[playerIndex].Count)
        {
            Console.WriteLine("Некорректный номер, введите ещё раз:");
        }
        return index;
    }

    private int GameOver()
    {
        int playersWithCards = 0;
        int lastPlayerWithCards = -1;

        for (int i = 0; i < game.Players.Length; i++)
        {
            if (game.Players[i].Count > 0)
            {
                playersWithCards++;
                lastPlayerWithCards = i;
            }
        }

        if (playersWithCards == 1)
            return lastPlayerWithCards;
        else
            return -1;
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Line();
            game.ShowTrump();
            Line();

            table.ClearTable();
            Console.WriteLine("\n=== Новый ход ===");

            Line();
            Console.WriteLine($"Игрок {attackerIndex + 1}, ваши карты для атаки:");
            game.ShowCardPlayer(attackerIndex);
            Line();

            int attackCardIndex;
            Console.Write("\nВиберіть карту для АТАКИ: ");
            while (int.TryParse(Console.ReadLine(), out attackCardIndex) == false || attackCardIndex < 0 || attackCardIndex >= game.Players[attackerIndex].Count)
            {
                Console.WriteLine("Некорректный номер, введите ещё раз:");
            }

            string attackCard = game.Players[attackerIndex][attackCardIndex];
            table.AddAttackCard(attackCard);
            game.Players[attackerIndex].RemoveAt(attackCardIndex);

            bool defended = true;
            bool defenderTookCards = false;

            for (int i = 0; i < table.AttackCount(); i++)
            {
                string aCard = table.GetAttackCard(i);
                ShowTable();
                Console.WriteLine($"Игрок {defenderIndex + 1}, ваши карты для защиты:");
                game.ShowCardPlayer(defenderIndex);

                Console.Write("\nВиберіите карту для защиты (-1 если не можете): ");
                int defenseCardIndex = InputCardIndex(defenderIndex);

                if (defenseCardIndex == -1 || !game.CanBeat(aCard, game.Players[defenderIndex][defenseCardIndex]))
                {
                    defended = false;
                    defenderTookCards = true;
                    break;
                }
                else
                {
                    string dCard = game.Players[defenderIndex][defenseCardIndex];
                    table.AddDefenseCard(dCard, i);
                    game.Players[defenderIndex].RemoveAt(defenseCardIndex);
                }
            }

            if (defended == true)
            {
                int defenderStartCount = game.Players[defenderIndex].Count;
                bool canMore = true;
                int currentPlayer = (attackerIndex + 1) % Game.countPlayer;
                int skipCount = 0;

                while (canMore == true)
                {
                    if (table.AttackCount() >= defenderStartCount){
                        canMore = false;
                        break;
                    }

                    if (currentPlayer == defenderIndex){
                        currentPlayer = (currentPlayer + 1) % Game.countPlayer;
                        continue;
                    }

                    ShowTable();
                    Console.WriteLine($"Гравець {currentPlayer + 1} ви можете подкинуть карту или -1 для пропуска:");
                    game.ShowCardPlayer(currentPlayer);

                    Console.Write("\nВиберите карту для подкидывания (-1 чтобы пропустить): ");
                    int throwIndex = InputCardIndex(currentPlayer);

                    if (throwIndex == -1){
                        skipCount++;
                        currentPlayer = (currentPlayer + 1) % Game.countPlayer;
                        if (currentPlayer == defenderIndex) currentPlayer = (currentPlayer + 1) % Game.countPlayer;
                        if (skipCount >= Game.countPlayer - 1) canMore = false;
                        continue;
                    }

                    string throwCard = game.Players[currentPlayer][throwIndex];
                    if (table.CanAddAttackCard(throwCard) == false){
                        Console.WriteLine("Эту карту нельзя подкидывать. Виберите другую.");
                        continue;
                    }

                    table.AddAttackCard(throwCard);
                    game.Players[currentPlayer].RemoveAt(throwIndex);
                    skipCount = 0;

                    int lastAtkIndex = table.AttackCount() - 1;
                    string lastaCard = table.GetAttackCard(lastAtkIndex);

                    ShowTable();
                    Console.WriteLine($"Игрок {defenderIndex + 1}, ваши карты для защиты:");
                    game.ShowCardPlayer(defenderIndex);

                    Console.Write("\nВиберите карту для защиты (-1 если не можете): ");
                    int defenseIndex = InputCardIndex(defenderIndex);

                    if (defenseIndex == -1 || game.CanBeat(lastaCard, game.Players[defenderIndex][defenseIndex]) == false){
                        defended = false;
                        defenderTookCards = true;
                        break;
                    }
                    else{
                        string dCard = game.Players[defenderIndex][defenseIndex];
                        
                        table.AddDefenseCard(dCard, lastAtkIndex);
                        game.Players[defenderIndex].RemoveAt(defenseIndex);
                        
                        if (game.Players[defenderIndex].Count == 0){
                            canMore = false;
                            break;
                        }
                    }

                    currentPlayer = (currentPlayer + 1) % Game.countPlayer;
                }
            }
            if (defenderTookCards == true){
                Console.WriteLine($"\nИгрок {defenderIndex + 1} забирает все карты!");
                game.Players[defenderIndex].AddRange(table.CollectCards());
            }
            else{
                Console.WriteLine($"\nИгрок {defenderIndex + 1} отбил все карты!");
                table.CollectCards();
            }

            game.RefillHands();

            int loser = GameOver();
            if (loser != -1){
                Console.WriteLine($"\nИгра завершена! Игрок {loser + 1} проиграл!");
                break;
            }

            if (defenderTookCards == true){
                attackerIndex = (defenderIndex + 1) % Game.countPlayer;
                defenderIndex = (attackerIndex + 1) % Game.countPlayer;
            }
            else{
                attackerIndex = (attackerIndex + 1) % Game.countPlayer;
                defenderIndex = (attackerIndex + 1) % Game.countPlayer;
            }

            Console.Write("\nНажмите Enter для следующего хода.");
            Console.ReadLine();
        }
    }
}

class Program
{
    static void Main()
    {
        DurakGame durak = new DurakGame();
        durak.Start();
    }
}