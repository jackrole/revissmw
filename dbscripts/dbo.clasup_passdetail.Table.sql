USE [wms_hg]
GO
/****** Object:  Table [dbo].[clasup_passdetail]    Script Date: 05/27/2017 17:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clasup_passdetail](
	[passid] [nvarchar](15) NOT NULL,
	[seq] [int] NOT NULL,
	[sku] [varchar](50) NULL,
	[pcs] [int] NULL,
	[nw] [numeric](18, 3) NULL,
	[cw] [numeric](18, 3) NULL,
	[pay] [numeric](18, 3) NULL,
	[currKind] [varchar](10) NULL,
	[origin] [varchar](10) NULL,
	[containerNo] [nvarchar](20) NULL,
	[color] [nvarchar](10) NULL,
	[size] [nvarchar](10) NULL,
	[createTime] [datetime] NULL,
 CONSTRAINT [PK_clasup_pass] PRIMARY KEY CLUSTERED 
(
	[passid] ASC,
	[seq] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'料号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'sku'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'pcs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毛重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'nw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'净重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'cw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'pay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'币种' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'currKind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'origin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'箱号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'containerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'color'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺寸' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_passdetail', @level2type=N'COLUMN',@level2name=N'size'
GO
