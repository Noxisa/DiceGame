# DiceGame
Task3 

Using the language of your choice—from the C#/JavaScript/TypeScript/Java/PHP/Ruby/Python/Rust set, please—to write a console script that implements a generalized non-transitive dice game (with the supports of arbitrary values on the dice). Of course, it's recommended to use the language of your "specialization," i.e. C# or JavaScript/TypeScript or PHP, but it's not required.

When launched with command line parameters—arguments to the main or Main method in the case of Java or C# correspondingly, sys.argv in Python, process.argv in Node.js, etc.—it accepts 3 or more strings, each containing 6 comma-separated integers. E.g., python game.py 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3. In principle, you may support any number of faces on a dice, like 4 or 20, but it's not very important

If the arguments are incorrect, you must display a neat error message,not a stacktrace—what exactly is wrong and an example of how to do it right (e.g., user specified only two dice or no dice at all, used non-integers, etc.). All messages should be in English.

mportant: dice configuration is passed as command line arguments; you don't "parse" it from the input stream.

The victory is defined as follows—computer and user select different dice, perform their "throws," and whoever rolls higher wins. 

The first step of the game is to determine who makes the first move. You have to prove to the user that choice is fair (it's not enough to generate a random bit 0 or 1; the user needs a proof of the fair play). 

When the users make the throw, they select dice using CLI "menu" and "generate" a random value with the help of the computer. The options consist of all the available dice, the exit (cancel) option, and the help option.

When the computer makes the throw, it selects dice and "generates" a random value. 

Of course, "random" generation is also should be provable fair. 
Please, note that the task is not "Implement some dice game". You need to implement all the specified requirements, including fair random generation, configurable dice, classes with limited responsibilities, etc.

To generate such a value, the computer generates a one-time cryptographically secure random key (using corresponding API like SecureRandom, RandomNumberGenerator, random_bytes, etc.—it's mandatory) with a length of at least 256 bits.

Then the computer generates a uniformly distributed integer in the required range (using secure random; note that % operator is not enough to get uniform distribution) and calculates HMAC (based on SHA3) from the generated integers as a message with the generated secret key. Then the computer displays the HMAC to the user. 

After that, the user selects an integer in the same range. The resulted value is calculated as the sum of user number and computer number using modular arithmetic. When the computer displays the result, it also shows the used secret keys.
