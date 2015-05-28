# MissionFixer
A mission fixer that fixes the root directory problems for interiors and moving platforms.

This code will fix missions that aren't compatible with mbp 1.14/1.20 because of directory settings. Moving platforms as well as interiors will work once upon doing this.

Note: DOING THE WHOLE ENTIRE CLA WILL CAUSE MARBLE BLAST TO CRASH, THUS NOT ALL LEVELS WILL GET FIXED.

# Usage (Tested on Windows):

To use, copy and paste that code into notepad/textedit, and save as missionFix.cs

Then, place the file into yourmarbleblastdirectory/marble/

or

yourmarbleblastdirectory/platinum/

then, open up Marble blast, and open the console (press ~)

in the console type this:

```
exec($usermods @ /missionFix.cs);
```

The code will compile. Afterwards, to check it, type this into the console:

```
fixMissions();
```

The code will do its thing, and it may take some time and even become unresponsive. Just let its do its thing. If it takes more than 5 minutes, then force quit marble blast and assume that it got most of the levels.

MBP LB USERS:

Afterwards, delete both the .cs and .dso file so that LBs will re-enable themselves in MBP.