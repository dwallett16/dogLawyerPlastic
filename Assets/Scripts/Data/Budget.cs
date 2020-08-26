
public class Budget
{
    public int CurrentBudget {get {return currentBudget;}}
    private int currentBudget;
    public int MaxBudget {get {return maxBudget;}}
    private int maxBudget;

    public void Purchase(int itemPrice)
    {
        currentBudget -= itemPrice;
    }

    public void Sell(int itemPrice)
    {
        currentBudget += itemPrice;
        if(currentBudget > maxBudget)
            currentBudget = maxBudget;
    }

    public void SetMaxBudget(int amount)
    {
        maxBudget = amount;
    }

    public void SetCurrentBudget(int amount)
    {
        currentBudget = amount;
    }
}
