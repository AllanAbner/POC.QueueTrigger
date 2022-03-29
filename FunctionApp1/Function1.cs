using FunctionApp1.Models;
using FunctionApp1.Repositorios;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("pessoa-queue", Connection = "")] string myQueueItem, ILogger log)
        {
            var pessoa = JsonSerializer.Deserialize<Pessoa>(myQueueItem);

            var repositorio = new RepositorioPessoa();
            repositorio.AdicionarPessoa(pessoa);

            var pessoaDb = repositorio.BuscarPessoa(pessoa.Id);

            log.LogInformation($"C# Queue trigger function processed: {JsonSerializer.Serialize(pessoaDb)}");

        }
    }
}