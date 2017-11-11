/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/

--:setvar RarityStandard 2
--:setvar Signet 7653089

--IF NOT EXISTS(SELECT 1 FROM Items WHERE Id = 9341504)
--BEGIN
--  INSERT INTO ITEMS (Id, Name, ItemCategory, Rarity) VALUES ('Signet of the Paragon', $(Signet), $(RarityStandard))
--END
--GO