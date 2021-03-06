USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_EnsureSku]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[wms2_pass_EnsureSku]
@passid nvarchar(15)
as

select distinct sku ccsku into #a from clasup_passdetail where passid=@passid
insert into items_sku(sku,ccsku,cnname,hscode,createdtime,iszara)
select ccsku,ccsku,'','',getdate(),1 from #a where ccsku not in (select ccsku from items_sku)

select ccsku into #b from #a where ccsku not in (select ccsku from vn_items_sku1)
drop table #a

if exists(select 1 from #b)
update items_sku set isvalid=0 where sku in (select ccsku from #b)

select ccsku from #b
drop table #b
GO
