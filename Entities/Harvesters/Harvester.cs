using System;
using System.Text;

public abstract class Harvester:IHarvester
{
    private const int InitialDurability = 1000;
    
    private double oreOutput;
    private double energyRequirement;
    private double durability;

    public Harvester(int id, double oreOutput, double energyRequirement)
    {
        this.ID = id;
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
        this.Durability = InitialDurability;
    }

    public int ID { get; }

    public double OreOutput { get; protected set; }

    public double EnergyRequirement { get; protected set; }

    public virtual double Durability
    {
        get { return this.durability; }
        protected set
        {

            if (value<0)
            {
                throw new ArgumentException("Durability cannot be less than zero");
            }

            this.durability = value;
        }
    }

    public void Broke()
    {
        this.Durability -= 100;

        
    }

    public double Produce()
    {
        return this.OreOutput;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{this.GetType().Name}");
        sb.AppendLine($"Durability: {this.Durability}");

        return sb.ToString().Trim();
    }
}