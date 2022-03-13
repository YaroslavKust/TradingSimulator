using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL
{
    public class ActiveConfiguration: IEntityTypeConfiguration<Active>
    {
        public void Configure(EntityTypeBuilder<Active> builder)
        {
            var actives = new[]
            {
                new Active
                {
                    Name = "Bitcoin",
                    Ticket = "BTC",
                    Type = ActiveTypes.Crypto
                },
                new Active
                {
                    Name = "Etherium",
                    Ticket = "ETH",
                    Type = ActiveTypes.Crypto
                },
                new Active
                {
                    Name = "Litecoin",
                    Ticket = "LTC",
                    Type = ActiveTypes.Crypto
                },
                new Active
                {
                    Name = "Tesla",
                    Ticket = "TSL",
                    Type = ActiveTypes.Share
                },
                new Active
                {
                    Name = "Meta Platform Inc.",
                    Ticket = "FB",
                    Type = ActiveTypes.Share
                },
                new Active
                {
                    Name = "Apple",
                    Ticket = "AAPL",
                    Type = ActiveTypes.Share
                },
                new Active
                {
                    Name = "Microsoft",
                    Ticket = "MSFT",
                    Type = ActiveTypes.Share
                },
                new Active
                {
                    Name = "S&P 500",
                    Ticket = "SPX",
                    Type = ActiveTypes.Index
                },
                new Active
                {
                    Name = "Индекс ММВБ",
                    Ticket = "MICEXINDEXCF",
                    Type = ActiveTypes.Index
                },
                new Active
                {
                    Name = "Index Dow Jones",
                    Ticket = "DJI",
                    Type = ActiveTypes.Index
                },
                new Active
                {
                    Name = "Gold",
                    Ticket = "XAU",
                    Type = ActiveTypes.Commodity
                },
                new Active
                {
                    Name = "Platinum",
                    Ticket = "XPT",
                    Type = ActiveTypes.Commodity
                },
                new Active
                {
                    Name = "Palladium",
                    Ticket = "XPD",
                    Type = ActiveTypes.Commodity
                },
                new Active
                {
                    Name = "Silver",
                    Ticket = "XAG",
                    Type = ActiveTypes.Commodity
                }
            };

            int id = 1;

            foreach(var active in actives)
            {
                active.Id = id;
                id++;
            }

            builder.HasData(actives);
        }
    }
}
