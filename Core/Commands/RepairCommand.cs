using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RepairCommand : Command
{
    private IProviderController providerController;
    public RepairCommand(IList<string> args, IProviderController providerController) : base(args)
    {
        this.providerController = providerController;
    }

    public override string Execute()
    {
        var id = double.Parse(this.Arguments[0]);

        var result = this.providerController.Repair(id);

        return result;
    }
}
