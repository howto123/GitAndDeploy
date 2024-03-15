using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Exceptions;
using Microsoft.Extensions.Configuration;



namespace GitAndDeploy;

class UserInterface
{
    private List<Types.Project>? _projects;

    private UserInterface() { /* hidden */ }

    public static UserInterface Create()
    {
        var instance = new UserInterface();
        instance.Initialize();


        return instance;
    }

    public void ShowArgList()
    {
        if(_projects is null) throw new PreconditionException("_project not initialized");

        Console.WriteLine($"Your projects and Actions are:");
        _projects.ForEach(p => PrintProject(p));

        var first = _projects.First();
        Console.WriteLine($"\nYou could type for example: gitad '{first.Name}' '{first.Actions.First().Name}'\n");

    }

    public void HandleArgs(string maybeProject, string maybeAction, string comment)
    {
        Types.Project project;
        Types.Action action;
        
        try
        {
            project = CheckAndGetProject(maybeProject);
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"This project is not specified.\n");
            ShowArgList();
            return;
        }

        try
        {
            action = CheckAndGetAction(project, maybeAction);
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"This action is not specified for this project.\n");
            ShowArgList();
            return;
        }

        ProceedWithValidatedArgs(project, action, comment);
    }

    private void Initialize()
    {
        // build config
        IConfigurationRoot? configuration;
        try
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", false);
            configuration = configurationBuilder.Build();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"There is a problem with appsettings.json: {e.Message}");
            return;
        }

        // initialize projects from config
        var projects = new List<Types.Project>();
        try
        {
            var projectSection = configuration.GetSection("Projects");

            projectSection.Bind(projects);
            if (projects is null || projects.Count == 0)
                throw new JsonException("Section 'Projects' is missing. Please refer to the Readme.");
            _projects = projects;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Parsing your projects went wrong: {e.Message}");
        }
    }

    private void PrintProject(Types.Project p)
    {
        Console.WriteLine($"{p.Name}");
        p.Actions.ForEach(a => Console.WriteLine($" - {a.Name}"));
    }


    private Types.Project CheckAndGetProject(string maybeProject)
    {
        if(_projects is null) throw new PreconditionException("_project not initialized");

        Types.Project? found = _projects.Find(p => p.Name == maybeProject) ??
            throw new ArgumentException("Project not found");
        return found;
    }

    private static Types.Action CheckAndGetAction(Types.Project project, string maybeAction)
    {
        Types.Action? found = project.Actions.Find(a => a.Name == maybeAction) ??
            throw new ArgumentException("Action not found");
        return found;
    }

    private static void ProceedWithValidatedArgs(Types.Project p, Types.Action a, string c)
    {
        GitHandler.GitAndDeploy(p, a, c);
    }
}