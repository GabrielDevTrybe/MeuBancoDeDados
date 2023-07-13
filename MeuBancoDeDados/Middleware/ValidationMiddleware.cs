using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Verificar o tipo de conteúdo da requisição
        var contentType = context.Request.ContentType;
        if (string.Equals(contentType, "application/json", StringComparison.OrdinalIgnoreCase))
        {
            // Ler o corpo da requisição como JSON
            var requestBody = await ReadJsonBody(context.Request);

            // Verificar o campo de email
            if (!IsValidEmail(requestBody.Email))
            {
                context.Response.StatusCode = 400; // Código de status "Bad Request"
                await context.Response.WriteAsync("O email fornecido não é válido.");
                return;
            }

            // Verificar o campo de senha
            if (!IsValidPassword(requestBody.Senha))
            {
                context.Response.StatusCode = 400; // Código de status "Bad Request"
                await context.Response.WriteAsync("A senha fornecida deve ter no mínimo 8 caracteres.");
                return;
            }
        }
        else
        {
            context.Response.StatusCode = 400; // Código de status "Bad Request"
            await context.Response.WriteAsync("Tipo de conteúdo inválido.");
            return;
        }

        // Chamar o próximo middleware na cadeia de execução
        await _next(context);
    }

    private bool IsValidEmail(string email)
    {
        // Validar o formato de email usando expressão regular
        string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.[a-zA-Z]{2,}$";
        return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, emailPattern);
    }

    private bool IsValidPassword(string password)
    {
        // Verificar o comprimento mínimo da senha
        return !string.IsNullOrWhiteSpace(password) && password.Length >= 8;
    }

    private async Task<dynamic> ReadJsonBody(HttpRequest request)
    {
        using (var streamReader = new StreamReader(request.Body))
        {
            var requestBody = await streamReader.ReadToEndAsync();
            return JsonConvert.DeserializeObject(requestBody);
        }
    }
}
