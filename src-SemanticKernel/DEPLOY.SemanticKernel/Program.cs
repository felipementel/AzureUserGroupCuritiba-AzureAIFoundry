using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;

ConfigurationBuilder builderConfig = new ConfigurationBuilder();
builderConfig.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

IConfigurationRoot configuration = builderConfig.Build();

var model = configuration.GetSection("model").Value;
var deploymentName = configuration.GetSection("deploymentName").Value!;
var apiKey = configuration.GetSection("apiKey").Value!;
var uri = configuration.GetSection("uri").Value!;

var builder = Kernel
    .CreateBuilder()
    .AddAzureOpenAIChatCompletion(deploymentName, uri, apiKey, model);

Kernel kernel = builder.Build();

kernel.Plugins.AddFromType<LightsPlugin>("Luzes");
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

OpenAIPromptExecutionSettings settings = new()
{
    //Temperature = 0.7f,
    //MaxTokens = 1000,
    //TopP = 1.0f,
    //FrequencyPenalty = 0.0f,
    //PresencePenalty = 0.0f,
    //StopSequences = new[] { "\n" },
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
};

Console.Clear();

ChatHistory history = new();
history.AddSystemMessage(
    "Você é um assistente que fala pouco e é sempre muito objetivo. " +
    "Sempre que alterar o estado de uma lampada, envie a lista das lampadas e seus estados.");

//history.AddSystemMessage(
//    "Você é um assistente grosseiro e sem paciencia " +
//    "que ajuda os usuários a manipular suas lampadas. " +
//    "Responda sempre com um tom de deboche e sarcasmo, mas " +
//    "sempre diga o estado na lampada ");

string userInput = string.Empty;

Console.WriteLine("Manipule suas lampadas. Digite 'sair' para fechar a conversa.");

while (true)
{
    Console.Write("Você: ");
    userInput = Console.ReadLine()!;

    if (userInput.Equals("sair", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    history.AddUserMessage(userInput);
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        settings,
        kernel);

    history.AddMessage(result.Role, result.Content!);
    Console.WriteLine();
    Console.WriteLine($"IA: {result.Content}");
    Console.WriteLine("---");
}

public class LightsPlugin
{
    private readonly List<LightModel> lights = new()
    {
        new LightModel { Id = "1", Name = "Luz da Sala", State = "off" },
        new LightModel { Id = "2", Name = "Luz do Quarto", State = "on" },
        new LightModel { Id = "3", Name = "Luz da Cozinha", State = "off" }
    };

    [KernelFunction("obter_luzes")]
    [Description("Obtenha uma lista das luzes e seu estado atual.")]
    [return: Description("Lista de luzes com seu estado atual.")]
    public Task<List<LightModel>> GetLightsAsync()
    {
        return Task.FromResult(lights);
    }

    [KernelFunction("alterar_estado")]
    [Description("Alterar o estado de uma luz")]
    [return: Description("O novo estado da luz")]
    public Task<LightModel?> ChangeLightStateAsync(
        [Description("O ID da luz a ser alterada")]
        string lightId,
        [Description("O novo estado da luz (ligado/desligado)")]
        string newState)
    {
        var light = lights.FirstOrDefault(l => l.Id == lightId);
        if (light == null)
        {
            return Task.FromResult((LightModel?)null);
        }

        light.State = newState;
        return Task.FromResult(light);
    }
}

public class LightModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
}
