## Overview
This C# Console Application Can Crack A TOTP From Just:
* Secret length (easy to find out from the target platform)
* 2 OTP codes and the time they were issued

## Example
1. Open the solution in visual studio and run ```dotnet restore```
2. Run the project
3. Enter ```344524``` as the first OTP
4. Enter ```1706572329``` as the first OTP time
5. Enter ```179396``` as the second OTP
6. Enter ```1706573415``` as the second OTP time
7. Enter ```5``` as the secret length
8. The application will emit three beeps once it is finished

This is a basic example which is has only a 5 charachter secret to speed things up.
The secret was ```k4ggq```
