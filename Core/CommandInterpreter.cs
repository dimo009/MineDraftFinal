using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class CommandInterpreter : ICommandInterpreter
{

    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController)
    {
        this.HarvesterController = harvesterController;
        this.ProviderController = providerController;
    }
    public IHarvesterController HarvesterController { get; private set; }
    

    public IProviderController ProviderController { get; private set; }
   

    public string ProcessCommand(IList<string> args)
    {
        ICommand command = this.CreateCommand(args);

        string result = command.Execute();

        return result;
    }

    private ICommand CreateCommand(IList<string> args)
    {
        string commandName = args[0];

        Assembly assembly = Assembly.GetCallingAssembly();

        Type commandType = assembly.GetTypes().Single(t => t.Name == commandName + "Command");

        if (commandType==null)
        {
            throw new ArgumentException(string.Format(Constants.NotFoundCommand,commandName));
        }

        if (!typeof(ICommand).IsAssignableFrom(commandType))
        {
            throw new InvalidOperationException(string.Format(Constants.InvalidCommand, commandName));
        }

        var constructor = commandType.GetConstructors().First();

        var parametersInfo = constructor.GetParameters();

        object[] parameters = new object[parametersInfo.Length];

        for (int i = 0; i < parametersInfo.Length; i++)
        {
            Type paramType = parametersInfo[i].ParameterType;

            if (paramType == typeof(IList<string>))
            {
                //using Skip(1) in order to skip the command name from the arguments
                parameters[i] = args.Skip(1).ToList();
            }

            else
            {
                PropertyInfo paramInfo = this.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == paramType);


                parameters[i] = paramInfo.GetValue(this);
            }
        }

        ICommand instance = (ICommand)Activator.CreateInstance(commandType, parameters);

        return instance;
    }
}
