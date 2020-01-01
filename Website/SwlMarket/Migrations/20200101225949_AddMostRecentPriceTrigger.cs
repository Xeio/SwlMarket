using Microsoft.EntityFrameworkCore.Migrations;

namespace SwlMarket.Migrations
{
    public partial class AddMostRecentPriceTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE TRIGGER Trigger_UpdateMostRecentPrice
									AFTER INSERT
									ON prices
									FOR EACH ROW
									BEGIN
										IF EXISTS(SELECT 1 FROM MostRecentPrices WHERE MostRecentPrices.ItemId = NEW.ItemId) THEN
											UPDATE MostRecentPrices
											SET MostRecentPrices.Time = NEW.Time, MostRecentPrices.Marks = NEW.Marks, MostRecentPrices.IPId = NEW.IPId
											WHERE MostRecentPrices.ItemId = NEW.ItemID;
										ELSE
											INSERT INTO MostRecentPrices (ItemId, Time, Marks, IPId) 
											VALUES (NEW.ItemId, NEW.Time, NEW.Marks, NEW.IPId);
										END IF;
									END");

			migrationBuilder.Sql(@"INSERT INTO mostrecentprices (ItemId, Time, Marks, IPId)
									SELECT ItemId, Time, Marks, IPId
									FROM Prices
									WHERE Id in (select max(Id) from prices group by ItemId) AND
										ItemId NOT IN (SELECT ItemId FROM MostRecentPrices)");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER Trigger_UpdateMostRecentPrice");
		}
    }
}
