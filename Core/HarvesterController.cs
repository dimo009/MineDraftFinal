using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class HarvesterController : IHarvesterController
{
    private List<IHarvester> harvesters;
    private IEnergyRepository energyRepository;
    private IHarvesterFactory factory;
    private string mode;
    private const string DefaultMode = "Full";

    public HarvesterController(IEnergyRepository energyRepository, IHarvesterFactory factory)
    {
        this.harvesters = new List<IHarvester>();
        this.mode = DefaultMode;

        this.energyRepository = energyRepository;
        this.factory = factory;
       

    }

    public double OreProduced { get; protected set;}

    public IReadOnlyCollection<IEntity> Entities => this.harvesters.AsReadOnly();
    public string ChangeMode(string mode)
    {
        this.mode = mode;

        List<IHarvester> reminder = new List<IHarvester>();

        foreach (var harvester in this.harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception)
            {
                reminder.Add(harvester);
            }
        }

        foreach (var entity in reminder)
        {
            this.harvesters.Remove(entity);
        }

        return string.Format(Constants.ChangingTheMode, mode);
    }

    public string Produce()
    {
        //var neededEnergy = this.harvesters.Select(h => h.Produce()).Sum();
        //this.energyRepository.TakeEnergy(neededEnergy);
        //this.OreProduced += this.harvesters.Sum(h => h.OreOutput);

        //calculate needed energy
        double neededEnergy = 0;
        foreach (var harvester in this.harvesters)
        {
            if (this.mode == "Full")
            {
                neededEnergy += harvester.EnergyRequirement;
            }
            else if (this.mode == "Half")
            {
                neededEnergy += harvester.EnergyRequirement * 50 / 100;
            }

            else if (this.mode == "Energy")
            {
                neededEnergy += harvester.EnergyRequirement * 20/ 100;
            }
        }

        //check if we can mine
        double minedOres = 0;
        if (this.energyRepository.TakeEnergy(neededEnergy))
        {
            //mine
            
            foreach (var harvester in this.harvesters)
            {
                minedOres += harvester.Produce();
            }
        }

        //take the mode in mind
        if (this.mode == "Energy")
        {
            minedOres = minedOres * 20 / 100;
        }
        else if (this.mode == "Half")
        {
            minedOres = minedOres * 50 / 100;
        }

        this.OreProduced += minedOres;

    

        

        return string.Format(Constants.OreOutputToday, minedOres);
    }

    public string Register(IList<string> args)
    {
       IHarvester harvester =  this.factory.GenerateHarvester(args);
        harvesters.Add(harvester);
        return string.Format(Constants.SuccessfullRegistration,
           harvester.GetType().Name);
    }
     public string Inspect(int id)
    {
        
        StringBuilder sb = new StringBuilder();

        if (this.harvesters.Any(c=>c.ID==id))
        {
            var harvester = harvesters.FirstOrDefault(h => h.ID == id);
            sb.AppendLine($"{harvester.GetType().Name}");
            sb.AppendLine($"Durability: {harvester.Durability}");
        }

        else
        {
            sb.AppendLine(string.Format(Constants.NotEntityFound, id));
        }

        return sb.ToString().Trim();

    }
}

