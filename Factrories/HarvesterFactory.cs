using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public  class HarvesterFactory : IHarvesterFactory
{
    public IHarvester GenerateHarvester(IList<string> args)
    {

        var typeByName = args[0];
        var id = int.Parse(args[1]);
        var oreOutput = double.Parse(args[2]);
        var energyRequirement = double.Parse(args[3]);
        var type = Assembly.GetCallingAssembly().GetTypes().Single(h => h.Name == typeByName + "Harvester");


        IHarvester harvester = (IHarvester)Activator.CreateInstance(type, id, oreOutput, energyRequirement);

        return harvester;



        
    }
}


       
   