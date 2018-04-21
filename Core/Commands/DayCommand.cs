using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DayCommand : Command
{
    
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public DayCommand(IList<string> args, IHarvesterController harvesterController, IProviderController providerController) : base(args)
    {
      
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        var sb = new StringBuilder();

        sb.AppendLine(this.providerController.Produce());
        sb.AppendLine(this.harvesterController.Produce());

        var result = sb.ToString().Trim();

        return result;
    }
}

