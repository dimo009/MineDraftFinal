using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    [TestFixture]
    public class Class1
    {
    private ProviderController providerController;
    private EnergyRepository energyRepository;
    private ProviderFactory providerFactory;
    [SetUp]
    public void SetUpProviderController()
    {
        this.energyRepository = new EnergyRepository();
        this.providerController = new ProviderController(energyRepository);
        this.providerFactory = new ProviderFactory();

    }
    [Test]
    public void RegisterProvider()
    {


        List<string> provider = new List<string> { "Solar", "20", "100" };
        var providersList = new List<IProvider>();
        var solarProvider = providerFactory.GenerateProvider(provider);
        providersList.Add(solarProvider);

        Assert.AreEqual(1, providersList.Count);
    }

    [Test]
    public void ProduceCorrectAmountOfEnergy()
    {
        List<string> providerArgs = new List<string> { "Solar", "20", "100" };
        var providersList = new List<IProvider>();

        var solarProvider = providerController.Register(providerArgs);

        for (int i = 0; i < 3; i++)
        {
            this.providerController.Produce();
        }
        Assert.AreEqual(this.providerController.TotalEnergyProduced, 300);
    }

    [Test]
    public void repairProvider()
    {
        List<string> providerArgs = new List<string> { "Solar", "20", "100" };


        this.providerController.Register(providerArgs);
        this.providerController.Repair(100);


        Assert.That(this.providerController.Entities.First().Durability, Is.EqualTo(1600));
    }

    [Test]
    public void ReturnsProperString()
    {
        List<string> providerArgs = new List<string> { "Solar", "20", "100" };
        var providersList = new List<IProvider>();

        this.providerController.Register(providerArgs);



        Assert.AreEqual(this.providerController.Produce(), "Produced 100 energy today!");
    }

    [Test]
    public void BreakTheProvider()
    {

        List<string> providerArgs = new List<string> { "Pressure", "20", "100" };
        var providersList = new List<IProvider>();

        this.providerController.Register(providerArgs);

        for (int i = 0; i < 8; i++)
        {
            this.providerController.Produce();
        }

        Assert.That(this.providerController.Entities.Count, Is.EqualTo(0));
    }
}

    

