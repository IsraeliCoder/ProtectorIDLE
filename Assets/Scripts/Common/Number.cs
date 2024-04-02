using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number
{

    public enum NumberShowFormat { eWords, eMath };

    public static string[] MoneyExtension = { "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Un-decillion", "Duo-decillion", "Tre-decillion", "Quattuor-decillion", "Quin-decillion", "Sex-decillion", "Septen-decillion", "Octo-decillion", "Novem-decillion", "Vigintillion", "Unvigintillion", "Duovigintillion", "Trevigintillion", "Quattuorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Tretrigintillion", "Quattuortigintillion", "Quintrigintillion", "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion", "Quadragintillion", "Unquadragintillion", "Duoquadragintillion", "Trequadragintillion", "Quattuorquadragintillion", "Quinquadragintillion", "Sexquadragintillion", "Septenquadragintillion", "Octoquadragintillion", "Novemquadragintillion", "Quinquagintillion", "Unquinquagintillion", "Duoquinquagintillion", "Trequinquagintillion", "Quattuorquinquagintillion", "Quinquinquagintillion", "Sexquinquagintillion", "Septenquinquagintillion", "Octoquinquagintillion", "Novemquinquagintillion", "Sexagintillion", "Unsexagintillion", "Duosexagintillion", "Tresexagintillion", "Quattuorsexagintillion", "Quinsexagintillion", "Sexsexagintillion", "Septensexagintillion", "Octosexagintillion", "Novemsexagintillion", "Septuagintillion", "Unseptuagintillion", "Duoseptuagintillion", "Treseptuagintillion", "Quattuorseptuagintillion", "Quinseptuagintillion", "Sexseptuagintillion", "Septenseptuagintillion", "Octoseptuagintillion", "Novemseptuagintillion", "Octogintillion", "Unoctogintillion", "Duooctogintillion", "Treoctogintillion", "Quattuoroctogintillion", "Quinoctogintillion", "Sexoctogintillion", "Septenoctogintillion", "Octooctogintillion", "Novemoctogintillion", "Nonagintillion", "Unnonagintillion", "Duononagintillion", "Trenonagintillion", "Quattuornonagintillion", "Quinnonagintillion", "Sexnonagintillion", "Septennonagintillion", "Octononagintillion", "Novemnonagintillion", "Centillion", "Centillion", "Uncentillion", "Uncentillion"};

    public int MainNumber;
    public int SubNumber;
    public int Power;

    public Number() : this(0,0,0)
    {
    }

    public Number(Number other) : this(other.Power, other.MainNumber, other.SubNumber)
    {
    }

    public Number(int power, int mainNumber, int subNumber)
    {
        Power = power;
        MainNumber = mainNumber;
        SubNumber = subNumber;
    }

    public Number Add(Number other)
    {


        if (other.BiggerOrEqual(this))
            return other.Add(this);

        if (Power > other.Power + 3)
        {
            return new Number(this);
        }
        else if (Power == other.Power + 3)
        {
            other = new Number(Power, 0, other.MainNumber);
        }

        Number toReturn = new Number(Power, MainNumber + other.MainNumber, SubNumber + other.SubNumber);

        toReturn.Clean();

        return toReturn;

    }

    public void Clean()
    {

        if (SubNumber > 999)
        {
            MainNumber += SubNumber / 1000;
            SubNumber %= 1000;
        }

        while (MainNumber > 999)
        {
            SubNumber = MainNumber % 1000;
            MainNumber /= 1000;
            Power += 3;
        }


        if (Power != 0)
        {
            if (MainNumber == 0 && SubNumber != 0)
            {
                Power -= 3;
                MainNumber = SubNumber;
                SubNumber = 0;
            }
        }


    }

    public Number Multiply(Number other)
    {

        if (BiggerOrEqual(other))
            return other.Multiply(this);

        // 6.25 * 0.55
        int newPower = Power + other.Power;
        Number smallSmall = new Number(newPower, 0, SubNumber * other.SubNumber / 1000);
        Number smallBig = new Number(newPower, SubNumber * other.MainNumber / 1000, (SubNumber * other.MainNumber) % 1000);
        Number bigSmall = new Number(newPower, MainNumber * other.SubNumber / 1000, (MainNumber * other.SubNumber) % 1000);
        Number bigBig = new Number(newPower, MainNumber * other.MainNumber, 0);

        Number toReturn = smallSmall.Add(smallBig).Add(bigSmall).Add(bigBig);
        toReturn.Clean();
        return toReturn;
    }

    public bool BiggerOrEqual(Number other)
    {

        if (Power == other.Power)
        {
            if (MainNumber == other.MainNumber)
            {

                return SubNumber >= other.SubNumber;

            }

            return MainNumber > other.MainNumber;
        }

        return Power > other.Power;
       
    }

    public Number Reduce(Number other)
    {

        // We are not supporting negative and never expecting it.
        if (other.BiggerOrEqual(this))
            return this;

        Number toReturn;

        if (Power == other.Power)
        {
            toReturn = new Number(Power, MainNumber - other.MainNumber, SubNumber - other.SubNumber);
        }
        else if (Power == other.Power + 3)
        {
            toReturn = new Number(Power, MainNumber, SubNumber - other.MainNumber);
        }
        else
        {
            return new Number(this);
        }

        if (toReturn.SubNumber < 0)
        {
            toReturn.SubNumber += 1000;
            toReturn.MainNumber -= 1;
        }

        if (toReturn.MainNumber == 0 && toReturn.Power > 0)
        {
            toReturn.MainNumber = toReturn.SubNumber;
            toReturn.SubNumber = 0;
            toReturn.Power -= 3;
        }


        return toReturn;

    }

    public string getRawNumberAsString()
    {

        string big = "" + MainNumber;
        string small = "" + SubNumber;
        while (small.Length < 3)
            small = "0" + small;
        return big + "." + (small.Substring(0, (big.Length < 3 ? 2 : 1)));

    }

    public string getString(NumberShowFormat format=NumberShowFormat.eMath)
    {

        Clean();

        if (format == NumberShowFormat.eMath)
        {
            return getRawNumberAsString() + " x 10 ^ " + (Power);
        }
        else
        {
            return getRawNumberAsString() + " " + MoneyExtension[Power / 3];
        }

    }

    public float getRealNumber()
    {
        float toReturn = float.Parse(getRawNumberAsString());
        return toReturn * Mathf.Pow(10, Power);
    }

}
