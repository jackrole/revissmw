USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_GetNW]    Script Date: 05/18/2017 15:11:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[wms2_pass_GetNW]
@passid nvarchar(15)
as

select sum(cw) cw from clasup_passdetail where passid=@passid group by sku
GO
