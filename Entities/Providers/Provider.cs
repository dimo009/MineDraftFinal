using System;

public abstract class Provider : IProvider
{
    private const double InitialDurability = 1000;

    private int id;
    private double energyOutput;
    private double durability;

    public Provider(int id, double energyOutput)
    {
        this.ID = id;
        this.EnergyOutput = energyOutput;
        this.Durability = InitialDurability;
    }

    public double Durability
    {
        get
        {
            return this.durability;
        }
        protected set
        {
            if (value<0)
            {
                throw new ArgumentException("Providers' durability cannot be less than zero");
            }

            this.durability = value;
        }
    }

    public double EnergyOutput
    {
        get
        {
            return this.energyOutput;
        }
        protected  set
        {
            this.energyOutput = value;
        }
    }

    public int ID
    {
        get
        {
            return this.id;
        }

        protected set
        {
            this.id = value;
        }
    }

    public void Broke()
    {
        this.Durability -= 100;
    }

    public double Produce()
    {
        return this.EnergyOutput; 
    }

    public void Repair(double val)
    {
        this.Durability += val;
    }
}