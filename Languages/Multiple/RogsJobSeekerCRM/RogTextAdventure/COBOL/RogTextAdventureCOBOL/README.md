# <u>RogsTextAdventure - COBOL Edition!</u>

**Original Remit:**
Create a simple 80s style text adventure that runs in a console, then translate it into as many console supporting languages as possible to show my flexibility and knowledge
of programming languages.

Originally done in VB .NET it was converted too:
- C#
- Delphi
- COBOL (!!)
- JavaScript (!!)

As a tech demo it is currently limited to movement only via simple commands such as: 
move north

Required some utilities to be written especially for converting the level file into a COBOL
friendly format.

This was the hardest to do in many ways as .Net concepts like "lists" do not exist, classes are not fully suppored in COBOL (for security reasons) and not supported at all by OpenCOBOLIDe as it uses the 2012 standard!

Also COBOL like Python has no native GUI as it runs usually on mainframes which made thing more interesting..

Was a nice challenge :)

*Future Changes* 

Currently has a **very** primtive parser needs to handle more commands other than movement directions
Also would be peachy to have objects in the level..  

**How To Play**

Navigate around the maze using movement commands e.g. move north
Available directions are listed in each room (if there are any)
Type: **help** to see a list of available commands

Cany you escape the maze before you experience a blue screen of death?