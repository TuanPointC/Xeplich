﻿using Xep_lich;
using Xep_lich.Functions;

//var Functions = new CheckConditions();

static bool solve(List<ClassInfo> sol, List<IEnumerable<int>> d, List<ClassInfo> listClasses, CheckConditions Functions)
{
    if (Functions == null || d == null || listClasses == null)
    {
        return false;
    }
    var size = sol.Count;
    if (size >= d.Count)
    {
        return true;
    }
    foreach (var index in d[size])
    {
        if (Functions.CheckAll(sol, listClasses[index]))
        {
            sol.Add(listClasses[index]);
            if (solve(sol, d, listClasses, Functions))
            {
                return true;
            }
            sol.RemoveAt(sol.Count - 1);
        }
    }
    return false;
}

var filePath = string.Concat(Directory.GetCurrentDirectory().AsSpan(0, 52), "Xep lich\\tkb.csv");
var readfileService = new ReadFileCsv(filePath);
var listClasses = readfileService.Read().ToList();
var d = readfileService.ReadCourse(listClasses).ToList();

List<ClassInfo> sol = new();
var Functions = new CheckConditions();
Console.WriteLine("Run");
solve(sol, d, listClasses, Functions);

foreach (var classSolved in sol)
{
    Console.WriteLine($"{classSolved.Course} {classSolved.Section} {classSolved.Itype}");
}

