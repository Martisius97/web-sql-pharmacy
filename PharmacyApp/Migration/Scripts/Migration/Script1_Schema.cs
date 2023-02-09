using System.Data;
using DbUp.Engine;

namespace PharmacyApp.Migration.Scripts;

public class Script1_Schema : IScript
{
    public string ProvideScript(Func<IDbCommand> commandFactory)
    {
        var command = commandFactory();

        command.CommandText = "CREATE TABLE [dbo].[Client]( [Id] [uniqueidentifier] NOT NULL,[Name] [varchar](255) NOT NULL,[Address] [nvarchar](255) NOT NULL,[PostCode] [varchar](16) NULL)";
        command.ExecuteNonQuery();
        
        command.CommandText = "CREATE TABLE [dbo].[Logs]([Id] [uniqueidentifier] NOT NULL,[Action] [nvarchar](255) NOT NULL,[Date] [datetime] NOT NULL)";
        command.ExecuteNonQuery();

        return "";
    }
}