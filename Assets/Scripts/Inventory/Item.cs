[System.Serializable]
public class Item 
{
    public int amount;
    public ItemType type;
    public Item(ItemType type, int amount)
    {
        this.amount = amount;
        this.type = type;
    }
}
