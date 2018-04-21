public class Program
{
    public static void Main(string[] args)
    {

        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();
        IEnergyRepository energyRepository = new EnergyRepository();
        IHarvesterFactory harvesterFactory = new HarvesterFactory();
        IProviderFactory providerFactory = new ProviderFactory();
        IHarvesterController hc = new HarvesterController(energyRepository, harvesterFactory);
        IProviderController pc = new ProviderController(energyRepository);

        ICommandInterpreter commandInterpreter = new CommandInterpreter(hc, pc);

        Engine engine = new Engine(commandInterpreter, reader, writer);
        engine.Run();
    }
}