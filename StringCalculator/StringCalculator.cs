using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class StringCalculator
    {

       static void Main(string[] args)
        {
            Console.WriteLine(Add(""));
            Console.WriteLine(Add("1,2,5"));
            Console.WriteLine(Add("1\n,2,3"));
            Console.WriteLine(Add("1,\n2,4"));
            Console.WriteLine(Add("//;\n1;\n3;4"));
            Console.WriteLine(Add("//$\n1$2$3"));
            Console.WriteLine(Add("//@\n2@3@8"));
            Console.WriteLine(Add("//$$\n5$$\n22$$3\n$$99$$1001"));
            Console.WriteLine(Add("1,2,\n3"));
            Console.WriteLine(Add("//,\n1,2,3"));
           // Console.WriteLine(Add("//$$,##\n5$$\n-22##3\n$$-99$$1001"));
        }

        /**
         * This function will take in a string and return a sum of all positive numbers in the string separated by a delimiter.
         * if no custom delmiter is specified, it will split by a comma.
         * To use a custom delimiter the string must be formatted as follows:
         * double forward slashes followed by the delimiters of choice. ( if more than one, the delimiters are separated by commas)
         * followed by a \n. all numbers after that will be summed and the value will be returned.
         * Negative numbers are not allowed and will throw an exception.
         * the string will ignore new line characters.
         * example string: //$$\n1$$23$$10 would return 44.
         *                 //,\n1,2,\n3 would return 6
         *                 //,\n5,-10,5 would throw an exception.
         * */
        static public int Add(string numbers)
        {
            //string array to hold the number values which will be evaluated below.
            string[] nums = null;
            //return variable used to store the sum of the values.
            int sum = 0;

            //string used to store the negative numbers which will be used when throwing an exception.
            string invalidNums = "";

            //checks to see if its an empty string. will return zero if it is.
            if (numbers.Length == 0)
            {
                return 0;
            }

            //checks to see if the string is using custom delimiters.
            if (numbers.Substring(0,2) == "//")
            {
                //a string array of the delimiters used to split the string. as it appears that when there are multiple delimiters, they are split with a comma
                //we pass into the Split function, a sub string of the passed in numbers string removing the // and going until the first occurence of a new line.
                //the split will then create an array of strings for each delimiter.
                string[] delim = numbers.Substring(2, numbers.IndexOf('\n') - 2).Split(',');

                //we want to remove the delimiter metaData from the string before we start split and analyze it. so we substring it without the metaData.
                string removedDelims = numbers.Substring(numbers.IndexOf('\n'));

                //we create a string array of values which are split on the delimiters from the delim array.
                 nums = removedDelims.Split(delim, StringSplitOptions.None);
            }
            //the string is not using custom delmiteres so we split on a comma.
            else
            {
                //splits the numbers string into an array of numbers split on a comma.
                nums = numbers.Split(',');
            }

            //a foreach loop which goes through every string in the array.
            foreach (string currentString in nums)
            {
                //if the current string in the array contains a -, we know its a negative.
                if (currentString.Contains('-'))
                {
                    //incase there are multiple negatives in the numbers string. instead of throwing an exception at the first occurence of a negative number
                    //I add the number to a string, which whill be used to show a list of negative numbers when the exception is thrown.
                    //kinda silly parsing an int from a string then turning it back into a string, something id play around with more.
                    invalidNums += "-" + int.Parse(new String(currentString.Where(Char.IsDigit).ToArray())).ToString() + ", ";
                }
                else
                {
                   //We use LINQ to allow us to filter the current string and only add characters that are digits which are passed into an array which are then turned
                   //into a string which we call int.Parse which grabs the value from the string and transforms it into an int.
                    int parsedInt = int.Parse(new String(currentString.Where(Char.IsDigit).ToArray()));
                    //if the value is greater then 0, we add 0 or ignore it, else we add it to the sum value.
                    sum += parsedInt < 1000 ? parsedInt : 0;
                }
            }
            //if the length of invalidNums is zero, the string contained no negative numbers and we are safe to return the sum, if the length of invalid nums
            //is greater than 0, we have added negative numbers to the string. and will throw an exception listing off all the negative numbers found in the string.
            return invalidNums.Length == 0 ? sum : throw new Exception("Negatives not allowed\nThe invalid numbers were " + invalidNums);
        }


    }
}
