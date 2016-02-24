# EulerSolver
Personal Project to solve (at least) the first 50 Eulers Problems in C#

This is a console application I wrote as a framework to help me solve Eulers problems without having to go through
a bunch of repetitive hassle. All you have to do to add or remove already existing 'solution's is to go into the 
Problems folder of the project. Each file represents the solution to a eulers problem. Problem1.cs is the solution
for Eulers Problem #1. You can delete everything here except BaseProblem.cs and add your own Solutions via
the create command on the console application. Alternatively you can just look at the required structure from 
ProblemShell.cs in the Resources folder and add it however it however you want.

After solving a problem be sure to swap the abstract property HasBeenSolved to true.

HOW TO USE:
Compile and run. These are the following options:

Enter a #:
1 For example. Just type in 1 and it will solve Euler Problem #1 (assuming it's been solved and added). 
The output comes in a brief and verbose mode.

Create: 
Create a shell Problem class file. Type create, 25 to create Problem25.cs. This file should be saved to the
Problem folder of the project. The default save location should be appropriate folder. Once the file has been
saved, close the executable and add the project class file to the project. You can now write your solution
in the Problem25.cs file between the Initialize() and Finalize() methods.

Q
Quit: Exit the program.

L
Displays a list of all of the currently added Problem files.

C
Clear the screen.

D
Debug: Shows the debug log of the last problem that was ran.

V
Verbose: Active or deactivate verbose mode.
