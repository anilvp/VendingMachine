using Domain;

namespace Tests;

[TestClass]
public class VendingMachineTests
{

    [TestMethod]
    public void AddProduct_ValidInput_ProductExistsInLocation()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(2, 1, "Coke", 120);

        Assert.IsNotNull(vendingMachine.Products[2]);
        Assert.AreEqual(1, vendingMachine.Products[2].Id);
        Assert.AreEqual("Coke", vendingMachine.Products[2].Name);
        Assert.AreEqual(120, (int)vendingMachine.Products[2].Price);
        Assert.AreEqual(0, vendingMachine.Products[2].Quantity);
    }

    [TestMethod]
    public void AddProduct_InvalidLocation_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.AddProduct(9, 1, "Coke", 120));
    }

    [TestMethod]
    public void AddProduct_AddDifferentProduct_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(2, 1, "Coke", 120);

        Assert.ThrowsException<Exception>(() => vendingMachine.AddProduct(2, 2, "Fanta", 120));
    }



    [TestMethod]
    public void AddStock_ValidInput_ProductQuantityIncreased()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(2, 1, "Coke", 120);
        vendingMachine.AddStock(2, 1, 3);

        Assert.AreEqual(3, vendingMachine.Products[2].Quantity);
    }

    [TestMethod]
    public void AddStock_InvalidLocation_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.AddStock(9, 1, 1));
    }

    [TestMethod]
    public void AddStock_ZeroQuantity_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.AddStock(1, 1, 0));
    }

    [TestMethod]
    public void AddStock_UnconfiguredLocation_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<Exception>(() => vendingMachine.AddStock(1, 1, 1));
    }

    [TestMethod]
    public void AddStock_DifferentProduct_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(1, 1, "Coke", 120);

        Assert.ThrowsException<Exception>(() => vendingMachine.AddStock(1, 2, 1));
    }



    [TestMethod]
    public void RemoveProduct_ValidInput_PoductRemoved()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(2, 1, "Coke", 120);
        vendingMachine.RemoveProduct(2);

        Assert.IsNull(vendingMachine.Products[2]);
    }

    [TestMethod]
    public void RemoveProduct_InvalidLocation_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.RemoveProduct(9));
    }

    [TestMethod]
    public void RemoveProduct_EmptyLocation_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<Exception>(() => vendingMachine.RemoveProduct(0));
    }



    [TestMethod]
    public void AddCoinsToBank_ValidInput_CoinsAddedToBank()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddCoinsToBank(10, 2);

        Assert.AreEqual(2, vendingMachine.Bank[10]);
    }

    [TestMethod]
    public void AddCoinsToBank_InvalidCoin_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.AddCoinsToBank(11, 1));
    }



    [TestMethod]
    public void WithdrawCoinsFromBank_ValidInput_CoinCountsReduced()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddCoinsToBank(10, 2);
        vendingMachine.WithdrawCoinsFromBank(10, 2);

        Assert.AreEqual(0, vendingMachine.Bank[10]);
    }

    [TestMethod]
    public void WithdrawCoinsFromBank_InvalidCoin_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.WithdrawCoinsFromBank(11, 1));
    }

    [TestMethod]
    public void WithdrawCoinsFromBank_InvalidQuantity_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddCoinsToBank(10, 2);

        Assert.ThrowsException<Exception>(() => vendingMachine.WithdrawCoinsFromBank(10, 3));
    }



    [TestMethod]
    public void InsertCoin_ValidInput_BalanceIncreased()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.InsertCoin(20);
        vendingMachine.InsertCoin(5);

        Assert.AreEqual(25, vendingMachine.Balance);
    }



    [TestMethod]
    public void PurchaseProduct_ValidInput_CorrectProductOutput()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(0, 1, "Coke", 120);
        vendingMachine.AddStock(0, 1, 1);
        vendingMachine.InsertCoin(20);
        vendingMachine.InsertCoin(100);

        Assert.AreEqual("Coke", vendingMachine.PurchaseProduct(0));
        Assert.AreEqual(0, vendingMachine.Balance);
        Assert.AreEqual(0, vendingMachine.Products[0].Quantity);
    }

    [TestMethod]
    public void PurchaseProduct_InvalidLocation_ArgumentExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<ArgumentException>(() => vendingMachine.PurchaseProduct(9));
    }

    [TestMethod]
    public void PurchaseProduct_UnconfiguredLocation_ExceptionThrown()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        Assert.ThrowsException<Exception>(() => vendingMachine.PurchaseProduct(1));
    }

    [TestMethod]
    public void PurchaseProduct_ZeroQuantity_ErrorMessageOutput()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(0, 1, "Coke", 120);
        vendingMachine.InsertCoin(100);
        vendingMachine.InsertCoin(20);

        Assert.AreEqual("Product is out of stock", vendingMachine.PurchaseProduct(0));
    }

    [TestMethod]
    public void PurchaseProduct_InsufficientBalance_ErrorMessageOutput()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(0, 1, "Coke", 120);
        vendingMachine.AddStock(0, 1, 5);
        vendingMachine.InsertCoin(100);

        Assert.AreEqual("Insufficient balance", vendingMachine.PurchaseProduct(0));
    }



    [TestMethod]
    public void EjectChange_ValidInput_CorrectChangeGiven()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.InsertCoin(100);
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(20);
        vendingMachine.InsertCoin(1);
        vendingMachine.InsertCoin(1);
        var change = vendingMachine.EjectChange();

        Assert.AreEqual(1, change[100]);
        Assert.AreEqual(2, change[10]);
        Assert.AreEqual(1, change[20]);
        Assert.AreEqual(2, change[1]);
        Assert.AreEqual(0, vendingMachine.Bank[100]);
        Assert.AreEqual(0, vendingMachine.Bank[10]);
        Assert.AreEqual(0, vendingMachine.Bank[20]);
        Assert.AreEqual(0, vendingMachine.Bank[1]);
        Assert.AreEqual(0, vendingMachine.Balance);
    }

    [TestMethod]
    public void EjectChange_InsufficientFundsInBank_CompromiseChangeGiven()
    {
        int[] denominations = { 1, 2, 5, 10, 20, 50, 100, 200 };
        VendingMachine vendingMachine = new VendingMachine(denominations);

        vendingMachine.AddProduct(0, 1, "Coke", 120);
        vendingMachine.AddStock(0, 1, 1);
        vendingMachine.InsertCoin(200);
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(20);
        vendingMachine.InsertCoin(1);
        vendingMachine.InsertCoin(1);
        vendingMachine.PurchaseProduct(0);
        var change = vendingMachine.EjectChange();

        Assert.AreEqual(2, change[10]);
        Assert.AreEqual(1, change[20]);
        Assert.AreEqual(2, change[1]);
        Assert.AreEqual(1, vendingMachine.Bank[200]);
        Assert.AreEqual(0, vendingMachine.Bank[10]);
        Assert.AreEqual(0, vendingMachine.Bank[20]);
        Assert.AreEqual(0, vendingMachine.Bank[1]);
        Assert.AreEqual(80, vendingMachine.Balance);
    }

}