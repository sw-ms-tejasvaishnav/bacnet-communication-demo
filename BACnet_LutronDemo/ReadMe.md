# Read me

### Create DB 'ESD_Lutron' and below table
```sh
SET ANSI_PADDING ON
GO
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
GO
SET ANSI_PADDING OFF
GO
```
Two lutron light device object as per doc below

```sh
public enum LutronObjectType : uint
{
    Lighting_Level = 2,
    Lighting_State = 3
}
```
### Reference document
http://www.lutron.com/TechnicalDocumentLibrary/369998.pdf
