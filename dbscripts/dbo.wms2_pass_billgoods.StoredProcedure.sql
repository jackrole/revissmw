USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_BillGoods]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[wms2_pass_BillGoods]
    @passid varchar(20)
AS

select sku, SUM(pcs) pcs 
    into #yk from clasup_passdetail 
    where passid = @passid group by sku

select sku, kwcode, SUM(pcs) pcs, row_number() over (order by sku, kwcode) as id
    into #rk from wms_rk
    where
        sku in (select sku from #yk)
        and isrk = 1 and isck = 0
        and bonded = (select isUnderBounded from clasup_pass where id = @passid)
	group by sku, kwcode

select
	bill.sku, bill.kwcode, bill.pcs,
	case when bill.pcs < bill.yk_pcs then bill.pcs else bill.yk_pcs end transfer_pcs
from (
    select rk.sku, rk.kwcode, rk.pcs, rk.pcs - (rk.acc_pcs - yk.pcs) as yk_pcs
    from (
        select
            rk.sku, rk.kwcode, rk.pcs,
            (select sum(pcs) from #rk where sku = rk.sku and id <= rk.id) as acc_pcs
        from #rk rk
    ) rk left join #yk yk on rk.sku = yk.sku
) bill where yk_pcs > 0
GO
