[System.Serializable]
public class Item 
{
    public int Amount { get; set; }
    public ItemType Type { get; set; }

    public Item(ItemType type, int amount)
    {
        Amount = amount;
        Type = type;
    }

    public void IncreaseAmount(int value)
    {
        Amount += value;
    }

    public void DecreaseAmount(int value)
    {
        Amount -= value;
        if (Amount < 0) Amount = 0;
    }

    public override bool Equals(object obj)
    {
        return obj is Item other && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode();
    }
}
