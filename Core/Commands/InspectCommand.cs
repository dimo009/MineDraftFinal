using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InspectCommand : Command
{
    private IHarvesterController harvesterController;
    private IProviderController providerController;

    public InspectCommand(IList<string> args, IHarvesterController harvesterController, IProviderController providerController) : base(args)
    {
        this.harvesterController = harvesterController;
        this.providerController = providerController;
    }

    public override string Execute()
    {
        int id = int.Parse(this.Arguments[0]);

        IEntity entity = this.harvesterController.Entities.FirstOrDefault(e => e.ID == id);
        if (entity==null)
        {
            entity = this.providerController.Entities.FirstOrDefault(e => e.ID == id);
        }
        if (entity==null)
        {
            return string.Format(Constants.NotEntityFound, id);
        }

        return string.Format(entity.ToString());
    }
}

