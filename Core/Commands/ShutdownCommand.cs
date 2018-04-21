using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ShutdownCommand : Command
{


    private IHarvesterController harvesterController;
    private IProviderController providerController;
    
    public ShutdownCommand(IList<string> args, IHarvesterController harvesterController, IProviderController providerController) : base(args)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;

        
    }

    public override string Execute()
    {
        var sb = new StringBuilder();
        sb.AppendLine("System Shutdown");
        
        sb.AppendLine($"Total Energy Produced: {this.providerController.TotalEnergyProduced}");
        sb.Append($"Total Mined Plumbus Ore: {this.harvesterController.OreProduced}");

        return sb.ToString();
    }
}

