USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_DN]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[wms2_pass_DN]
@passid nvarchar(15)
as

select a.passid, a.sku, dbo.f_origin(origin) cn_origin, '' tariff_group, '' cn_family, rtrim(ltrim(b.currkind)) currency, '' incoterm, 
	a.pcs, a.nw gw, a.cw nw, a.pay amount, a.createTime
into #a 
from clasup_passdetail a inner join clasup_pass b on a.passid=b.id where a.passid=@passid

select a.*,
CONVERT(NUMERIC(18,3), amount/pcs) price, b.sku0, b.zcxh, b.hscode, b.code_s, b.bFJ, b.bCC, b.g_unit, b.unit_1, b.unit_2, b.baname pinm, a.currency curr--, dbo.f_curr(a.currency) curr
from #a a 
INNER join vn_items_sku1 b on a.sku= b.ccsku
order by a.passid, a.createTime

drop table #a
GO
