using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Text;
using System;
using System.Runtime.InteropServices;
using Random = System.Random;
using Debug = UnityEngine.Debug;


public class sccscrypto : MonoBehaviour {

    static char a = 'a';
    static char b = 'b';
    static char c = 'c';
    static char d = 'd';
    static char e = 'e';
    static char f = 'f';


    static int[] _numbers = new int[]
    {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9
    };

    static string[] bitletters = new string[]
    {
            "1010",
            "1011",
            "1100",
            "1101",
            "1110",
            "1111",
    };


    static char[] charArray = new char[]
    {
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
    };

    static string[] bitNumbers = new string[]
    {
            "0000",
            "0001",
            "0010",
            "0011",
            "0100",
            "0101",
            "0110",
            "0111",
            "1000",
            "1001",
    };

    static string[] bitTotalZeroToNineAndAToF = new string[]
    {
            "0000", // 0
            "0001", // 1
            "0010", // 2
            "0011", // 3
            "0100", // 4
            "0101", // 5
            "0110", // 6
            "0111", // 7
            "1000", // 8
            "1001", // 9
            "1010", // a
            "1011", // b
            "1100", // c
            "1101", // d
            "1110", // e
            "1111", // f
    };


    static string[] bitTotal0To35 = new string[]
{
            "0000", //0
            "0001", //1
            "0010", //2
            "0011", //3
            "0100", //4
            "0101", //5
            "0110", //6
            "0111", //7
            "1000", //8
            "1001", //9
            "1010", //10
            "1011", //11
            "1100", //12
            "1101", //13
            "1110", //14
            "1111", //15
            "10000", //16
            "10001", //17
            "10010", //18
            "10011", //19
            "10100", //20
            "10101", //21
            "10110", //22
            "10111", //23
            "11000", //24
            "11001", //25
            "11010", //26
            "11011", //27
            "11100", //28
            "11101", //29
            "11110", //30
            "11111", //31
            "100000", //32
            "100001", //33
            "100010", //34
            "100011", //35
};






    static string[] bitTotal0To15 = new string[]
    {
                "0000", //0
                "0001", //1
                "0010", //2
                "0011", //3
                "0100", //4
                "0101", //5
                "0110", //6
                "0111", //7
                "1000", //8
                "1001", //9
                "1010", //10
                "1011", //11
                "1100", //12
                "1101", //13
                "1110", //14
                "1111", //15
    };









    static Stopwatch stopWatch = new Stopwatch();

    static string[] arrayOfConstantWhatever = new string[8];
    static int minSeparator = 0;
    static int maxSeparator = 7;
    static int length = 8;
    static int index = 0;



    static string majOne = "";
    static int zeros = 0;
    static int ones = 0;
    static int aa = 0;
    static bool reset = true;
    static int iOfC = 0;



    // Use this for initialization
    void Start ()
    {  
        //THISWASNONDISCARDED
        //THISWASNONDISCARDED
        //THISWASNONDISCARDED
        //NUMBER ONE FOR THE MOMENT // well I already got an increase of 10000% performance and its still super slow.
        //IntPtr test = Marshal.UnsafeAddrOfPinnedArrayElement(Databyte, 0);
        //byte* memBytePtr = (byte*)test.ToPointer();
        //UnmanagedMemoryStream unmanagedmemstream = new UnmanagedMemoryStream(memBytePtr, Databyte.Length, Databyte.Length, FileAccess.Write);
        //SHA256 hashAlg = new SHA256Managed();
        //CryptoStream cs = new CryptoStream(unmanagedmemstream, hashAlg, CryptoStreamMode.Write);
        //cs.FlushFinalBlock();
        //ScryptResult = hashAlg.Hash;
        //DeleteObject(test);
        //unmanagedmemstream.Close();
        //cs.Close();
        //THISWASNONDISCARDED
        //THISWASNONDISCARDED
        //THISWASNONDISCARDED
    }

    // Update is called once per frame
    void Update ()
    {

    }


