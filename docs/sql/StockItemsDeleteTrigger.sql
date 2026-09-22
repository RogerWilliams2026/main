SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =========================================================================================
-- Author:		Roger Williams
-- Create date: 15/01/2026
-- Description:	cascades delete to: stock_description, stock_media, stock_loc
-- Note: future update should insert data into history tables FIRST
--       frontend would need to be changed so queries to loc/lot_TRN can use history itemids
--       stock_lot is deleted in stock_loc delete trigger
-- =========================================================================================
CREATE TRIGGER CascadeDelete_StockItem 
   ON RogStock.dbo.Stock_Items FOR DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   --delete from stock_descriptions
   DELETE FROM Stock_Description WHERE Stock_Description.STKD_ItemID IN (SELECT deleted.STKI_ItemID FROM deleted);
   DELETE FROM Stock_Media WHERE Stock_Media.STKM_ID IN (SELECT deleted.STKI_ItemID FROM deleted);
   DELETE FROM Stock_Loc WHERE Stock_Loc.LOC_ItemID IN (SELECT deleted.STKI_ItemID FROM deleted);

END
GO
