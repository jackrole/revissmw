USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_BGD]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--/*
CREATE proc [dbo].[wms2_pass_BGD]
@rkno varchar(20),
@isdel int=0
as
if @isdel=1
begin
	delete from Form_LIST where pre_entry_id in (select pre_entry_id from form_head where rkno=@rkno)
	delete from form_head where rkno=@rkno
	delete from form_detail where rkno=@rkno
end
--*/
/*
declare @rkno varchar(20)
set @rkno='0160425611'
*/
if not exists(select 1 from form_head where rkno=@rkno)
begin

	declare @bgdno nvarchar(50)
	set @bgdno = @rkno+'_1'
	exec wms2_pass_BGD_HEAD @rkno,@bgdno out

	select sku, sum(pcs) pcs, sum(nw) nw, sum(pay) amount 
	into #a from clasup_passdetail r where passid=@rkno
	group by sku

	declare @iNum int, @skun varchar(30), @cn_origin nvarchar(50), @currency nvarchar(50), @gid int, @iPage int,
		@pcs numeric(18,3), @gw numeric(18,3), @pay numeric(18,3), @price numeric(18,4),@zcxh varchar(10), @bFJ varchar(10), @bCC varchar(10),
		@tariff_group nvarchar(20), @cn_family nvarchar(50), @Desc_section nvarchar(10), @desp nvarchar(255)

	set @gid =1
	set @iPage = 1

	select distinct a.sku, pcs, a.nw, amount, c.zcxh1 zcxh, isnull(c.bFJ,'') bFJ, isnull(c.bCC,'') bCC, cn_family
		,'' cn_origin,'' currency,'' tariff_group,'' Desc_section
	into #b from #a a inner join vn_items_sku1 c on a.sku = c.ccsku 

	declare c1 cursor for 

	select cn_origin, currency, sum(pcs) pcs, sum(nw) nw, sum(amount) amount, zcxh, bFJ, bCC,  tariff_group, Desc_section
	from #b group by cn_origin, currency, zcxh, bFJ, bCC, tariff_group, Desc_section
	order by dbo.f_fjno(bFJ), bCC desc, convert(int,zcxh) asc, cn_origin, tariff_group, Desc_section

	open c1 fetch next from c1 into @cn_origin, @currency, @pcs, @gw, @pay, @zcxh, @bFJ, @bCC, @tariff_group, @Desc_section
	while (@@fetch_status = 0)
	begin
		if (@gid>1 and (@gid-1) % 20=0)
		begin
			exec pWMS_BGD_HEAD @rkno,@bgdno out
		end

		begin
			set @skun = ''
			select @iNum = count(*) from #b where zcxh=@zcxh and cn_origin=@cn_origin and tariff_group=@tariff_group and currency=@currency and Desc_section=@Desc_section

			select top 1 @skun=sku from #b where zcxh=@zcxh and cn_origin=@cn_origin and tariff_group=@tariff_group and currency=@currency and Desc_section=@Desc_section order by pcs desc
			select @desp = desp from items_sku where ccsku=@skun

			if @iNum > 1
				set @desp = replace(@desp, dbo.f_sku(@skun), dbo.f_sku(@skun)+'等')

			insert into form_detail(rkno, zcxh, sku, fj, cc, g_id, tariff_group, cn_origin, currency, pcs, nw, amount, Desc_section)
			select @rkno,@zcxh,sku,@bFJ,@bCC,@gid, @tariff_group, @cn_origin, @currency, pcs, nw, amount, @Desc_section
			from #b where zcxh=@zcxh and cn_origin=@cn_origin and tariff_group=@tariff_group and currency=@currency and Desc_section=@Desc_section

			set @price = @pay/@pcs

			Insert Into Form_LIST(FJ,CC,pre_entry_id, code_t, code_s, g_name, g_model, qty_1,create_date,g_no,g_id, 
				g_unit, decl_price, trade_total, trade_curr, qty_conv, unit_1, qty_2, unit_2, origin_country, duty_mode, sku,iNum) 
	
			select top 1 @bFJ,@bCC, @bgdno, hscode, code_s, dbo.f_pinm(hscode, cnname, @skun, @iNum), dbo.f_desp(@desp, @skun, @iNum), @pcs, getdate(), @zcxh, @gid, 
				g_unit, @price, @pay, @currency, convert(varchar(20),@pcs), unit_1, @gw, unit_2, @cn_origin, '3', @skun, @iNum
			from dbo.vn_items_sku1 where ccsku=@skun

			set @gid = @gid+1
		end
		fetch next from c1 into @cn_origin, @currency, @pcs, @gw, @pay, @zcxh, @bFJ, @bCC, @tariff_group, @Desc_section
	end
	close c1
	deallocate c1

	drop table #a
	drop table #b

	exec pWMS_WW @rkno
end
GO
