USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_GetNW]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[wms2_pass_GetNW]
@passid nvarchar(15)
as

select sum(cw) cw from clasup_passdetail where passid=@passid group by sku
GO
