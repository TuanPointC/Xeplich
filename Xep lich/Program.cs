using Xep_lich;
using Xep_lich.Functions;

static bool solve(List<ClassInfo> sol, List<IEnumerable<int>> d, List<ClassInfo> listClasses, CheckConditions Functions)
{
    var size = sol.Count;
    if (size == d.Count)
    {
        return true;
    }
    foreach (var index in d[size])
    {
        if (Functions.CheckAll(sol, listClasses[index]))
        {
            sol.Add(listClasses[index]);
            foreach (var classSolved in sol)
            {
                Console.WriteLine($"{classSolved.Course} {classSolved.Section} {classSolved.Itype} {classSolved.Date_Pattern} {classSolved.Room} {classSolved.Instructor}");
            }
            Console.WriteLine("__________");
            if (solve(sol, d, listClasses, Functions))
            {
                return true;
            }
            sol.RemoveAt(sol.Count - 1);
        }
    }
    return false;
}

var filePath = string.Concat(Directory.GetCurrentDirectory().AsSpan(0, 31), "Xep lich\\tkb.csv");
var readfileService = new ReadFileCsv(filePath);
var listClasses = readfileService.Read().ToList();
var d = readfileService.ReadCourse(listClasses).ToList();


List<ClassInfo> sol = new();
var Functions = new CheckConditions();
Console.WriteLine("Run");

Console.WriteLine(solve(sol, d, listClasses, Functions));

foreach (var classSolved in sol)
{
    Console.WriteLine($"{classSolved.Course} {classSolved.Section} {classSolved.Itype}");
}

