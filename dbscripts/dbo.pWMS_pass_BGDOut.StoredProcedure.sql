USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[pWMS_pass_BGDOut]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[pWMS_pass_BGDOut]
@bgdno varchar(30)
as
select *,convert(int,qty_1) pcs,convert(numeric(18,3),qty_2) nw,convert(numeric(18,3),trade_total) amount into #b from form_list where pre_entry_id=@bgdno

declare @iNum int, @skun varchar(30), @cn_origin nvarchar(50), @currency nvarchar(50), @gid int,
	@pcs int, @gw numeric(18,3), @pay numeric(18,3), @zcxh varchar(10), @bFJ varchar(10), @bCC varchar(10),
	@tariff_group nvarchar(20),  @Desc_section nvarchar(10), @desp nvarchar(255)

set @gid =1

select * into #c from Form_LIST where 1>2

declare c1 cursor for 

select sum(pcs) pcs, sum(nw) nw, sum(amount) amount, g_no
from #b 
group by g_no
order by g_no asc

open c1 fetch next from c1 into @pcs, @gw, @pay, @zcxh
while (@@fetch_status = 0)
begin
	select @iNum = count(*) from #b where g_no=@zcxh
	
	select top 1 @skun=sku,@cn_origin=origin_country,@currency=trade_curr from #b where g_no=@zcxh order by pcs desc

	Insert Into #c(FJ,CC,pre_entry_id, code_t, code_s, g_name, g_model, qty_1,create_date,g_no,g_id, 
		g_unit, decl_price, trade_total, trade_curr, qty_conv, unit_1, qty_2, unit_2, origin_country, duty_mode, sku,iNum) 

	select top 1 '','', @bgdno, hscode, code_s, isnull(gbsku,''), baname, @pcs, getdate(), @zcxh, @gid, 
		g_unit, @pay/@pcs, @pay, dbo.f_curr0(@currency), @pcs, unit_1, @gw, unit_2, '142', '3', dbo.f_sku(@skun),@iNum
	from dbo.vn_items_sku where ccsku=@skun

	set @gid = @gid+1

	fetch next from c1 into @pcs, @gw, @pay,@zcxh
end
close c1
deallocate c1

drop table #b
SELECT *,dbo.f_origin0(origin_country) origin,dbo.f_curr1(trade_curr) currency FROM #C ORDER BY G_ID
drop table #c
GO
