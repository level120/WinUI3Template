namespace Atelier.Models;

/// <summary>
/// LogItem is a record that represents a log item with a message.
/// </summary>
public sealed class LogItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogItem"/> class.
    /// </summary>
    /// <param name="message">message</param>
    public LogItem(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Message is the log item.
    /// </summary>
    public string? Message { get; set; }
};