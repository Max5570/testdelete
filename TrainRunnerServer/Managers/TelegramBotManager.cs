using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TrainRunnerServer.Database;
using TrainRunnerServer.Models;

namespace TrainRunnerServer.Managers;

public class TelegramBotManager
{
    private const string TOKEN = "7857829920:AAFG7vAAYspwh2DgWkhZ9RH1MezRmEpVD34";
    private TelegramBotClient bot;
    private IServiceScopeFactory _scopeFactory;
    
    public TelegramBotManager(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        
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
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            int refererId = 0;
            var parts = msg.Text.Split(' ');
            
            if (parts.Length > 1 && parts[1].StartsWith("ref_"))
            {
                string number = parts[1].Substring(4); // отрезаем "ref_"
                refererId = int.Parse(number);
            }
            else
            {
                //todo log !!!
            }

            var relationsList = await dbContext.TelegramReferalRelation.ToListAsync();
            
            var exist = relationsList.Any(x => x.RefererId == refererId && x.ReferalId == (int) msg.From.Id);

            if (!exist)
            {
                var newRelation = new TelegramChatUserModel();
                newRelation.ReferalId = (int)msg.From.Id;
                newRelation.RefererId = refererId;

                dbContext.TelegramReferalRelation.Add(newRelation);
                await dbContext.SaveChangesAsync();
            }

            relationsList = await dbContext.TelegramReferalRelation.ToListAsync();
            var responce = "";
            relationsList.ForEach(x => responce += $"ref: {x.ReferalId}, referer: {x.RefererId}\n");
        
            await bot.SendMessage(msg.Chat, responce);
        }
        
    }
}