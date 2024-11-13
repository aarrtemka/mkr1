
 
    {
        // Зчитуємо кінцевий рахунок із файлу INPUT.TXT
        string[] input = File.ReadAllText("INPUT.TXT").Trim().Split(':');
        int scoreA = int.Parse(input[0]);
        int scoreB = int.Parse(input[1]);

        // Викликаємо функцію для підрахунку кількості можливих партій
        long result = CountDistinctGames(scoreA, scoreB);

        // Записуємо результат у файл OUTPUT.TXT
        File.WriteAllText("OUTPUT.TXT", result.ToString());
        Console.WriteLine("Результат записано в файл OUTPUT.TXT");
    }

    static long CountDistinctGames(int scoreA, int scoreB)
    {
        // Якщо переможний рахунок не відповідає умові, повертаємо 0
        if (Math.Max(scoreA, scoreB) < 25 || Math.Abs(scoreA - scoreB) < 2)
            return 0;

        // Ініціалізуємо таблицю для збереження кількості способів набору очок
        long[,] dp = new long[scoreA + 1, scoreB + 1];
        dp[0, 0] = 1;

        for (int a = 0; a <= scoreA; a++)
        {
            for (int b = 0; b <= scoreB; b++)
            {
                if (a > 0)
                    dp[a, b] += dp[a - 1, b];
                if (b > 0)
                    dp[a, b] += dp[a, b - 1];
            }
        }

        // Враховуємо умови завершення партії
        if (scoreA == 25 && scoreB < 24)
            return dp[25, scoreB];
        if (scoreB == 25 && scoreA < 24)
            return dp[scoreA, 25];
        if (scoreA > 24 && scoreB > 24 && Math.Abs(scoreA - scoreB) == 2)
            return dp[scoreA, scoreB];

        return 0;
    }
