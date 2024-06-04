public record Subscription(string Topic, string Callback);

public class WebhookService
{
    private readonly List<Subscription> _subscriptions = [];
    private readonly HttpClient _httpClient = new();

    public void Subscribe(Subscription subscription)
    {
        _subscriptions.Add(subscription);
        Console.WriteLine($"Subscription added. Topic: {subscription.Topic}, Callback: {subscription.Callback}");
    }

    public async Task PublishMessage(string topic, object message)
    {
        Console.WriteLine($"{topic} {message}");
        var subscribedWebhooks = _subscriptions.Where(w => w.Topic == topic);

        foreach (var webhook in subscribedWebhooks)
        {
            await _httpClient.PostAsJsonAsync(webhook.Callback, message);
            Console.WriteLine($"Message sent to subscription. Callback: {webhook.Callback}");
        }
    }
}