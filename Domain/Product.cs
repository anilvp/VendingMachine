namespace Domain;

public class Product
{

    public Product(int id, string name, uint price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public void ChangeProductQuantity(int quantity)
    {
        Quantity += quantity;
    }


    public int Id { get; private set; }

    public string Name { get; private set; }

    public uint Price { get; private set; }

    public int Quantity { get; private set; }

}