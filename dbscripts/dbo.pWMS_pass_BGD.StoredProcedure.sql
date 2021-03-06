USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[pWMS_pass_BGD]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--/*
create proc [dbo].[pWMS_pass_BGD]
@rkno varchar(20)
as
--*/
/*
declare @rkno varchar(20)
set @rkno='1507270014'
*/
select sku, sum(pcs) pcs, sum(nw) nw, sum(pay) amount 
	into #a from clasup_passdetail r where passid=@rkno
	group by sku

declare @iNum int, @skun varchar(30), @cn_origin nvarchar(50), @currency nvarchar(50), @gid int,
	@pcs int, @gw numeric(18,3), @pay numeric(18,3), @zcxh varchar(10), @bFJ varchar(10), @bCC varchar(10),
	@tariff_group nvarchar(20),  @Desc_section nvarchar(10), @desp nvarchar(255),@currkind varchar(10)

set @gid =1

select @currkind=currkind from clasup_pass where id=@rkno

select a.sku, '' cn_origin,'' currency, pcs, nw, amount, c.zcxh
into #b from #a a inner join items_sku c on a.sku= c.ccsku

select * into #c from Form_LIST where 1>2

declare c1 cursor for 

select sum(pcs) pcs, sum(nw) nw, sum(amount) amount, zcxh--, bFJ, bCC
from #b 
group by zcxh--, bFJ, bCC
order by convert(int,zcxh) asc

open c1 fetch next from c1 into @pcs, @gw, @pay,@zcxh--,@bFJ,@bCC--, @tariff_group, @Desc_section
while (@@fetch_status = 0)
begin
	select @iNum = count(*) from #b where zcxh=@zcxh --  and Desc_section=@Desc_section
	
	select top 1 @skun=sku,@cn_origin=cn_origin,@currency=currency from #b where zcxh=@zcxh order by pcs desc-- and Desc_section=@Desc_section
	
	--select @desp = desp from vn_items_sku where ccsku=@skun

	--if @iNum > 1
	--set @desp = replace(@desp, dbo.f_sku(@skun), dbo.f_sku(@skun)+'等')

	print @iNum
	print @skun


	Insert Into #c(FJ,CC,pre_entry_id, code_t, code_s, g_name, g_model, qty_1,create_date,g_no,g_id, 
		g_unit, decl_price, trade_total, trade_curr, qty_conv, unit_1, qty_2, unit_2, origin_country, duty_mode, sku,iNum) 

	select top 1 '','', @rkno, hscode, code_s, isnull(gbsku,''), baname, @pcs, getdate(), @zcxh, @gid, 
		g_unit, convert(numeric(18,4),@pay/@pcs), @pay, '', @pcs, unit_1, @gw, unit_2, @cn_origin, '3', dbo.f_sku(@skun),@iNum
	from dbo.vn_items_sku where ccsku=@skun

	set @gid = @gid+1

	fetch next from c1 into @pcs, @gw, @pay,@zcxh--,@bFJ,@bCC--, @tariff_group, @Desc_section
end
close c1
deallocate c1

drop table #a
drop table #b
SELECT *, dbo.f_origin0(@currkind) origin, dbo.f_originE(@currkind) cn_origin, dbo.f_curr1(@currkind) currency FROM #C ORDER BY G_ID
drop table #c
GO
