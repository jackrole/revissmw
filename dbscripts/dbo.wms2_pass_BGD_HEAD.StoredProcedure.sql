USE [wms_hg]
GO
/****** Object:  StoredProcedure [dbo].[wms2_pass_BGD_HEAD]    Script Date: 05/27/2017 17:47:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[wms2_pass_BGD_HEAD]
@rkno varchar(25),
@bgdno varchar(25) output
as

declare @tno varchar(30), @trade_co nvarchar(30), @trade_name nvarchar(500)
select top 1 @tno = replace(replace(pre_entry_id,@rkno,''),'_','') 
from form_list 
where pre_entry_id in (select pre_entry_id from form_head 
where rkno=@rkno) order by convert(int, replace(replace(pre_entry_id,@rkno,''),'_','')) desc
if isnull(@tno,'') = ''
	set @tno = '0'
--set @tno = right(@tno, len(@tno)-charindex('_',@tno))
set @tno = convert(varchar(10), convert(int, @tno)+1 )
set @bgdno = @rkno +'_'+ @tno

if not exists(select 1 from form_head where pre_entry_id=@bgdno)
begin

select @trade_co=a.code,@trade_name=a.ccname 
from outcust a inner join clasup_pass b on a.code=b.cid where b.id=@rkno

insert into dbo.form_head(
	rkno,ywno,pre_entry_id, i_e_port, ie_flag, customs_id, manual_no, 
        contr_no, i_e_date, d_date, trade_co, trade_name, 
       owner_code, owner_name, agent_code, agent_name, traf_mode, 
	traf_name, voyage_no, bill_no, trade_mode, cut_mode, 
	in_ratio, pay_way, lisence_no, trade_country, distinate_port, 
	district_code, appr_no, trans_mode, fee_mark, fee_rate, 
	fee_curr, insur_mark, insur_rate, insur_curr, other_mark, 
	other_rate, other_curr, pack_no, wrap_type, gross_wt, 
	net_wt, note_s, ex_source, RaDeclNo, StoreNo, 
	type_er, entry_group, is_status, username, create_date, 
	del_flag, RaManualNo, PrdtID, CreatedTime, Creator) 

select @rkno,@bgdno, @bgdno, '2216', '9', @bgdno, 'H22161000002' , '', '', getdate(),@trade_co,@trade_name,
	@trade_co, @trade_name, '3122610003', '上海中远空港保税物流有限公司','Y','保税港区', '', '', '0110', '',
	'0', '', '', '', '', '', '', '1', null, null,
	null, null, null, null,null,null, null, 0, null, 0,
	weight, '', '0(0)', '',  '', 0, 'Entr', 4, 'ZYA2', getdate(), 
	0, '', '', getdate(), '' 
from outbound where ckno=@rkno

end

--select * from outbound
GO
