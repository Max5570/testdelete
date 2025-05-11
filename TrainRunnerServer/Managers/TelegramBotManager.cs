using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TrainRunnerServer.Managers;

public class TelegramBotManager
{
    private const string TOKEN = "7857829920:AAFG7vAAYspwh2DgWkhZ9RH1MezRmEpVD34";
    private TelegramBotClient bot;
    
    public TelegramBotManager()
    {
        StartBot();
    }
    
    public async void StartBot()
    {
        bot = new TelegramBotClient(TOKEN);
        var me = await bot.GetMe();
        
        bot.OnMessage += OnMessage;
    }
    
    private async Task OnMessage(Message msg, UpdateType type)
    {
        if (msg.Text is null) return;

        if (msg.Text.Contains("ref_"))
        {
            
        }
        
        await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
    }
}