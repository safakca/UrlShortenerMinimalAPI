using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });
}

var urlMap= new Dictionary<string, string>();

app.MapPost("/shorten", (string longUrl) =>
{
    var shortCode = GenerateShortCode();
    urlMap[shortCode] = longUrl;
    
    return Results.Ok($"http://localhost:5013/{shortCode}");
});

app.MapGet("/{code}", (string code) =>
{
    if (urlMap.TryGetValue(code, out var longUrl))
    {
        return Results.Redirect(longUrl);
    }
    return Results.NotFound("Short URL not found");
});

string GenerateShortCode()
{
    char[] Base32Chars = "ABCDEFGHIJKLMNOPRSTUVWXYZ0123456789".ToCharArray();
    var bytes = new byte[8]; // create random data 40-bit (about 8 base32 character)
    RandomNumberGenerator.Fill(bytes);
    var key = new StringBuilder(8);
    for (int i = 0; i < bytes.Length; i++)
    {
        key.Append(Base32Chars[bytes[i] % Base32Chars.Length]);
    }
    return key.ToString();
}


app.Run();
