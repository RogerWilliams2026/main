# <u>RogJobCRMPlus</u>

**Conversion Of The Access Application For Use With Walsall Works**

**Why Do It?**

For the challenge, could I really convert the Access application into a client-server SQL based system?
Is not a commercial application was created for me.
<br>

**What It Does**

As with the Access application allows the job seeker to record details about jobs applied for and track the
application process.

Features Include:

- full job application tracking
- mailshot creation
- log visits to events
- contact listing
- reports created as Word documents(!)
- search facility


**Folder Structure:**

Drive C has a folder **RogJobCRMPlus**

Contents:
- Mailshots
- Resources


**Usage:**

Obviously need SQL Server!

From gitHub download everything into a folder of your choice

From there copy the contents of: DriveCRogJobCRMPlus to drive C

In there is this file:
RogJobCRMPlus_backup_2026_08_08_114233_0110965.bak

This is a backup with sample data retore that into SQL Server
Load the project and compile

**Note:** Requires .Net 4.72 or above


**Technical**

Uses the RogEngine for the GUI, a system, which provides facilities such as:

- login
- menu security
- automatic menu option creation
- automatic form control labelling
- automtatic main menu creation and option population
- custom theme (not implemented in this application but is in the RogStock series)
- SQL CRUD functions
- custom SQL error handler for use in stored procedures


Has an experimental custom SQL Server error handler which passes back a custom defined error value from
a stored procedure

Creating the Word documents was fun both CoPilot and chatGPT gave incorrect instructions regardins creating
paragraphs via automation StackOverFlow had working examples from the first forum page found!

Walsall Works prefer Word documents to PDFs that is the only reason I did it else would have been SSRS!

**Issues**

- Admin button appearing in the main menu as the RogEngine creates the main menu options from a SQL table
  and I forgot to delete the entry!