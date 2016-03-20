# EulerSolver
Project to streamline solving Eulers Problems in C#

This is a console application I wrote as a framework to help me solve Eulers problems without having to go through
a bunch of repetitive hassle. Problem solutions sit in Problem class files inside of the Problem folder. Each Solution is a Class
file that inherits from BaseProblem. You can create a Problem class file by using the CREATE command. You can customize your own
solutions. 

HOW TO USE:
Compile and run. These are the following command

Enter a #:
If a Problem class file has been added to the solution you can simply enter it's number to see the solution. The solver will run that Problem's implementation of Solve() and the answer will be output. If verbose mode is enabled you will see the answer as well as how many ticks it took to solve.

Create: 
Create a shell Problem class file. Type create 25 to create Problem25.cs. This file should be saved to the
Problem folder of the project. The default save location should be appropriate folder. Once the file has been
saved, close the executable and add the project class file to the project. You can now implement Solve()

Q
Quit: Exit the program.

L
Displays a list of all of the currently added Problem files.

C
Clear the screen.

D
Debug: Shows the debug log of the last problem that was ran.
d 25: See the Debug log for Problem 25's debug text file if it exists. Not all implementations of Solve() display something to the DebugLog() by default. You can add your own lines by adding DebugLogger.AddLine() anywhere in the Problem class files.

V
Toggle verbose mode.

Any questions/comments/suggestions/feedback can be sent to wentimo@gmail.com
