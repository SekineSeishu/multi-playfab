using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public static class CodeGenerator
{
    private static readonly Random random = new Random();
    //�I�΂��p����
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateCode(int length)
    {
        //�w�肵��������Chars���烉���_���ɑI��
        return new string(Enumerable.Repeat(Chars, length)
           .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
