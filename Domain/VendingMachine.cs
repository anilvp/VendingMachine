namespace Domain;

public class VendingMachine
{
    public VendingMachine() 
    {
        Products = new Product[9];
        Bank = new Dictionary<int, int>()
        {
            {1, 0 },
            {2, 0 },
            {5, 0 },
            {10, 0 },
            {20, 0 },
            {50, 0 },
            {100, 0 },
            {200, 0 }
        };
        Balance = 0;
    }

    public void AddProduct(int location, int productId, string productName, int price, uint quantity)
    {
        if (location < 0 || location > 8)
        {
            throw new ArgumentException("Location is out of range");
        }
        else if (quantity == 0)
        {
            throw new ArgumentException("Input quantity is zero");
        }
        else if (Products[location] == null)
        {
            Products[location] = new Product(productId, productName, price, (int)quantity);
        }
        else if (Products[location].Id == productId)
        {
            Products[location].ChangeProductQuantity((int)quantity);
        }
        else
        {
            throw new Exception($"Location {location} is stocked with a different product");
        }
    }

    public void RemoveProduct(int location)
    {
        if (location < 0 || location > 8)
        {
            throw new ArgumentException("Location is out of range");
        }
        else if (Products[location] == null)
        {
            throw new Exception($"Location {location} is empty");
        }
        else
        {
            Products[location] = null;
        }
    }

    public void AddCoinsToBank(int coin, uint quantity)
    {
        if (Bank.TryGetValue(coin, out _))
        {
            Bank[coin] += (int)quantity;
        }
        else
        {
            throw new ArgumentException($"Invalid coin {coin}");
        }
    }

    public void WithdrawCoinsFromBank(int coin, uint quantity)
    {
        if (Bank.TryGetValue(coin, out int coinCount))
        {
            if (coinCount >= quantity)
            {
                Bank[coin] -= (int)quantity;
            }
            else
            {
                throw new Exception($"Not enough of {coin} to withdraw {quantity}");
            }
        }
        else
        {
            throw new ArgumentException($"Invalid coin {coin}");
        }
    }

    public void InsertCoin(int coin)
    {
        AddCoinsToBank(coin, 1);
        Balance += coin;
    }

    public string PurchaseProduct(int location)
    {
        if (location < 0 || location > 8)
        {
            throw new ArgumentException("Location is out of range");
        }
        else if (Products[location] == null)
        {
            throw new Exception($"Location {location} is empty");
        }
        else if (Balance < Products[location].Price)
        {
            return "Insufficient balance";
        }
        else
        {
            Balance -= Products[location].Price;
            Products[location].ChangeProductQuantity(-1);
            string name = Products[location].Name;
            if (Products[location].Quantity == 0)
            {
                Products[location] = null;
            }
            return name;
        }
    }

    public Dictionary<int, int> EjectChange()
    {
        Dictionary<int, int> change = new Dictionary<int, int>()
        {
            {1, 0 },
            {2, 0 },
            {5, 0 },
            {10, 0 },
            {20, 0 },
            {50, 0 },
            {100, 0 },
            {200, 0 }
        };
        foreach (int coin in change.Keys.Reverse())
        {
            change[coin] = Balance / coin;
            if (Bank[coin] >= change[coin])
            {
                Balance = Balance % coin;
            }
            else
            {
                change[coin] = Bank[coin];
                Balance -= coin * change[coin];
            }
            WithdrawCoinsFromBank(coin, (uint)change[coin]);
        }
        return change;
    }

    public Product?[] Products { get; private set; }

    public Dictionary<int, int> Bank { get; private set; }

    public int Balance { get; private set; }
}