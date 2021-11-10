public class HighScore
{
    private int score;

    public int Score => score;

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
    }
}
