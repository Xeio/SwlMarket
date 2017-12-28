INSERT INTO MostRecentPrices (ItemId, [Time], Marks, ApiKeyId, ExpiresIn)
SELECT ItemId, [Time], Marks, ApiKeyId, ExpiresIn
FROM Prices
WHERE Id in (select max(Id) from Prices group by ItemId) AND
      ItemId NOT IN (SELECT ItemId FROM MostRecentPrices)