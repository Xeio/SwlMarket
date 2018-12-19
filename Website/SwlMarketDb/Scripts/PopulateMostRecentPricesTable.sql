INSERT INTO MostRecentPrices (ItemId, [Time], Marks, IPId)
SELECT ItemId, [Time], Marks, IPId
FROM Prices
WHERE Id in (select max(Id) from Prices group by ItemId) AND
      ItemId NOT IN (SELECT ItemId FROM MostRecentPrices)