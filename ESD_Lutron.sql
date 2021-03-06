/* First you need to create database with "ESD_Lutron" name and then run this scrip */
USE [ESD_Lutron]
GO
/****** Object:  Table [dbo].[BACnetDeviceMapping]    Script Date: 9/8/2017 8:25:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BACnetDeviceMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BACnetDeviceMapping](
	[bacnet_device_mapping_id] [int] IDENTITY(1,1) NOT NULL,
	[device_id] [int] NULL,
	[object_instance] [int] NULL,
	[floor_id] [int] NULL,
	[suite_id] [int] NULL,
	[room_id] [int] NULL,
 CONSTRAINT [PK_BACnetDeviceMapping] PRIMARY KEY CLUSTERED 
(
	[bacnet_device_mapping_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BACnetDevices]    Script Date: 9/8/2017 8:25:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BACnetDevices]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BACnetDevices](
	[bacnet_device_id] [int] IDENTITY(1,1) NOT NULL,
	[network_id] [varchar](100) NULL,
	[device_id] [int] NULL,
	[object_type] [varchar](500) NULL,
	[object_instance] [int] NULL,
	[object_name] [varchar](500) NULL,
	[routed_source] [varchar](100) NULL,
	[routed_net] [int] NULL,
 CONSTRAINT [PK_BACnetDevices] PRIMARY KEY CLUSTERED 
(
	[bacnet_device_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
