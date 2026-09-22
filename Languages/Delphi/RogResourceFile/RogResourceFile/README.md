# <u>RogResource Creator</u>

**What It Does**

Creates a standalone resource file that can contain:
- .txt
- .wav
- .mp3
- .aiff
- .png
- .gif
- .jpeg
- .jpg

Very much inspired by the way Delphi does it, as .Net does not support a proper structured storage resource file!

Also included is the resource reader project which shows the contents of a resource file


## Technical

**File Format:**

Header:

[info]
    internal comments mot used by program
[/info]

sections:

[text]
    ;   <- start of item
    name: 
    controlname:
    length: <in bytes>
    <.....data stored in bytes....>
    /;
[/text]

[sound]
    ;   <- start of item
    name: 
    controlname:
    length: <in bytes>
    <.....data stored in bytes....>
    /;
[/sound]

[image]
    ;   <- start of item
    name: 
    controlname:
    length: <in bytes>
    <.....data stored in Color data type....>
    /;
[/image]


