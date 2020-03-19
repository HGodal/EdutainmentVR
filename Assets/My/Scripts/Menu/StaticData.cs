using System;

public static class StaticData
{
    public static int[] levelScores = new int[6];
    private static int totalScore;

    public static int getScore()
    {
        totalScore = 0;
        foreach (int score in levelScores)
        {
            totalScore += score;
        }
        return totalScore;
    }

    public static void resetScores()
    {
        Array.Clear(levelScores, 0, levelScores.Length);
    }
}

