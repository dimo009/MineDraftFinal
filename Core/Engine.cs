using System;
using System.Collections.Generic;
using System.Linq;

public class Engine
{
    private ICommandInterpreter commandInterpreter;

    private IReader reader;
    private IWriter writer; 

    public Engine(ICommandInterpreter commandInterpreter, IReader reader, IWriter writer)
    {
        this.commandInterpreter = commandInterpreter;
        this.reader = reader;
        this.writer = writer;
    }

    public void Run()
    {
        while (true)
        {

            List<string> inputArgs = this.reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string result = commandInterpreter.ProcessCommand(inputArgs);

            writer.WriteLine(result);

            if (inputArgs[0]=="Shutdown")
            {
                break;
            }
        }
    }
}
