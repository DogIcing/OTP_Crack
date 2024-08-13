using OTP_Crack;
using OtpNet;
using System.Collections.Generic;
using System.Diagnostics;
string[] chars = {"2", "3", "4", "5", "6", "7", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
long count = 0;
int match = 0;
long oldtimestamp = 0;
Console.WriteLine("First OTP:");
string FirstOtp = Console.ReadLine();
Console.WriteLine("First OTP Unix Timestamp (seconds):");
long FirstOtpTime = Convert.ToInt64(Console.ReadLine());
Console.WriteLine("Second OTP:");
string SecondOtp = Console.ReadLine();
Console.WriteLine("Second OTP Unix Timestamp (seconds):");
long SecondOtpTime = Convert.ToInt64(Console.ReadLine());
Console.WriteLine("OTP Secret Length (generally 16 chars):");
int length = Convert.ToInt32(Console.ReadLine());
List<Codes> output = new List<Codes>();
List<Codes> finalisedOutput = new List<Codes>();

Console.Beep();
nextChar("");

void nextChar(string current)
{
    if (current.Length == length)
    {
        if (count % 1000000 == 0)
        {
            Console.Clear();
            Console.WriteLine((count / Math.Pow(32, length)) * 100 + "% complete | " + Convert.ToString((((Math.Pow(32, length)) - count) / 1000000) * (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - oldtimestamp)) + " seconds remaining | " + count + "/" + Math.Pow(32, length).ToString() + " possibilities calculated | " + current + " is the current combination");
            oldtimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        var bytes = Base32Encoding.ToBytes(current);

        var totp = new Totp(bytes, step: 30);

        if (totp.ComputeTotp(DateTimeOffset.FromUnixTimeSeconds(FirstOtpTime).DateTime) == FirstOtp)
        {
            output.Add(new Codes { secret = current });
        }
        count++;
    }
    else
    {
        for (int i = 0; i < chars.Length; i++)
        {
            nextChar(current + chars[i]);
        }
    }
}
Console.Clear();
Console.WriteLine("---------------------------------------------------");
Console.WriteLine("Count: " + count);
Console.WriteLine("---------------------------------------------------");
Console.WriteLine("Secret Found:");
for (int i = 0;i < output.Count;i++)
{
    var bytes = Base32Encoding.ToBytes(output[i].secret);

    var totp = new Totp(bytes, step: 30);

    if (totp.ComputeTotp(DateTimeOffset.FromUnixTimeSeconds(SecondOtpTime).DateTime) == SecondOtp)
    {
        finalisedOutput.Add(new Codes { secret = output[i].secret });
    }
}
Console.Beep();
Console.Beep();
Console.Beep();

finalisedOutput.ForEach(x => Console.WriteLine(x.secret));
Console.ReadLine();
