# <u>RogStock 2026</u>

**Revison of the simple stock system written in C#**

Still in development, purposely posted on GITHub early so people can follow the
developemnt of the project via the branches, with the latest branch being the
latest version of the project


**Features Include:**
- Uses SQL Server Reporting Services for reports
- SQL Server backend
- custom user colour scheme!
- Location/Lot tracking with transaction history
- UOM
- Product families
- Location renaming
- Uses the RogEngine a low code framework for database applications features include:
  - fully working main menu
  - main menu security
  - configurable main menu security levels
  - API for read/writing to tables
  - complete schema system support
  - login system
  - SQl Server native
  - user login management
  - populates labels on data entry screens with human friendly desciptions at runtime e.g. label1 becomes: Item ID
  - local customisable theme with dynamic preview
  - transaction log system
  - bespoke SQl Server error system for custom errors

**Usage**

Download from gitHub into a folder
From that folder copy the contents of: **DriveCRogStock2025** to drive C
From **SQLServerDatabase** copy that to the drive C folder
In SSMS restore the BAK file
Open the project compile and run

**Additions For This Version Include:**
- FULL transaction history for everything! (ISO9001)
- Purchasing system with:
  = Purchase orders -> lines -> delivery dates etc
  = PO inspect <- in development
  = PO receipt <- in development
  = Vendors
  = Vendor billing addresses e.g. buy from branch pay head office

**Other Changes:**
- Slightly revamped interface and forms
- Uses SQL column descriptions for dataentry form field labels
  (an idea I used a lot when developing in MSAccess)