    public void sccscryptoworktesting()
    {
        //---------RANDOM PASSWORD----------
        Random _rand = new Random();
        string password = "20000000";
        /*Random _random0 = new Random();
        Random _random1 = new Random();
        for (int i = 0; i < 56; i++)
        {
            int letterOrDigit = _rand.Next(0, 2);

            if (letterOrDigit == 0) // 
            {
                int num0 = _random0.Next(0, 5);
                char let = (char)('a' + num0);
                password += let;
            }
            else
            {
                int num1 = _random1.Next(0, 9);
                password += num1;
            }
        }*/
        //---------------------------------
        //ON AWAKE OF SHOOT OR HIT OR WHATNOT MAYBE, DO A SHA256 to test performance. and then test the one below and than instance shaders of sha256 until it works and that it is able to mine bitcoins
        //this way. gotta go smoke a cigarette. brb.
        //string sha256password = scsha256(password);
        //Debug.Log("before: " + password + " after: " + sha256password);
    }

    //https://www.youtube.com/watch?v=y3dqhixzGVo
    public string scsha256(string password) //object sender, DoWorkEventArgs e
    {
        /*//---------RANDOM PASSWORD----------
        Random _rand = new Random();
        string password = "20000000";
        Random _random0 = new Random();
        Random _random1 = new Random();
        for (int i = 0; i < 56; i++)
        {
            int letterOrDigit = _rand.Next(0, 2);

            if (letterOrDigit == 0) // 
            {
                int num0 = _random0.Next(0, 5);
                char let = (char)('a' + num0);
                password += let;
            }
            else
            {
                int num1 = _random1.Next(0, 9);
                password += num1;
            }
        }*/
        //---------------------------------
        

        //--------------SPLITTING THE PASSWORD INTO 8 IDENTICAL PARTS-------------------
        string[] arrayOfConstantWhatever = new string[8];
        int minSeparator = 0;
        int maxSeparator = 7;
        int length = 8;
        int index = 0;
        for (int c = 0; c < 8; c++)
        {
            string totalPass = password;
            arrayOfConstantWhatever[index] = totalPass.Substring(minSeparator, length);
            minSeparator += length;
            index++;
        }
        //------------------------------------------------------------------------------



        //----------------CONVERTING THE 8 PARTS FROM DIGITS/NUMBERS TO 4 BIT BINARY-------
        //arrayOfConstantWhatever = THE 8 PARTS FROM DIGITS/NUMBERS
        //arrayOfToBitConstantWhatever = THE 8 PARTS CONVERTED TO 4 BIT BINARY
        index = 0;
        string[] arrayOfToBitConstantWhatever = new string[8];
        for (int w = 0; w < arrayOfConstantWhatever.Length; w++)
        {
            for (int c = 0; c < arrayOfConstantWhatever[w].Length; c++)
            {



                for (int l = 0; l < charArray.Length; l++)
                {
                    if (arrayOfConstantWhatever[w][c] == charArray[l])
                    {
                        arrayOfToBitConstantWhatever[w] += bitletters[l];
                    }
                }


                for (int l = 0; l < _numbers.Length; l++)
                {
                    if (Char.GetNumericValue(arrayOfConstantWhatever[w][c]) == _numbers[l])
                    {
                        arrayOfToBitConstantWhatever[w] += bitNumbers[l];
                    }
                }


                /*bool isLetter = char.IsLetter(arrayOfConstantWhatever[w][c]);

                bool isDigit = char.IsDigit(arrayOfConstantWhatever[w][c]);
                if (isLetter)
                {
                    for (int l = 0; l < charArray.Length; l++)
                    {
                        if (arrayOfConstantWhatever[w][c] == charArray[l])
                        {
                            arrayOfToBitConstantWhatever[w] += bitletters[l];
                        }
                    }
                }
                if (isDigit)
                {
                    for (int l = 0; l < _numbers.Length; l++)
                    {
                        if (Char.GetNumericValue(arrayOfConstantWhatever[w][c]) == _numbers[l])
                        {
                            arrayOfToBitConstantWhatever[w] += bitNumbers[l];
                        }
                    }
                }*/
            }
        }
        //------------------------------------------------------------------------------


        //CALCULATING THE MAJORITY FROM THE FIRST 3 PARTS SO A,B AND C AND LEAVE BEHIND D,E,F,G,H FOR THE MOMENT
        string majOne = "";
        int zeros = 0;
        int ones = 0;
        int aa = 0;
        bool reset = true;
        int iOfC = 0;
        while (true)// only A B AND C
        {
            if (iOfC >= 32)
            {
                break;
            }
            if (reset)
            {
                zeros = 0;
                ones = 0;
                aa = 0;
                reset = false;
            }
            //Console.WriteLine(aa);
            int value = (int)Char.GetNumericValue(arrayOfToBitConstantWhatever[aa][iOfC]);

            if (value == 0)
            {
                zeros++;
            }
            else
            {
                ones++;
            }

            if (aa < 2)
            {
                aa++;
            }
            else
            {
                if (iOfC < 32)
                {
                    reset = true;
                    if (zeros > ones)
                    {
                        majOne += "0";
                    }
                    else
                    {
                        majOne += "1";
                    }
                    iOfC++;
                }
                else
                {
                    break;
                }
            }

        }
        //Console.WriteLine(majOne); //Console.WriteLine(majOne.Length);
        //Console.WriteLine(majOne.Length);
        //------------------------------------------------------------------------------



        //------------------CONVERT MAJ BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfMajOne = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = majOne;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfMajOne += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfMajOne += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfMajOne);
        //------------------------------------------------------------------------------




        //----------------SHIFTING BITS TO 2 THEN 13 THEN 22 TO THE RIGHT---------------
        string A = arrayOfToBitConstantWhatever[0];
        var end2 = A.Substring(A.Length - 2, 2);
        var begin2 = A.Substring(0, A.Length - 2);
        string shift2bitsToTheRight = end2 + begin2;

        var end13 = A.Substring(A.Length - 13, 13);
        var begin13 = A.Substring(0, A.Length - 13);
        string shift13bitsToTheRight = end13 + begin13;

        var end22 = A.Substring(A.Length - 22, 22);
        var begin22 = A.Substring(0, A.Length - 22);
        string shift22bitsToTheRight = end22 + begin22;

        string[] shiftersA = new string[]
        {
                shift2bitsToTheRight,
                shift13bitsToTheRight,
                shift22bitsToTheRight
        };
        //Console.WriteLine(shift2bitsToTheRight.Length + " _ " + shift13bitsToTheRight.Length + " _ " + shift22bitsToTheRight.Length);
        //------------------------------------------------------------------------------



        //--------------CALCULATING THE XOR (ODD OR EVEN)-------------------------------
        string AXORODDOREVEN = "";
        zeros = 0;
        ones = 0;
        aa = 0;
        iOfC = 0;
        reset = true;

        while (true)// only A
        {
            if (iOfC >= 32)
            {
                break;
            }
            if (reset)
            {
                zeros = 0;
                ones = 0;
                aa = 0;
                reset = false;
            }

            int value = (int)Char.GetNumericValue(shiftersA[aa][iOfC]);

            if (value == 0)
            {
                zeros++;
            }
            else
            {
                ones++;
            }

            if (aa < 2)
            {

                aa++;
            }
            else
            {
                if (iOfC < 32)
                {
                    reset = true;

                    var oddOrEven = ones & 1; // even == 0 and odd = 1

                    //Console.WriteLine(ones + " _ " + oddOrEven);
                    if (oddOrEven == 0)
                    {
                        AXORODDOREVEN += "0";
                    }
                    else
                    {
                        AXORODDOREVEN += "1";
                    }

                    iOfC++;
                }
                else
                {
                    break;
                }
            }
        }
        //------------------------------------------------------------------------------


        //------------------CONVERT AXORODDOREVEN BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfAXORODDOREVEN = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = AXORODDOREVEN;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfAXORODDOREVEN += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfAXORODDOREVEN += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfAXORODDOREVEN);
        //------------------------------------------------------------------------------


        //-----------THE CHOSING WITH E, F AND G---------------------------------------- LEAVING BEHIND A,B,C,D,H
        string THECHOSING = "";
        aa = 4;
        iOfC = 0;
        reset = true;

        while (true)// only A
        {
            if (iOfC >= 32)
            {
                break;
            }
            //Console.WriteLine(aa + " _ " + iOfC);

            int value = (int)Char.GetNumericValue(arrayOfToBitConstantWhatever[aa][iOfC]);

            if (value == 0)
            {
                THECHOSING += arrayOfToBitConstantWhatever[6][iOfC];
            }
            else
            {
                THECHOSING += arrayOfToBitConstantWhatever[5][iOfC];
            }

            if (iOfC < 32)
            {
                reset = true;
                iOfC++;
            }
            else
            {
                break;
            }
        }
        //Console.WriteLine(THECHOSING);
        //------------------------------------------------------------------------------



        //------------------CONVERT CHOSING BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfTHECHOSING = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = THECHOSING;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfTHECHOSING += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfTHECHOSING += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfTHECHOSING);
        //------------------------------------------------------------------------------


        //----------------SHIFTING BITS TO 6 THEN 11 THEN 25 TO THE RIGHT---------------
        string E = arrayOfToBitConstantWhatever[4];
        var end6 = E.Substring(E.Length - 6, 6);
        var begin6 = E.Substring(0, E.Length - 6);
        string shift6bitsToTheRight = end6 + begin6;

        var end11 = E.Substring(E.Length - 11, 11);
        var begin11 = E.Substring(0, E.Length - 11);
        string shift11bitsToTheRight = end11 + begin11;

        var end25 = E.Substring(E.Length - 25, 25);
        var begin25 = E.Substring(0, E.Length - 25);
        string shift25bitsToTheRight = end25 + begin25;

        string[] shiftersE = new string[]
        {
                shift6bitsToTheRight,
                shift11bitsToTheRight,
                shift25bitsToTheRight
        };
        //------------------------------------------------------------------------------




        //-------------ADDITION SHIFTERSE BINARIES AND MODULO 2-------------------------
        string totalOfBitAddition = "";
        aa = 0;
        iOfC = 0;
        reset = true;
        minSeparator = 0;
        length = 4;

        string firstBinary = "";
        string secondBinary = "";
        string thirdBinary = "";

        while (true) //only A
        {
            if (minSeparator > shiftersE[0].Length - 4)
            {
                break;
            }

            string totalString = shiftersE[aa];
            string tempString = totalString.Substring(minSeparator, length);

            if (aa < 2)
            {
                if (aa == 0)
                {
                    firstBinary = tempString;
                }
                else if (aa == 1)
                {
                    secondBinary = tempString;
                }
                aa++;
            }
            else
            {

                thirdBinary = tempString;

                int firster = 0;
                int seconder = 0;
                int thirder = 0;

                for (int i = 0; i < bitTotal0To15.Length; i++)
                {
                    if (bitTotal0To15[i] == firstBinary)
                    {
                        firster = i;
                    }
                    else if (bitTotal0To15[i] == secondBinary)
                    {
                        seconder = i;
                    }
                    else if (bitTotal0To15[i] == thirdBinary)
                    {
                        thirder = i;
                    }
                }

                var total = firster + seconder + thirder;
                var modulo = total % 2; // TO REVERIFY**********************************************************************************************************************************************************

                totalOfBitAddition += bitTotalZeroToNineAndAToF[modulo];


                minSeparator += length;

                if (minSeparator > shiftersE[0].Length - 4)
                {
                    break;
                }
            }
        }
        //Console.WriteLine(totalOfBitAddition);
        //------------------------------------------------------------------------------


        string[] totalShiftersAndtotalOfBitAddition = new string[]
        {
                    shiftersE[0],
                    shiftersE[1],
                    shiftersE[2],
                    totalOfBitAddition
        };

        //--------------CALCULATING THE XORTWO (ODD OR EVEN)-------------------------------
        string totalOfBitAdditionAndshiftersEXORODDOREVEN = "";
        zeros = 0;
        ones = 0;
        aa = 0;
        iOfC = 0;
        reset = true;

        while (true)// only A
        {
            if (iOfC >= 32)
            {
                break;
            }
            if (reset)
            {
                zeros = 0;
                ones = 0;
                aa = 0;
                reset = false;
            }

            int value = (int)Char.GetNumericValue(totalShiftersAndtotalOfBitAddition[aa][iOfC]);

            if (value == 0)
            {
                zeros++;
            }
            else
            {
                ones++;
            }

            if (aa < 3)
            {

                aa++;
            }
            else
            {
                if (iOfC < 32)
                {
                    reset = true;

                    var oddOrEven = ones & 1; // even == 0 and odd = 1

                    //Console.WriteLine(ones + " _ " + oddOrEven);
                    if (oddOrEven == 0)
                    {
                        totalOfBitAdditionAndshiftersEXORODDOREVEN += "0";
                    }
                    else
                    {
                        totalOfBitAdditionAndshiftersEXORODDOREVEN += "1";
                    }

                    iOfC++;
                }
                else
                {
                    break;
                }
            }
        }
        //------------------------------------------------------------------------------


        //------------------CONVERT XORTWO BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfXORTWOorSummationOfE = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = totalOfBitAdditionAndshiftersEXORODDOREVEN;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfXORTWOorSummationOfE += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfXORTWOorSummationOfE += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfXORTWO);
        //------------------------------------------------------------------------------

        string wConst = "02000000";
        string kConst = "428a2f98";
        string hWhatever = arrayOfConstantWhatever[7];
        string chosing = endOfTHECHOSING;
        string eSummation = endOfXORTWOorSummationOfE;


        string[] arrayOfHexSummation = new string[]
        {
                wConst,
                kConst,
                hWhatever,
                chosing,
                eSummation
        };

        /*for (int i = 0; i < arrayOfHexSummation.Length; i++)
        {
            Console.WriteLine(arrayOfHexSummation[i]);
        }*/


        //---------------THE HEX SUMMATION---------------------------------------------
        string theHEXSummation = "";
        aa = 0;
        iOfC = 0;
        reset = true;

        int hexSum = 0;
        int leftOverCarry = 0;

        while (true)// only A
        {
            if (iOfC >= 8)
            {
                break;
            }

            if (aa >= 5)
            {
                hexSum = 0;
                aa = 0;
            }

            bool isLetter = char.IsLetter(arrayOfHexSummation[aa][iOfC]);
            bool isDigit = char.IsDigit(arrayOfHexSummation[aa][iOfC]);




            for (int l = 0; l < charArray.Length; l++)
            {
                if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                {
                    var indexTot = 10 + l;
                    hexSum += indexTot;
                }
            }


            for (int l = 0; l < _numbers.Length; l++)
            {
                if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                {
                    hexSum += l;
                }
            }



            /*if (isLetter)
            {
                for (int l = 0; l < charArray.Length; l++)
                {
                    if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                    {
                        var indexTot = 10 + l;
                        hexSum += indexTot;
                    }
                }
            }
            if (isDigit)
            {
                for (int l = 0; l < _numbers.Length; l++)
                {
                    if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                    {
                        hexSum += l;
                    }
                }
            }*/
            aa++;

            if (aa == 5)
            {
                hexSum += leftOverCarry;

                if (hexSum < 16)
                {
                    theHEXSummation += bitTotalZeroToNineAndAToF[hexSum];
                }
                else
                {
                    int total16 = 0;
                    for (int add = 0; add < hexSum; add++)
                    {
                        if (add % 16 == 0 && add != 0)
                        {
                            total16++;
                        }
                    }
                    int modulo = hexSum % 16;

                    if (total16 != 0)
                    {
                        leftOverCarry = total16;
                    }
                    theHEXSummation += bitTotalZeroToNineAndAToF[modulo];
                }
                if (iOfC < 8)
                {
                    iOfC++;
                }
                else
                {
                    break;
                }
            }
        }
        //Console.WriteLine(theHEXSummation);
        //------------------------------------------------------------------------------

        //------------------CONVERT HEXSUMMATION BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfHEXSUMMATION = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = theHEXSummation;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfHEXSUMMATION += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfHEXSUMMATION += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfHEXSUMMATION);
        //------------------------------------------------------------------------------

        //endOfAXORODDOREVEN
        //endOfMajOne
        //endOfHEXSUMMATION




        //-----------HEX SUMMATION TWO-------------------------------------------------
        string[] arrayOfHexSummationTWO = new string[]
        {
                endOfAXORODDOREVEN,
                endOfMajOne,
                endOfHEXSUMMATION,
        };



        string theHEXSummationTWO = "";
        aa = 0;
        iOfC = 0;
        reset = true;

        int hexSumTWO = 0;
        int leftOverCarryTWO = 0;

        while (true)// only A
        {
            if (iOfC >= 8)
            {
                break;
            }

            if (aa >= 3)
            {
                hexSumTWO = 0;
                aa = 0;
            }








            for (int l = 0; l < charArray.Length; l++)
            {
                if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                {
                    var indexTot = 10 + l;
                    hexSumTWO += indexTot;
                }
            }
            for (int l = 0; l < _numbers.Length; l++)
            {
                if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                {
                    hexSumTWO += l;
                }
            }

            /*bool isLetter = char.IsLetter(arrayOfHexSummation[aa][iOfC]);
            bool isDigit = char.IsDigit(arrayOfHexSummation[aa][iOfC]);

            if (isLetter)
            {
                for (int l = 0; l < charArray.Length; l++)
                {
                    if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                    {
                        var indexTot = 10 + l;
                        hexSumTWO += indexTot;
                    }
                }
            }
            if (isDigit)
            {
                for (int l = 0; l < _numbers.Length; l++)
                {
                    if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                    {
                        hexSumTWO += l;
                    }
                }
            }*/
            aa++;

            if (aa == 3)
            {
                hexSumTWO += leftOverCarryTWO;

                if (hexSumTWO < 16)
                {
                    theHEXSummationTWO += bitTotalZeroToNineAndAToF[hexSumTWO];
                }
                else
                {
                    int total16 = 0;
                    for (int add = 0; add < hexSumTWO; add++)
                    {
                        if (add % 16 == 0 && add != 0)
                        {
                            total16++;
                        }
                    }
                    int modulo = hexSumTWO % 16;

                    if (total16 != 0)
                    {
                        leftOverCarryTWO = total16;
                    }
                    theHEXSummationTWO += bitTotalZeroToNineAndAToF[modulo];
                }
                if (iOfC < 8)
                {
                    iOfC++;
                }
                else
                {
                    break;
                }
            }
        }
        //Console.WriteLine(theHEXSummationTWO);
        //------------------------------------------------------------------------------

        //------------------CONVERT HEXSUMMATIONTWO BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfHEXSUMMATIONTWO = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = theHEXSummationTWO;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfHEXSUMMATIONTWO += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfHEXSUMMATIONTWO += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }

        //Console.WriteLine(endOfHEXSUMMATIONTWO);
        //------------------------------------------------------------------------------


        //--------COMPUTE NEW E HEX SUMMATION THREE--------------------------------------------------------
        //string test = arrayOfConstantWhatever[3];

        string[] arrayOfHexSummationTHREE = new string[]
        {
                arrayOfConstantWhatever[3],
                endOfHEXSUMMATION
        };



        string theHEXSummationTHREE = "";
        aa = 0;
        iOfC = 0;
        reset = true;

        int hexSumTHREE = 0;
        int leftOverCarryTHREE = 0;

        while (true)// only A
        {
            if (iOfC >= 8)
            {
                break;
            }

            if (aa >= 2)
            {
                hexSumTWO = 0;
                aa = 0;
            }



            for (int l = 0; l < charArray.Length; l++)
            {
                if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                {
                    var indexTot = 10 + l;
                    hexSumTHREE += indexTot;
                }
            }
            for (int l = 0; l < _numbers.Length; l++)
            {
                if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                {
                    hexSumTHREE += l;
                }
            }




            /*bool isLetter = char.IsLetter(arrayOfHexSummation[aa][iOfC]);
            bool isDigit = char.IsDigit(arrayOfHexSummation[aa][iOfC]);

            if (isLetter)
            {
                for (int l = 0; l < charArray.Length; l++)
                {
                    if (arrayOfHexSummation[aa][iOfC] == charArray[l])
                    {
                        var indexTot = 10 + l;
                        hexSumTHREE += indexTot;
                    }
                }
            }
            if (isDigit)
            {
                for (int l = 0; l < _numbers.Length; l++)
                {
                    if (Char.GetNumericValue(arrayOfHexSummation[aa][iOfC]) == _numbers[l])
                    {
                        hexSumTHREE += l;
                    }
                }
            }*/
            aa++;

            if (aa == 2)
            {
                hexSumTHREE += leftOverCarryTHREE;

                if (hexSumTHREE < 16)
                {
                    theHEXSummationTHREE += bitTotalZeroToNineAndAToF[hexSumTHREE];
                }
                else
                {
                    int total16 = 0;
                    for (int add = 0; add < hexSumTHREE; add++)
                    {
                        if (add % 16 == 0 && add != 0)
                        {
                            total16++;
                        }
                    }
                    int modulo = hexSumTHREE % 16;

                    if (total16 != 0)
                    {
                        leftOverCarryTHREE = total16;
                    }
                    theHEXSummationTHREE += bitTotalZeroToNineAndAToF[modulo];
                }
                if (iOfC < 8)
                {
                    iOfC++;
                }
                else
                {
                    break;
                }
            }
        }
        //Console.WriteLine(theHEXSummationTHREE);
        //------------------------------------------------------------------------------


        //------------------CONVERT HEXSUMMATIONTHREE BACK TO LETTERS AND DIGITS .--------
        index = 0;
        minSeparator = 0;
        length = 4;
        string endOfHEXSUMMATIONTHREE = "";
        for (int c = 0; c < 8; c++)
        {
            string totalMaj = theHEXSummationTHREE;
            string tempMajOne = totalMaj.Substring(minSeparator, length);

            for (int i = 0; i < bitTotalZeroToNineAndAToF.Length; i++)
            {
                if (bitTotalZeroToNineAndAToF[i] == tempMajOne)
                {
                    if (i < 10)
                    {
                        endOfHEXSUMMATIONTHREE += _numbers[i];
                    }
                    else
                    {
                        int tempIndex = i - 10;
                        endOfHEXSUMMATIONTHREE += charArray[tempIndex];
                    }
                }
            }

            minSeparator += length;
            index++;
        }
        //Console.WriteLine(endOfHEXSUMMATIONTHREE);
        //------------------------------------------------------------------------------

        string newA = endOfHEXSUMMATIONTWO;
        string newB = arrayOfConstantWhatever[0]; // old A
        string newC = arrayOfConstantWhatever[1]; // old B
        string newD = arrayOfConstantWhatever[2]; // old C
        string newE = endOfHEXSUMMATIONTHREE;
        string newF = arrayOfConstantWhatever[4]; // old E
        string newG = arrayOfConstantWhatever[5]; // old F
        string newH = arrayOfConstantWhatever[6]; // old G
        return newA + newB + newC + newD + newE + newF + newG + newH;
    }
}













