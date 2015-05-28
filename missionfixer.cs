//-----------------------------------------------------------------------------
// Copyright (c) 2013-2015 Jeff Hutchinson
// Copyright (c) 2013-2015 The Platinum Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------
 
function fixMissions()
{
   echo("Fixing missions in progress.");
   echo("It might take a while depending on how many mission files there are.");
   echo("This will ignore leaderboard missions.");
   echo("REMEMBER: when you are finished fixing, remove missionFix.cs and missionFix.cs.dso");
   echo("          so that the anti hack will stop in the leaderboards.");
   
   // in 50 milliseconds, we will start.  Give the system time to show the echo.
   schedule(50, 0, fixThoseMissions);
}
 
function fixThoseMissions()
{
   // input and output streams
   %fIn  = new FileObject();
   %fOut = new FileObject();    
   
   // Itterate through all of the mission files.
   %search = "*.mis";
   %file = findFirstFile(%search);
   while (%file !$= "")
   {
      // ignore Lb files
      if (strStr(%file, "lbmissions") != -1 || strStr(%file, "lb_custom") != -1)
         continue;      
     
      // grab the contents of every line in the file so that we can rewrite it
      if (%fIn.openForRead(%file))
      {
         %count = 0;
         while (!%fIn.isEOF())
         {
            // Jeff: parse the line
            %currLine = %fIn.readLine();
           
            // Jeff: replace occurrences of marble/data | platinum/data | ~
            // with $usermods @ "/data
            %firstWord = firstWord(trim(%currLine));
            if (%firstWord $= "interiorFile" || %firstWord $= "interiorResource")
            {
               %currLine = strreplace(%currLine, "\"marble/data", "$usermods @ \"/data");
               %currLine = strreplace(%currLine, "\"platinum/data", "$usermods @ \"/data");
               %currLine = strreplace(%currLine, "\"~/data", "$usermods @ \"/data");
                           
                           // this will auto correct any errors
                           %currLine = strreplace(%currLine, "\"$usermods @ \"/data", "$usermods @ \"/data");
            }
           
            %line[%count] = %currLine;
            %count ++;
         }
      }
      %fIn.close();
     
      // now we write it out!
      if (%fOut.openForWrite(%file))
      {
         for (%i = 0; %i < %count; %i ++)
            %fOut.writeLine(%line[%i]);
      }
      %fOut.close();
     
      %file = findNextFile(%search);
   }
   
   echo("DONE!  Please restart marble blast.");
}