# Azure AI Foundry – Exemplos de Implementação

Exemplos práticos apresentados no **Azure User Group Curitiba**, demonstrando como explorar o potencial do Azure AI Foundry utilizando **Semantic Kernel** e **Azure AI Agents**.

## Sobre o Repositório
Este repositório reúne dois exemplos apresentados no evento da **Azure User Group Curitiba**, realizado dentro da **Bosch Brasil**, onde demonstrei algumas das capacidades mais recentes do Azure AI Foundry, incluindo:

- Deploy rápido de modelos
- Comparação de custos e latência
- Geração de vídeos com **SORA** e **SORA-2**
- Criação de um RAG com portal publicado em menos de 5 minutos
- E outros recursos que ficaram de fora da apresentação por falta de tempo

Aqui você encontrará implementações funcionais que podem ser utilizadas como referência, estudo ou ponto de partida para seus próprios projetos.

---

## Conteúdo do Repositório

### 0. Apresentação
Slides utilizados na apresentação do evento.
**Arquivo:** `AzureAIFoundry.pdf`

---

### 1. Semantic Kernel com Plugins
Exemplo demonstrando:

- Criação e uso de **plugins nativos e semânticos**
- Chamadas a modelos hospedados no **Azure AI Foundry**
- Execução de pipelines orquestrados via Semantic Kernel
- Estrutura simples para expansão com novos plugins

**Diretório:** `src-SemanticKernel/`
Inclui instruções de execução e dependências.

---

### 2. Azure AI Agents
Implementação mostrando:

- Criação de agentes inteligentes usando **Azure AI Agents**
- Configuração de habilidades, ferramentas e memória
- Execução de tarefas guiadas por linguagem natural
- Integração com modelos do Azure e fluxos automatizados

**Diretório:** `src-AgentFramework/`
Inclui exemplo mínimo funcional para iniciar rapidamente.

---

## Como Executar os Exemplos

### Pré-requisitos
- .NET 9 SDK instalado
- Visual Studio Code ou outro editor de sua preferência
- Conta Azure com acesso ao **Azure AI Foundry**
- Resource com modelos configurados
- Chave e endpoint disponíveis em variáveis de ambiente:

Configuração esperada das variáveis de ambiente no arquivo `appSettings.json`:
```bash
{
  "model": "",
  "deploymentName": "",
  "apiKey": "",
  "uri": "",
}
````

Qualquer dúvida, sinta-se à vontade para abrir uma issue ou entrar em contato diretamente.

[LinkedIn](linkedin.com/in/felipementel)
