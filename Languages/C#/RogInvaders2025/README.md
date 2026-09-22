# <u>RogInvaders!</u>

## Classic game written in C#

Prepare yourself! 

Fight against the evil aliens in this 70s classic.

Just when you thought space was safe the *invaders* arrive! Their primary focus total domination,
humanity must go the way of the Dodo and only you stand in their evil way!


**Features**:

- 4 levels of difficulty
- music and sound effects composed by me
- graphics original retro designs
- different types of aliens!

**Install and Play Instructions**
- Copy the contents of the *DriveCRogInvaders_Folder* to Drive C
- Copy the project to your project folder
- Compile the project and run

*Movement* 
Key a = move left
Key d = move right
Key space = fire

**Note:** Needs .Net 4.72 or above to run


**Plans For Version 2**:

- high score table
- more aliens!
- ability to shoot the aliens shot
- multiple aliens fire 

**Challenges**:

Writing a game requires a whole new set of skills from writing a database back end,
desktop utility or CRM.

When does collision detection occur in T-SQL? When in an ERP system or backup program
do multiple asynchronous procedures occur, let alone just one need to be managed?

How often does a CRM need to fire at the user?

**Folders**

RogInvaders uses a folder on drive C which contains text, images and sounds, this is done
on purpose to allow possible external customisation by the player!

Folder structure:
<br>

C:\RogInvaders  
  - Images
  - Sounds
  - Text

<br>  
  
**Technical**:

Originally used Timers for movement and collision detection, then remebered these are
Window controls so have message handlers, and of course a complete class structure
stored in the memory. Not exactly efficient! So replace with asynchronous procedures,
which increased the speed of the game.

Only area I decided to keep the old timers was the alien invasion as there is no
strategic advantage to converting them as the entire procedure is self contained, plus
the slight drag caused by them being Windows controls added to the scene.

Purposely used MediaPlayer for the music as the .Net audio player cannot play multiple
audio files at the same time. That is if have two instances of the audio player and
player1 is playing say the background music, if player2 starts playing something
player1 stops!

Also noticed .Net does not support true bitmap transparency, as any images with that
look fine over the form, but as is best seen with the alien shot, once one image starts
to move over another the background of the second image becomes black and covers
the first image where it should be transparent!

Did a test to confirm this isn't what I am used to (has been many moons since I wrote
a game) and fired up Delphi and sure enough in that system setting the image to transparent
worked properly with the SAME images, no unwanted backgrounds.

Thanks Microsoft!

Needless to say future games requiring images to move over each other will either be
done in JavaScript/HTML/CSS because CSS does transparency correctly or I will write
it in Delphi/C-Builder.... 
