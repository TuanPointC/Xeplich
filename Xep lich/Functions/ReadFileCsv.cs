using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xep_lich.Functions
{
    public class ReadFileCsv
    {
        public string? FilePath { get; set; }
        public ReadFileCsv(string filePath)
        {
            FilePath = filePath;
        }

        public IEnumerable<ClassInfo> Read()
        {
            var listClass = new List<ClassInfo>();
            if (FilePath != null)
            {
                using var reader = new StreamReader(FilePath);
                var i = 0;
                while (!reader.EndOfStream)
                {
                    if (i != 0)
                    {
                        var line = reader.ReadLine();
                        var elements = line?.Split(',');
                        listClass.Add(new ClassInfo()
                        {
                            Course = elements?[0],
                            Date_Pattern = elements?[3],
                            Day = elements?[4],
                            End_Time = elements?[6],
                            Instructor = elements?[8],
                            Itype = elements?[1],
                            Room = elements?[7],
                            Section = Convert.ToInt32(elements?[2]),
                            Start_Time = elements?[5],
                        });
                    }
                    else
                    {
                        i = 1;
                        var line = reader.ReadLine();
                    }

                }
            }
            return listClass;
        }

        public IEnumerable<IEnumerable<int>> ReadCourse(List<ClassInfo> listClasses)
        {
            var result = new List<List<int>>();
            var course = listClasses[0].Course;
            var type = listClasses[0].Itype;
            result.Add(new List<int>());

            for (var i = 0; i < listClasses.Count; i++)
            {
                if (listClasses[i].Course == course && listClasses[i].Itype == type)
                {
                    result[^1].Add(i);
                }
                else
                {
                    course = listClasses[i].Course;
                    type  = listClasses[i].Itype;
                    result.Add(new List<int>());
                    result[^1].Add(i);
                }
            }
            return result;
        }
    }
}