//ScryptResult = Replicon.Cryptography.SCrypt.SCrypt.DeriveKey(Databyte, Databyte, 1024, 1, 1, 32);
//CryptSharp.Utility.SCrypt.ComputeKey(Databyte, Databyte, 32, 32, 32, 1, ScryptResult);

//SEEMS TO WORK BUT I DONT KNOW WTF IT DOES
//TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
//PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Databyte);
//ScryptResult  = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV); // tdes.Key

//TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
//PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Databyte);
//ScryptResult  = pdb.CryptDeriveKey("TripleDES", "SHA256 ", 192, tdes.IV); // tdes.Key //AES/ECB/PKCS5Padding//AES128

//NUMBER TWO FOR THE MOMENT
//MemoryStream encryptionStream = new MemoryStream();
//encryptionStream.Write(Databyte, 0, Databyte.Length);
//SHA256 hashAlg = new SHA256Managed();
//CryptoStream cs = new CryptoStream(encryptionStream, hashAlg, CryptoStreamMode.Write);
// Write data here
//cs.FlushFinalBlock();
//ScryptResult = hashAlg.Hash;
//encryptionStream.Flush();
//encryptionStream.Close();
//cs.Close();

//NUMBER ONE FOR THE MOMENT
//IntPtr test = Marshal.UnsafeAddrOfPinnedArrayElement(Databyte, 0);
//byte* memBytePtr = (byte*)test.ToPointer();
//UnmanagedMemoryStream unmanagedmemstream = new UnmanagedMemoryStream(memBytePtr, Databyte.Length, Databyte.Length, FileAccess.Write);
//SHA256 hashAlg = new SHA256Managed();
//CryptoStream cs = new CryptoStream(unmanagedmemstream, hashAlg, CryptoStreamMode.Write);
//cs.FlushFinalBlock();
//ScryptResult = hashAlg.Hash;
//DeleteObject(test);

