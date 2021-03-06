USE [wms_hg]
GO
/****** Object:  Table [dbo].[clasup_pass]    Script Date: 05/27/2017 17:46:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clasup_pass](
	[id] [nvarchar](15) NOT NULL,
	[passTime] [datetime] NULL,
	[boxNum] [int] NULL,
	[pcs] [int] NULL,
	[cw] [numeric](18, 3) NULL,
	[nw] [numeric](18, 3) NULL,
	[pay] [numeric](18, 3) NULL,
	[currKind] [varchar](10) NULL,
	[cid] [nvarchar](50) NULL,
	[isUnderBounded] [bit] NULL,
	[superviseSts] [int] NOT NULL,
	[isTransferOut] [bit] NOT NULL,
	[isDeclared] [bit] NOT NULL,
	[isDeclareCommitted] [bit] NOT NULL,
	[isPassed] [bit] NOT NULL,
	[isTransferIn] [bit] NOT NULL,
	[isFinished] [bit] NOT NULL,
	[PI_SPECIAL] [bit] NOT NULL,
	[PI_PRICE] [bit] NOT NULL,
	[PI_PAY] [bit] NOT NULL,
	[creator] [nvarchar](50) NULL,
	[createTime] [datetime] NULL,
 CONSTRAINT [PK_clasup_pass_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'passTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'箱数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'boxNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'pcs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'净重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'cw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毛重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'nw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'pay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'币种' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'currKind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属客户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'cid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物属性（保税/非报税）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isUnderBounded'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监管状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'superviseSts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已移库（移出）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isTransferOut'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已报关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isDeclared'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已提交海关' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isDeclareCommitted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已放行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isPassed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已移库（移回）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isTransferIn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'isFinished'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'clasup_pass', @level2type=N'COLUMN',@level2name=N'createTime'
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_superviseSts]  DEFAULT ((0)) FOR [superviseSts]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isYk]  DEFAULT ((0)) FOR [isTransferOut]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isYk1]  DEFAULT ((0)) FOR [isDeclared]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isYK2]  DEFAULT ((0)) FOR [isDeclareCommitted]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isYK3]  DEFAULT ((0)) FOR [isPassed]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isYK4]  DEFAULT ((0)) FOR [isTransferIn]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_isFinished]  DEFAULT ((0)) FOR [isFinished]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_PI_SPECIAL]  DEFAULT ((0)) FOR [PI_SPECIAL]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_PI_PRICE]  DEFAULT ((0)) FOR [PI_PRICE]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_PI_PAY]  DEFAULT ((0)) FOR [PI_PAY]
GO
ALTER TABLE [dbo].[clasup_pass] ADD  CONSTRAINT [DF_clasup_pass_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
