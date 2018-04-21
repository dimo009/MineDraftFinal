using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ProviderFactory : IProviderFactory
{
    public IProvider GenerateProvider(IList<string> args)
    {
        string typeName = args[0];
        int id = int.Parse(args[1]);
        double energyOutput = double.Parse(args[2]);

        Type type = Assembly.GetExecutingAssembly().GetTypes().Single(t => t.Name == typeName + "Provider");

        IProvider provider = (IProvider)Activator.CreateInstance(type, id, energyOutput);
        return provider;
    }
}