//NUMBER THREE FOR THE MOMENT
//IntPtr test = Marshal.UnsafeAddrOfPinnedArrayElement(Databyte, 0);
//byte* memBytePtr = (byte*)test.ToPointer();
//UnmanagedMemoryStream unmanagedmemstream = new UnmanagedMemoryStream(memBytePtr, Databyte.Length, Databyte.Length, FileAccess.Write);
//SHA256 hashAlg = new SHA256Managed();
//CryptoStream cs = new CryptoStream(unmanagedmemstream, hashAlg, CryptoStreamMode.Write);
//cs.FlushFinalBlock();
//IntPtr tester = Marshal.UnsafeAddrOfPinnedArrayElement(hashAlg.Hash, 0);   //ScryptResult = hashAlg.Hash;
//Marshal.Copy(tester, ScryptResult, 0, ScryptResult.Length); // I know this is a slow part
//DeleteObject(test);
//DeleteObject(tester);



//_threadStarter.Stop();
//_threadStarter.Reset();
//_threadStarter.Start();







                    //NUMBER THREE OR FOUR FOR THE MOMENT
                    //SHA256 hashAlg = new SHA256Managed();
                    //ScryptResult = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));


                    //SHA256 hashAlg = new SHA256Managed();
                    //ScryptResult = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));

































                    //SHA256 hashAlg = new SHA256Managed();
                    //ScryptResult = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));

                    //ScryptResult = Utilities.HexStringToByteArray(sha256(password, Databyte));



                    /*StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    ScryptResult = builder.ToString();*/










                    //Console.WriteLine(password);
                    //sha256(password);

                    /*password = "";
                    for (int i = 0; i < Databyte.Length; i++)
                    {
                        password += bitTotal0To15[Databyte[i]];
                    }*/

                    //sha256(password, Databyte);



                    //ScryptResult = Utilities.HexStringToByteArray(sha256(password, Databyte));

                    //Console.WriteLine(_threadStarter.Elapsed.Ticks);

                    /*string pass = sha256(password);

                    byte[] bytes = new byte[ScryptResult.Length];
                    for (int i = 0; i < ScryptResult.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(pass.Substring(i, 1), 16);
                    }
                    ScryptResult = bytes;*/












                    //ScryptResult = Encoding.ASCII.GetBytes(sha256(password));
                    //ScryptResult = u8.GetBytes(sha256(password));
                    //ScryptResult = Utilities.HexStringToByteArray(sha256(password));
                    //ScryptResult = Enumerable.Range(0, sha256(password).Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(sha256(password).Substring(x, 2), 16)).ToArray();
                    //byteArray = 
                    /*string pass = sha256(password);
                    for (int i = 0; i < pass.Length; i++)
                    {
                        var test = pass[i];
                        ScryptResult[i] = Convert.ToByte(test);
                        
                    }*/