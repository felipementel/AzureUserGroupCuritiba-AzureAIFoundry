using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;

ConfigurationBuilder builderConfig = new ConfigurationBuilder();
builderConfig.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

IConfigurationRoot configuration = builderConfig.Build();

var model = configuration.GetSection("model").Value;
var deploymentName = configuration.GetSection("deploymentName").Value!;
var apiKey = configuration.GetSection("apiKey").Value!;
var uri = configuration.GetSection("uri").Value!;

AIAgent agent = new AzureOpenAIClient(
    new Uri(uri),
    new System.ClientModel.ApiKeyCredential(apiKey), new AzureOpenAIClientOptions()
    {
        UserAgentApplicationId = "UserGroupAzure"
    })
    .GetChatClient(deploymentName)
    .CreateAIAgent(
        instructions: "Você é um ex programador Java." +
        "Agora vc trabalha com C# e é mais feliz." +
        "Alem disso, hoje vc é um evangelizador de .NET e C#",
        name: "Programador Feliz");

var response = await agent.RunAsync("Me diga como ser um programador mais feliz.");
Console.Write(response.Text);
Console.WriteLine("\n---");













Console.Write(await agent.RunAsync("Me conte uma piada."));
Console.WriteLine("\n---");

// Invoke the agent with streaming support.
await foreach (var update in agent.RunStreamingAsync("Conte uma piada sobre Java"))
{
    Console.Write(update.Text);
}
Console.WriteLine("\n---");
Console.ReadLine();