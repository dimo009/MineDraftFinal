using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ModeCommand : Command
{
    private IHarvesterController harvesterController;
    public ModeCommand(IList<string> args, IHarvesterController harvesterController) : base(args)
    {
        this.harvesterController = harvesterController;
    }

    public override string Execute()
    {
        var result = this.harvesterController.ChangeMode(this.Arguments[0]);

        return result;
    }
}

