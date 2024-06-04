const string server = "http://localhost:5269";
const string callback = "http://localhost:5029/callback/item/new";
const string topic = "item.new";

var client = new HttpClient();

await client.PostAsJsonAsync(server + "/subscribe", new { topic, callback });


var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapPost("/callback/item/new", (object payload) =>
{
    Console.WriteLine($"Received payload: {payload}");
});

app.Run();
